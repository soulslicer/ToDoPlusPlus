//@raaj A0081202Y
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ToDo
{
    public partial class UserInputForm : Form
    {
        public UserInputForm()
        {
            InitializeComponent();
            System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20)); 
        }

        // ******************************************************************
        // Properties - Title,SubTitle,UserInput
        // ******************************************************************

        #region Properties

        public string UserInput { get { return userInputBox.Text; } set { userInputBox.Text = value; } }
        public string Title { get { return titleLabel.Text; } set { titleLabel.Text = value; } }
        public string SubTitle { get { return subtitleLabel.Text; } set { subtitleLabel.Text = value; } }

        #endregion

        // ******************************************************************
        // Win32 Functions
        // ******************************************************************

        #region Win32Functions

        //Creates rounded edge
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

        //Creates Shadow (DISABLED)
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

        //Make form draggable
        #region MakeDraggable

        //const int WM_NCHITTEST = 0x0084;
        //const int HTCLIENT = 1;
        //const int HTCAPTION = 2;

        //protected override void WndProc(ref Message msg)
        //{
        //    if (msg.Msg == 0x0084) // WM_NCHITTEST
        //        msg.Result = (IntPtr)2; // HTCAPTION
        //    else
        //        base.WndProc(ref msg);
        //}

        #endregion

        #endregion

        // ******************************************************************
        // Public Methods to Get Set User Input, Title, Subtitle
        // ******************************************************************

        #region PublicGetterSetterMethods

        /// <summary>
        /// Sets Title and Subtitle of PopUp
        /// </summary>
        /// <param name="title">Specify the title</param>
        /// <param name="subtitle">Specify the subtitle</param>
        public void SetTitle(string title,string subtitle)
        {
            titleLabel.Text = title;
            subtitleLabel.Text = subtitle;
        }

        /// <summary>
        /// Sets User Input prior to displaying
        /// </summary>
        /// <param name="input">Specify the a preset user input</param>
        public void SetUserInput(string input)
        {
            userInputBox.Text = input;
        }

        /// <summary>
        /// Checks for Valid Data
        /// </summary>
        /// <returns>Boolean of whether there was valid data or not</returns>
        public bool UserEnteredData()
        {
            if (UserInput == "")
                return false;
            else
                return true;
        }

        #endregion

        // ******************************************************************
        // Event Handlers for keyboard/button inputs
        // ******************************************************************

        #region EventHandlers

        //Cancel Button
        private void cancelButton_Click(object sender, EventArgs e)
        {
            userInputBox.Text = "";
            this.Close();
        }

        //Confirm Button
        private void confirmButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Keyboard Commands
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Enter))
            {
                this.Close();
                return true;
            }
            else if (keyData == (Keys.Escape))
            {
                userInputBox.Text = "";
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void UserInputForm_Resize(object sender, EventArgs e)
        {
            System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        #endregion

    }
}
