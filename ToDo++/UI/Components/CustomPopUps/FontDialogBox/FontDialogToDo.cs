using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms.VisualStyles;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ToDo
{
    public partial class FontDialogToDo : Form
    {
        bool fontSelectionEnable; 
        bool sizeSelectionEnable;
        bool colorSelectionEnable;
        bool confirmData;

        public FontDialogToDo()
        {
            InitializeComponent();
            InitializeFontDialog();
            SetFormattingForPreview();
        }

        /// <summary>
        /// Creates rounded edge
        /// </summary>
        /// 
        #region Rounded Edge
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // x-coordinate of upper-left corner
            int nTopRect, // y-coordinate of upper-left corner
            int nRightRect, // x-coordinate of lower-right corner
            int nBottomRect, // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );
        #endregion

        /// <summary>
        /// Creates Shadow (DISABLED)
        /// </summary>
        #region Shadow


        private const int CS_DROPSHADOW = 0x20000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }


        #endregion


        // ******************************************************************
        // Getters for Font,Color and Size
        // ******************************************************************

        #region Getters

        public int GetSize() { if (sizeSelectionEnable == false) { Debug.Assert(false, "Size Disabled"); } return (int)this.sizeSelection.SelectedItem; }
        public string GetFont() { if (fontSelectionEnable == false) { Debug.Assert(false, "Font Disabled"); } return this.fontSelection.publicFont.GetName(0); }
        public Color GetColor() { if (colorSelectionEnable == false) { Debug.Assert(false, "Color Disabled"); } return this.colorSelection.SelectedColor; }

        #endregion

        // ******************************************************************
        // Presets thiswith initial Font,Size,Color and Enabling controls
        // ******************************************************************

        #region FormattingInitializationFunctions

        /// <summary>
        /// Initialize the controls with default values and add event handlers
        /// </summary>
        private void InitializeFontDialog()
        {
            this.fontSelection.SelectedIndexChanged += m_comboBox_SelectedIndexChanged;
            this.sizeSelection.RemoveDuplicate();
            InitializeOptions("Arial Black", 8, Color.White);
        }

        /// <summary>
        /// Public method to preset Font,Size and Color
        /// </summary>
        /// <param name="font">Set Font</param>
        /// <param name="size">Set Size</param>
        /// <param name="color">Set Color</param>
        public void InitializeOptions(string font, int size, Color color)
        {
            this.sizeSelection.SelectedItem = size;
            this.fontSelection.SelectedFontFamily = new FontFamily(font);
            this.colorSelection.SelectedColor = color;
            SetFormattingForPreview();
        }

        /// <summary>
        /// Public method to enable or disable controls
        /// </summary>
        /// <param name="font">Enable/Disable Font</param>
        /// <param name="size">Enable/Disable Size</param>
        /// <param name="color">Enable/Disable Color</param>
        public void EnableDisableControls(bool font, bool size, bool color)
        {
            this.fontSelectionEnable = font;
            this.sizeSelectionEnable = size;
            this.colorSelectionEnable = color;

            this.fontSelection.Enabled = this.fontSelectionEnable;
            this.sizeSelection.Enabled = this.sizeSelectionEnable;
            this.colorSelection.Enabled = this.colorSelectionEnable;
        }

        /// <summary>
        /// Sets the Preview Text with the user selected formatting
        /// </summary>
        private void SetFormattingForPreview()
        {
            int size = Convert.ToInt32(sizeSelection.SelectedItem.ToString());
            FontFamily temp = fontSelection.publicFont;
            string fontName = temp.GetName(0);

            try
            {
                Font x = new Font(fontName, size, FontStyle.Regular);
                previewLabel.Font = x;
                previewLabel.ForeColor = colorSelection.SelectedColor;
            }
            catch (Exception e)
            {
                Logger.Error(e, "SetFormattingForPreview::FontDialogToDo");
                AlertBox.Show("This font can't be used");
            }
        }

        #endregion

        // ******************************************************************
        // Event Handlers for calling SetFormattingForPreview()
        // ******************************************************************

        #region EventHandlersForOptions

        private void sizeComboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SetFormattingForPreview();
        }

        private void colorSelection_ColorChanged(object sender, ColorComboTestApp.ColorChangeArgs e)
        {
            SetFormattingForPreview();
        }

        private void m_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            fontSelection.publicFont = fontSelection.SelectedFontFamily;
            SetFormattingForPreview();
        }

        #endregion

        // ******************************************************************
        // Event Handlers for components/buttons and Keyboard commands
        // ******************************************************************

        #region EventHandlersForComponent

        /// <summary>
        /// Checks if the User Hit Okay or Cancel, and returns a boolean
        /// </summary>
        /// <returns>Boolean is true if Okay was hit</returns>
        public bool CheckValidData()
        {
            if (confirmData == true)
                return true;
            else
                return false;
        }

        //Okay Button
        private void okButton_Click(object sender, EventArgs e)
        {
            confirmData = true;
            this.Close();
        }

        //Cancel Button
        private void cancelButton_Click(object sender, EventArgs e)
        {
            confirmData = false;
            this.Close();
        }

        //Enter and Escape Keys
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Enter))
            {
                confirmData = true;
                this.Close();
                return true;
            }
            else if (keyData == (Keys.Escape))
            {
                confirmData = false;
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

    }
}
