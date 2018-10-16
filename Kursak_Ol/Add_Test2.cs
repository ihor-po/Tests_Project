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
        int testId;

        public Add_Test2(int id)
        {
            InitializeComponent();
            this.testId = id;

            textBox_AddQuestion.TextChanged += new EventHandler(text_changed);
            comboBox_SelectQuestion.SelectedIndexChanged += new EventHandler(comboBoxChanged);
            renderListQuestions();
            
        }

        private void comboBoxChanged(object sender, EventArgs e)
        {
            renderListAnswers();
        }

        private void renderListAnswers()
        {
            textBox_EnterdQuestions.Text = "";
            int id = Convert.ToInt32(comboBox_SelectQuestion.SelectedValue);
            using (Tests_DBContainer tests = new Tests_DBContainer()) { 
                foreach (TestQuestionAnswer tqa in tests.TestQuestionAnswer.Where(t => t.TestQuestionId == id))
                {
                    textBox_EnterdQuestions.Text += tqa.Answer + Environment.NewLine;
                }
            }
        }

        private void renderListQuestions(int ind = 0)
        {
            using (Tests_DBContainer tests = new Tests_DBContainer())
            {

                comboBox_SelectQuestion.DataSource = tests.TestQuestion.Where(t => t.TestId == this.testId).ToList();
                comboBox_SelectQuestion.ValueMember = "Id";
                comboBox_SelectQuestion.DisplayMember = "Question";
               

                if (comboBox_SelectQuestion.Items.Count > 0)
                {
                    comboBox_SelectQuestion.SelectedIndex = ind;
                    renderListAnswers();
                }
                
                
            }
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

            bool error = false;

            using (Tests_DBContainer tests = new Tests_DBContainer())
            {
                if (textBox_AddQuestion.Text == "")
                {
                    MessageBox.Show("Введите вопрос");
                    error = true;
                }
                if (checkedListBox_QuestionVariants.Items.Count == 0)
                {
                    MessageBox.Show("Добавьте ответы");
                    error = true;
                }
                if (checkedListBox_QuestionVariants.SelectedItem == null || checkedListBox_QuestionVariants.CheckedItems.Count == 0)
                { 
                    MessageBox.Show("Выберите верный ответ");
                    error = true;
                }

                if (tests.TestQuestion.Where(t => t.TestId == this.testId && t.Question == textBox_AddQuestion.Text).ToList().Count > 0)
                {
                    MessageBox.Show("Есть такой вопрос");
                    error = true;
                }
                if (!error)
                {


                    TestQuestion testquestion = new TestQuestion();
                    testquestion.Question = textBox_AddQuestion.Text;

                    testquestion.IsActual = 0;
                    if (checkBox_QuestionTrue.Checked)
                    {
                        testquestion.IsActual = 1;
                    }

                    testquestion.TestId = this.testId;
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
                    textBox_AddQuestion.Text = "";
                    renderListQuestions((comboBox_SelectQuestion.Items.Count));

                }
            }
        }

        private void button_FinishAddTest_Click(object sender, EventArgs e)
        {
            using (Tests_DBContainer tests = new Tests_DBContainer())
            {

                if(tests.TestQuestion.Where(t => t.TestId == this.testId).Count() == 0)
                {
                    MessageBox.Show("Добавьте вопрос");
                }
                else if (tests.TestQuestionAnswer.Where(t => t.TestQuestion.TestId == this.testId).Count() == 0)
                {
                    MessageBox.Show("Добавьте ответы");
                }
                else
                {
                    Test test = tests.Test.FirstOrDefault(t => t.Id == this.testId);
                    if(test != null)
                    {
                        test.IsActual = 1;
                        tests.SaveChanges();
                        this.Close();
                    }
                    
                }

            }
        }

        private void bunifuImageButton1_Close_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Сохраните тест");
        }
    }
}
