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
    public partial class TransactionHistory : Form
    {
        public string userid { get; set; }
        public TransactionHistory()
        {
            InitializeComponent();
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            var uri = new Uri(string.Format("https://apupay.azurewebsites.net/api/Transaction?storeID=" + userid));
            decimal total = 0;
            HttpResponseMessage response;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            response = client.GetAsync(uri).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                string x = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<TransactionObject>>(x);
                transactionHistoryGrid.DataSource = result;
                foreach (var item in result)
                {
                    total += item.TransactionAmount;
                }
                label1.Text = "RM: " + Convert.ToString(total);
            }
            else
            {
                
            }
        }

        private async void button2_ClickAsync(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            List<TransactionObject> result;
            decimal total = 0;
            string date = datePicker.Value.ToShortDateString();
            var uri = new Uri(string.Format("https://apupay.azurewebsites.net/api/Transaction?dateTime=" + date + "&storeID=" + userid));
            HttpResponseMessage response;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            response = client.GetAsync(uri).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                string x = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<TransactionObject>>(x);
                transactionHistoryGrid.DataSource = result;
                foreach (var item in result)
                {
                    total += item.TransactionAmount;
                }
                label1.Text ="RM: " + Convert.ToString(total);
            }
            else
            {

            }
        }
    }
}