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
    public partial class Add_Test2 : Form
    {
        Test test;

        public Add_Test2(Test test)
        {
            InitializeComponent();
            this.test = test;

            textBox_AddQuestion.TextChanged += new EventHandler(text_changed);
            comboBox_SelectQuestion.Items.Add(test.Title);
            comboBox_SelectQuestion.SelectedIndex = 0;
            
        }

        private void text_changed(object sender, EventArgs e)
        {

            if (textBox_AddQuestion.Text != "" && textBox_AddQuestion.Text.Length <= 255)
            {
                panelAddQuestion.Visible = true;
            }
            else
            {
                panelAddQuestion.Visible = false;
            }
            
        }

        private void button_AddQuestionVariant_Click(object sender, EventArgs e)
        {
            if (textBox_TitleQuestion.Text != "" && textBox_TitleQuestion.Text.Length <= 255 && checkedListBox_QuestionVariants.Items.IndexOf(textBox_TitleQuestion.Text) == -1)
            {
                checkedListBox_QuestionVariants.Items.Add(textBox_TitleQuestion.Text);
                textBox_TitleQuestion.Text = "";
            }
        }

        private void button_CancelQuestionVariant_Click(object sender, EventArgs e)
        {

        }

        private void button_AddNewQuestion_Click(object sender, EventArgs e)
        {
            
            if (textBox_AddQuestion.Text == "")
            {
                MessageBox.Show("Введите вопрос");
            }
            else if (checkedListBox_QuestionVariants.Items.Count == 0)
            {
                MessageBox.Show("Добавьте ответы");
            }
            else if (checkedListBox_QuestionVariants.SelectedItem == null)
            {
                MessageBox.Show("Выберите верный ответ");
            }
            else
            {
                using (Tests_DBContainer tests = new Tests_DBContainer())
                {

                    TestQuestion testquestion = new TestQuestion();
                    testquestion.Question = textBox_AddQuestion.Text;

                    testquestion.IsActual = 0;
                    if (checkBox_QuestionTrue.Checked)
                    {
                        testquestion.IsActual = 1;
                    }
                    
                    testquestion.Test = this.test;
                    tests.TestQuestion.Add(testquestion);


                    int i;
                    for (i = 0; i <= (checkedListBox_QuestionVariants.Items.Count - 1); i++)
                    {

                        TestQuestionAnswer answer = new TestQuestionAnswer();
                        answer.Answer = checkedListBox_QuestionVariants.Items[i].ToString();
                        if (checkedListBox_QuestionVariants.GetItemChecked(i))
                        {
                            answer.IsAnswer = Convert.ToByte(true);
                        }
                        answer.TestQuestion = testquestion;
                        tests.TestQuestionAnswer.Add(answer);

                    }

       
                    tests.SaveChanges();

                    checkedListBox_QuestionVariants.Items.Clear();
                    textBox_EnterdQuestions.Text += textBox_AddQuestion.Text + Environment.NewLine;
                    textBox_AddQuestion.Text = "";
                }
            }
        }

        private void button_FinishAddTest_Click(object sender, EventArgs e)
        {
            using (Tests_DBContainer tests = new Tests_DBContainer())
            {

                if(tests.TestQuestion.Where(t => t.TestId == test.Id).Count() == 0)
                {
                    MessageBox.Show("Добавьте вопрос");
                }
                else if (tests.TestQuestionAnswer.Where(t => t.TestQuestion.TestId == test.Id).Count() == 0)
                {
                    MessageBox.Show("Добавьте ответы");
                }
                else
                {
                    this.test.IsActual = 1;
                    tests.SaveChanges();
                    this.Close();
                }

            }
        }

        private void bunifuImageButton1_Close_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Сохраните тест");
        }
    }
}
