using System;
using System.Drawing;
using System.Windows.Forms;

namespace FormApp
{
    public partial class HomeScreen : Form
    {
        public HomeScreen()
        {
            InitializeComponent();
        }

        private void lbl_NewGame_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form = new MainScreen(this);
            form.Show();
        }        

        private void Label_MouseEnter(object sender, EventArgs e)
        {
            var label = (Label)sender;
            label.ForeColor = Color.White;
            label.BackColor = Color.Brown;
        }
        private void Label_MouseLeave(object sender, EventArgs e)
        {
            var label = (Label)sender;
            label.ForeColor = Color.Black;
            label.BackColor = Color.Khaki;
        }

        private void lbl_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
