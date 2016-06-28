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
using Android.Support.V4.App;
using Android.Support.V4.View;
using System.Net;
using System.Collections.Specialized;

namespace LoginSystem
{
    [Activity(Label = "Morris EC")]
    public class Activity1 : FragmentActivity, CalendarView.IOnDateChangeListener
    {
        ViewPager _viewPager;
        JavaList<Android.Support.V4.App.Fragment> fragments;
        int pYear, PMonth, PDay;
        
    protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ViewPager);
            _viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            _viewPager.Adapter = new LayoutFragmentadapter(SupportFragmentManager, getfragments());
            _viewPager.SetCurrentItem(1, false);
        }
       

    private JavaList<Android.Support.V4.App.Fragment> getfragments()
        {
            fragments = new JavaList<Android.Support.V4.App.Fragment>();
            fragments.Add(new FriendsActivity());
            fragments.Add(new CalendarActivity());
            fragments.Add(new EventActivity());
            return fragments;
        }

        public void OnSelectedDayChange(CalendarView view, int year, int month, int dayOfMonth)
        {
        }
    }
    
    public class LayoutFragmentadapter : FragmentPagerAdapter
    {
        JavaList<Android.Support.V4.App.Fragment> mJavalist;

        public LayoutFragmentadapter(Android.Support.V4.App.FragmentManager fm, JavaList<Android.Support.V4.App.Fragment> mjavalist) : base(fm)
        {
            mJavalist = mjavalist;
        }
        public override int Count
        {
            get
            {
                return 3;
            }
        }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return mJavalist[position];
        }
    }
        
    
}