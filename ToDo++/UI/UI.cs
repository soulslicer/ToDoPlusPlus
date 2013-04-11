//@raaj A0081202Y
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Hotkeys;
using Microsoft.Win32;

namespace ToDo
{
    public partial class UI : Form
    {

        // ******************************************************************
        // Constructors (BEGIN HERE)
        // ******************************************************************

        #region Constructor

        private Hotkeys.GlobalHotkey ghk;       //Global Hotkey to Minimize to System Tray
        Logic logic;                            //Instance of Logic that handles Data structure and File Operations

        /// <summary>
        /// Creates a new instance of the Main Program (UI) and loads the various Initialization Functions in order
        /// </summary>
        public UI(Logic logic)
        {
            IntializeTinyAlert();                 //Load UI into TinyAlert to register Form Movements   
            InitializeComponent();                //Create Components         
            InitializeLogic(logic);               //Sets logic            
            InitializeSystemTray();               //Loads Code to place App in System Tray
            InitializeSettings();                 //Set up and Load Settings into ToDo
            InitializeOutputBox();                //Loads Output/Console Box    
            InitializeEventHandlers();            //Register All Event Handlers
            InitializePreferencesPanel();         //Load Settings into Preferences Panel
            IntializeTopMenu();                   //Load Settings into Top Menu Control
            InitializeTaskListView();             //Load Settings into Task List View            
            InitializeTextInput();                //Sets Text Input in Focus
            IntializeHelpPanel();                 //Loads Help Panel

            Logger.Info("All UI Elements loaded correctly...", "UI");

                        /* HEAD TO LOGIC CONTROL TO DELVE FURTHER */
        }

        #endregion

        // ******************************************************************
        // ToDo Intialization Functions
        // ******************************************************************

        #region IntializationFunction

        /// <summary>
        /// Initialize Help Control - Pass an instance of UI into it
        /// </summary>
        private void IntializeHelpPanel()
        {
            bool firstLoadStatus = logic.MainSettings.GetFirstLoadStatus();
            helpControl.SetUI(this,firstLoadStatus);
            if (firstLoadStatus == true)
                SwitchToHelpPanel();
        }

        /// <summary>
        /// Set TextInput as the Active Control
        /// </summary>
        /// <returns></returns>
        private void InitializeTextInput()
        {
            this.ActiveControl = textInput;
            grayFadePictureBox.Hide();
        }

        /// <summary>
        /// Intialize Tiny Alert - Passing an instance of UI and set fade out timing
        /// </summary>
        private void IntializeTinyAlert()
        {
            TinyAlertView.SetUI(this);
            TinyAlertView.SetTiming(5);
        }

        /// <summary>
        /// Intialize Task List View - Pass settings into it and load default view
        /// </summary>
        private void InitializeTaskListView()
        {
            taskListViewControl.InitializeSettings(logic.MainSettings);
            List<Task> displayedList = taskListViewControl.UpdateDisplay(logic.GetDefaultView());
            logic.UpdateLastDisplayedTasksList(displayedList);
        }

        /// <summary>
        /// Initialize Top Menu - Pass an instance of UI into it
        /// </summary>
        private void IntializeTopMenu()
        {
            topMenuControl.InitializeWithUI(this);
        }

        /// <summary>
        /// Initialize Preferences Panel - Pass an instance of settings into it
        /// </summary>
        private void InitializePreferencesPanel()
        {
            preferencesPanel.InitializeWithSettings(logic.MainSettings);
        }

        /// <summary>
        /// Prepare the Output Box. Pass an instance of settings into it
        /// </summary>
        private void InitializeOutputBox()
        {
            outputBox.InitializeWithSettings(logic.MainSettings);
        }

        /// <summary>
        /// Pair Logic with UI
        /// </summary>
        /// <param name="logic"></param>
        private void InitializeLogic(Logic logic)
        {
            this.logic = logic;
            logic.SetUI(this);
        }

        #endregion

        // ******************************************************************
        // Internal Design Functions
        // ******************************************************************

        #region InternalDesignFunctions

        //Placing ToDo++ in System Tray
        #region SystemTray

        const int WM_NCHITTEST = 0x0084;
        const int HTCLIENT = 1;
        const int HTCAPTION = 2;

        /// <summary>
        /// Register HotKeys with ToDo++
        /// </summary>
        private void InitializeSystemTray()
        {
            ghk = new Hotkeys.GlobalHotkey(Constants.ALT, Keys.Q, this);
            ghk.Register();
            notifyIcon_taskBar.Visible = false;
        }

        /// <summary>
        /// Calling this Minimizes or Maximizes the application into system tray depending on state
        /// </summary>
        public void MinimiseMaximiseTray()
        {
            notifyIcon_taskBar.BalloonTipTitle = "ToDo++";
            notifyIcon_taskBar.BalloonTipText = "Hit Alt+Q to bring it up";

            //If Window is Open
            if (notifyIcon_taskBar.Visible == false)
            {
                StartFadeOut();
                TinyAlertView.DismissEarly();
                notifyIcon_taskBar.Visible = true;
                notifyIcon_taskBar.ShowBalloonTip(500);
            }
            //If Window is in tray
            else
            {
                notifyIcon_taskBar.Visible = false;
                StartFadeIn();
                this.WindowState = FormWindowState.Normal;
                this.ActiveControl = textInput;
            }
        }

        //Allow Borderless Form to be resized and Hotkeys to function
        protected override void WndProc(ref Message m)
        {
            const int htBottomLeft = 16;
            const int htBottomRight = 17;
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    int x = (int)(m.LParam.ToInt64() & 0xFFFF);
                    int y = (int)((m.LParam.ToInt64() & 0xFFFF0000) >> 16);
                    Point pt = PointToClient(new Point(x, y));
                    Size clientSize = ClientSize;
                    if (pt.X >= clientSize.Width - 16 && pt.Y >= clientSize.Height - 16 && clientSize.Height >= 16)
                    {
                        m.Result = (IntPtr)(IsMirrored ? htBottomLeft : htBottomRight);
                        return;
                    }
                    break;

                case Hotkeys.Constants.WM_HOTKEY_MSG_ID:
                    MinimiseMaximiseTray();
                    break;
            }
            base.WndProc(ref m);
        }

        //Draw Gripper on borderless form
        public void DrawGripper(PaintEventArgs e)
        {
            if (VisualStyleRenderer.IsElementDefined(
                VisualStyleElement.Status.Gripper.Normal))
            {
                VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.Status.Gripper.Normal);
                Rectangle rectangle1 = new Rectangle((Width) - 18, (Height) - 20, 20, 20);
                renderer.DrawBackground(e.Graphics, rectangle1);
            }
        }

        //Double click the tray icon and it pops back up
        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MinimiseMaximiseTray();
        }

        //De registers the hot-keys when the application closes
        private void UI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ghk.Unregiser())
                MessageBox.Show("Hotkeys failed to unregister!");
        }

        #endregion

        //Registers the App with the Registry to open on Startup
        #region RegisterToOpenOnStartup

        private void RegisterInStartup(bool isChecked)
        {
            if (isChecked == true)
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (isChecked)
                {
                    Logger.Info("Registered ToDo++ in Registry to open on startup..", "UI");
                    registryKey.SetValue("ApplicationName", Application.ExecutablePath);
                }
                else
                {
                    Logger.Info("Dregistered ToDo++ in Registry to open on startup..", "UI");
                    registryKey.DeleteValue("ApplicationName");
                }
            }
        }

        #endregion

        //Allows resizing of borderless form
        #region Resizing
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void UI_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        #endregion

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

        //Creates Shadow
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

        //Form Fade In and Out Timers
        #region FormFadeInOut

        double i = 1;

        /// <summary>
        /// Start ToDo++ FadeOut
        /// </summary>
        public void StartFadeOut()
        {
            timerFadeIn.Enabled = false;
            timerFadeOut.Enabled = true;
        }

        /// <summary>
        /// Start ToDo++ FadeIn
        /// </summary>
        public void StartFadeIn()
        {
            this.Show();
            timerFadeIn.Enabled = true;
            timerFadeOut.Enabled = false;
        }

        private void timerFadeIn_Tick(object sender, EventArgs e)
        {
            i += 0.05;
            if (i >= 1)
            {
                this.Opacity = 1;
                timerFadeIn.Enabled = false;
                return;
            }
            this.Opacity = i;
        }

        private void timerFadeOut_Tick(object sender, EventArgs e)
        {
            i -= 0.05;
            if (i <= 0.01)
            {//if form is invisible we execute the Fade In Effect again
                this.Opacity = 0.0;
                //timerFadeIn.Enabled = true;//start the Fade In Effect
                timerFadeOut.Enabled = false;//stop the Fade Out Effect
                this.Hide();
                return;
            }
            this.Opacity = i;
        }

        #endregion

        //Collapse Expand ToDo++
        #region CollapseExpand

        bool isCollapsed = false;
        int setHeight;
        int prevHeight;

        //         CONTROL COLLAPSE FLOW OF EVENTS        //
        /*
         * 1. Set Opacity of GrayFade to 0, Bring to Front
         * 2. Start Fading in Gray (Controls Fade Out)
         * 3. Hide all controls
         * 4. Begin Collapse
         * ------------------------------------------------
         * 1. Begin Expand
         * 2. Show all controls
         * 3. Start Gray Fading (Controls Fade In)
         * 4. Send Gray Fade to Back and Hide
        */
        //         CONTROL COLLAPSE FLOW OF EVENTS        //

        /// <summary>
        /// Toggles - Expand or Collapse ToDo++
        /// </summary>
        public void ToggleCollapsedState()
        {
            if (isCollapsed == false)
            {
                topMenuControl.SetCollapsedStatus(true);
                this.MinimumSize = new Size(522, 60);
                grayFadePictureBox.BringToFront();
                isCollapsed = true;
                grayFadePictureBox.Opacity = 0;
                grayFadePictureBox.Show();
                grayFadeTimer.Enabled = true;
            }
            else
            {
                topMenuControl.SetCollapsedStatus(false);
                setHeight = 60;
                isCollapsed = false;  
                timerCollapse.Enabled = false;
                timerExpand.Enabled = true;
                this.MaximumSize = new System.Drawing.Size(1000, 1000);
            }
            topMenuControl.SetUpDownButton(isCollapsed);
        }

        private void timerCollapse_Tick(object sender, EventArgs e)
        {
            setHeight -= 20;
            if (setHeight <= 60)
            {
                timerCollapse.Enabled = false;
                this.MaximumSize = new System.Drawing.Size(1000, 60);
            }
            this.Height = setHeight;
        }

        private void timerExpand_Tick(object sender, EventArgs e)
        {
            setHeight += 20;
            if (setHeight >= prevHeight)
            {
                grayFadePictureBox.Show();
                taskListViewControl.Show();
                preferencesPanel.Show();
                helpControl.Show();
                timerExpand.Enabled = false;
                grayFadeTimer.Enabled = true;
            }
            this.Height = setHeight;
        }

        private void grayFadeTimer_Tick(object sender, EventArgs e)
        {
            if (isCollapsed == true)
            {
                grayFadePictureBox.Opacity += 15;
                if (grayFadePictureBox.Opacity == 100)
                {
                    grayFadeTimer.Enabled = false;
                    taskListViewControl.Hide();
                    preferencesPanel.Hide();
                    grayFadePictureBox.Hide();
                    helpControl.Hide();

                    setHeight = this.Height;
                    prevHeight = this.Height;
                    timerCollapse.Enabled = true;
                    timerExpand.Enabled = false;
                    this.ActiveControl = textInput;
                }
            }
            else
            {
                grayFadePictureBox.Opacity -= 15;
                if (grayFadePictureBox.Opacity == 0)
                {
                    grayFadeTimer.Enabled = false;
                    grayFadePictureBox.SendToBack();
                    grayFadePictureBox.Hide();
                    this.MinimumSize = new Size(522, 397);
                    this.ActiveControl = textInput;
                }
            }
        }

        #endregion

        #endregion

        // ******************************************************************
        // Load all Relevant Settings into ToDo++ on startup
        // ******************************************************************

        #region PrepareSettings

        /// <summary>
        /// Ensures ToDo++ is minimized on startup and load on startup is set
        /// </summary>
        private void InitializeSettings()
        {
            MinimiseToTrayWhenChecked();
            RegisterLoadOnStartupWhenChecked();
        }

        /// <summary>
        /// Minimizes App to System tray if true
        /// </summary>
        private void MinimiseToTrayWhenChecked()
        {
            if (logic.MainSettings.GetStartMinimizeStatus() == true)
                MinimiseMaximiseTray();
        }

        /// <summary>
        /// Sets the Load on Startup Status
        /// </summary>
        private void RegisterLoadOnStartupWhenChecked()
        {
            if (logic.MainSettings.GetLoadOnStartupStatus() == true)
                RegisterInStartup(true);
            else
                RegisterInStartup(false);
        }

        #endregion

        // ******************************************************************
        // Switch Between Panels (Preferences and ToDo++)
        // ******************************************************************

        #region PanelSwitching

        const int TASKDISPLAY_PANEL = 0;
        const int PREFERENCES_PANEL = 1;
        const int CONSOLE_PANEL = 2;
        const int HELP_PANEL = 3;

        /// <summary>
        /// Switches to Settings Panel
        /// </summary>
        public void SwitchToSettingsPanel()
        {
            this.customPanelControl.SelectedIndex = PREFERENCES_PANEL;
        }

        /// <summary>
        /// Switches to TaskListView Panel
        /// </summary>
        public void SwitchToTaskListViewPanel()
        {
            this.customPanelControl.SelectedIndex = TASKDISPLAY_PANEL;
        }

        /// <summary>
        /// Switches to Help Panel
        /// </summary>
        public void SwitchToHelpPanel()
        {
            this.customPanelControl.SelectedIndex = HELP_PANEL;
        }

        /// <summary>
        /// Toggle between Help and ToDo Panels
        /// </summary>
        public void ToggleHelpToDoPanel()
        {
            if (this.customPanelControl.SelectedIndex == TASKDISPLAY_PANEL)
            {
                this.customPanelControl.SelectedIndex = HELP_PANEL;
            }
            else
            {
                this.customPanelControl.SelectedIndex = TASKDISPLAY_PANEL;
            }
        }

        /// <summary>
        /// Toggle between TaskListView and Settings Panels
        /// </summary>
        public void ToggleToDoPreferencesPanel()
        {
            if (this.customPanelControl.SelectedIndex == TASKDISPLAY_PANEL)
            {
                this.customPanelControl.SelectedIndex = PREFERENCES_PANEL;
            }
            else
            {
                this.customPanelControl.SelectedIndex = TASKDISPLAY_PANEL;
            }
        }

        /// <summary>
        /// Toggle between TaskListView and Console Panels
        /// </summary>
        public void ToggleConsolePanel()
        {
            if (this.customPanelControl.SelectedIndex == TASKDISPLAY_PANEL)
            {
                this.customPanelControl.SelectedIndex = CONSOLE_PANEL;
                this.ActiveControl = textInput;
            }
            else
            {
                this.customPanelControl.SelectedIndex = TASKDISPLAY_PANEL;
            }
        }

        #endregion
        
        // ******************************************************************
        // Shortcut Keys (Hotkeys)
        // ******************************************************************

        #region Hotkeys

        /// <summary>
        /// Holds all Shortcut Keys
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Q))
            {
                Exit();
                return true;
            }
            if (keyData == (Keys.Control | Keys.A))
            {
                textInput.SelectAll();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.Space))
            {
                this.ActiveControl = textInput;
                return true;
            }
            if (keyData == (Keys.Alt | Keys.S))
            {
                ToggleToDoPreferencesPanel();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.S))
            {
                ToggleToDoPreferencesPanel();
                return true;
            }
            if ((keyData == (Keys.Alt | Keys.Up)) || (keyData == (Keys.Alt | Keys.Down)))
            {
                ToggleCollapsedState();
                return true;
            }
            if (keyData == Keys.Up)
            {
                textInput.UpdateWithPrevCommand();
                return true;
            }
            if (keyData == Keys.Down)
            {
                textInput.UpdateWithNextCommand();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.Left))
            {
                textInput.MoveBackOneWord();
            }
            if (keyData == (Keys.Alt | Keys.Right))
            {
                textInput.MoveForwardOneWord();
            }
            if (keyData == (Keys.Alt | Keys.Back))
            {
                textInput.DeleteLastWord();
            }
            if (keyData == (Keys.Alt | Keys.H))
            {
                ToggleHelpToDoPanel();
            }
            if (keyData == (Keys.Alt | Keys.C))
            {
                ToggleConsolePanel();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        // ******************************************************************
        // Event Handlers
        // ******************************************************************

        #region EventHandlers

        /// <summary>
        /// Adds Event Handlers relating to UI here
        /// </summary>
        private void InitializeEventHandlers()
        {
            this.MouseWheel += new MouseEventHandler(ScrollIfOverDisplay);
            EventHandlers.StayOnTopHandler += SetStayOnTop;
            EventHandlers.UpdateUIHandler += UpdateUI;
        }

        #region CustomEventHandlers

        /// <summary>
        /// When event received, Form always stays on Top
        /// </summary>
        private void SetStayOnTop(object sender, EventArgs args)
        {
            bool onTop = Convert.ToBoolean(sender);
            this.TopMost = onTop;
            UserInputBox.OnTop(onTop);
            FontBox.OnTop(onTop);
            AlertBox.OnTop(onTop);
        }

        /// <summary>
        /// When event received, UI updates itself with the latest settings
        /// </summary>
        private void UpdateUI(object sender, EventArgs args)
        {
            //taskListViewControl.RefreshListView();
            taskListViewControl.InitializeSettings(logic.MainSettings);
            taskListViewControl.BuildList();
        }

        #endregion

        #region UIEventHandlers

        private void UI_Resize(object sender, EventArgs e)
        {
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            //TinyAlertView.Location = new Point(this.Right, this.Bottom - 10);
            TinyAlertView.SetLocation();
        }

        private void UI_Move(object sender, EventArgs e)
        {
            TinyAlertView.SetLocation();
        }

        private void taskListViewControl_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs row)
        {
            taskListViewControl.SetRowIndex(row);
            taskListViewControl.ColorRows(row);
        }

        private void taskListViewControl_BeforeSorting(object sender, BrightIdeasSoftware.BeforeSortingEventArgs e)
        {
            e.GroupByOrder = SortOrder.None;
        }

        bool MouseIsOverDisplayList;

        private void taskListViewControl_MouseEnter(object sender, EventArgs e)
        {
            MouseIsOverDisplayList = true;
            taskListViewControl.Focus();
        }

        private void taskListViewControl_MouseLeave(object sender, EventArgs e)
        {
            MouseIsOverDisplayList = false;
        }

        private void SelectTextInput(object sender, KeyPressEventArgs e)
        {
            textInput.Text += e.KeyChar;
            textInput.Focus();
            textInput.DeselectAll();
            textInput.Select(textInput.TextLength, 0);
            e.Handled = true;
        }

        private void ScrollIfOverDisplay(object sender, MouseEventArgs e)
        {
            if (MouseIsOverDisplayList)
                taskListViewControl.Focus();
        }

        internal void SetMessageTaskListIsEmpty(bool empty)
        {
            taskListViewControl.SetMessageTaskListIsEmpty(empty);
        }

        #endregion

        #endregion

        // ******************************************************************
        // Code that interacts with logic and returns an output goes here
        // ******************************************************************

        #region LogicControl

        /// <summary>
        /// Passes an the user text to Logic, which processes it and returns an output to be displayed
        /// </summary>
        private void ProcessText()
        {
            string input = textInput.Text;
            Response output = logic.ProcessCommand(input);
            outputBox.DisplayCommand(input, output.FeedbackString);

            if (output == null)
            {
                Logger.Error("Null Response detected", "ProcessText::UI");
                AlertBox.Show("An invalid response was returned.");
                return;
            }

            List<Task> displayedList = taskListViewControl.UpdateDisplay(output);

            TinyAlertView.StateTinyAlert alertType;
            if (output.IsSuccessful())
                alertType = TinyAlertView.StateTinyAlert.SUCCESS;                
            else if (output.WarnUser())
                alertType = TinyAlertView.StateTinyAlert.WARNING;
            else
                alertType = TinyAlertView.StateTinyAlert.FAILURE;
            TinyAlertView.Show(alertType, output.FeedbackString);

            textInput.Clear();

            if(customPanelControl.SelectedIndex!=CONSOLE_PANEL)
                SwitchToTaskListViewPanel();
            logic.UpdateLastDisplayedTasksList(displayedList);
        }

        /// <summary>
        /// When Enter Button Pressed in inputBox
        /// </summary>
        private void textBox_input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                textInput.AddToList(textInput.Text);
                ProcessText();
            }
        }

        /*
        // deprecated -- was used to unit test new UI implementation
        private void TaskDisplayTestDriver()
        {
            List<Task> displayList = new List<Task>();
            Task addTask;
            addTask = new TaskEvent("test task", DateTime.Now, DateTime.Now, new DateTimeSpecificity());
            displayList.Add(addTask);
            addTask = new TaskEvent("test task 2", DateTime.Now, DateTime.Now, new DateTimeSpecificity());
            displayList.Add(addTask);
            addTask = new TaskEvent("this is a super long test task. who writes this sort of tasks??", new DateTime(2012, 12, 31), new DateTime(2013, 1, 1), new DateTimeSpecificity());            
            displayList.Add(addTask);
            addTask = new TaskDeadline("deaddddline is near. =[", new DateTime(2013, 1, 1), new DateTimeSpecificity());
            addTask.DoneState = true;
            displayList.Add(addTask);            
            addTask = new TaskFloating("floating task test");
            displayList.Add(addTask);
            Response testResponse = new Response(Result.SUCCESS, Format.NAME, typeof(OperationAdd), displayList);
            taskListViewControl.UpdateDisplay(testResponse);
        }
        */
        #endregion

        /// <summary>
        /// Exit the Application
        /// </summary>
        public void Exit()
        {
            Logger.Info("Exiting the application normally...", "UI");
            Application.Exit();
        }
    }
}
