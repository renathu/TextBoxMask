using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Controls;

namespace System.Windows.Forms
{
    public class TextEdit : TextBox
    {
        private const int WM_PASTE = 0x302;

        private const int WM_CUT = 0x0300;

        private const int WM_CLEAR = 0x303;

        public string Text
        {
            get { return base.Text; }
            set
            {
                if (value != base.Text)
                {
                    ValidateFormat(EditType.Text, value);
                }
            }
        }

        private string DecimalSeparator
        {
            get
            {
                return CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
            }
        }

        private string GroupSeparator
        {
            get
            {
                return CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
            }
        }

        private MaskProperties maskProp = null;

        [Browsable(true),
        CategoryAttribute("Mask"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public MaskProperties MaskProp
        {
            get
            {
                if (this.maskProp == null)
                {
                    this.maskProp = new MaskProperties();
                    this.maskProp.PropertyChanged += Mask_PropertyChanged;
                }
                return maskProp;
            }
        }

        private void SetDefaultValue()
        {
            switch (MaskProp.Mask)
            {
                case Mask.All:
                case Mask.Letter:
                case Mask.LetterAndNumber:
                    base.Text = string.Empty;
                    break;
                case Mask.Number:
                    if (MaskProp.NumberEmpty == NumberEmpty.Empty)
                    {
                        base.Text = string.Empty;
                    }
                    else
                    {
                        base.Text = "0";
                    }
                    break;
                case Mask.Decimal:
                    if (MaskProp.NumberEmpty == NumberEmpty.Empty)
                    {
                        base.Text = string.Empty;
                    }
                    else
                    {
                        base.Text = string.Concat("0", DecimalSeparator, new string('0', MaskProp.NumberDecimalDigits));
                    }
                    break;
            }
        }

        private Cancel ValidateFormat(EditType editType)
        {
            return ValidateFormat(editType, "");
        }

        private Cancel ValidateFormat(EditType editType, string character)
        {
            Cancel resultCancel = new Cancel();
            string previewText = string.Empty;
            string text = base.Text;

            #region Negative

            if (character == "-")
            {
                if (this.MaskProp.Mask == System.Windows.Forms.Controls.Mask.Number || this.MaskProp.Mask == System.Windows.Forms.Controls.Mask.Decimal)
                {
                    if (this.MaskProp.AllowNegative == false || this.Text.Contains("-"))
                    {
                        resultCancel.IsCancel = true;
                        return resultCancel;
                    }

                    if (text != string.Empty && Convert.ToDecimal(text) != 0)
                    {
                        int selectionStart = this.SelectionStart + 1;
                        base.Text = string.Concat("-", text);
                        this.Select(selectionStart, 0);

                        resultCancel.IsCancel = true;
                        return resultCancel;
                    }
                }
            }
            else if (character == "+" && text.Contains("-"))
            {
                if (this.MaskProp.Mask == System.Windows.Forms.Controls.Mask.Number || this.MaskProp.Mask == System.Windows.Forms.Controls.Mask.Decimal)
                {
                    int selectionStart = this.SelectionStart > 0 ? this.SelectionStart - 1 : 0;
                    base.Text = text.Substring(1);
                    this.Select(selectionStart, 0);

                    resultCancel.IsCancel = true;
                    return resultCancel;
                }
            }

            #endregion

            #region Separator

            if (new string[] { ".", "," }.Contains(character) && this.MaskProp.Mask == System.Windows.Forms.Controls.Mask.Decimal)
            {
                int index = text.IndexOf(DecimalSeparator);
                if (index != -1)
                {
                    this.Select(index + 1, 0);
                }
                resultCancel.IsCancel = true;
                return resultCancel;
            }

            #endregion

            #region Move cursor

            if (this.MaskProp.Mask == System.Windows.Forms.Controls.Mask.Decimal)
            {
                if (editType == EditType.Delete)
                {
                    int index = text.IndexOf(DecimalSeparator);
                    if (index != -1 && this.SelectionStart == index && this.SelectionLength == 0)
                    {
                        this.Select(index + 1, 0);

                        resultCancel.IsCancel = true;
                        return resultCancel;
                    }
                }
                else if (editType == EditType.Backspace)
                {
                    int index = text.IndexOf(DecimalSeparator);
                    if (index != -1 && this.SelectionStart == index + 1 && this.SelectionLength == 0)
                    {
                        this.Select(index, 0);
                        resultCancel.IsCancel = true;
                        return resultCancel;
                    }
                }
            }

            #endregion

            #region Preview Text

            string left_text = text.Substring(0, this.SelectionStart);
            string selected_text = this.SelectedText;
            string right_text = text.Substring(this.SelectionStart + this.SelectionLength);

            switch (editType)
            {
                case EditType.NewCharacter:
                    previewText = left_text + character + right_text;

                    break;
                case EditType.Cut:
                    previewText = left_text + right_text;
                    if (this.SelectionLength > 0)
                    {
                        Clipboard.Clear();
                        Clipboard.SetText(selected_text);
                    }
                    break;
                case EditType.Paste:
                    if (Clipboard.ContainsText())
                    {
                        selected_text = Clipboard.GetText();
                    }
                    previewText = left_text + selected_text + right_text;
                    break;
                case EditType.Delete:
                    if (selected_text.Length == 0)
                    {
                        if (right_text.Length > 0)
                        {
                            right_text = right_text.Substring(1);
                        }
                    }
                    else
                    {
                        selected_text = "";
                    }

                    previewText = left_text + selected_text + right_text;

                    break;
                case EditType.Backspace:
                    if (selected_text.Length == 0)
                    {
                        if (left_text.Length > 0)
                        {
                            left_text = left_text.Substring(0, this.SelectionStart - 1);
                        }
                    }
                    else
                    {
                        selected_text = "";
                    }

                    previewText = left_text + selected_text + right_text;
                    break;
                case EditType.Text:
                    selected_text = character;
                    previewText = character;
                    break;
            }

            #endregion

            #region Integer digits number

            if (this.MaskProp.Mask == System.Windows.Forms.Controls.Mask.Number)
            {
                if (previewText.Replace("-", "").Length > MaskProp.IntegerDigits)
                {
                    resultCancel.IsCancel = true;
                    return resultCancel;
                }
            }

            #endregion

            #region  Number/Decimal Empty

            if (this.MaskProp.NumberEmpty == NumberEmpty.Zero && previewText.Length == 0)
            {
                if (this.MaskProp.Mask == System.Windows.Forms.Controls.Mask.Number)
                {
                    base.Text = "0";
                    this.SelectionStart = editType == EditType.Backspace ? 0 : 1;

                    resultCancel.IsCancel = true;
                    return resultCancel;
                }
                else if (this.MaskProp.Mask == System.Windows.Forms.Controls.Mask.Decimal)
                {
                    base.Text = 0m.ToString("N" + MaskProp.NumberDecimalDigits);
                    this.SelectionStart = 0;

                    resultCancel.IsCancel = true;
                    return resultCancel;
                }
            }

            #endregion

            #region Validate type

            if (this.MaskProp.Mask == System.Windows.Forms.Controls.Mask.Number)
            {
                Int64 test_value;
                if (Int64.TryParse(previewText, out test_value) == false)
                {
                    resultCancel.IsCancel = true;
                    return resultCancel;
                }
                else if (test_value < 0 && this.MaskProp.AllowNegative == false)
                {
                    resultCancel.IsCancel = true;
                    return resultCancel;
                }
            }
            else if (this.MaskProp.Mask == System.Windows.Forms.Controls.Mask.Decimal)
            {
                if (previewText.Contains(DecimalSeparator) == false)
                {
                    #region Decimal separator

                    Int32 indexSep = base.Text.IndexOf(DecimalSeparator);
                    if (indexSep == -1)
                    {
                        indexSep = previewText.Length;
                    }
                    Int32 endCursor = this.SelectionStart + this.SelectionLength;
                    if (endCursor == indexSep + 1)
                    {
                        previewText = previewText.Insert(previewText.Length - MaskProp.NumberDecimalDigits, DecimalSeparator);
                    }
                    else if (endCursor == this.Text.Length)
                    {
                        previewText = string.Concat(previewText, DecimalSeparator, new string('0', MaskProp.NumberDecimalDigits));
                    }
                    else
                    {
                        previewText = string.Concat(previewText.Substring(0, previewText.Length - (this.Text.Length - endCursor)), DecimalSeparator, new string('0', MaskProp.NumberDecimalDigits - (this.Text.Length - endCursor)), previewText.Substring(previewText.Length - (this.Text.Length - endCursor)));
                    }

                    #endregion
                }

                previewText = previewText.Replace(GroupSeparator, "");
                Int32 indexSeparator = previewText.IndexOf(DecimalSeparator);
                if (indexSeparator != -1 && previewText.Substring(indexSeparator + 1).Length > MaskProp.NumberDecimalDigits)
                {
                    previewText = previewText.Substring(0, indexSeparator + 1 + MaskProp.NumberDecimalDigits);
                }

                Decimal test_value;
                if (Decimal.TryParse(previewText, out test_value) == false)
                {
                    resultCancel.IsCancel = true;
                    return resultCancel;
                }
                else if (test_value < 0 && this.MaskProp.AllowNegative == false)
                {
                    resultCancel.IsCancel = true;
                    return resultCancel;
                }
            }
            else if (this.MaskProp.Mask == System.Windows.Forms.Controls.Mask.Letter)
            {
                if (previewText != string.Empty && System.Text.RegularExpressions.Regex.IsMatch(previewText, @"^[a-zA-Z]+$") == false)
                {
                    resultCancel.IsCancel = true;
                    return resultCancel;
                }

            }
            else if (this.MaskProp.Mask == System.Windows.Forms.Controls.Mask.LetterAndNumber)
            {
                if (previewText != string.Empty && System.Text.RegularExpressions.Regex.IsMatch(previewText, @"^[a-zA-Z0-9]+$") == false)
                {
                    resultCancel.IsCancel = true;
                    return resultCancel;
                }
            }

            #endregion

            #region Zero left number

            if (this.MaskProp.Mask == System.Windows.Forms.Controls.Mask.Number && this.MaskProp.NumberZeroLeft == false && previewText.Length > 1 && previewText.StartsWith("0"))
            {
                int rightText = text.Substring(this.SelectionStart + this.SelectionLength).Length;

                base.Text = Convert.ToInt64(previewText).ToString();
                this.Select((rightText > base.Text.Length ? 0 : base.Text.Length - rightText), 0);

                resultCancel.IsCancel = true;
                return resultCancel;
            }

            #endregion

            #region Decimal digits number

            if (this.MaskProp.Mask == System.Windows.Forms.Controls.Mask.Decimal)
            {
                if (this.SelectionStart <= base.Text.IndexOf(DecimalSeparator) && previewText.Replace("-", "").Substring(0, previewText.IndexOf(DecimalSeparator)).Length > MaskProp.IntegerDigits)
                {
                    resultCancel.IsCancel = true;
                    return resultCancel;
                }
            }

            #endregion

            #region Format decimal

            if (this.MaskProp.Mask == System.Windows.Forms.Controls.Mask.Decimal && previewText != string.Empty)
            {
                int selectionStartRigth = 0;
                int indexSeparator = 0;
                int selectionLength = 0;
                bool fullSelection = false;
                if (this.SelectionLength > 0)
                {
                    selectionStartRigth = base.Text.Length - (this.SelectionStart + this.SelectionLength);
                    selectionLength = base.SelectionLength;
                    fullSelection = base.Text.Length == base.SelectionLength;
                    base.Text = Convert.ToDecimal(previewText).ToString("N" + MaskProp.NumberDecimalDigits);
                    indexSeparator = base.Text.IndexOf(DecimalSeparator);
                }
                else
                {
                    selectionStartRigth = base.Text.Length - this.SelectionStart;
                    base.Text = Convert.ToDecimal(previewText).ToString("N" + MaskProp.NumberDecimalDigits);
                    indexSeparator = base.Text.IndexOf(DecimalSeparator);
                }

                if (editType == EditType.Backspace)
                {
                    if (this.Text.Length - selectionStartRigth > indexSeparator)
                    {
                        this.Select(selectionStartRigth >= base.Text.Length ? 0 : this.Text.Length - selectionStartRigth - 1, 0);
                    }
                    else
                    {
                        if (this.Text.Length - selectionStartRigth > 0 && this.Text[this.Text.Length - selectionStartRigth - 1].ToString() == GroupSeparator)
                        {
                            this.Select(this.Text.Length - selectionStartRigth - 1, 0);
                        }
                        else
                        {
                            this.Select(selectionStartRigth >= base.Text.Length ? 0 : this.Text.Length - selectionStartRigth, 0);
                        }
                    }
                }
                else if (editType == EditType.Delete)
                {
                    if (this.Text.Length - selectionStartRigth < indexSeparator && this.Text.Length - selectionStartRigth > 0 && this.Text[this.Text.Length - selectionStartRigth].ToString() == GroupSeparator)
                    {
                        this.Select(this.Text.Length - selectionStartRigth + 1, 0);
                    }
                    else if (this.Text.Length - selectionStartRigth > indexSeparator && selectionLength == 0)
                    {
                        this.Select(this.Text.Length - selectionStartRigth + 1, 0);
                    }
                    else if (this.Text.Length - selectionStartRigth > indexSeparator && selectionLength > 0)
                    {
                        this.Select(this.Text.Length - selectionStartRigth, 0);
                    }
                    else
                    {
                        this.Select(selectionStartRigth > base.Text.Length ? 0 : this.Text.Length - selectionStartRigth, 0);
                    }
                }
                else
                {
                    if (fullSelection)
                    {
                        this.Select(indexSeparator, 0);
                    }
                    else if (this.Text.Length - selectionStartRigth > indexSeparator && selectionLength == 0)
                    {
                        this.Select(this.Text.Length - selectionStartRigth + 1, 0);
                    }
                    else if (this.Text.Length - selectionStartRigth > indexSeparator && selectionLength > 0)
                    {
                        this.Select(this.Text.Length - selectionStartRigth, 0);
                    }
                    else
                    {
                        this.Select(this.Text.Length - selectionStartRigth, 0);
                    }
                }


                resultCancel.IsCancel = true;
                resultCancel.Sucess = true;
                return resultCancel;
            }

            #endregion

            #region Letter, number and all

            if (this.MaskProp.Mask == Mask.Letter || this.MaskProp.Mask == Mask.LetterAndNumber || this.MaskProp.Mask == Mask.Number || this.MaskProp.Mask == Mask.All)
            {
                bool fullSelection = base.Text.Length == base.SelectionLength && base.Text.Length > 0;
                int selectionLength = this.SelectionLength;
                int selectionStart = this.SelectionStart;
                base.Text = previewText;

                if (editType == EditType.Backspace)
                {
                    if (selectionLength == 0)
                    {
                        this.Select(selectionStart > 0 ? selectionStart - 1 : 0, 0);
                    }
                    else
                    {
                        this.Select(selectionStart > 0 ? selectionStart : 0, 0);
                    }
                }
                else if (editType == EditType.Delete || editType == EditType.Cut)
                {
                    this.Select(selectionStart <= base.Text.Length ? selectionStart : base.Text.Length, 0);
                }
                else
                {
                    if (fullSelection && editType == EditType.NewCharacter)
                    {
                        this.Select(1, 0);
                    }
                    else if (fullSelection)
                    {
                        this.Select(this.Text.Length, 0);
                    }
                    else if (editType == EditType.Paste)
                    {
                        this.Select(selectionStart + selected_text.Length, 0);
                    }
                    else
                    {
                        this.Select(selectionStart + 1, 0);
                    }
                }

                resultCancel.IsCancel = true;
                resultCancel.Sucess = true;
                return resultCancel;
            }

            #endregion

            return resultCancel;
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            // Special characters
            if ((e.KeyChar < ' ') || (e.KeyChar > '~')) return;

            e.Handled = ValidateFormat(EditType.NewCharacter, e.KeyChar.ToString()).IsCancel;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            bool cancel_event = false;

            if (e.Control && (e.KeyCode == Keys.A))
            {
                this.Select(0, base.Text.Length);
                cancel_event = true;
            }
            else if (e.Control && (e.KeyCode == Keys.X))
            {
                cancel_event = ValidateFormat(EditType.Cut).IsCancel;
            }
            else if (e.Control && (e.KeyCode == Keys.V))
            {
                cancel_event = ValidateFormat(EditType.Paste).IsCancel;
            }
            else if (e.Shift && (e.KeyCode == Keys.Insert))
            {
                cancel_event = ValidateFormat(EditType.Paste).IsCancel;
            }
            else if (e.Shift && (e.KeyCode == Keys.Delete))
            {
                cancel_event = ValidateFormat(EditType.Cut).IsCancel;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                cancel_event = ValidateFormat(EditType.Delete).IsCancel;
            }
            else if (e.KeyCode == Keys.Back)
            {
                cancel_event = ValidateFormat(EditType.Backspace).IsCancel;
            }

            if (cancel_event)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        protected override void OnEnter(EventArgs e)
        {
            if ((Control.MouseButtons ^ MouseButtons.Left) != 0)
            {
                this.SelectAll();
            }

            base.OnEnter(e);
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_PASTE:
                    {
                        ValidateFormat(EditType.Paste);

                        return;
                    }
                case WM_CUT:
                    {
                        ValidateFormat(EditType.Cut);

                        return;
                    }
                case WM_CLEAR:
                    {
                        ValidateFormat(EditType.Delete);

                        return;
                    }

            }
            base.WndProc(ref m);
        }

        private void Mask_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Mask":
                case "NumberEmpty":
                case "AllowNegative":
                case "NumberDecimalDigits":
                case "IntegerDigits":
                case "NumberZeroLeft":
                    if (ValidateFormat(EditType.Text, base.Text).Sucess == false)
                    {
                        SetDefaultValue();
                    }
                    break;
            }
        }
    }

    internal class Cancel
    {
        public bool IsCancel { get; set; }

        public bool Sucess { get; set; }
    }

    internal enum EditType
    {
        NewCharacter,
        Cut,
        Paste,
        Delete,
        Backspace,
        Text
    }
}
