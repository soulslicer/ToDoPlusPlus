//@raaj A0081202Y
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ToDo
{
    public partial class FontColorSettings : UserControl
    {
        private Settings settings;

        /// <summary>
        /// Intialize FlexiCommands with an instance of settings
        /// </summary>
        /// <param name="settings"></param>
        public void InitializeFontColorControl(Settings settings)
        {
            this.settings = settings;
        }

        public FontColorSettings()
        {
            InitializeComponent();
        }

        #region EventHandlers

        #region ButtonHandlers

        private void textSizeButton_Click(object sender, EventArgs e)
        {
            FontBox.InitializeOptions(settings.GetFontSelection(), settings.GetTextSize(), Color.White);
            FontBox.Show(true, true, false);
            if (FontBox.ConfirmHit())
            {
                settings.SetTextSize(FontBox.GetSize());
                settings.SetFontSelection(FontBox.GetFont());
                EventHandlers.UpdateUI();
            }
        }

        private void taskDoneColorButton_Click(object sender, EventArgs e)
        {
            FontBox.InitializeOptions(settings.GetFontSelection(), settings.GetTextSize(), settings.GetTaskDoneColor());
            FontBox.Show(false, false, true);
            if (FontBox.ConfirmHit())
            {
                settings.SetTaskDoneColor(FontBox.GetColor());
                EventHandlers.UpdateUI();
            }
        }

        private void taskDeadlineColorButton_Click(object sender, EventArgs e)
        {
            FontBox.InitializeOptions(settings.GetFontSelection(), settings.GetTextSize(), settings.GetTaskMissedDeadlineColor());
            FontBox.Show(false, false, true);
            if (FontBox.ConfirmHit())
            {
                settings.SetTaskMissedDeadlineColor(FontBox.GetColor());
                EventHandlers.UpdateUI();
            }
        }

        private void taskDeadlineDayColor_Click(object sender, EventArgs e)
        {
            FontBox.InitializeOptions(settings.GetFontSelection(), settings.GetTextSize(), settings.GetTaskNearingDeadlineColor());
            FontBox.Show(false, false, true);
            if (FontBox.ConfirmHit())
            {
                settings.SetTaskNearingDeadlineColor(FontBox.GetColor());
                EventHandlers.UpdateUI();
            }
        }

        private void taskEventColor_Click(object sender, EventArgs e)
        {
            FontBox.InitializeOptions(settings.GetFontSelection(), settings.GetTextSize(), settings.GetTaskOverColor());
            FontBox.Show(false, false, true);
            if (FontBox.ConfirmHit())
            {
                settings.SetTaskOverColor(FontBox.GetColor());
                EventHandlers.UpdateUI();
            }
        }

        #endregion

        #region MouseHoverHandlers

        private void SetDefaultPreviewBox()
        {
            previewBox.Clear();
            SetFormat(Color.Gray, "Hover over an option to find out more :)", 10);
        }

        private void textSizeButton_MouseEnter(object sender, EventArgs e)
        {
            SetDefaultPreviewBox();
            SetFormat(Color.Black, "\n\n", 10);
            SetFormat(Color.Black, "Set the default Font and Size for the Task List View", 10);
            SetFormat(Color.Black, "\n", 10);
        }

        private void textSizeButton_MouseLeave(object sender, EventArgs e)
        {
            SetDefaultPreviewBox();
        }

        private void taskDoneColorButton_MouseEnter(object sender, EventArgs e)
        {
            SetDefaultPreviewBox();
            SetFormat(Color.Black, "\n\n", 10);
            SetFormat(Color.Black, "Set the color for a task that is done or completed", 10);
            SetFormat(Color.Black, "\n", 10);
            SetFormat(Color.Black, "The Color you have set is: ", 10);
            SetFormat(settings.GetTaskDoneColor(), settings.GetTaskDoneColor().ToString(), 10);
        }

        private void taskDoneColorButton_MouseLeave(object sender, EventArgs e)
        {
            SetDefaultPreviewBox();
        }

        private void taskMissedDeadlineColorButton_MouseEnter(object sender, EventArgs e)
        {
            SetDefaultPreviewBox();
            SetFormat(Color.Black, "\n\n", 10);
            SetFormat(Color.Black, "Set the color for a task that you have missed the deadline, or not set as done before the deadline", 10);
            SetFormat(Color.Black, "\n", 10);
            SetFormat(Color.Black, "The Color you have set is: ", 10);
            SetFormat(settings.GetTaskMissedDeadlineColor(), settings.GetTaskMissedDeadlineColor().ToString(), 10);
        }

        private void taskMissedDeadlineColorButton_MouseLeave(object sender, EventArgs e)
        {
            SetDefaultPreviewBox();
        }

        private void taskDeadlineDayColor_MouseEnter(object sender, EventArgs e)
        {
            SetDefaultPreviewBox();
            SetFormat(Color.Black, "\n\n", 10);
            SetFormat(Color.Black, "Set the color for a task that is nearing the deadline", 10);
            SetFormat(Color.Black, "\n", 10);
            SetFormat(Color.Black, "The Color you have set is: ", 10);
            SetFormat(settings.GetTaskNearingDeadlineColor(), settings.GetTaskNearingDeadlineColor().ToString(), 10);
        }

        private void taskDeadlineDayColor_MouseLeave(object sender, EventArgs e)
        {
            SetDefaultPreviewBox();
        }

        private void taskEventColor_MouseEnter(object sender, EventArgs e)
        {
            SetDefaultPreviewBox();
            SetFormat(Color.Black, "\n\n", 10);
            SetFormat(Color.Black, "Set the color for a task that is over", 10);
            SetFormat(Color.Black, "\n", 10);
            SetFormat(Color.Black, "The Color you have set is: ", 10);
            SetFormat(settings.GetTaskOverColor(), settings.GetTaskOverColor().ToString(), 10);
        }

        private void taskEventColor_MouseLeave(object sender, EventArgs e)
        {
            SetDefaultPreviewBox();
        }

        #endregion

        #endregion

        #region FormattingControl

        /// <summary>
        /// Set Formatting of Text to be set into OutputBox
        /// </summary>
        public void SetFormat(Color color, string text, int size)
        {
            RichTextBox box = previewBox;
            int start = box.TextLength;
            box.AppendText(text);
            int end = box.TextLength;

            box.Select(start, end - start + 1);
            box.SelectionColor = color;
            box.SelectionFont = new Font("Century Gothic", size, FontStyle.Regular);
            box.SelectionLength = 0;
        }

        #endregion


    }
}
