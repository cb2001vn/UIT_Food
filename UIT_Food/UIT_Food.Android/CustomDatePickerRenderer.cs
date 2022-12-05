using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIT_Food.Droid;
using UIT_Food;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using DatePicker = Xamarin.Forms.DatePicker;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]

namespace UIT_Food.Droid
{
    internal class CustomDatePickerRenderer:DatePickerRenderer
    {
        public CustomDatePickerRenderer(Context context): base(context) { }
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if(Control != null)
            {
                Control.SetBackgroundColor(Android.Graphics.Color.White);
                Control.Text = "Chọn ngày sinh";
            }
        }
    }
}