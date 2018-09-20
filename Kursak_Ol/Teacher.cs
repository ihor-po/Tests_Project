using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Threading.Timer;


namespace Kursak_Ol
{
    public partial class Teacher : Form
    {
        public Teacher( User user)
        {
            InitializeComponent();
            label1_Name.Text = $"{user.LastName} {user.FirstName} {user.MiddleName}";
            //bunifu это теже кнопки 
            // 4 кнопки которые закрывают сворачивают и т.д
            this.bunifuImageButton1_Close.Click += BunifuImageButton1_Close_Click;
            this.bunifuImageButton1_Max.Click += BunifuImageButton1_Max_Click;
            this.bunifuImageButton2_Norm.Click += BunifuImageButton2_Norm_Click;
            this.bunifuImageButton1_Min.Click += BunifuImageButton1_Min_Click;
            this.panel14_Opoves.Visible = false;
            this.label16_Log_Opov.Text = user.Login;
            //выводит на несколько секунд сообщение
            timer1.Tick += Timer1_Tick;
            timer1.Start();
            this.button1_Close.Click += Button1_Close_Click;
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            //Вывожу а потом закрываю таймером потока
            timer1.Stop();
            bunifuTransition1.ShowSync(panel14_Opoves);
            TimerCallback stCallback=new TimerCallback(Panal_Visibl);
            Timer timer = new Timer(stCallback);
            timer.Change(2500, 3000);
        }

        private void Panal_Visibl(object state)
        {
            (state as Timer).Dispose();
            bunifuTransition1.HideSync(panel14_Opoves);
        }

        private void Button1_Close_Click(object sender, EventArgs e)
        {
            //для вас как она будет функционировать не знаю
            MessageBox.Show("Событие еще не определенно! что делать", "Оповещение", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        private void BunifuImageButton1_Min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BunifuImageButton2_Norm_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.bunifuImageButton2_Norm.Visible = false;
            this.bunifuImageButton1_Max.Visible = true;
        }

        private void BunifuImageButton1_Max_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.bunifuImageButton1_Max.Visible = false;
            this.bunifuImageButton2_Norm.Visible = true;
        }

        private void BunifuImageButton1_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
