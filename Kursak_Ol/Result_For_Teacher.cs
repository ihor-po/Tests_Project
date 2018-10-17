using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursak_Ol
{
    public partial class Result_For_Teacher : MyForm, IResult
    {
        private List<Category> Lcategor=new List<Category>();
        private List<Test>Ltest=new List<Test>();
        private User user;
        public Result_For_Teacher(User user)
        {
            InitializeComponent();
            this.user = user;
            //наследуемый метод
            base.Top_Button(bunifuImageButton1_Min, bunifuImageButton1_Max, bunifuImageButton2_Norm, bunifuImageButton1_Close);
            this.comboBox_SelectCategory.SelectedIndexChanged += ComboBox_SelectCategory_SelectedIndexChanged;
            
            this.comboBox_Select_Test.SelectedIndexChanged += ComboBox_Select_Test_SelectedIndexChanged;
            this.Load += Result_For_Teacher_Load;
            this.button_CancelTestResultShow.Click += Button_CancelTestResultShow_Click;
        }

        private void Button_CancelTestResultShow_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Result_For_Teacher_Load(object sender, EventArgs e)//загружаю боксы по умолчанию
        {
            ComboBox_SelectCategory_Show();// метод который выводит категории в комбобокс наследованный от интерфейса
            Combobox_Selected_Test();// метод который выводит тесты в комбобокс наследованный от интерфейса
            Show_user();// метод который выводит пользователей в листбокс наследованный от интерфейса
        }

        private void ComboBox_Select_Test_SelectedIndexChanged(object sender, EventArgs e)//событие на замену теста
        {
            Show_user();// метод который выводит пользователей в листбокс
        }

        private void ComboBox_SelectCategory_SelectedIndexChanged(object sender, EventArgs e)//событие на замену категории
        {
            Combobox_Selected_Test();// метод который выводит тесты в комбобокс
        }

        public void ComboBox_SelectCategory_Show()
        {
            using (Tests_DBContainer db=new Tests_DBContainer())
            {
                Lcategor = db.Category.ToList();//заполняем лист категории для долнейшего использования
            }

            foreach (Category VARIABLE in Lcategor)
            {
                this.comboBox_SelectCategory.Items.Add(VARIABLE.Title);//заполняем комбобокс категориями
            }

            if (Lcategor.Count != 0)
            {
                this.comboBox_SelectCategory.SelectedIndex = 0;//ставим первый айтем по умолчанию как выбраный
            }
            
        }

        public void Combobox_Selected_Test()
        {
            var categori = Lcategor.Find(z => z.Title == this.comboBox_SelectCategory.SelectedItem.ToString());//находим по выбраному из комбобокса селект
            this.comboBox_Select_Test.Items.Clear();//всегда очишаем комбобокс тестов

            using (Tests_DBContainer db = new Tests_DBContainer())
            {
                var ticher = db.TestCreator.FirstOrDefault(z => z.UserId == user.Id);
                if (ticher != null)
                {
                    var test = db.Test.Where(z => z.CategoryId == categori.Id && z.IsActual == 1 && z.Id == ticher.TestId).ToList();//люмбда выражение для нахождения id лист Test
                    if (test.Count != 0)
                    {
                        Ltest = test;
                        foreach (Test VARIABLE in test)
                        {
                            this.comboBox_Select_Test.Items.Add(VARIABLE.Title);//заполняем комбобокс тестов
                        }

                        if (test.Count != 0)
                        {
                            this.comboBox_Select_Test.SelectedIndex = 0;//ставим первый айтем как выбранный
                        }

                    }
                    else
                    {
                        this.comboBox_Select_Test.Items.Add("");
                        this.comboBox_Select_Test.SelectedIndex = 0;
                    }
                }
            }
        }

        public void Show_user()
        {
            this.listBox_Test_Results_For_Teacher.Items.Clear();//очистка лист бокса
            var test = Ltest.Find(z => z.Title == this.comboBox_Select_Test.SelectedItem.ToString());//находим выбранный тест
            if (test != null)
            {
                using (Tests_DBContainer db = new Tests_DBContainer())
                {
                    var userTest = db.UserTest.Where(z => z.TestId == test.Id).ToList();//по айдишнику теста находим пользователей которые его проходили

                    foreach (var VARIABLE in userTest)
                    {
                        //для расчета времени прохождения теста
                        TimeSpan span = (VARIABLE.EndDate - VARIABLE.StartDate);
                        //выводим в лист бокс информацию
                        this.listBox_Test_Results_For_Teacher.Items.Add(
                            $"[{VARIABLE.User.FirstName} {VARIABLE.User.LastName} {VARIABLE.User.MiddleName}] Дата прохождения теста: {VARIABLE.StartDate.ToString("d")} Результат: {VARIABLE.Result} Потрачено времени: {span.Duration()}");
                    }
                }
            }
        }
    }
}
