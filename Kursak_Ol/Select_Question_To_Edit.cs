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
    public partial class Select_Question_To_Edit : MyForm
    {
        private int testId;
        private int currentQuestion = 0;
        private byte questionIsActual = 0;

        public Select_Question_To_Edit(int testId)
        {
            InitializeComponent();

            this.testId = testId;

            //наследуемый метод
            base.Top_Button(bunifuImageButton1_Min, bunifuImageButton1_Max, bunifuImageButton2_Norm, bunifuImageButton1_Close);
            this.button_EditQuestion.Click += Button_EditQuestion_Click;

            using (Tests_DBContainer tests = new Tests_DBContainer())
            {
                var row = tests.Test.FirstOrDefault(t => t.Id == testId);
                if (row != null)
                {
                    label10.Text = row.Title;
                    renderQuestionList();
                }
            }
        }

        private void renderQuestionList()
        {
            using (Tests_DBContainer tests = new Tests_DBContainer())
            {
                var ds = tests.TestQuestion.Where(t => t.TestId == testId).ToList();
                listBox_SelectQuestionToEdit.DataSource = ds;
                listBox_SelectQuestionToEdit.DisplayMember = "Question";
                listBox_SelectQuestionToEdit.ValueMember = "Id";
            }
        }

        private void Button_EditQuestion_Click(object sender, EventArgs e)
        {
            if (currentQuestion > 0)
            {
                Edit_Test test = new Edit_Test(testId, currentQuestion);
                test.ShowDialog();
            }
        }

        private void listBox_SelectQuestionToEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            int.TryParse(listBox_SelectQuestionToEdit.SelectedValue.ToString(), out currentQuestion);

            using (Tests_DBContainer tests = new Tests_DBContainer())
            {
                var row = tests.TestQuestion.FirstOrDefault(t => t.Id == currentQuestion);
                if (row != null)
                {
                    questionIsActual = row.IsActual;

                    if (row.IsActual == 1)
                    {
                        button_TurnOn_OffQuestion.Text = "Отключить";
                    }
                    else
                    {
                        button_TurnOn_OffQuestion.Text = "Включить";
                    }
                }
            }
        }

        private void button_TurnOn_OffQuestion_Click(object sender, EventArgs e)
        {
            using (Tests_DBContainer tests = new Tests_DBContainer())
            {
                var row = tests.TestQuestion.FirstOrDefault(t => t.Id == currentQuestion);
                if (row != null)
                {
                    byte reverse = questionIsActual != (byte)0 ? (byte)0 : (byte)1;
                    row.IsActual = reverse;
                    tests.SaveChanges();
                }
            }

            this.renderQuestionList();
        }

        private void button_CancelEditQuestion_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_Delete_Question_Click(object sender, EventArgs e)
        {
            using (Tests_DBContainer tests = new Tests_DBContainer())
            {
                var row = tests.TestQuestion.FirstOrDefault(t => t.Id == currentQuestion);
                if (row != null)
                {
                    tests.TestQuestion.Remove(row);
                    tests.SaveChanges();
                }
            }

            this.renderQuestionList();
        }
    }
}
