using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Text;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace APUPayMobile
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        ImageView logo;
        EditText userID, userPassword;
        Button btnLogin;
        User user = new User();
        ProgressBar progressBar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            logo = FindViewById<ImageView>(Resource.Id.logoView);
            userID = FindViewById<EditText>(Resource.Id.loginUsername);
            userPassword = FindViewById<EditText>(Resource.Id.loginPassword);
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            logo.SetImageResource(Resource.Drawable.apiit_group_logo_merged_H100);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            
            btnLogin.Click += Login;
        }
        private async void Login(object sender, EventArgs e)
        {
            if (TextUtils.IsEmpty(userID.Text) || TextUtils.IsEmpty(userPassword.Text))
            {
                Toast.MakeText(this, "Don't Leave Fields Empty", ToastLength.Long).Show();
            }
            else
            {
                try
                {
                    progressBar.Visibility = ViewStates.Visible;
                    HttpClient client = new HttpClient();
                    string pw = EncryptionDecryption.ComputeSha256Hash(userPassword.Text);
                    var uri = new Uri(string.Format("http://apupay.azurewebsites.net/api/Login?username=" + userID.Text + "&password=" + pw));
                    userID.Enabled = false;
                    userPassword.Enabled = false;
                    btnLogin.Enabled = false;
                    HttpResponseMessage response;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    response = await client.GetAsync(uri);
                    if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        progressBar.Visibility = ViewStates.Gone;
                        user = JsonConvert.DeserializeObject<User>(json);
                        Toast.MakeText(this, "Welcome", ToastLength.Short).Show();
                        Intent i = new Intent(this, typeof(MainMenu));
                        i.PutExtra("user", JsonConvert.SerializeObject(user));
                        this.StartActivity(i);
                    }
                    else
                    {
                        var errorMessage1 = response.Content.ReadAsStringAsync().Result.Replace("\\", "").Trim(new char[1]
                        {
                '"'
                        });
                        Toast.MakeText(this, errorMessage1, ToastLength.Long).Show();
                        userID.Enabled = true;
                        userPassword.Enabled = true;
                        btnLogin.Enabled = true;
                        userID.Dispose();
                        userPassword.Dispose();
                        progressBar.Visibility = ViewStates.Gone;
                    }
                }
                catch
                {
                    Toast.MakeText(this, "Something Went Wrong", ToastLength.Long).Show();
                    userID.Enabled = true;
                    userPassword.Enabled = true;
                    btnLogin.Enabled = true;
                    userID.Dispose();
                    userPassword.Dispose();
                    progressBar.Visibility = ViewStates.Gone;
                }
            }
            
        }
    }
}