using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace Kursak_Ol
{
    public partial class Registration : MyForm
    {
        private int iSlaider;
        public Registration()
        {
            InitializeComponent();
            //bunifu это теже кнопки 

            this.bunifuImageButton1_Close.Click += BunifuImageButton1_Close_Click;
            //наследуемый метод
            base.Top_Button(bunifuImageButton1_Min, bunifuImageButton1_Max, bunifuImageButton2_Norm);
            this.Load += Registration_Load;
            this.button2_Vhod_Form.Click += Button2_Vhod_Form_Click;
            this.button2_Registretion_Form.Click += Button2_Registretion_Form_Click;
            this.button1_Registretion.Click += Button2_Registretion_Form_Click;
            //Работа логотипа
            TimerCallback startCallback = new TimerCallback(Show_Slider);
            Timer timer = new Timer(startCallback);
            timer.Change(4000, 4000);

        }

        private void Registration_Load(object sender, EventArgs e)//загружаю все события 
        {
            //проверки всех боксов на пробелы
            this.textBox1_Adres.TextChanged += new EventHandler(textBox);
            this.textBox1_LastName.TextChanged += new EventHandler(textBox);
            this.textBox1_Login.TextChanged += new EventHandler(textBox);
            this.textBox1_Login_Registr.TextChanged += new EventHandler(textBox);
            this.textBox1_Middle_name.TextChanged += new EventHandler(textBox);
            this.textBox1_Name.TextChanged += new EventHandler(textBox);
            this.textBox1_Password.TextChanged += new EventHandler(textBox);
            this.textBox1_Password_Registr.TextChanged += new EventHandler(textBox);
            this.textBox1_Phone.TextChanged += new EventHandler(textBox);
            this.button1_Registration_DB.Click += Button1_Registration_DB_Click;
            //labl который будет выводить ошибки на форме для пользователя
            label14_Null.Visible = false;
            label6_Error.Visible = false;
            label14_phone.Visible = false;
            label14_Log_Povtor.Visible = false;
            label14_Phone_Error.Visible = false;
            panel12_Opovesh.Visible = false;
            label15_Vhod_Null.Visible = false;
            timer1.Tick += Timer1_Tick1;
            this.button1_Vkhod.Click += Button1_Vkhod_Click;
        }

        private void Button1_Vkhod_Click(object sender, EventArgs e)
        {
            if (textBox1_Login.Text == "" || textBox1_Password.Text == "")
            {
                label15_Vhod_Null.Visible = true;
                return;
            }

            using (Tests_DBContainer tests = new Tests_DBContainer())
            {
                var user = tests.User.FirstOrDefault(z =>
                    z.Login == textBox1_Login.Text && z.Password == textBox1_Password.Text);
                if (user == null)
                {
                    this.label6_Error.Visible = true;
                    return;
                }

                if (user.Role.Title == "Студент")
                {
                    Pupil pupil = new Pupil(user);
                    textBox1_Login.Text = null;
                    textBox1_Password.Text = null;
                    pupil.ShowDialog();
                }
                else if (user.Role.Title == "Преподователь")
                {
                    Teacher teacher = new Teacher(user);
                    textBox1_Login.Text = null;
                    textBox1_Password.Text = null;
                    teacher.ShowDialog();
                }
            }
        }

        private void Timer1_Tick1(object sender, EventArgs e)
        {
            bunifuTransition3.HideSync(this.panel12_Opovesh);
            timer1.Stop();
        }

        private void Button1_Registration_DB_Click(object sender, EventArgs e)
        {
            if (textBox1_Adres.Text == "" || textBox1_LastName.Text == "" || textBox1_Phone.Text == "" ||
                textBox1_Login_Registr.Text == "" ||
                textBox1_Middle_name.Text == "" || textBox1_Password_Registr.Text == "" || textBox1_Name.Text == "")
            {
                label14_Null.Visible = true;
                return;
            }
            string phoneNumber = @"^380\d{9}$";
            if (!Regex.IsMatch(textBox1_Phone.Text, phoneNumber, RegexOptions.IgnoreCase))//проверка на правильность написания телефона
            {
                label14_phone.Visible = true;
                return;
            }

            using (Tests_DBContainer tests = new Tests_DBContainer())
            {
                string login = textBox1_Login_Registr.Text;
                var log = tests.User.FirstOrDefault(z => z.Login == login);
                if (log != null)//проверка на логин есть или нет его в БД
                {
                    label14_Log_Povtor.Visible = true;
                    return;
                }

                string ph = textBox1_Phone.Text;
                var phone = tests.User.FirstOrDefault(z => z.Phone == ph);
                if (phone != null)//проверка на телефона есть или нет его в БД
                {
                    label14_Phone_Error.Visible = true;
                    return;
                }

                var id = tests.Role.FirstOrDefault(z => z.Title == "Студент");//роль
                if (id == null)//проверка роли есть она в БД 
                {
                    return;
                }
                //Создаем пользователя
                User rUser = new User
                {
                    Address = textBox1_Adres.Text,
                    FirstName = textBox1_Name.Text,
                    Login = textBox1_Login_Registr.Text,
                    LastName = textBox1_LastName.Text,
                    MiddleName = textBox1_Middle_name.Text,
                    Password = textBox1_Password_Registr.Text,
                    Phone = textBox1_Phone.Text,
                    RoleId = id.Id
                };
                tests.User.Add(rUser);
                tests.SaveChanges();
            }
            //все происходят манипуляции
            this.label16_Log_Opov.Text = textBox1_Login_Registr.Text;
            bunifuTransition3.ShowSync(this.panel12_Opovesh);
            timer1.Start();
            this.textBox1_Adres.Text = null;
            this.textBox1_LastName.Text = null;
            this.textBox1_Login_Registr.Text = null;
            this.textBox1_Middle_name.Text = null;
            this.textBox1_Password_Registr.Text = null;
            this.textBox1_Name.Text = null;
            this.textBox1_Phone.Text = null;
        }

        private void textBox(object sender, EventArgs a)
        {
            label14_Null.Visible = false;
            label14_phone.Visible = false;
            label14_Log_Povtor.Visible = false;
            label6_Error.Visible = false;
            label14_Phone_Error.Visible = false;
            label15_Vhod_Null.Visible = false;
            TextBox text = sender as TextBox;
            if (text.Name == textBox1_Adres.Name)
            {
                if (String.IsNullOrWhiteSpace(textBox1_Adres.Text))
                {
                    textBox1_Adres.Text = null;
                }
            }
            else if (text.Name == textBox1_LastName.Name)
            {
                if (String.IsNullOrWhiteSpace(textBox1_LastName.Text))
                {
                    textBox1_LastName.Text = null;
                }
            }
            else if (text.Name == textBox1_Login.Name)
            {
                if (String.IsNullOrWhiteSpace(textBox1_Login.Text))
                {
                    textBox1_Login.Text = null;
                }
            }
            else if (text.Name == textBox1_Login_Registr.Name)
            {
                if (String.IsNullOrWhiteSpace(textBox1_Login_Registr.Text))
                {
                    textBox1_Login_Registr.Text = null;
                }
            }
            else if (text.Name == textBox1_Middle_name.Name)
            {
                if (String.IsNullOrWhiteSpace(textBox1_Middle_name.Text))
                {
                    textBox1_Middle_name.Text = null;
                }
            }
            else if (text.Name == textBox1_Name.Name)
            {
                if (String.IsNullOrWhiteSpace(textBox1_Name.Text))
                {
                    textBox1_Name.Text = null;
                }
            }
            else if (text.Name == textBox1_Password.Name)
            {
                if (String.IsNullOrWhiteSpace(textBox1_Password.Text))
                {
                    textBox1_Password.Text = null;
                }
            }
            else if (text.Name == textBox1_Password_Registr.Name)
            {
                if (String.IsNullOrWhiteSpace(textBox1_Password_Registr.Text))
                {
                    textBox1_Password_Registr.Text = null;
                }
            }
            else if (text.Name == textBox1_Phone.Name)
            {
                if (String.IsNullOrWhiteSpace(textBox1_Phone.Text))
                {
                    textBox1_Phone.Text = null;
                }
            }
        }

        private void Show_Slider(object state)
        {
            Close_Slaider();
            Show_Slaider();
        }

        private void Show_Slaider()
        {
            Bitmap My_image = new Bitmap(String.Format(@"..\..\images\Slider\{0}.png", iSlaider));
            this.pictureBox3_Slider.Image = My_image;
            this.bunifuTransition1.Show(this.pictureBox3_Slider);

        }

        private void Button2_Registretion_Form_Click(object sender, EventArgs e)
        {
            this.panel7_Registr.Visible = true;
        }

        private void Button2_Vhod_Form_Click(object sender, EventArgs e)
        {
            this.panel7_Registr.Visible = false;
            this.textBox1_Adres.Text = null;
            this.textBox1_LastName.Text = null;
            this.textBox1_Login_Registr.Text = null;
            this.textBox1_Middle_name.Text = null;
            this.textBox1_Password_Registr.Text = null;
            this.textBox1_Name.Text = null;
            this.textBox1_Phone.Text = null;
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

        private void BunifuImageButton1_Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
