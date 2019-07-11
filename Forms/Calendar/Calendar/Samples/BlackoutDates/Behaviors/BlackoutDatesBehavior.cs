#region Copyright Syncfusion Inc. 2001-2019.
// Copyright Syncfusion Inc. 2001-2019. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.ObjectModel;
using SampleBrowser.Core;
using Syncfusion.SfCalendar.XForms;
using Xamarin.Forms;

namespace SampleBrowser.SfCalendar
{
    internal class BlackoutDatesBehavior : Behavior<SampleView>
    {
        private Syncfusion.SfCalendar.XForms.SfCalendar calendar;

        protected override void OnAttachedTo(SampleView bindable)
        {
            base.OnAttachedTo(bindable);
            this.calendar = bindable.Content.FindByName<Syncfusion.SfCalendar.XForms.SfCalendar>("calendar");
            MonthViewSettings monthSettings = new MonthViewSettings();
            if (Device.RuntimePlatform == "Android")
            {
                if (Device.Idiom == TargetIdiom.Tablet)
                {
                    monthSettings.SelectionRadius = 25;
                }
                else
                {
                    monthSettings.SelectionRadius = 20;
                }
            }
            else if (Device.RuntimePlatform == "iOS")
            {
                monthSettings.SelectionRadius = 15;
            }

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                this.calendar.MonthViewSettings.SelectionRadius = 20;
            }

            monthSettings.HeaderBackgroundColor = Color.White;
            monthSettings.InlineBackgroundColor = Color.FromHex("#EEEEEE");
            monthSettings.DateSelectionColor = Color.FromHex("#EEEEEE");
            monthSettings.TodayTextColor = Color.FromHex("#2196F3");
            monthSettings.SelectedDayTextColor = Color.Black;
            this.calendar.MonthViewSettings = monthSettings;
            if (Device.RuntimePlatform == "Android" || (Device.RuntimePlatform == "UWP" && Device.Idiom == TargetIdiom.Desktop))
            {
                this.calendar.HeaderHeight = 50;
            }
            else if (Device.RuntimePlatform == "iOS")
            {
                this.calendar.HeaderHeight = 40;
            }

            this.calendar.OnMonthCellLoaded += Calendar_OnMonthCellLoaded;
        }

        private void Calendar_OnMonthCellLoaded(object sender, MonthCellLoadedEventArgs e)
        {
            if (e.Date.DayOfWeek == DayOfWeek.Sunday || e.Date.DayOfWeek == DayOfWeek.Saturday)
            {
                var blackoutDates = new ObservableCollection<DateTime>();
                if (this.calendar.BlackoutDates != null)
                {
                    blackoutDates = (ObservableCollection<DateTime>)this.calendar.BlackoutDates;
                }

                blackoutDates.Add(e.Date);
                this.calendar.BlackoutDates = blackoutDates;
            }
        }

        protected override void OnDetachingFrom(SampleView bindable)
        {
            base.OnDetachingFrom(bindable);
            calendar = null;
        }
    }
}

