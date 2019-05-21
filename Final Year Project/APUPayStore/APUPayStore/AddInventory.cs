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
    public partial class AddInventory : Form
    {
        public string userid { get; set; }
        public ShopMenu shop;
        public AddInventory(ShopMenu shopMenu)
        {
            InitializeComponent();
            shop = shopMenu;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && textBox2.Text != "" && textBox1.Text != "")
            {
                HttpClient client = new HttpClient();
                string pw = EncryptionDecryption.ComputeSha256Hash(textBox3.Text);
                var uri = new Uri(string.Format("http://apupay.azurewebsites.net/api/Shop?username=" + userid + "&password=" + pw));
                HttpResponseMessage response;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                response = client.GetAsync(uri).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    string itemname = textBox1.Text;
                    decimal price = Convert.ToDecimal(textBox2.Text);
                    HttpClient clien1t = new HttpClient();
                    var uri1 = new Uri(string.Format("https://apupay.azurewebsites.net/api/Shop?storeID=" + userid + "&item=" + itemname + "&price=" + price));
                    HttpResponseMessage response1;
                    clien1t.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var content = new StringContent(String.Empty, Encoding.UTF8, "application/json");
                    response1 = await client.PostAsync(uri1, content);
                    if (response1.StatusCode == System.Net.HttpStatusCode.Accepted)
                    {
                        MessageBox.Show("Accepted");
                    }
                    else
                    {
                        shop.Show();
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private async void button2_Click_1Async(object sender, EventArgs e)
        {
            
            HttpClient client = new HttpClient();
            string pw = EncryptionDecryption.ComputeSha256Hash(textBox3.Text);
            var uri = new Uri(string.Format("http://apupay.azurewebsites.net/api/Shop?username=" + userid + "&password=" + pw));
            HttpResponseMessage response;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            response = client.GetAsync(uri).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                string x = await response.Content.ReadAsStringAsync();
                ShopMenu sm = new ShopMenu()
                {
                    userID = userid,
                    itemJson = x
                };
                sm.Show();
                this.Hide();
            }

        }
    }
}
