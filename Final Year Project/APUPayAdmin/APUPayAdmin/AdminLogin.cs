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
    public partial class AdminLogin : Form
    {
        
        public AdminLogin()
        {
            InitializeComponent();
        }
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string type = "Admin";
            string pw = EncryptionDecryption.ComputeSha256Hash(userPw.Text);
            HttpClient client = new HttpClient();
            var uri = new Uri(string.Format("http://apupay.azurewebsites.net/api/Login?username="+ userID.Text +"&password="+pw +"&type="+type));

            HttpResponseMessage response;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            response = client.GetAsync(uri).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                AdminMenu admin = new AdminMenu
                {
                    userID = userID.Text
                };
                admin.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Rejected");
                response.Dispose();
                client.Dispose();
            }
        }
    }
}
