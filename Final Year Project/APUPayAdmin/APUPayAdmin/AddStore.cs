using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APUPayAdmin
{
    public partial class AddStore : Form
    {
        AdminMenu adminMenu;
        public AddStore(AdminMenu admin)
        {
            InitializeComponent();
            adminMenu = admin;
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            adminMenu.Show();
            this.Close();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text != null && txtPassword.Text != null)
            {
                HttpClient client = new HttpClient();
                string pw = EncryptionDecryption.ComputeSha256Hash(txtPassword.Text);
                var uri = new Uri(string.Format("https://apupay.azurewebsites.net/api/Shop?username=" + txtUsername.Text + "&password=" + pw));
                HttpResponseMessage response;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var content = new StringContent(String.Empty, Encoding.UTF8, "application/json");
                response = client.PostAsync(uri, content).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    MessageBox.Show("Registered Successfully");
                    adminMenu.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Rejected");
                    response.Dispose();
                    client.Dispose();
                    txtUsername.Clear();
                    txtPassword.Clear();
                }
            }
            else
            {
                MessageBox.Show("Please Fill Up all Details");
            }
        }
    }
}
