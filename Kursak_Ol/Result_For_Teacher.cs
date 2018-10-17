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
    public partial class Result_For_Teacher : MyForm
    {
        private List<Category> Lcategor=new List<Category>();
        private List<Test>Ltest=new List<Test>();
        public Result_For_Teacher()
        {
            InitializeComponent();
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
            ComboBox_SelectCategory_Show();// метод который выводит категории в комбобокс
            Selected_Category();// метод который выводит тесты в комбобокс
            Vivod_Users();// метод который выводит пользователей в листбокс
        }

        private void ComboBox_Select_Test_SelectedIndexChanged(object sender, EventArgs e)//событие на замену теста
        {
            Vivod_Users();// метод который выводит пользователей в листбокс
        }

        private void ComboBox_SelectCategory_SelectedIndexChanged(object sender, EventArgs e)//событие на замену категории
        {
            Selected_Category();// метод который выводит тесты в комбобокс
        }

        private void ComboBox_SelectCategory_Show()
        {
            using (Tests_DBContainer db=new Tests_DBContainer())
            {
                Lcategor = db.Category.ToList();//заполняем лист категории для долнейшего использования
            }

            foreach (Category VARIABLE in Lcategor)
            {
                this.comboBox_SelectCategory.Items.Add(VARIABLE.Title);//заполняем комбобокс категориями
            }

            if (Lcategor.Count!=0)
            {
                this.comboBox_SelectCategory.SelectedIndex = 0;//ставим первый айтем по умолчанию как выбраный
            }
        }

        private void Selected_Category()
        {
            var categori = Lcategor.Find(z => z.Title == this.comboBox_SelectCategory.SelectedItem.ToString());//находим по выбраному из комбобокса селект
            this.comboBox_Select_Test.Items.Clear();//всегда очишаем комбобокс тестов

            using (Tests_DBContainer db = new Tests_DBContainer())
            {
                var test = db.Test.Where(z => z.CategoryId == categori.Id && z.IsActual==1).ToList();//люмбда выражение для нахождения id лист Test
                Ltest = test;
                foreach (Test VARIABLE in test)
                {
                    this.comboBox_Select_Test.Items.Add(VARIABLE.Title);//заполняем комбобокс тестов
                }

                if (test.Count!=0)
                {
                    this.comboBox_Select_Test.SelectedIndex = 0;//ставим первый айтем как выбранный
                }
            }

            
        }

        private void Vivod_Users()
        {
            this.listBox_Test_Results_For_Teacher.Items.Clear();//очистка лист бокса
            var test = Ltest.Find(z => z.Title == this.comboBox_Select_Test.SelectedItem.ToString());//находим выбранный тест
            using (Tests_DBContainer db = new Tests_DBContainer())
            {
                var userTest = db.UserTest.Where(z => z.Id == test.Id).ToList();//по айдишнику теста находим пользователей которые его проходили
                var countQvechin = db.TestQuestion.Count(z => z.TestId == test.Id && z.IsActual == 1);//расчитываем количество вопросов в тесте
                foreach (var VARIABLE in userTest)
                {
                    //для расчета времени прохождения теста
                    DateTime a = new DateTime(VARIABLE.StartDate.Millisecond);
                    DateTime b = new DateTime(VARIABLE.EndDate.Millisecond);
                    //выводим в лист бокс информацию
                    this.listBox_Test_Results_For_Teacher.Items.Add(
                        $"[{VARIABLE.User.FirstName} {VARIABLE.User.LastName} {VARIABLE.User.MiddleName}] количество вопросов: {countQvechin} количество правельных ответов: {VARIABLE.Result} потрачено времени: {b.Subtract(a).Ticks}");
                }
            }
        }
    }
}
