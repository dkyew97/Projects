using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace APUPayMobile
{
    [Activity(Label = "ViewHistory")]
    public class ViewHistory : Activity
    {
        private RecyclerView recycler;
        private TransactionHistoryAdapter adapter;
        private RecyclerView.LayoutManager layoutManager;
        private List<TransactionObject> history = new List<TransactionObject>();
        private User user;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.view_transaction);
            if (this.Intent.Extras != null)
            {
                user = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra("user"));
            }
            Task.Run(async () => { await GetHistoryAsync(); }).Wait();
            recycler = FindViewById<RecyclerView>(Resource.Id.recycler);
            recycler.HasFixedSize = true;
            layoutManager = new LinearLayoutManager(this);
            recycler.SetLayoutManager(layoutManager);
            adapter = new TransactionHistoryAdapter(history);
            recycler.SetAdapter(adapter);
        }
        private async Task GetHistoryAsync()
        {
            HttpClient client = new HttpClient();
            
            var uri = new Uri(string.Format("https://apupay.azurewebsites.net/api/Transaction?userID=" + user.UserID));
            HttpResponseMessage response;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            response = await client.GetAsync(uri);
            string x = null;
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                x = await response.Content.ReadAsStringAsync();
            }
            else
            {
                //Toast.MakeText(this, "Failed", ToastLength.Short).Show();
            }
            List<TransactionObject> orderedList = JsonConvert.DeserializeObject<List<TransactionObject>>(x);
            history = orderedList.OrderBy(i => i.TransactionTime).ToList();
        }
    }
}