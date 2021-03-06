﻿using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.IO;
using Android.Graphics;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using Newtonsoft.Json;

namespace LoginSystem
{
    public class FriendsActivity : Android.Support.V4.App.Fragment
    {

        public LinearLayout mFriendsLayout;
        
        private ListView mListView;
        private FriendsListAdapter mAdapter;
        private List<Friend> mFriends;
        ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
        public Uri url = new Uri("http://192.168.1.125/calendarusers/LoadFriends.php");
        public string usernamefromsp;

        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var view = inflater.Inflate(Resource.Layout.FriendsActivity, container, false);
            HasOptionsMenu = true;
            mListView = view.FindViewById<ListView>(Resource.Id.listView);
            mFriendsLayout = view.FindViewById<LinearLayout>(Resource.Id.FriendsLayout);
            NameValueCollection parameters = new NameValueCollection();
            usernamefromsp = pref.GetString("Username", String.Empty);
            parameters.Add("username", usernamefromsp);
            WebClient client = new WebClient();
            client.UploadValuesCompleted += Client_UploadValuesCompleted;
            client.UploadValuesAsync(url, "POST", parameters);
            return view;
        }
        
        

        private void Client_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
         {
            string json = Encoding.UTF8.GetString(e.Result);
            mFriends = new List<Friend>();
            mFriends = JsonConvert.DeserializeObject<List<Friend>>(json);
            mAdapter = new FriendsListAdapter(this.Activity, Resource.Layout.row_friend, mFriends);
            mAdapter.OnFriendRemoved += MAdapter_OnFriendRemoved;
            mListView.Adapter = mAdapter;
         }

        private void MAdapter_OnFriendRemoved(object sender, EventArgs e)
        {
            WebClient client3 = new WebClient();
            NameValueCollection parameters = new NameValueCollection();
            client3.UploadValuesCompleted += Client_UploadValuesCompleted;
            usernamefromsp = pref.GetString("Username", String.Empty);
            parameters.Add("username", usernamefromsp);
            client3.UploadValuesAsync(url, "POST", parameters);
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            menu.Clear();
            inflater.Inflate(Resource.Menu.actionbar_friend, menu);
            return;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            FragmentTransaction transaction = this.Activity.FragmentManager.BeginTransaction();
            
            switch (item.ItemId)
            {
                case Resource.Id.add:
                    dialog_addfriend dialog = new dialog_addfriend();
                    dialog.Show(transaction, "add friend");
                    return true;

                case Resource.Id.action_logout:
                        ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
                        ISharedPreferencesEditor edit = pref.Edit();
                        edit.Clear();
                        edit.Apply();
                        Intent intent = new Intent(this.Activity, typeof(LoginRegisterActivity));
                        this.StartActivity(intent);
                        this.Dispose();
                    return true;

                case Resource.Id.friendrequests:
                    FriendRequestDialog friendrequestdialog = new FriendRequestDialog();
                    friendrequestdialog.updatefriends += Friendrequestdialog_updatefriends;
                    friendrequestdialog.Show(transaction, "dialog fragment");
                    return true;
                    
                default:
                    return base.OnOptionsItemSelected(item);
            }
           
        }

        private void Friendrequestdialog_updatefriends(object sender, EventArgs e)
        {
            WebClient client2 = new WebClient();
            NameValueCollection parameters = new NameValueCollection(); 
            usernamefromsp = pref.GetString("Username", String.Empty);
            parameters.Add("username", usernamefromsp);
            client2.UploadValuesCompleted += Client_UploadValuesCompleted;
            client2.UploadValuesAsync(url, "POST", parameters);
        }
        
    }
}

