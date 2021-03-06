﻿using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using Android.Media;

using Newtonsoft.Json;
using Java.Sql;
using Android.Support.V4.App;

namespace LoginSystem
{ 
    
    public class CalendarActivity : Android.Support.V4.App.Fragment, DatePicker.IOnDateChangedListener
    {

        private ListView mListView;
        private CalendarEventListAdapter mAdapter;
        private List<CalendarEvent> mEvents;
        public Uri url = new Uri("http://192.168.1.125/calendarusers/LoadEvents.php");
        public string usernamefromsp;
        ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
        public DatePicker mDatePicker;
        public string senddate;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.CalendarActivity, container, false);
            HasOptionsMenu = true;
            mDatePicker = view.FindViewById<DatePicker>(Resource.Id.datePicker1);

            int year = mDatePicker.Year, month = mDatePicker.Month, day = mDatePicker.DayOfMonth;
            mDatePicker.Init(year, month, day, this);
            mListView = view.FindViewById<ListView>(Resource.Id.EventsListView);
            NameValueCollection parameters = new NameValueCollection();
            usernamefromsp = pref.GetString("Username", String.Empty);
            parameters.Add("username", usernamefromsp);
            parameters.Add("selecteddate", mDatePicker.Year + "-" + (mDatePicker.Month + 1) + "-" + mDatePicker.DayOfMonth);
            WebClient client = new WebClient();
           // mDatePicker.CalendarView.DateChange += CalendarView_DateChange;
            client.UploadValuesCompleted += Client1_UploadValuesCompleted;
            client.UploadValuesAsync(url, "POST", parameters);

            return view;
        }
        
        private void CalendarView_DateChange(object sender, CalendarView.DateChangeEventArgs e)
        {
            WebClient client = new WebClient();
            NameValueCollection parameters = new NameValueCollection();
            usernamefromsp = pref.GetString("Username", String.Empty);
            parameters.Add("username", usernamefromsp);
            parameters.Add("selecteddate", mDatePicker.Year + "-" + (mDatePicker.Month + 1) + "-" + mDatePicker.DayOfMonth);
            client.UploadValuesCompleted += Client1_UploadValuesCompleted;
            client.UploadValuesAsync(url, "POST", parameters);
        }

        private void MDatePicker_update(object sender, EventArgs e)
        {
            WebClient client1 = new WebClient();
            NameValueCollection parameters1 = new NameValueCollection();
            usernamefromsp = pref.GetString("Username", String.Empty);
            parameters1.Add("username", usernamefromsp);
            parameters1.Add("selecteddate", mDatePicker.Year + "-" + (mDatePicker.Month + 1) + "-" + mDatePicker.DayOfMonth);
            client1.UploadValuesCompleted += Client1_UploadValuesCompleted;
            client1.UploadValuesAsync(url, "POST", parameters1);
        }

        private void Client1_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            string json1 = System.Text.Encoding.UTF8.GetString(e.Result);
            mEvents = new List<CalendarEvent>();
            mEvents = JsonConvert.DeserializeObject<List<CalendarEvent>>(json1);
            mAdapter = new CalendarEventListAdapter(this.Activity, Resource.Layout.row_event, mEvents, this.Activity.FragmentManager);
            mListView.Adapter = mAdapter;
            
        }

        
        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            menu.Clear();
            inflater.Inflate(Resource.Menu.actionbar_calendar, menu);
            
            return;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            Android.App.FragmentTransaction transaction = this.Activity.FragmentManager.BeginTransaction();

            switch (item.ItemId)
            {
                case Resource.Id.action_logout:
                    ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
                    ISharedPreferencesEditor edit = pref.Edit();
                    edit.Clear();
                    edit.Apply();
                    Intent intent = new Intent(this.Activity, typeof(LoginRegisterActivity));
                    this.StartActivity(intent);
                    this.Dispose();
                    return true;

                case Resource.Id.addevent:
                    DateTime mDate2 = mDatePicker.DateTime;
                    Console.WriteLine(mDate2);
                    CreateEventDialog createeventdialog = new CreateEventDialog(mDate2);
                    createeventdialog.Show(transaction, "dialog fragment");
                    createeventdialog.eventcreated += MDatePicker_update;
                    return true;

                case Resource.Id.eventinvites:
                    dialog_eventinvites eventinvitedialog = new dialog_eventinvites();
                    eventinvitedialog.Show(transaction, "dialog fragment");
                    return true;

                default:
                return base.OnOptionsItemSelected(item);
            }   
        }

        public void OnSelectedDayChange(CalendarView view, int year, int month, int dayOfMonth)
        {
            WebClient client = new WebClient();
            NameValueCollection parameters = new NameValueCollection();
            usernamefromsp = pref.GetString("Username", String.Empty);
            parameters.Add("username", usernamefromsp);
            parameters.Add("selecteddate", mDatePicker.Year + "-" + (mDatePicker.Month + 1) + "-" + mDatePicker.DayOfMonth);
            client.UploadValuesCompleted += Client1_UploadValuesCompleted;
            client.UploadValuesAsync(url, "POST", parameters);
        }

        public void OnDateChanged(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            WebClient client = new WebClient();
            NameValueCollection parameters = new NameValueCollection();
            usernamefromsp = pref.GetString("Username", String.Empty);
            parameters.Add("username", usernamefromsp);
            parameters.Add("selecteddate", mDatePicker.Year + "-" + (mDatePicker.Month + 1) + "-" + mDatePicker.DayOfMonth);
            client.UploadValuesCompleted += Client1_UploadValuesCompleted;
            client.UploadValuesAsync(url, "POST", parameters);
        }
    }
}

