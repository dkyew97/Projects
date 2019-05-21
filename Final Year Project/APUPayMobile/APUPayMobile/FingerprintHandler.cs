using Android;
using Android.Content;
using Android.Hardware.Fingerprints;
using Android.OS;
using Android.Support.V4.App;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace APUPayMobile
{
    internal class FingerprintHandler : FingerprintManager.AuthenticationCallback
    {
        private Context mainActivity;
        private TransactionObject transaction;
        private User user;
        public FingerprintHandler(Context mainActivity, TransactionObject transactionObject, User user)
        {
            this.mainActivity = mainActivity;
            this.transaction = transactionObject;
            this.user = user;
        }
        internal void StartAuthentication(FingerprintManager fingerprintManager, FingerprintManager.CryptoObject cryptoObject)
        {
            CancellationSignal cancellationSignal = new CancellationSignal();
            if (ActivityCompat.CheckSelfPermission(mainActivity, Manifest.Permission.UseFingerprint)
                != (int)Android.Content.PM.Permission.Granted)
                return;
            fingerprintManager.Authenticate(cryptoObject, cancellationSignal, 0, this, null);
        }
        public override void OnAuthenticationFailed()
        {
            Toast.MakeText(mainActivity, "Fingerprint Authentication failed!", ToastLength.Short).Show();
        }
        public override void OnAuthenticationSucceeded(FingerprintManager.AuthenticationResult result)
        {
            
            user.UserBalance = user.UserBalance - transaction.TransactionAmount;
            Toast.MakeText(mainActivity, user.UserBalance.ToString(), ToastLength.Short).Show();
            Intent i = new Intent(mainActivity, typeof(BiometricResult));
            i.PutExtra("transactionObject", JsonConvert.SerializeObject(transaction));
            i.PutExtra("user", JsonConvert.SerializeObject(user));
            mainActivity.StartActivity(i);
        }
        
    }
}