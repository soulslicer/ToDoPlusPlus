//@qianpan A0103985Y
using System;
using System.Windows.Forms;

namespace ToDo
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            try
            {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Logger.Info("Starting Application...", "Main");
            Logic logic = new Logic();
            Application.Run(new UI(logic));
            }
            catch (System.IO.FileNotFoundException e)
            {
                AlertBox.Show("Missing some DLL files!");
                Logger.Error(e, "Main::Program");
            }
            catch (Exception e)
            {
                AlertBox.Show(e.ToString());
                Logger.Error(e, "Main::Program");
            }
            Logger.Info("Application terminated!\r\n", "Main");         
        }
    }
}
