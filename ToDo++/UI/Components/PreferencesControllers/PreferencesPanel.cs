//@raaj A0081202Y
using System;
using System.Windows.Forms;

namespace ToDo
{
    public partial class PreferencesPanel : UserControl
    {

        private Settings settings;

        /// <summary>
        /// Initializes child preferences controls with settings
        /// </summary>
        public void InitializeWithSettings(Settings settings)
        {
            this.settings = settings;
            startingOptionsControl.InitializeStartingOptions(settings);
            flexiCommandsControl.InitializeFlexiCommands(settings);
            fontColorSettingsControl.InitializeFontColorControl(settings); 
        }

        /// <summary>
        /// Intialize Components and Load UI Elements
        /// </summary>
        public PreferencesPanel()
        {
            InitializeComponent();
            LoadPreferencesTree();
        }

        //Loads preference names
        private void LoadPreferencesTree()
        {
            preferencesTree.Nodes.Clear();
            TreeNode treeNode = new TreeNode("Starting Options");
            preferencesTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("Flexi Commands");
            preferencesTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("Font and Colors");
            preferencesTree.Nodes.Add(treeNode);
        }

        //Event handler for selecting preference - loads relavent tab (see designer)
        private void preferencesTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int selectedNodeText = e.Node.Index;
            switch (selectedNodeText)
            {
                case 0:
                    preferencesSelector.SelectedIndex = 0;
                    break;

                case 1:
                    preferencesSelector.SelectedIndex = 1;
                    break;

                case 2:
                    preferencesSelector.SelectedIndex = 2;
                    break;
            }
        }

    }
}
