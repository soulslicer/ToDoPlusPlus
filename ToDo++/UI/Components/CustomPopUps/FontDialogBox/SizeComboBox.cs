using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDo
{
    class SizeComboBox : ComboBox
    {
        public SizeComboBox()
        {
        }

        public void RemoveDuplicate()
        {
            this.Items.Clear();
            this.Items.Add(8);
            this.Items.Add(9);
            this.Items.Add(10);
            this.Items.Add(11);
            this.Items.Add(12);
            this.Items.Add(13);
            this.Items.Add(14);
        }
    }
}
