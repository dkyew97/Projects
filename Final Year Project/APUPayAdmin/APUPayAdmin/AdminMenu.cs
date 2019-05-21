using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APUPayAdmin
{
    public partial class AdminMenu : Form
    {
        public string userID { get; set; }

        public AdminMenu()
        {
            InitializeComponent();
        }

        private void AdminMenu_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = "Welcome " + userID;
        }

        private void buttonAddStore_Click(object sender, EventArgs e)
        {
            AddStore addStore = new AddStore(this);
            addStore.Show();
            Hide();
        }

        private void buttonAddUser_Click(object sender, EventArgs e)
        {
            AddUser addUser = new AddUser(this);
            addUser.Show();
            Hide();
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            ReloadMenu reloadMenu = new ReloadMenu(this);
            reloadMenu.Show();
            Hide();
        }
    }
}
