//@qianpan A0103985Y
using System.Collections.Generic;
using System.Windows.Forms;
using System;

namespace ToDo
{
    /// <summary>
    /// Logic controller to interface between a GUI and necessary parsers / other business logic.
    /// </summary>
    public class Logic
    {
        // ******************************************************************
        // Attributes
        // ******************************************************************

        #region Attributes
        UI ui;        
        CommandParser commandParser;
        Settings mainSettings;
        Storage storage;
        List<Task> taskList;
        public Settings MainSettings
        {
            get { return mainSettings; }
            set { mainSettings = value; }
        }

        #endregion
  
        /// <summary>
        /// Constructor for Logic class. Initializes all necessary components.
        /// </summary>
        public Logic()
        {
            mainSettings = new Settings();

            storage = new Storage("ToDo++.xml", "ToDoSettings.xml");

            mainSettings.UpdateSettings(storage.LoadSettingsFromFile());
            EventHandlers.UpdateSettingsHandler += UpdateSettings;

            commandParser = new CommandParser();

            taskList = storage.LoadTasksFromFile();
            while (taskList == null)
            {
                PromptUser_CreateNewTaskFile();
                taskList = storage.LoadTasksFromFile();
            }
        }

        /// <summary>
        /// Sets up a UI with logic for two-way communication.
        /// </summary>
        /// <param name="ui">UI to set up with.</param>
        internal void SetUI(UI ui)
        {
            this.ui = ui;
        }

        /// <summary>
        /// Processes a input string command and returns
        /// the processed Response which contains the result of
        /// the operation which can be displayed to the user.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <returns>Response containing the a list of Tasks which can be displayed and the Result of the operation.</returns>
        public Response ProcessCommand(string input)
        {
            Operation operation = null;
            try
            {
                operation = ParseCommand(input);
            }
            catch (InvalidDateTimeException e)
            {
                Logger.Warning("Entered invalid datetime", "ProcessCommand::Logic");
                AlertBox.Show(e.Message);
            }
            catch (InvalidTimeRangeException e)
            {
                Logger.Warning("Entered invalid time range", "ProcessCommand::Logic");
                AlertBox.Show(e.Message);
            }
            catch (MultipleCommandsException)
            {
                AlertBox.Show(@"Multiple commands were entered that could not be resolved. Use delimiters.");
            }
            catch (Exception e)
            {
                Logger.Error("Unhandled Exception Occured!!!", "ProcessCommand::Logic");
                Logger.Error(e,"ProcessCommand::Logic");
                AlertBox.Show("Something bad happened and we could not process your command.");
            }
            if (operation == null)
            {
                return new Response(Result.INVALID_COMMAND);
            }
            else
            {
                Response feedback = ExecuteCommand(operation);
                if (ui != null)
                {
                    if (taskList.Count == 0)
                        ui.SetMessageTaskListIsEmpty(true);
                    else
                        ui.SetMessageTaskListIsEmpty(false);
                }
                return feedback;
            }
        }

        /// <summary>
        /// Parses a command using a CommandParser and returns
        /// the resultant Operation.
        /// </summary>
        /// <param name="command">String to parse.</param>
        /// <returns>Operation which represents the input string.</returns>
        private Operation ParseCommand(string command)
        {
            if (command.Equals(null))
            {
                return null;
            }
            else
            {
                Operation derivedOperation = commandParser.ParseOperation(command);
                return derivedOperation;
            }
        }

        /// <summary>
        /// Executes an Operation and the Response representing the result of the Operation.
        /// </summary>
        /// <param name="operation">The Operation to execute.</param>
        /// <returns>Response representing the result of the Operation.</returns>
        private Response ExecuteCommand(Operation operation)
        {
            Response response;
            response = operation.Execute(taskList, storage);
            return response;
        }

        /// <summary>
        /// Prompts a user to create a new file.
        /// </summary>
        private void PromptUser_CreateNewTaskFile()
        {
            UserInputBox.Show("Error!", "Task storage file seems corrupted. Error reading from it!\r\n Create new file?");
            DialogResult dialogResult = MessageBox.Show("Sure", "Create new task file?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                storage.CreateNewTaskFile();
            }
            else if (dialogResult == DialogResult.No)
            {
                AlertBox.Show("Exiting application..");
                Application.Exit();
            }
        }
        
        /// <summary>
        /// The event handler which will write settings to storage whenever it is changed.
        /// </summary>
        /// <param name="sender">The object which sent this event.</param>
        /// <param name="args">Event arguments for this event.</param>
        private void UpdateSettings(object sender, EventArgs args)
        {
            Logger.Info("Updated Settings File", "UI::Logic");
            UpdateSettingsFile((SettingInformation)sender);
        }

        /// <summary>
        /// This method writes current settings to file
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        private bool UpdateSettingsFile(SettingInformation settings)
        {
            return storage.WriteSettingsToFile(settings);
        }

        /// <summary>
        /// Executes the DisplayDefault operation so that the 
        /// Response given by the operation can be returned.
        /// </summary>
        /// <returns>The default view.</returns>
        internal Response GetDefaultView()
        {
            return new OperationDisplayDefault().Execute(taskList, storage);
        }

        /// <summary>
        /// Updates the currently displayed list of tasks in Operation.
        /// </summary>
        /// <param name="displayedList">The new list to update to.</param>
        internal void UpdateLastDisplayedTasksList(List<Task> displayedList)
        {
            Operation.UpdateCurrentListedTasks(displayedList);
        }
    } 
}
