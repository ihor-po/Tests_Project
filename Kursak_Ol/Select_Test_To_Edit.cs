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
        public Select_Test_To_Edit()
        {
            InitializeComponent();
            //наследуемый метод
            base.Top_Button(bunifuImageButton1_Min, bunifuImageButton1_Max, bunifuImageButton2_Norm, bunifuImageButton1_Close);
            this.button_EditTest.Click += Button_EditTest_Click;
        }

        private void Button_EditTest_Click(object sender, EventArgs e)
        {
            Select_Question_To_Edit selectQuestion=new Select_Question_To_Edit();
            selectQuestion.ShowDialog();
        }
    }
}
