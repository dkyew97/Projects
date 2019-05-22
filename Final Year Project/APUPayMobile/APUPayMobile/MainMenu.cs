using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using ZXing.Mobile;

namespace APUPayMobile
{
    [Activity(Label = "MainMenu")]
    public class MainMenu : Activity
    {
        User user;
        Button Pay, History;
        TextView name, balance;
        View userdetails;
        TransactionObject transaction;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.main_menu);
            if (this.Intent.Extras != null)
            {
                user = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("user"));
            }
            Pay = FindViewById<Button>(Resource.Id.btnPay);
            History = FindViewById<Button>(Resource.Id.btnHistory);
            userdetails = FindViewById<View>(Resource.Id.OnlineOffline);
            name = userdetails.FindViewById<TextView>(Resource.Id.user_name);
            balance = FindViewById<TextView>(Resource.Id.txtBalance);
            name.Text = user.FullName;
            balance.Text = "RM " + user.UserBalance.ToString("N2");
            Intent i = new Intent(this, typeof(ViewHistory));
            i.PutExtra("user", JsonConvert.SerializeObject(user));

            Pay.Click += btnClickAsync;
            History.Click += delegate { StartActivity(i); };
        }
        public async void btnClickAsync(object sender, EventArgs e)
        {
            MobileBarcodeScanner.Initialize(Application);
            var scan = new MobileBarcodeScanner();
            var result = await scan.Scan();
            HandleResult(result);
        }
        void HandleResult(ZXing.Result result)
        {
            string msg = "No Barcode";
            if (result != null)
            {
                msg = result.Text;
                try
                {
                    Intent intent = new Intent(this, typeof(ResultActivity));
                    transaction = JsonConvert.DeserializeObject<TransactionObject>(msg);
                    intent.PutExtra("User", JsonConvert.SerializeObject(user));
                    intent.PutExtra("Token", JsonConvert.SerializeObject(transaction));

                    this.StartActivity(intent);

                }
                catch 
                {
                    AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                    dialog.SetTitle("Invalid QR");
                    dialog.SetMessage("This is an invalid QR code");
                    dialog.SetNeutralButton("Ok", delegate { dialog.Dispose(); });
                    dialog.Show();
                }

            }
        }
    }
}