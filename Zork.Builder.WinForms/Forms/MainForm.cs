using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using Zork;


namespace Zork.Builder.WinForms
{
    public partial class MainForm : Form
    {

        internal GameViewModel ViewModel { get; private set; }

        public MainForm()
        {
            InitializeComponent();
            ViewModel = new GameViewModel();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string jsonString = File.ReadAllText(openFileDialog.FileName);
                ViewModel.Game = JsonConvert.DeserializeObject<Zork.Game>(jsonString);
            }
        }




    }
}
