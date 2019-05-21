using Newtonsoft.Json;
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

namespace APUPayStore
{
    public partial class ShopLogin : Form

    {

        public ShopLogin()
        {
            InitializeComponent();
        }

        private async void Button_Login_ClickAsync(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            string pw = EncryptionDecryption.ComputeSha256Hash(userPw.Text);
            var uri = new Uri(string.Format("http://apupay.azurewebsites.net/api/Shop?username=" + userID.Text + "&password=" + pw));
            
            HttpResponseMessage response;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            response = client.GetAsync(uri).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                string x = await response.Content.ReadAsStringAsync();
                ShopMenu sm = new ShopMenu()
                {
                    userID = userID.Text,
                    itemJson = x
                };
                sm.Show();
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
