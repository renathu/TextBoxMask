namespace TextBoxMask
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textEdit5 = new System.Windows.Forms.TextEdit();
            this.textEdit4 = new System.Windows.Forms.TextEdit();
            this.textEdit3 = new System.Windows.Forms.TextEdit();
            this.textEdit2 = new System.Windows.Forms.TextEdit();
            this.textEdit1 = new System.Windows.Forms.TextEdit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(58, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "All";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(58, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Letter";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(58, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "LetterAndNumber";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(288, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Decimal";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(288, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Number";
            // 
            // textEdit5
            // 
            this.textEdit5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEdit5.Location = new System.Drawing.Point(291, 51);
            this.textEdit5.MaskProp.Mask = System.Windows.Forms.Controls.Mask.Number;
            this.textEdit5.Name = "textEdit5";
            this.textEdit5.Size = new System.Drawing.Size(190, 25);
            this.textEdit5.TabIndex = 8;
            this.textEdit5.Text = "0";
            this.textEdit5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textEdit4
            // 
            this.textEdit4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEdit4.Location = new System.Drawing.Point(291, 115);
            this.textEdit4.MaskProp.Mask = System.Windows.Forms.Controls.Mask.Decimal;
            this.textEdit4.Name = "textEdit4";
            this.textEdit4.Size = new System.Drawing.Size(190, 25);
            this.textEdit4.TabIndex = 6;
            this.textEdit4.Text = "0,00";
            this.textEdit4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textEdit3
            // 
            this.textEdit3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEdit3.Location = new System.Drawing.Point(61, 187);
            this.textEdit3.MaskProp.Mask = System.Windows.Forms.Controls.Mask.LetterAndNumber;
            this.textEdit3.Name = "textEdit3";
            this.textEdit3.Size = new System.Drawing.Size(190, 25);
            this.textEdit3.TabIndex = 4;
            // 
            // textEdit2
            // 
            this.textEdit2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEdit2.Location = new System.Drawing.Point(61, 115);
            this.textEdit2.MaskProp.Mask = System.Windows.Forms.Controls.Mask.Letter;
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Size = new System.Drawing.Size(190, 25);
            this.textEdit2.TabIndex = 2;
            // 
            // textEdit1
            // 
            this.textEdit1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textEdit1.Location = new System.Drawing.Point(61, 51);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(190, 25);
            this.textEdit1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(514, 241);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textEdit5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textEdit4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textEdit3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textEdit2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textEdit1);
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextEdit textEdit1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextEdit textEdit2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextEdit textEdit3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextEdit textEdit4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextEdit textEdit5;
    }
}

