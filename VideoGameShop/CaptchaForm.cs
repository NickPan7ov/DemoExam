using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoGameShop
{
    public partial class CaptchaForm : Form
    {
       
        int stoptimer;
        int randomnumber;
        static System.Timers.Timer timer = new System.Timers.Timer();
        bool isTimerActive = true;
        public CaptchaForm()
        {
            InitializeComponent();
            TextCaptcha.MaxLength = 4;
            CaptchaButton.Enabled = true;
            TextCaptcha.Enabled = true;
            Random random = new Random();
            randomnumber = random.Next(1, 4);
            switch (randomnumber)
            {
                case 1:
                    Captcha1.Visible = true;
                    Captcha2.Visible = false;
                    Captcha3.Visible = false;
                    TextCaptcha.Visible = true;
                    CaptchaButton.Visible = true;

                    break;
                case 2:
                    Captcha1.Visible = false;
                    Captcha2.Visible = true;
                    Captcha3.Visible = false;
                    TextCaptcha.Visible = true;
                    CaptchaButton.Visible = true;
                    break;
                case 3:
                    Captcha1.Visible = false;
                    Captcha2.Visible = false;
                    Captcha3.Visible = true;
                    TextCaptcha.Visible = true;
                    CaptchaButton.Visible = true;
                    break;
            }
        }

        private void CaptchaForm_Load(object sender, EventArgs e)
        {
        }
        public void onTimerStop()
        {
            stoptimer = 1;
            timer.Stop();
        }

        private async void CaptchaButton_Click(object sender, EventArgs e)
        {
            string Captchastring = TextCaptcha.Text;
            string Capthca1text = "F5D2";
            string Capthca2text = "3RD1";
            string Capthca3text = "E21S";
            if (randomnumber == 1 && isTimerActive)
            {
                if (Captchastring != Capthca1text)
                {
                    MessageBox.Show("Captcha Не верная!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    CaptchaButton.Enabled = false;
                    TextCaptcha.Enabled = false;
                    button1.Enabled = false;
                    TextCaptcha.Text = "";
                    await Task.Delay(10000);
                    unlockbuttons();
                    return;
                }
                if (Captchastring == Capthca1text)
                {
                    MessageBox.Show("Captcha введена верно!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CaptchaButton.Visible = false;
                    TextCaptcha.Visible = false;
                    Captcha1.Visible = false;
                    TextCaptcha.Text = "";
                    this.Close();
                  
                    return;
                }
            }
            if (randomnumber == 2 && isTimerActive)
            {
                if (Captchastring != Capthca2text)
                {
                    MessageBox.Show("Captcha Не верная!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    CaptchaButton.Enabled = false;
                    TextCaptcha.Enabled = false;
                    TextCaptcha.Text = "";
                    button1.Enabled = false;
                    await Task.Delay(10000);
                    unlockbuttons();
                    return;
                }
                if (Captchastring == Capthca2text)
                {

                    MessageBox.Show("Captcha введена верно!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    CaptchaButton.Visible = false;
                    TextCaptcha.Visible = false;
                    Captcha2.Visible = false;
                    TextCaptcha.Text = "";
                    this.Close();
                    return;
                }
            }
            if (randomnumber == 3 && isTimerActive)
            {
                if (Captchastring != Capthca3text)
                {
                    MessageBox.Show("Captcha Не верная!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    CaptchaButton.Enabled = false;
                    TextCaptcha.Enabled = false;
                    TextCaptcha.Text = "";
                    button1.Enabled = false;
                    await Task.Delay(10000);
                    unlockbuttons();
                    return;
                }
                if (Captchastring == Capthca3text)
                {

                    MessageBox.Show("Captcha введена верно!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                    CaptchaButton.Visible = false;
                    TextCaptcha.Visible = false;
                    Captcha3.Visible = false;
                    TextCaptcha.Text = "";
                    this.Close();
                    return;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            randomnumber = random.Next(1, 4);
            switch (randomnumber)
            {
                case 1:
                    Captcha1.Visible = true;
                    Captcha2.Visible = false;
                    Captcha3.Visible = false;
                    TextCaptcha.Visible = true;
                    CaptchaButton.Visible = true;

                    break;
                case 2:
                    Captcha1.Visible = false;
                    Captcha2.Visible = true;
                    Captcha3.Visible = false;
                    TextCaptcha.Visible = true;
                    CaptchaButton.Visible = true;
                    break;
                case 3:
                    Captcha1.Visible = false;
                    Captcha2.Visible = false;
                    Captcha3.Visible = true;
                    TextCaptcha.Visible = true;
                    CaptchaButton.Visible = true;
                    break;
            }
        }
        private void unlockbuttons()
        {
            CaptchaButton.Enabled = true;
            TextCaptcha.Enabled = true;
            button1.Enabled = true;
        }
    }
}
