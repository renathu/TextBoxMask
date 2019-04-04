using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace System.Windows.Forms.Controls
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MaskProperties : INotifyPropertyChanged
    {
        private Mask mask = Mask.All;

        [DefaultValue(Mask.All)]
        public Mask Mask
        {
            get { return mask; }
            set
            {
                if (mask != value)
                {
                    mask = value;
                    RaisePropertyChanged("Mask");
                }
            }
        }

        private NumberEmpty numberEmpty = NumberEmpty.Zero;

        [DefaultValue(NumberEmpty.Zero)]
        public NumberEmpty NumberEmpty
        {
            get { return numberEmpty; }
            set
            {
                if (numberEmpty != value)
                {
                    numberEmpty = value;
                    RaisePropertyChanged("NumberEmpty");
                }
            }
        }

        private bool allowNegative = false;

        [DefaultValue(false)]
        public bool AllowNegative
        {
            get { return allowNegative; }
            set
            {
                if (allowNegative != value)
                {
                    allowNegative = value;
                    RaisePropertyChanged("AllowNegative");
                }
            }
        }

        private int numberDecimalDigits = 2;

        [DefaultValue(2)]
        public int NumberDecimalDigits
        {
            get { return numberDecimalDigits; }
            set
            {
                if (numberDecimalDigits != value)
                {
                    if (value <= 0)
                    {
                        value = 1;
                    }

                    numberDecimalDigits = value;
                    RaisePropertyChanged("NumberDecimalDigits");
                }
            }
        }

        private int integerDigits = 10;

        [DefaultValue(10)]
        public int IntegerDigits
        {
            get { return integerDigits; }
            set
            {
                if (integerDigits != value)
                {
                    if (value <= 0)
                    {
                        value = 1;
                    }

                    integerDigits = value;
                    RaisePropertyChanged("IntegerDigits");
                }
            }
        }

        private bool numberZeroLeft = false;

        [DefaultValue(false)]
        public bool NumberZeroLeft
        {
            get { return numberZeroLeft; }
            set
            {
                if (numberZeroLeft != value)
                {
                    numberZeroLeft = value;
                    RaisePropertyChanged("NumberZeroLeft");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        public override string ToString()
        {
            return mask.ToString();
        }
    }

    public enum Mask
    {
        All,
        Letter,
        LetterAndNumber,
        Number,
        Decimal
    }

    public enum NumberEmpty
    {
        Empty,
        Zero
    }
}
