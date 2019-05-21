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
    public partial class ReloadMenu : Form
    {
        AdminMenu adminMenu;
        public ReloadMenu(AdminMenu admin)
        {
            InitializeComponent();
            adminMenu = admin;
        }

       
        private void buttonReturn_Click(object sender, EventArgs e)
        {
            adminMenu.Show();
            this.Close();
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            if (txtTPNumber.Text != "" && txtAmount.Text != "")
            {
                HttpClient client = new HttpClient();
                var uri = new Uri(string.Format("https://apupay.azurewebsites.net/api/Login/" + txtTPNumber.Text + "?balance=" + txtAmount.Text + "&transactionType=Reload"));
                HttpResponseMessage response;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var content = new StringContent(String.Empty, Encoding.UTF8, "application/json");
                response = client.PostAsync(uri, content).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    MessageBox.Show("Reload Successfully");
                    adminMenu.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Rejected");
                    response.Dispose();
                    client.Dispose();
                    txtTPNumber.Clear();
                    txtAmount.Clear();
                }
            }
            else
            {
                MessageBox.Show("Please Fill Up all Details");
            }
        }
    }
}
