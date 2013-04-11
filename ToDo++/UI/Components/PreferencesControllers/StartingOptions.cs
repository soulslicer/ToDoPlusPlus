//@raaj A0081202Y
using System;
using System.Windows.Forms;

namespace ToDo
{
    public partial class StartingOptions : UserControl
    {
        private Settings settings;
        bool firstLoad = false;
        
        public StartingOptions()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes Starting Options control with settings
        /// </summary>
        /// <param name="settings"></param>
        public void InitializeStartingOptions(Settings settings)
        {
            this.settings = settings;
            InitializeCheckBoxes();
        }

        /// <summary>
        /// Load initial values into Starting Options check boxes
        /// </summary>
        private void InitializeCheckBoxes()
        {
            minimisedCheckbox.Checked = settings.GetStartMinimizeStatus();
            loadOnStartupCheckbox.Checked = settings.GetLoadOnStartupStatus();
            stayOnTopCheckBox.Checked = settings.GetStayOnTopStatus();
            firstLoad = true;
        }

        #region CheckBoxEventHandlers

        private void minimisedCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (firstLoad == true)
                settings.SetStartMinimized(minimisedCheckbox.Checked);
        }

        private void loadOnStartupCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (firstLoad == true)
                settings.SetLoadOnStartupStatus(loadOnStartupCheckbox.Checked);
        }

        private void stayOnTopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (firstLoad == true)
                settings.SetStayOnTop(stayOnTopCheckBox.Checked);
            EventHandlers.StayOnTop(stayOnTopCheckBox.Checked);
        }

        #endregion

    }
}
