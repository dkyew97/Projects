using Newtonsoft.Json;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace APUPayStore
{
    public partial class ShopMenu : Form
    {
        public string userID { get; set; }
        public string itemJson { get; set; }
        List<ShopInventory> x = new List<ShopInventory>();

        public ShopMenu()
        {
            InitializeComponent();
            
        }

        private void ShopMenu_Load(object sender, EventArgs e)
        {
            lblShop.Text = "Welcome, " +userID;
            txtPrice.Enabled = false;
            x = JsonConvert.DeserializeObject<List<ShopInventory>>(itemJson);
            foreach (ShopInventory item in x)
            {
                inventoryBox.Items.Add(item.item);
            }
        }

        private void Button_Generate_Click(object sender, EventArgs e)
        {
            String result;
            if (txtPrice.Text != "")
            {
                try
                {
                    Random ran = new Random();
                    string transactionid = ran.Next(0, 9999).ToString().PadLeft(4, '0') + DateTime.Now.ToString("yyyyMMddHHmmss");
                    TransactionObject transactionObject = new TransactionObject { TransactionAmount = Convert.ToDecimal(txtPrice.Text), TransactionSeller = userID, TransactionID = transactionid, TransactionTime = DateTime.Now };
                    result = JsonConvert.SerializeObject(transactionObject);

                    QRCodeGenerator qr = new QRCodeGenerator();
                    QRCodeData code = qr.CreateQrCode(result, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrcode = new QRCode(code);

                    pictureBox1.Image = qrcode.GetGraphic(5);
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid Information Entered");
                }
            }
            else
            {
                MessageBox.Show("Please Select an Item");

            }
        }

        private void inventoryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal value = 0;
            foreach(var item in inventoryBox.CheckedItems)
            {
                foreach(var items in x)
                {
                    if (item.Equals(items.item))
                    {
                        value += items.price;
                    }
                }
            }
            txtPrice.Text = Convert.ToString(value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TransactionHistory sm = new TransactionHistory()
            {
                userid = userID
            };

            sm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddInventory ai = new AddInventory(this)
            {
                userid = userID
            };

            ai.Show();
            this.Hide();
        }
    }
}
