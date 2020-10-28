using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MontgomerySoftware.Controls
{
    partial class FolderBrowserForm : Form
    {
        private string m_strRootFolder = string.Empty;
        private string m_strSelectedPath = string.Empty;

        public FolderBrowserForm()
        {
            InitializeComponent();
        }
        public string Title
        {
            get { return this.Text; }
            set { this.Text = value; }
        }
        public string Description
        {
            get { return this.lblDescription.Text; }
            set { this.lblDescription.Text = value; }
        }
        public bool ShowNewFolderButton
        {
            get { return btnNewFolder.Visible; }
            set { btnNewFolder.Visible = value; }
        }
        public string RootFolder
        {
            get { return m_strRootFolder; }
            set 
            { 
                m_strRootFolder = value;
                if (m_strRootFolder.EndsWith(@"\"))
                    m_strRootFolder = m_strRootFolder.Substring(0, m_strRootFolder.Length - 1);
            }
        }
        public string SelectedPath
        {
            get { return m_strSelectedPath; }
            set 
            { 
                m_strSelectedPath = value;
            }
        }

        private void FolderBrowserForm_Load(object sender, EventArgs e)
        {
            if (m_strRootFolder.Length == 0)
            {
                m_strRootFolder = System.IO.Path.GetPathRoot(Application.ExecutablePath);
            }
            string strDisplay = System.IO.Path.GetFileName(m_strRootFolder);
            if (strDisplay.Length == 0)
                strDisplay = m_strRootFolder;
            TreeNode tnRoot = tvFolders.Nodes.Add(m_strRootFolder, strDisplay);
            tnRoot.Tag = m_strRootFolder;
            AddChildFolders(m_strRootFolder, tnRoot);
            tnRoot.Expand();
        }
        private void AddChildFolders(string path, TreeNode tn)
        {
            if (!path.EndsWith(@"\"))
                path += @"\";
            try
            {
                string[] dirs = System.IO.Directory.GetDirectories(path);
                foreach (string dir in dirs)
                {
                    TreeNode tnChild = tn.Nodes.Add(System.IO.Path.GetFileName(dir));
                    tnChild.Tag = dir;

                    // don't recurse, just load as they are expanded
                    // but only make expandable those which have child directories
                    if (System.IO.Directory.GetDirectories(dir).Length>0)
                        tnChild.Nodes.Add("");
                    
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Unable to show the contents of folder (" + path + ").", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnNewFolder_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            // add new folder under currently selected folder
            // and put it in label edit mode
        }

        private void tvFolders_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            // Now, populate all children
            e.Node.Nodes.Clear();
            AddChildFolders(e.Node.Tag.ToString(), e.Node);
        }

        private void tvFolders_AfterSelect(object sender, TreeViewEventArgs e)
        {
            m_strSelectedPath=e.Node.Tag.ToString();
        }
    }
}
