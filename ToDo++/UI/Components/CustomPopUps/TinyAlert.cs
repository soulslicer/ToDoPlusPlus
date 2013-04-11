//@raaj A0081202Y
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ToDo
{
    public partial class TinyAlert : Form
    {
        private UI ui;
        int timing=3;
        string tinyAlertText="";

        public TinyAlert()
        {
            InitializeComponent();
            this.Opacity = 100;
            System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        /// <summary>
        /// Pass in an instance of UI so that TinyAlert is aware of it's position
        /// </summary>
        /// <param name="ui">instance of UI</param>
        public void SetUI(UI ui)
        {
            this.ui = ui;
        }

        /// <summary>
        /// Set time period before fade out
        /// </summary>
        /// <param name="time">time period before fade out</param>
        public void SetTiming(int time)
        {
            this.timing = time;
        }

        /// <summary>
        /// Fade out TinyAlert even before time period ends
        /// </summary>
        public void Dismiss()
        {
            timerFadeIn.Enabled = false;//start the Fade In Effect
            timerFadeOut.Enabled = true;
        }
        
        /// <summary>
        /// Show TinyAlert
        /// </summary>
        public void ShowDisplay()
        {
            if (tinyAlertText == "")
                Debug.Assert(false, "You have failed to set Text for TinyAlert");

            this.tinyAlertLabel.Text = this.tinyAlertText;
            this.tinyAlertText = "";

            int size=System.Windows.Forms.TextRenderer.MeasureText(tinyAlertLabel.Text, new Font(tinyAlertLabel.Font.FontFamily, tinyAlertLabel.Font.Size, tinyAlertLabel.Font.Style)).Width;
            this.Width = size+20;
            this.Show();
            StartFader();
            //StartDrop();
        }

        /// <summary>
        /// Set the BackColor,TextColor and text. Automatically resizes based on text length
        /// </summary>
        /// <param name="backColor"></param>
        /// <param name="textColor"></param>
        /// <param name="text"></param>
        public void SetColorText (Color backColor,Color textColor, string text)
        {
            this.BackColor = backColor;
            this.tinyAlertLabel.ForeColor = textColor;
            this.tinyAlertText = text;
        }

        #region Win32Functions

        // Allows resizing of borderless form
        #region Resizing
        //public const int WM_NCLBUTTONDOWN = 0xA1;
        //public const int HT_CAPTION = 0x2;

        //[DllImportAttribute("user32.dll")]
        //public static extern int SendMessage(IntPtr hWnd,
        //                 int Msg, int wParam, int lParam);
        //[DllImportAttribute("user32.dll")]
        //public static extern bool ReleaseCapture();
        //private void UI_MouseDown(object sender, MouseEventArgs e)
        //{

        //}

        #endregion

        /// Creates rounded edge
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

        //event handler for rounded edge
        private void displayForm_Resize(object sender, EventArgs e)
        {
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        #endregion

        /// Creates Shadow (DISABLED)
        #region Shadow

        /*
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
         * */

        #endregion

        //Allows for any width for form
        #region Sizing

        private const int WM_WINDOWPOSCHANGING = 0x0046;
        private const int WM_GETMINMAXINFO = 0x0024;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_WINDOWPOSCHANGING)
            {
                WindowPos windowPos = (WindowPos)m.GetLParam(typeof(WindowPos));

                // Make changes to windowPos

                // Then marshal the changes back to the message
                Marshal.StructureToPtr(windowPos, m.LParam, true);
            }

            base.WndProc(ref m);

            // Make changes to WM_GETMINMAXINFO after it has been handled by the underlying
            // WndProc, so we only need to repopulate the minimum size constraints
            if (m.Msg == WM_GETMINMAXINFO)
            {
                MinMaxInfo minMaxInfo = (MinMaxInfo)m.GetLParam(typeof(MinMaxInfo));
                minMaxInfo.ptMinTrackSize.x = this.MinimumSize.Width;
                minMaxInfo.ptMinTrackSize.y = this.MinimumSize.Height;
                Marshal.StructureToPtr(minMaxInfo, m.LParam, true);
            }
        }

        struct WindowPos
        {
            public IntPtr hwnd;
            public IntPtr hwndInsertAfter;
            public int x;
            public int y;
            public int width;
            public int height;
            public uint flags;
        }

        struct POINT
        {
            public int x;
            public int y;
        }

        struct MinMaxInfo
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        }

        #endregion

        #endregion

        #region FadeInOutAnimation

        double i = 0.1;
        private void StartFader()
        {
            this.Opacity = i;
            timerFadeIn.Enabled = true;
            timerFadeOut.Enabled = false;
        }

        private void timerFadeIn_Tick(object sender, EventArgs e)
        {
            i += 0.05;
            if (i >= timing)
            {
                this.Opacity = 1;
                timerFadeIn.Enabled = false;
                timerFadeOut.Enabled = true;
                return;
            }
            this.Opacity = i;
        }


        private void timerFadeOut_Tick(object sender, EventArgs e)
        {
            i -= 0.05;
            if (i <= 0.01)
            {
                this.Opacity = 0.0;
                timerFadeOut.Enabled = false;
                this.Hide();
                return;
            }
            this.Opacity = i;
        }

        #endregion

        #region DropAnimation

        /* depreciated

        int m;
        private void StartDrop()
        {
            m = ui.Bottom-50;

            this.Location = new Point(this.Right, m);
            timerDrop.Enabled = true;//start the drop
            timerUp.Enabled = false;
        }

        private void timerDrop_Tick(object sender, EventArgs e)
        {
            m += 1;
            if (m >= ui.Bottom)
            {
                timerDrop.Enabled = false;
            }
            this.Location = new Point(ui.Right, ui.Bottom+m);
        }

        private void timerUp_Tick(object sender, EventArgs e)
        {
            m -= 1;
            if (m >= ui.Bottom)
            {
                timerDrop.Enabled = false;
            }
            this.Location = new Point(ui.Right, ui.Bottom + m);
        }

         * */

        #endregion
    }
}
