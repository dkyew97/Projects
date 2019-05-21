using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;

namespace APUPayMobile
{
    [Activity(Label = "ResultActivity")]
    public class ResultActivity : Activity
    {
        View paymentDetails;
        TextView txtresult, txtamt;
        Button pay;
        User user;
        TransactionObject transactionObject;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.result_activity);
            paymentDetails = FindViewById<View>(Resource.Id.message);
            txtresult = paymentDetails.FindViewById<TextView>(Resource.Id.txtSellerResult);
            txtamt = paymentDetails.FindViewById<TextView>(Resource.Id.txtAmtResult);
            pay = FindViewById<Button>(Resource.Id.btnPay);
            if (this.Intent.Extras != null)
            {
                string token = this.Intent.Extras.GetString("Token");
                string users = this.Intent.Extras.GetString("User");
                try
                {
                    transactionObject = JsonConvert.DeserializeObject<TransactionObject>(token);
                    user = JsonConvert.DeserializeObject<User>(users);
                    transactionObject.UserID = user.UserID;
                }
                catch (Exception e)
                {
                    AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                    dialog.SetTitle("Something Went Wrong");
                    dialog.SetMessage("Test");
                    dialog.SetNeutralButton("Ok", delegate { dialog.Dispose(); });
                    dialog.Show();
                }
                txtresult.Text = transactionObject.TransactionSeller;
                txtamt.Text = transactionObject.TransactionAmount.ToString();
            }
            if (transactionObject.TransactionAmount < user.UserBalance)
            {
                pay.Text = "Pay";
                pay.SetBackgroundResource(Resource.Drawable.bckGroundTurquise);
            }
            else
            {
                pay.Text = "Insufficient Funds";
            }
            pay.Click += gonext;
        }
        public void gonext(object sender, EventArgs e)
        {
            if (transactionObject.TransactionAmount < user.UserBalance)
            {
                Intent i = new Intent(this, typeof(BiometricActivity));
                i.PutExtra("transactionObject", JsonConvert.SerializeObject(transactionObject));
                i.PutExtra("users", JsonConvert.SerializeObject(user));
                StartActivity(i);
            }
            else
            {
                Intent i = new Intent(this, typeof(MainMenu));
                i.PutExtra("user", JsonConvert.SerializeObject(user));
                StartActivity(i);
            }
        }
    }
}