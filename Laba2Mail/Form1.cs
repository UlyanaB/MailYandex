using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace Laba2Mail
{
    public partial class Form1 : Form
    {
        SmtpClient smtp;
        private string email = "";
        private string password = "";
       
        private string file = "";

        public Form1()
        {
            InitializeComponent();
            TextBoxPassword.PasswordChar = '*';
        }

        public void Avtor()
        {
            try
            {
                smtp = new SmtpClient("smtp.yandex.ru", 25)
                {
                    Credentials = new NetworkCredential(email, password),
                    EnableSsl = true
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка");
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnAvtor_Click(object sender, EventArgs e)
        {
            email = TextBoxEmail.Text;
            password = TextBoxPassword.Text;

            if(email == "" || password == "")
            {
                MessageBox.Show("Введите данные в поля почта и пароль");
            }
            else
            {
                Avtor();
            }
        }

        private void BtnFile_Click(object sender, EventArgs e)
        {
           OpenFileDialog OFD = new OpenFileDialog();
           OFD.Filter = "all| *.*";
            if (OFD.ShowDialog() == DialogResult.OK)
            {
                file = OFD.FileName;
                LableFile.Text = OFD.FileName;
            }
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
           
                try
                {
                    MailAddress from = new MailAddress(email);
                    MailAddress to = new MailAddress(TextBoxTo.Text);
                    MailMessage message = new MailMessage(from, to)
                    {
                        Subject = TextBoxTatle.Text,
                        IsBodyHtml = false,
                        Body = TextBoxText.Text,
                    };

                    if (file != "") message.Attachments.Add(new Attachment($"{file}"));
                    smtp.Send(message);
                    MessageBox.Show("Сообщение отправлено!");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Неверный формат электронной почты. Почта должна иметь окончания - ****@****.**");
                    TextBoxTo.Clear();
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("Строка с адресом не должна быть пуста");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            
        }
    }
}
