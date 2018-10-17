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
    public partial class Edit_Test : MyForm
    {
        private int testId;
        private int questionId;

        public Edit_Test(int testId, int questionId)
        {
            InitializeComponent();

            this.testId = testId;
            this.questionId = questionId;

            //наследуемый метод
            base.Top_Button(bunifuImageButton1_Min, bunifuImageButton1_Max, bunifuImageButton2_Norm, bunifuImageButton1_Close);

            using (Tests_DBContainer tests = new Tests_DBContainer())
            {
                var rowTest = tests.Test.FirstOrDefault(t => t.Id == testId);
                if (rowTest != null)
                {
                    //textBox_AddTestTitle.Text = rowTest.Title;
                }

                var rowQuestion = tests.TestQuestion.FirstOrDefault(t => t.Id == questionId);
                if (rowQuestion != null)
                {
                    textBox_AddQuestion.Text = rowQuestion.Question;
                }

                renderAnswerList();
            }
        }

        private void renderAnswerList()
        {
            using (Tests_DBContainer tests = new Tests_DBContainer())
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();

                var answersList = tests.TestQuestionAnswer.Where(t => t.TestQuestionId == questionId).ToList();
                foreach(var row in answersList)
                {
                    dataGridView1.Rows.Add(
                        new object[] {
                            row.Answer,
                            row.IsAnswer == 1 ? true : false,
                            row.Id
                        }
                    );
                }
            }
        }

        private void button_AddNewQuestion_Click(object sender, EventArgs e)
        {
            using (Tests_DBContainer tests = new Tests_DBContainer())
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    int aId;
                    int.TryParse(row.Cells["Id"].Value.ToString(), out aId);

                    var answer = tests.TestQuestionAnswer.FirstOrDefault(t => t.Id == aId);

                    if (answer != null)
                    {
                        answer.Answer = row.Cells["Answer"].Value.ToString();
                        answer.IsAnswer = Convert.ToByte(row.Cells["IsAnswer"].Value);
                        tests.SaveChanges();
                    }
                }

                var question = tests.TestQuestion.FirstOrDefault(t => t.Id == questionId);
                if (question != null)
                {
                    question.Question = textBox_AddQuestion.Text;
                    tests.SaveChanges();
                }

                var test = tests.Test.FirstOrDefault(t => t.Id == testId);
                if (test != null)
                {
                    //test.Title = textBox_AddTestTitle.Text;
                    tests.SaveChanges();
                }
            }
        }

        private void button__Add_Click(object sender, EventArgs e)
        {
            using (Tests_DBContainer tests = new Tests_DBContainer())
            {
                var answer = new TestQuestionAnswer() { Answer = "", TestQuestionId = questionId, IsAnswer = 0 };
                tests.TestQuestionAnswer.Add(answer);
                tests.SaveChanges();
                renderAnswerList();
            }
        }

        private void button_FinishAddTest_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
