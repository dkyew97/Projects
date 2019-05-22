using Android;
using Android.App;
using Android.Content;
using Android.Hardware.Fingerprints;
using Android.OS;
using Android.Security.Keystore;
using Android.Support.V4.App;
using Android.Widget;
using Java.Security;
using Javax.Crypto;
using Newtonsoft.Json;
using System;

namespace APUPayMobile
{
    [Activity(Label = "BiometricActivity")]
    public class BiometricActivity : Activity
    {
        private Cipher cipher;
        private string KEY_NAME = "Darryl";
        private KeyStore keyStore;
        TransactionObject transactionObject;
        User user;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.biometric_activity);
            if (this.Intent.Extras != null)
            {
                transactionObject = JsonConvert.DeserializeObject<TransactionObject>(Intent.GetStringExtra("transactionObject"));
                user = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("users"));
            }
            KeyguardManager keyguardManager = (KeyguardManager)GetSystemService(KeyguardService);
            FingerprintManager fingerprintManager = (FingerprintManager)GetSystemService(FingerprintService);
            if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.UseFingerprint)
                != (int)Android.Content.PM.Permission.Granted)
                return;
            if (!fingerprintManager.IsHardwareDetected)
                Toast.MakeText(this, "FingerPrint authentication permission not enable", ToastLength.Short).Show();
            else
            {
                if (!fingerprintManager.HasEnrolledFingerprints)
                    Toast.MakeText(this, "Register at least one fingerprint in Settings", ToastLength.Short).Show();
                else
                {
                    if (!keyguardManager.IsKeyguardSecure)
                        Toast.MakeText(this, "Lock screen security not enable in Settings", ToastLength.Short).Show();
                    else
                        GenKey();
                    if (CipherInit())
                    {
                        FingerprintManager.CryptoObject cryptoObject = new FingerprintManager.CryptoObject(cipher);
                        FingerprintHandler handler = new FingerprintHandler(this, transactionObject,user);
                        handler.StartAuthentication(fingerprintManager, cryptoObject);
                    }
                }
            }
        }
        private bool CipherInit()
        {
            try
            {
                cipher = Cipher.GetInstance(KeyProperties.KeyAlgorithmAes
                    + "/"
                    + KeyProperties.BlockModeCbc
                    + "/"
                    + KeyProperties.EncryptionPaddingPkcs7);
                keyStore.Load(null);
                IKey key = (IKey)keyStore.GetKey(KEY_NAME, null);
                cipher.Init(CipherMode.EncryptMode, key);
                return true;
            }
            catch { return false; }
        }
        private void GenKey()
        {
            keyStore = KeyStore.GetInstance("AndroidKeyStore");
            KeyGenerator keyGenerator = null;
            keyGenerator = KeyGenerator.GetInstance(KeyProperties.KeyAlgorithmAes, "AndroidKeyStore");
            keyStore.Load(null);
            keyGenerator.Init(new KeyGenParameterSpec.Builder(KEY_NAME, KeyStorePurpose.Encrypt | KeyStorePurpose.Decrypt)
                .SetBlockModes(KeyProperties.BlockModeCbc)
                .SetUserAuthenticationRequired(true)
                .SetEncryptionPaddings(KeyProperties
                .EncryptionPaddingPkcs7).Build());
            keyGenerator.GenerateKey();
        }
    }
}