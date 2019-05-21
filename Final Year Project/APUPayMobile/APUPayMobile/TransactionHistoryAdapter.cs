using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;

namespace APUPayMobile
{
    class RecyclerViewHolder: RecyclerView.ViewHolder
    {
        public TextView txtTitle, txtDesc;
        public RecyclerViewHolder(View itemView):base(itemView)
        {
            txtTitle = itemView.FindViewById<TextView>(Resource.Id.txtTitle);
            txtDesc = itemView.FindViewById<TextView>(Resource.Id.txtDescription);
        }
    }
    class TransactionHistoryAdapter : RecyclerView.Adapter
    {
        private List<TransactionObject> history = new List<TransactionObject>();
        public TransactionHistoryAdapter(List<TransactionObject> history)
        {
            this.history = history;
        }
        
        public override int ItemCount
        {
            get
            {
                return history.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewHolder viewHolder = holder as RecyclerViewHolder;
            viewHolder.txtTitle.Text = "Shop: "+history[position].TransactionSeller.ToString();
            viewHolder.txtDesc.Text = "RM " +history[position].TransactionAmount.ToString("N2");
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.list_history, parent, false);
            return new RecyclerViewHolder(itemView);
        }
    }
}