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
    public partial class Pupil : MyForm
    {
        public Pupil(User user)
        {
            InitializeComponent();
            label1_Name.Text = $"{user.LastName} {user.FirstName} {user.MiddleName}";
            //наследуемый метод
            base.Top_Button(bunifuImageButton1_Min, bunifuImageButton1_Max, bunifuImageButton2_Norm, bunifuImageButton1_Close);
            this.panel14_Opoves.Visible = false;
            this.label16_Log_Opov.Text = user.Login;
            //выводит на несколько секунд сообщение
            timer1.Tick += Timer1_Tick;
            timer1.Start();
            this.button1_Close.Click += Button1_Close_Click;
            this.button1_Statistika.Click += Button1_Statistika_Click;
        }

        private void Button1_Statistika_Click(object sender, EventArgs e)
        {
            Result_For_Pupil pub=new Result_For_Pupil();
            pub.ShowDialog();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            //Вывожу а потом закрываю таймером потока
            timer1.Stop();
            bunifuTransition1.ShowSync(panel14_Opoves);
            TimerCallback startCallback=new TimerCallback(Panal_Visibl);
            Timer timer=new Timer(startCallback);
            timer.Change(2500, 3000);
        }

        private void Panal_Visibl(object state)
        {
            (state as Timer).Dispose();
            bunifuTransition1.HideSync(panel14_Opoves);
        }

        private void Button1_Close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        
    }
}
