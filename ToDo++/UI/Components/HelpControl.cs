//@raaj A0081202Y
using System;
using System.Windows.Forms;

namespace ToDo
{
    public partial class HelpControl : UserControl
    {
        private UI ui;
        int currImage = 1;
        bool fadeLock=false;
        bool firstLoad;
        bool slideshowEnabled = false;

        public HelpControl()
        {
            InitializeComponent();
            transpControl.Opacity = 0;
            transpControl.BringToFront();
        }

        /// <summary>
        /// Pass an instance of UI into HelpControl
        /// </summary>
        /// <param name="ui">instance of ui</param>
        /// <param name="firstLoad">loads slideshow or not</param>
        public void SetUI(UI ui,bool firstLoad)
        {
            this.ui = ui;
            this.firstLoad = firstLoad;
            LoadFullHelpPanel();
        }

        /// <summary>
        /// Loads actual help panel instead of slideshow
        /// </summary>
        private void LoadFullHelpPanel()
        {
            if (!firstLoad)
                customPanelControl.SelectedIndex = 1;
        }

        /// <summary>
        /// Generate slideshow images
        /// </summary>
        private void GenerateNextImage()
        {
            currImage += 1;

            switch (currImage)
            {
                case 2:
                    pictureBox.Image = Properties.Resources.help0002;
                    break;

                case 3:
                    pictureBox.Image = Properties.Resources.help0003;
                    break;

                case 4:
                    pictureBox.Image = Properties.Resources.help0004;
                    break;

                case 5:
                    pictureBox.Image = Properties.Resources.help0005;
                    break;

                case 6:
                    pictureBox.Image = Properties.Resources.help0006;
                    break;

                case 7:
                    pictureBox.Image = Properties.Resources.help0007;
                    break;

                case 8:
                    pictureBox.Image = Properties.Resources.help0008;
                    break;

                case 9:
                    pictureBox.Image = Properties.Resources.help0009;
                    break;

                case 10:
                    pictureBox.Image = Properties.Resources.help0010;
                    break;

                case 11:
                    pictureBox.Image = Properties.Resources.help0011;
                    break;
            }
        }


        /// <summary>
        /// Resets to help layout and closes slidshow
        /// </summary>
        private void ResetHelpPanel()
        {
            customPanelControl.SelectedIndex = 1;
            currImage = 1;
            ui.SwitchToTaskListViewPanel();
            pictureBox.Image = Properties.Resources.help0001;
        }

        #region FadeOutFadeInAnimator

        /// <summary>
        /// Go to next slide in animation sequence
        /// </summary>
        private void StartFadeInFadeOut()
        {
            slideshowEnabled = true;
            fadeLock = true;
            fadeInTimer.Enabled = true;
        }

        private void fadeInTimer_Tick(object sender, EventArgs e)
        {
            transpControl.Opacity += 5;
            if (transpControl.Opacity >= 100)
            {
                GenerateNextImage();
                fadeInTimer.Enabled = false;
                fadeOutTimer.Enabled = true;
            }
        }


        private void fadeOutTimer_Tick(object sender, EventArgs e)
        {
            transpControl.Opacity -= 5;
            if (transpControl.Opacity <= 0)
            {
                fadeInTimer.Enabled = false;
                fadeOutTimer.Enabled = false;
                fadeLock = false;
            }
        }

        #endregion

        #region EventHandlers

        private void introButton_Click(object sender, EventArgs e)
        {
            slideshowEnabled = true;
            customPanelControl.SelectedIndex = 0;
        }

        private void HelpControl_Leave(object sender, EventArgs e)
        {
            if (slideshowEnabled)
            {
                slideshowEnabled = false;
                ResetHelpPanel();
            }
        }

        private void transpControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (currImage == 11)
            {
                ResetHelpPanel();
                return;
            }

            if (!fadeLock)
                StartFadeInFadeOut();
        }

        #endregion

        private void manualButton_Click(object sender, EventArgs e)
        {
            string local=Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string openPDFFile = string.Format("{0}\\fullManual.pdf", local);
            System.IO.File.WriteAllBytes(openPDFFile, Properties.Resources.UserGuideToDo);
            System.Diagnostics.Process.Start(openPDFFile);        
        }
    }
}
