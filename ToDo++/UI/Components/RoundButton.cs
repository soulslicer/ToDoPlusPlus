//@raaj A0081202Y
using System.Drawing;
using System.Windows.Forms;

namespace ToDo
{
    public partial class RoundButton : UserControl
    {
        public RoundButton()
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
