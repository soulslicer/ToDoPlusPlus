//@raaj A0081202Y
using System.Drawing;
using System.Windows.Forms;

namespace ToDo
{
    public static class UserInputBox
    {
        private static UserInputForm popUp = new UserInputForm();

        /// <summary>
        /// Shows the UserInputBox
        /// </summary>
        /// <param name="title">Specify the title</param>
        /// <param name="subTitle">Specify the subtitle</param>
        internal static void Show(string title,string subTitle)
        {
            popUp.SetTitle(title, subTitle);
            popUp.SetUserInput("");
            popUp.StartPosition = FormStartPosition.CenterParent;
            popUp.ShowDialog();
        }

        /// <summary>
        /// Returns a list of all added/available user commands
        /// </summary>
        /// <returns>Returns true if data was actually entered</returns>
        internal static bool ValidData()
        {
            if (popUp.UserInput == "")
                return false;
            else
                return true;
        }

        /// <summary>
        /// Returns what the user entered. to be used with ValidData()
        /// </summary>
        /// <returns>Returns user input</returns>
        internal static string GetInput()
        {
            return popUp.UserInput;
        }

        /// <summary>
        /// Sets This UserInputBox to be on top
        /// </summary>
        /// <param name="val">Specify if on top or not</param>
        internal static void OnTop(bool val)
        {
            popUp.TopMost = val;
        }

    }

    public static class AlertBox
    {
        private static AlertForm popUp = new AlertForm();

        /// <summary>
        /// Shows the AlertBox
        /// </summary>
        /// <param name="alertText">Specify the alert text</param>
        internal static void Show(string alertText)
        {
            popUp.SetAlertText(alertText);
            popUp.StartPosition = FormStartPosition.CenterParent;
            popUp.ShowDialog();
        }

        /// <summary>
        /// Sets This AlertBox to be on top
        /// </summary>
        /// <param name="val">Specify if on top or not</param>
        internal static void OnTop(bool val)
        {
            popUp.TopMost = val;
        }
    }

    public static class FontBox
    {

        private static FontDialogToDo popUp = new FontDialogToDo();

        /// <summary>
        /// Pres-Set the required Font,Size,Color
        /// </summary>
        /// <param name="font">Specify the font</param>
        /// <param name="size">Specify the size</param>
        /// <param name="color">Specify the color</param>
        internal static void InitializeOptions(string font, int size, Color color)
        {
            popUp.InitializeOptions(font, size, color);
        }

        /// <summary>
        /// Gets size selected by user
        /// </summary>
        /// <returns>Size selected by user</returns>
        internal static int GetSize() { return popUp.GetSize(); }
        /// <summary>
        /// Gets Font selected by user
        /// </summary>
        /// <returns>Font seleceted by user</returns>
        internal static string GetFont() { return popUp.GetFont(); }
        /// <summary>
        /// Gets Color Selected by user
        /// </summary>
        /// <returns>Color selected by user</returns>
        internal static Color GetColor() { return popUp.GetColor(); }

        /// <summary>
        /// Checks if the Okay Button was Hit or Not
        /// </summary>
        /// <returns>Returns true if Okay was hit, false if Cancel as hit</returns>
        internal static bool ConfirmHit()
        {
            if (popUp.CheckValidData())
                return true;
            else
                return false;
        }

        /// <summary>
        /// Displays the FontBox
        /// </summary>
        /// <param name="font">Enable/Disable font</param>
        /// <param name="size">Enable/Disable size</param>
        /// <param name="color">Enable/Disable color</param>
        internal static void Show(bool font, bool size, bool color)
        {
            popUp.EnableDisableControls(font, size, color);
            popUp.StartPosition = FormStartPosition.CenterParent;
            popUp.ShowDialog();
        }

        /// <summary>
        /// Sets This FontBox to be on top
        /// </summary>
        /// <param name="val">Specify if on top or not</param>
        internal static void OnTop(bool val)
        {
            popUp.TopMost = val;
        }

    }

    public static class TinyAlertView
    {
        private static TinyAlert tinyAlert = new TinyAlert();
        private static UI ui;
        public enum StateTinyAlert { SUCCESS, FAILURE, WARNING };

        /// <summary>
        /// Passes an instance of UI into TinyAlertView to capture UI Movements
        /// </summary>
        /// <param name="uiPass">Instance of UI passed in</param>
        internal static void SetUI(UI uiPass)
        {
            ui = uiPass;
        }

        /// <summary>
        /// Displays TinyAlert for the specified period of time
        /// </summary>
        /// <param name="state">Set state of TinyAlert</param>
        /// <param name="response">Set the response to be shown</param>
        /// <returns></returns>
        internal static void Show(StateTinyAlert state, string response)
        {
            switch (state)
            {
                case StateTinyAlert.SUCCESS:
                    tinyAlert.SetColorText(Color.Green, Color.White, response);
                    break;

                case StateTinyAlert.FAILURE:
                    tinyAlert.SetColorText(Color.Maroon, Color.White, response);
                    break;

                case StateTinyAlert.WARNING:
                    tinyAlert.SetColorText(Color.DarkOrange, Color.Black, response);
                    break;

            }
            tinyAlert.ShowInTaskbar = false;
            tinyAlert.ShowDisplay();
            ui.BringToFront();
            SetLocation();
        }

        /// <summary>
        /// Ensures TinyAlert moves with UI
        /// </summary>
        internal static void SetLocation()
        {
            tinyAlert.Location = new Point(ui.Left, ui.Bottom +5);
        }

        /// <summary>
        /// Set how long you want TinyAlert to display before fading out
        /// </summary>
        /// <param name="time"></param>
        internal static void SetTiming(int time)
        {
            tinyAlert.SetTiming(time);
        }

        /// <summary>
        /// Dismiss even if TinyAlert is still showing
        /// </summary>
        internal static void DismissEarly()
        {
            tinyAlert.Dismiss();
        }

        
    }

}
