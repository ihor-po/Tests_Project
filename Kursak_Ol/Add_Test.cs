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
        public Add_Test()
        {
            InitializeComponent();
            //наследуемый метод
            base.Top_Button(bunifuImageButton1_Min, bunifuImageButton1_Max, bunifuImageButton2_Norm, bunifuImageButton1_Close);
            //Кнопка для вызова панели добавить категорию
            this.button_AddCategory.Click += Button_AddCategory_Click;
            //Кнопка для скрытия панели добавить категорию
            this.button2_Categori_Close.Click += Button2_Categori_Close_Click;
        }

        private void Button2_Categori_Close_Click(object sender, EventArgs e)
        {
            this.bunifuTransition1.HideSync(this.panel_AddNewCategory);
        }

        private void Button_AddCategory_Click(object sender, EventArgs e)
        {
            this.bunifuTransition1.ShowSync(this.panel_AddNewCategory);
        }
    }
}
