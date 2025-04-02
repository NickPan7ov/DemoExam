
namespace VideoGameShop
{
    partial class CaptchaForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Captcha1 = new System.Windows.Forms.PictureBox();
            this.Captcha3 = new System.Windows.Forms.PictureBox();
            this.TextCaptcha = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CaptchaButton = new System.Windows.Forms.Button();
            this.Captcha2 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Captcha1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Captcha3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Captcha2)).BeginInit();
            this.SuspendLayout();
            // 
            // Captcha1
            // 
            this.Captcha1.Image = global::VideoGameShop.Properties.Resources.Captcha1;
            this.Captcha1.Location = new System.Drawing.Point(49, 39);
            this.Captcha1.Name = "Captcha1";
            this.Captcha1.Size = new System.Drawing.Size(288, 132);
            this.Captcha1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Captcha1.TabIndex = 12;
            this.Captcha1.TabStop = false;
            // 
            // Captcha3
            // 
            this.Captcha3.Image = global::VideoGameShop.Properties.Resources.Captcha3;
            this.Captcha3.Location = new System.Drawing.Point(49, 39);
            this.Captcha3.Name = "Captcha3";
            this.Captcha3.Size = new System.Drawing.Size(288, 132);
            this.Captcha3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Captcha3.TabIndex = 14;
            this.Captcha3.TabStop = false;
            // 
            // TextCaptcha
            // 
            this.TextCaptcha.BackColor = System.Drawing.SystemColors.Window;
            this.TextCaptcha.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TextCaptcha.ForeColor = System.Drawing.SystemColors.MenuText;
            this.TextCaptcha.Location = new System.Drawing.Point(110, 180);
            this.TextCaptcha.Margin = new System.Windows.Forms.Padding(6);
            this.TextCaptcha.Name = "TextCaptcha";
            this.TextCaptcha.Size = new System.Drawing.Size(176, 26);
            this.TextCaptcha.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(152, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 24);
            this.label1.TabIndex = 17;
            this.label1.Text = "Captcha";
            // 
            // CaptchaButton
            // 
            this.CaptchaButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.CaptchaButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CaptchaButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.CaptchaButton.Location = new System.Drawing.Point(127, 226);
            this.CaptchaButton.Name = "CaptchaButton";
            this.CaptchaButton.Size = new System.Drawing.Size(137, 46);
            this.CaptchaButton.TabIndex = 18;
            this.CaptchaButton.Text = "Ввести каптчу";
            this.CaptchaButton.UseVisualStyleBackColor = false;
            this.CaptchaButton.Click += new System.EventHandler(this.CaptchaButton_Click);
            // 
            // Captcha2
            // 
            this.Captcha2.Image = global::VideoGameShop.Properties.Resources.Captcha2;
            this.Captcha2.Location = new System.Drawing.Point(49, 39);
            this.Captcha2.Name = "Captcha2";
            this.Captcha2.Size = new System.Drawing.Size(288, 132);
            this.Captcha2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Captcha2.TabIndex = 15;
            this.Captcha2.TabStop = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(295, 177);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 36);
            this.button1.TabIndex = 19;
            this.button1.Text = "Обновить";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CaptchaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(400, 290);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CaptchaButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextCaptcha);
            this.Controls.Add(this.Captcha2);
            this.Controls.Add(this.Captcha3);
            this.Controls.Add(this.Captcha1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CaptchaForm";
            this.Text = "CaptchaForm";
            this.Load += new System.EventHandler(this.CaptchaForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Captcha1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Captcha3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Captcha2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Captcha1;
        private System.Windows.Forms.PictureBox Captcha3;
        private System.Windows.Forms.TextBox TextCaptcha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CaptchaButton;
        private System.Windows.Forms.PictureBox Captcha2;
        private System.Windows.Forms.Button button1;
    }
}