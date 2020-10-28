using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MontgomerySoftware.Controls
{
    public partial class FolderBrowserDialog : Component
    {
        private FolderBrowserForm m_frm;

        public FolderBrowserDialog()
        {
            InitializeComponent();
            m_frm = new FolderBrowserForm();
        }

        public FolderBrowserDialog(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        public string RootFolder
        {
            get { return m_frm.RootFolder; }
            set { m_frm.RootFolder = value; }
        }
        public string SelectedPath
        {
            get { return m_frm.SelectedPath; }
            set { m_frm.SelectedPath = value; }
        }
        public DialogResult ShowDialog()
        {
            return m_frm.ShowDialog();
        }
        public string Title
        {
            get { return m_frm.Title; }
            set { m_frm.Title=value;}
        }
        public string Description
        {
            get { return m_frm.Description; }
            set { m_frm.Description = value; }
        }
        public bool ShowNewFolderButton
        {
            get { return m_frm.ShowNewFolderButton; }
            set { m_frm.ShowNewFolderButton = value; }
        }
    }
}
