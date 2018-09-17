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
    public partial class Registration : Form
    {
        private int iSlaider;
        public Registration()
        {
            InitializeComponent();
            this.bunifuImageButton1_Close.Click += BunifuImageButton1_Close_Click;
            this.bunifuImageButton1_Max.Click += BunifuImageButton1_Max_Click;
            this.bunifuImageButton2_Norm.Click += BunifuImageButton2_Norm_Click;
            this.bunifuImageButton1_Min.Click += BunifuImageButton1_Min_Click;
           
            TimerCallback startCallback=new TimerCallback(Show_Slider);
            Timer timer=new Timer(startCallback);
            timer.Change(0,3000);
        }

        private void Show_Slider(object state)
        {
            Close_Slaider();
            Show_Slaider();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Close_Slaider();
            Show_Slaider();
        }

        private void Show_Slaider()
        {
            Bitmap My_image = new Bitmap(String.Format(@"..\..\images\Slider\{0}.png", iSlaider));
            this.pictureBox3_Slider.Image = My_image;
            this.bunifuTransition1.Show(this.pictureBox3_Slider);
            this.button2_Vhod_Form.Click += Button2_Vhod_Form_Click;
            this.button2_Registretion_Form.Click += Button2_Registretion_Form_Click;
            this.button1_Registretion.Click+= Button2_Registretion_Form_Click;
        }

        private void Button2_Registretion_Form_Click(object sender, EventArgs e)
        {
            this.panel7_Registr.Visible = true;
        }

        private void Button2_Vhod_Form_Click(object sender, EventArgs e)
        {
            this.panel7_Registr.Visible = false;
        }

        private void Close_Slaider()
        {
            this.bunifuTransition2.HideSync(this.pictureBox3_Slider);
            iSlaider++;
            if (iSlaider == 3)
            {
                iSlaider = 0;
            }
            
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
            Application.Exit();
        }
    }
}
