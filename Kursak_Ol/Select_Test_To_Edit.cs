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
    public partial class Select_Test_To_Edit : MyForm
    {
        private int currentCategory = 0;
        private int currentTest = 0;
        private byte testIsActual = 0;

        public Select_Test_To_Edit()
        {
            InitializeComponent();
            //наследуемый метод
            base.Top_Button(bunifuImageButton1_Min, bunifuImageButton1_Max, bunifuImageButton2_Norm, bunifuImageButton1_Close);
            this.button_EditTest.Click += Button_EditTest_Click;

            using (Tests_DBContainer tests = new Tests_DBContainer())
            {
                comboBox_SelectCategory.DataSource = tests.Category.ToList();
                comboBox_SelectCategory.ValueMember = "Id";
                comboBox_SelectCategory.DisplayMember = "Title";

                if (comboBox_SelectCategory.Items.Count > 0)
                {
                    comboBox_SelectCategory.SelectedIndex = comboBox_SelectCategory.Items.Count - 1;
                    this.renderTestList();
                }
            }
        }

        private void Button_EditTest_Click(object sender, EventArgs e)
        {
            if (currentTest > 0)
            {
                Select_Question_To_Edit selectQuestion = new Select_Question_To_Edit(currentTest);
                selectQuestion.ShowDialog();
            }
        }

        private void comboBox_SelectCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int.TryParse(comboBox_SelectCategory.SelectedValue.ToString(), out currentCategory);
            this.renderTestList();
        }

        private void renderTestList()
        {
            using (Tests_DBContainer tests = new Tests_DBContainer())
            {
                var ds = tests.Test.Where(t => t.CategoryId == currentCategory).ToList();
                listBox_SelectTestToEdit.DataSource = ds;
                listBox_SelectTestToEdit.DisplayMember = "Title";
                listBox_SelectTestToEdit.ValueMember = "Id";
            }
        }

        private void button_CancelEditTest_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBox_SelectTestToEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            int.TryParse(listBox_SelectTestToEdit.SelectedValue.ToString(), out currentTest);

            using (Tests_DBContainer tests = new Tests_DBContainer())
            {
                var row = tests.Test.FirstOrDefault(t => t.Id == currentTest);
                if (row != null)
                {
                    testIsActual = row.IsActual;

                    if (row.IsActual == 1)
                    {
                        button_TurnOn_OffTest.Text = "Отключить";
                    }
                    else
                    {
                        button_TurnOn_OffTest.Text = "Включить";
                    }
                }
            }   
        }

        private void button_TurnOn_OffTest_Click(object sender, EventArgs e)
        {
            using (Tests_DBContainer tests = new Tests_DBContainer())
            {
                var row = tests.Test.FirstOrDefault(t => t.Id == currentTest);
                if (row != null)
                {
                    byte reverse = testIsActual != (byte)0 ? (byte)0 : (byte)1;
                    row.IsActual = reverse;
                    tests.SaveChanges();
                }
            }

            this.renderTestList();
        }

        private void button_Delete_Test_Click(object sender, EventArgs e)
        {
            using (Tests_DBContainer tests = new Tests_DBContainer())
            {
                var row = tests.Test.FirstOrDefault(t => t.Id == currentTest);
                if (row != null)
                {
                    tests.Test.Remove(row);
                    tests.SaveChanges();
                }
            }

            this.renderTestList();
        }
    }
}
