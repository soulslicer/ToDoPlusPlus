using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDo
{
    public partial class CustomButton : UserControl
    {
        public CustomButton()
        {
            InitializeComponent();
        }

        public void SetMouseDown()
        {
            buttonText.ForeColor = Color.Silver;
        }

        public void SetMouseUp()
        {
            buttonText.ForeColor = Color.White;
        }

        public string ButtonText
        {
            get { return buttonText.Text; }
            set { buttonText.Text = value; }
        }


    }
}
