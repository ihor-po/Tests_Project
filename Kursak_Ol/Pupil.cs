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
        string nameTest;
        int IdUser;
        private User user;
        public Pupil(User user)
        {
            this.user = user;
            IdUser = user.Id;
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
            //добавляем названия предметов из базы
            using (Tests_DBContainer db = new Tests_DBContainer())
            {
                var category = db.Category.ToList();
                foreach (var item in category)
                {
                    comboBox1_Predmet.Items.Add(item.Title.ToString());
                }
            }
            //изменения предмета
            comboBox1_Predmet.TextChanged += ComboBox1_Predmet_TextChanged;
            button1_Start.Click += Button1_Start_Click;
            listView1_Name_Test.SelectedIndexChanged += ListView1_Name_Test_SelectedIndexChanged;
        }

        private void ListView1_Name_Test_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1_Name_Test.SelectedItems.Count == 0)
            {
                button1_Start.Enabled = false;
                nameTest = "";
            }
            else
            {
                nameTest = listView1_Name_Test.SelectedItems[0].Text;
                button1_Start.Enabled = true;
            }
        }

        private void Button1_Start_Click(object sender, EventArgs e)
        {
            Pupil_Test form3 = new Pupil_Test(IdUser, nameTest);
            form3.Show();
        }

        private void ComboBox1_Predmet_TextChanged(object sender, EventArgs e)
        {
            listView1_Name_Test.Items.Clear();
            var namePredmet = comboBox1_Predmet.SelectedItem.ToString();
            using (Tests_DBContainer db = new Tests_DBContainer())
            {
                var tests = db.Test.Join(
                    db.Category,
                    t => t.CategoryId,
                    c => c.Id,
                    (t, c) => new
                    {
                        TitleTest = t.Title,
                        TitleCategory = c.Title
                    }).Where(n => n.TitleCategory.ToString() == namePredmet).ToList();

                foreach (var item in tests)
                {
                    ListViewItem list = new ListViewItem(item.TitleTest.ToString());
                    listView1_Name_Test.Items.Add(list);
                }
            }
        }

        private void Button1_Statistika_Click(object sender, EventArgs e)
        {
            Result_For_Pupil pub=new Result_For_Pupil(user);
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
