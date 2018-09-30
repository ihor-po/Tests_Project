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
    public partial class Add_Test : MyForm
    {

        User user = null;

        public Add_Test(User user)
        {
            InitializeComponent();
            //наследуемый метод
            base.Top_Button(bunifuImageButton1_Min, bunifuImageButton1_Max, bunifuImageButton2_Norm, bunifuImageButton1_Close);
            //Кнопка для вызова панели добавить категорию
            this.button_AddCategory.Click += Button_AddCategory_Click;
            //Кнопка для скрытия панели добавить категорию
            this.button2_Categori_Close.Click += Button2_Categori_Close_Click;

            this.renderCategoryList();

            this.user = user;
        }

        private void renderCategoryList()
        {
            using (Tests_DBContainer tests = new Tests_DBContainer())
            {
                comboBox_SelectCategory.DataSource = tests.Category.ToList();
                comboBox_SelectCategory.ValueMember = "Id";
                comboBox_SelectCategory.DisplayMember = "Title";
            }
        }

        private void Button2_Categori_Close_Click(object sender, EventArgs e)
        {
            this.bunifuTransition1.HideSync(this.panel_AddNewCategory);
        }

        private void Button_AddCategory_Click(object sender, EventArgs e)
        {
            this.bunifuTransition1.ShowSync(this.panel_AddNewCategory);
        }

        private void button1_Categiri_Click(object sender, EventArgs e)
        {
            if(textBox_AddNewCategory.Text != "")
            {
                using (Tests_DBContainer tests = new Tests_DBContainer())
                {

                    Category category = new Category();
                    category.Title = textBox_AddNewCategory.Text;
                    tests.Category.Add(category);
                    tests.SaveChanges();

                    this.bunifuTransition1.HideSync(this.panel_AddNewCategory);
                    this.renderCategoryList();
                }
            }
        }

        private void button__Add_Click(object sender, EventArgs e)
        {
            this.bunifuTransition1.ShowSync(this.panelAddQuestion);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox_TitleQuestion.Text != "")
            {
                checkedListBox1.Items.Add(textBox_TitleQuestion.Text);
                this.bunifuTransition1.HideSync(this.panelAddQuestion);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.bunifuTransition1.HideSync(this.panelAddQuestion);
        }

        private void button_FinishAddTest_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_AddNewQuestion_Click(object sender, EventArgs e)
        {
            label_ErrorAddTest.Text = "";
            if (textBox_AddTestTitle.Text == "")
            {
                label_ErrorAddTest.Text = "Введите название";
            }
            else if (textBox_AddQuestion.Text == "")
            {
                label_ErrorAddTest.Text = "Введите вопрос";
            }
            else if(checkedListBox1.Items.Count == 0)
            {
                label_ErrorAddTest.Text = "Добавьте ответы";
            }
            else if(checkedListBox1.SelectedItem == null)
            {
                label_ErrorAddTest.Text = "Выберите верный ответ";
            }
            else
            {
                using (Tests_DBContainer tests = new Tests_DBContainer())
                {

                    Test test = new Test();
                    test.Title = textBox_AddTestTitle.Text;
                    test.IsActual = 1;
                    int idCat = Convert.ToInt32(comboBox_SelectCategory.SelectedValue.ToString());
                    test.Category = tests.Category.FirstOrDefault(cat => cat.Id == idCat);

                    tests.Test.Add(test);

                    TestQuestion testquestion = new TestQuestion();
                    testquestion.Question = textBox_AddQuestion.Text;
                    testquestion.IsActual = 1;
                    testquestion.Test = test;

                    tests.TestQuestion.Add(testquestion);

                    TestCreator testcreator = new TestCreator();
                    testcreator.TestId = test.Id;
                    testcreator.UserId = user.Id;

                    tests.TestCreator.Add(testcreator);



                    int i;
                    for (i = 0; i <= (checkedListBox1.Items.Count - 1); i++)
                    {

                        TestQuestionAnswer answer = new TestQuestionAnswer();
                        answer.Answer = checkedListBox1.Items[i].ToString();
                        if (checkedListBox1.GetItemChecked(i))
                        {
                            answer.IsAnswer = Convert.ToByte(true);
                        }
                        answer.TestQuestion = testquestion;
                        tests.TestQuestionAnswer.Add(answer);
                        
                    }


                    

                    tests.SaveChanges();

                    this.Close();
                }
            }
        }
    }
}
