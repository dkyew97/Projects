using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace APUPayMobile
{
    [Activity(Label = "BiometricResult")]
    public class BiometricResult : Activity
    {
        TextView receipt, balance, thankYou,lblReceipt, lblBalance;
        TransactionObject transactionObject;
        Button btnReturn;
        User user;
        ImageView code;
        bool balanceBoolean, receiptBoolean;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.biometric_result);
            if (this.Intent.Extras != null)
            {
                transactionObject = JsonConvert.DeserializeObject<TransactionObject>(Intent.GetStringExtra("transactionObject"));
                user = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("user"));
            }
            Random r = new Random();
            int rannum = r.Next(0, 99999);
            string receiptnumber = DateTime.Now.ToString("yyyyMMddHHmmss") + rannum.ToString().PadLeft(5, '0');
            code = FindViewById<ImageView>(Resource.Id.check);
            btnReturn = FindViewById<Button>(Resource.Id.btnReturn);
            thankYou = FindViewById<TextView>(Resource.Id.ThankYou);
            
            Task.Run(async () => { receiptBoolean = await InsertTransactionAsync(); }).Wait();
            if (receiptBoolean)
            {
                Task.Run(async () => { balanceBoolean = await UpdateBalanceAsync(); }).Wait();
            }
            if(balanceBoolean && receiptBoolean)
            {
                receipt = FindViewById<TextView>(Resource.Id.txtReceipt);
                lblBalance = FindViewById<TextView>(Resource.Id.lblBalance);
                lblReceipt = FindViewById<TextView>(Resource.Id.lblReceipt);
                balance = FindViewById<TextView>(Resource.Id.txtBalance);

                code.SetImageResource(Resource.Drawable.check_outline);
                thankYou.Text = "Thank You for Using APUPay";
                lblReceipt.Text = "Receipt Number: ";
                receipt.Text = receiptnumber;
                lblBalance.Text = "Balance: ";
                balance.Text = user.UserBalance.ToString();
            }
            else
            {
                code.SetImageResource(Resource.Drawable.error);
                thankYou.TextSize = 15;
                thankYou.Text = "Something Went Wrong. No funds has been deducted. Refresh the QR code and Try Again";
                
            }
            btnReturn.Click += btnClickReturn;
        }
        public async Task<bool> UpdateBalanceAsync()
        {
            HttpClient client = new HttpClient();
            var uri = new Uri(string.Format("https://apupay.azurewebsites.net/api/Login/" + user.UserID + "?balance=" + transactionObject.TransactionAmount + "&transactionType=Spend"));
            HttpResponseMessage response;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            response = await client.PostAsync(uri, content);
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task <bool> InsertTransactionAsync()
        {
            HttpClient client = new HttpClient();
            var uri = new Uri(string.Format("https://apupay.azurewebsites.net/api/Transaction?user_id=" + transactionObject.UserID+ "&store_id="+ transactionObject.TransactionSeller+"&date=" + transactionObject.TransactionTime+ "&transactionID=" + transactionObject.TransactionID + "&amount=" + transactionObject.TransactionAmount));
            HttpResponseMessage response;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            response = await client.PostAsync(uri, content);
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        async void btnClickReturn(object sender, EventArgs e)
        {
            Boolean verify = true;
            var uri = new Uri(string.Format("http://apupay.azurewebsites.net/api/Login?username=" + user.UserID + "&password=" + user.UserPassword + "&authenticate=" + verify));
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            response = await client.GetAsync(uri);
            var json = await response.Content.ReadAsStringAsync();
            user = JsonConvert.DeserializeObject<User>(json);
            Intent i = new Intent(this, typeof(MainMenu));
            i.PutExtra("user", JsonConvert.SerializeObject(user));
            this.StartActivity(i);
        }
    }
}