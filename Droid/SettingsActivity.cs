﻿
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

namespace Stations.Droid
{
    [Activity(Label = "SettingsActivity", Theme = "@style/MyTheme")]
    public class SettingsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_settings);

			FragmentManager.BeginTransaction().
						   Replace(Resource.Id.container, new SettingsFragment())
						   .Commit();
        }
    }
}
