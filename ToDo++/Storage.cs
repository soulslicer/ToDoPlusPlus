//@ivan A0086401M
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace ToDo
{
    /// <summary>
    /// Storage class. Controls file creation, I/O. Maintains all necessary information
    /// on disk as XML files.
    /// </summary>
    public class Storage
    {
        string taskStorageFile, settingsFile;

        /// <summary>
        /// Constructs a Storage I/O handler class, creating two XML files for task storage and settings storage using
        /// the specified taskStorageFile and settingsFile as their respective filenames.
        /// </summary>
        /// <param name="taskStorageFile">String representing the filename to create the task storage XML file.</param>
        /// <param name="settingsFile">String representing the filename to create the settings XML file.</param>
        public Storage(string taskStorageFile, string settingsFile)
        {
            this.taskStorageFile = taskStorageFile;
            this.settingsFile = settingsFile;            
            if (!ValidateTaskFile())
                CreateNewTaskFile();
        }

        /// <summary>
        /// Checks if the task file is well-formed.
        /// </summary>
        /// <returns>True if task file is well-formed; False if not.</returns>
        private bool ValidateTaskFile()
        {
            //check for well-formedness
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            try
            {
                using (XmlReader reader = XmlReader.Create(taskStorageFile, settings))
                {
                    // check for "tasks" node
                    reader.MoveToContent();
                    if (reader.NodeType == XmlNodeType.Element)
                        if (reader.Name == "tasks")
                            return true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "ValidateTaskFile::Storage");
                return false;
            }            
        }

        /// <summary>
        /// Creates a new task XML file.
        /// </summary>
        /// <returns>True if operation was successful; False if not.</returns>
        internal bool CreateNewTaskFile()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml("<tasks>" +
                            "</tasks>");              
                doc.Save(taskStorageFile);                
            }
            catch (ArgumentNullException e)
            {
                Logger.Error(e, "CreateNewTaskFile::Storage");
                UserInputBox.Show("Error!", "Task filename was not set!");
                return false;
            }
            catch (InvalidOperationException e)
            {
                Logger.Error(e, "CreateNewTaskFile::Storage");
                UserInputBox.Show("Error!", "Failed to create task file.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Load all settings from the settings file.
        /// </summary>
        /// <returns>The settings information loaded from the file.</returns>
        internal SettingInformation LoadSettingsFromFile()
        {
            SettingInformation settingInfo = new SettingInformation();
            try
            {
                using (StreamReader file = new StreamReader(settingsFile))
                {
                    string xml = file.ReadToEnd();  
                    settingInfo = xml.Deserialize<SettingInformation>();
                    file.Close();
                }
            }
            // Write default settings if file not found or invalid.
            catch (FileNotFoundException e)
            {
                Logger.Error(e, "LoadSettingsFromFile::Storage");
                AlertBox.Show("Settings file not found.\nNew file will be created");
                WriteSettingsToFile(settingInfo);
            }
            catch (System.Runtime.Serialization.SerializationException e)
            {
                Logger.Error(e, "LoadSettingsFromFile::Storage");
                AlertBox.Show("There was an error with the settings file, a new file will be created");
                WriteSettingsToFile(settingInfo);
            }
            return settingInfo;
        }

        /// <summary>
        /// Write all settings to an XML file.
        /// </summary>
        /// <param name="settingInfo">The settings information to write to file.</param>
        /// <returns>True if operation was successful; False if not.</returns>
        internal bool WriteSettingsToFile(SettingInformation settingInfo)
        {
            try
            {
                StreamWriter file = new StreamWriter(settingsFile);
                file.Write(settingInfo.ToXML());
                file.Close();
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e, "WriteSettingsToFile::Storage");
                AlertBox.Show("Failed to write to settings file!");
                return false;
            }
        }

        /// <summary>
        /// Appends a task to the task file.
        /// </summary>
        /// <param name="taskToAdd">The task to append.</param>
        /// <returns>True if operation was successful; False if not.</returns>
        internal bool AddTaskToFile(Task taskToAdd)
        {
            try
            {
                XDocument doc = XDocument.Load(taskStorageFile);
                XElement newTaskElem = taskToAdd.ToXElement();
                doc.Root.Add(newTaskElem);
                doc.Save(taskStorageFile);
            }
            catch (Exception e)
            {
                Logger.Error(e, "AddTaskToFile::Storage");
                AlertBox.Show("A problem was encoutered saving the new task to file.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Deletes a task from the task file.
        /// </summary>
        /// <param name="taskToDelete">The task to delete.</param>
        /// <returns>True if operation was successful; False if not.</returns>
        internal bool RemoveTaskFromFile(Task taskToDelete)
        {
            XDocument doc = XDocument.Load(taskStorageFile);
            
            var taskNode =  from node in doc.Descendants("Task")
                        let attr = node.Attribute("id")
                        where attr != null && attr.Value == taskToDelete.ID.ToString()
                        select node;

            if (taskNode == null) return false;

            try
            {
                taskNode.ToList().ForEach(x => x.Remove());
            }
            catch (Exception e)
            {
                Logger.Error(e, "RemoveTaskFromFile::Storage");
                return false;
            }

            doc.Save(taskStorageFile);

            return true;            
        }

        /// <summary>
        /// Updates a task in the task file.
        /// </summary>
        /// <param name="taskToUpdate">The task to update.</param>
        /// <returns>True if operation was successful; False if not.</returns>
        internal bool UpdateTask(Task taskToUpdate)
        {
            XDocument doc = XDocument.Load(taskStorageFile);

            var task = from node in doc.Descendants("Task")
                       let attr = node.Attribute("id")
                       where attr != null && attr.Value == taskToUpdate.ID.ToString()
                       select node;

            if (task == null) return false;

            try
            {
                XElement taskNode = task.First();
                taskNode.ReplaceWith(taskToUpdate.ToXElement());
            }
            catch (Exception e)
            {
                Logger.Error(e, "UpdateTask::Storage");
                return false;
            }

            doc.Save(taskStorageFile);
            return true;
        }

        /// <summary>
        /// Marks a task in the task file.
        /// </summary>
        /// <param name="taskToMarkAsDone">The task to mark.</param>
        /// <param name="done">Task will be marked as done if true, undone if false.</param>
        /// <returns>True if operation was successful; False if not.</returns>
        internal bool MarkTaskAs(Task taskToMarkAsDone, bool done)
        {
            XDocument doc = XDocument.Load(taskStorageFile);

            var task = from node in doc.Descendants("Task")
                       let attr = node.Attribute("id")
                       where attr != null && attr.Value == taskToMarkAsDone.ID.ToString()
                       select node;

            if (task == null) return false;

            try
            {
                if(done)
                    task.First().Element("Done").ReplaceNodes("True");
                else
                    task.First().Element("Done").ReplaceNodes("False");
            }
            catch (Exception e)
            {
                Logger.Error(e, "MarkTaskAs::Storage");
                return false;
            }

            doc.Save(taskStorageFile);
            return true;
        }

        /// <summary>
        /// Loads all tasks from the task file into a list.
        /// </summary>
        /// <returns>The loaded list of tasks</returns>
        public List<Task> LoadTasksFromFile()
        {
            List<Task> taskList = new List<Task>();
            try
            {
                XDocument doc = XDocument.Load(taskStorageFile);
                IEnumerable<XElement> tasks =
                    (from task in doc.Root.Elements("Task") select task);
                foreach (XElement task in tasks)
                {
                    try
                    {
                        Task addTask = GenerateTaskFromXElement(task);
                        if (addTask == null)
                        {
                            return null;
                        }
                        taskList.Add(addTask);
                    }
                    catch (ArgumentNullException e)
                    {
                        Logger.Error(e, "LoadTasksFromFile::Storage");
                        return null;
                    }
                    catch (TaskFileCorruptedException e)
                    {
                        Logger.Error(e, "LoadTasksFromFile::Storage");
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, "LoadTasksFromFile::Storage");
                return null;
            }
            return taskList;
        }
  
        /// <summary>
        /// Regenerates a task from an XElement node
        /// </summary>
        /// <param name="task">The XElement node to regenerate the task from.</param>
        /// <returns>The regenerated task.</returns>
        private Task GenerateTaskFromXElement(XElement task)
        {
            Task newTask = null;

            try
            {
                string type = task.Attribute("type").Value;
                int id = Int32.Parse(task.Attribute("id").Value);
                string taskName = task.Element("Name").Value;
                DateTime startTime, endTime;
                DateTimeSpecificity isSpecific = new DateTimeSpecificity();

                XElement DTSpecElement = task.Element("DateTimeSpecificity");
                if (DTSpecElement != null) isSpecific = DTSpecElement.FromXElement<DateTimeSpecificity>();
                bool state;

                if (task.Element("Done").Value == "True") state = true;
                else state = false;
                switch (type)
                {
                    case "Floating":
                        newTask = new TaskFloating(taskName, state, id);
                        break;
                    case "Deadline":
                        endTime = DateTime.Parse(task.Element("EndTime").Value);
                        newTask = new TaskDeadline(taskName, endTime, isSpecific, state, id);
                        break;
                    case "Event":
                        endTime = DateTime.Parse(task.Element("EndTime").Value);
                        startTime = DateTime.Parse(task.Element("StartTime").Value);
                        newTask = new TaskEvent(taskName, startTime, endTime, isSpecific, state, id);
                        break;
                }
            }
            catch (NullReferenceException)
            {
                throw new TaskFileCorruptedException();
            }
            return newTask;

        }
    }
}
