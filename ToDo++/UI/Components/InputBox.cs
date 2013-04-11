//@raaj A0081202Y
using System.Collections.Generic;
using System.Windows.Forms;

namespace ToDo
{
    class InputBox:TextBox
    {
        int currentIndex=0;
        List<string> commandsEntered = new List<string>();

        /// <summary>
        /// Adds a command entry to the input box
        /// </summary>
        /// <param name="commandEntered"></param>
        public void AddToList(string commandEntered)
        {
            commandsEntered.Add(commandEntered);
            currentIndex = commandsEntered.Count;
        }

        /// <summary>
        /// Goes to previous command
        /// </summary>
        public void UpdateWithPrevCommand()
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                this.Text = commandsEntered[currentIndex];
                this.SelectionStart = this.Text.Length;
            }
        }

        /// <summary>
        /// Move cursor back to start of previous word
        /// </summary>
        public void MoveBackOneWord()
        {
            int lastPositionOfSpace = -1;
            if (this.SelectionStart > 2)
            {
                lastPositionOfSpace = this.Text.LastIndexOf(' ', this.SelectionStart - 2);
            }
            if (lastPositionOfSpace > -1)
            {
                this.SelectionStart = lastPositionOfSpace + 1;
            }
            else
            {
                this.SelectionStart = 0;
            }
        }

        /// <summary>
        /// Move cursor back to start of previous word
        /// </summary>
        public void DeleteLastWord()
        {
            if (this.Text.Length == 0)
            {
                return;
            }
            int lastPositionOfSpace = -1;
            if (this.SelectionStart > 2)
            {
                lastPositionOfSpace = this.Text.LastIndexOf(' ', this.SelectionStart - 2);
            }
            if (lastPositionOfSpace > -1)
            {
                this.Text = this.Text.Remove(lastPositionOfSpace, this.SelectionStart-lastPositionOfSpace);
                this.SelectionStart = lastPositionOfSpace;
            }
            else
            {
                this.Text = string.Empty;
            }
        }

        /// <summary>
        /// Move cursor to end of next work
        /// </summary>
        public void MoveForwardOneWord()
        {
            int nextPositionOfSpace = this.Text.IndexOf(' ', this.SelectionStart);
            if (nextPositionOfSpace > -1)
            {
                this.SelectionStart = nextPositionOfSpace + 1;
            }
            else
            {
                this.SelectionStart = this.Text.Length;
            }
        }

        /// <summary>
        /// Goes to next command
        /// </summary>
        public void UpdateWithNextCommand()
        {
            if (currentIndex < (commandsEntered.Count-1))
            {
                currentIndex++;
                this.Text = commandsEntered[currentIndex];
                this.SelectionStart = this.Text.Length;
            }
        }
    }
}
