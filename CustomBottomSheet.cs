using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using BottomSheetDialogXamarinAndroid.SRC.EventHandlers;

namespace BottomSheetDialogXamarinAndroid
{
    class CustomBottomSheet : BottomSheetDialogFragment
    {
        private TextView mOffsetText;
        private TextView mStateText;


        private LinearLayoutManager mLinearLayoutManager;

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            Dialog dialog = base.OnCreateDialog(savedInstanceState);
            if (Build.VERSION.SdkInt >= BuildVersionCodes.OMr1)
            {
                setWhiteNavigationBar(dialog);
            }
            return dialog;
        }

        private void setWhiteNavigationBar(Dialog dialog)
        {
            Window window = dialog.Window;
            if (window != null)
            {
                DisplayMetrics metrics = new DisplayMetrics();
                window.WindowManager.DefaultDisplay.GetMetrics(metrics);

                GradientDrawable dimDrawable = new GradientDrawable();
                // ...customize your dim effect here

                GradientDrawable navigationBarDrawable = new GradientDrawable();
                //navigationBarDrawable.Shape = GradientDrawable.Rectangle;
                navigationBarDrawable.SetColor(Color.White);

                Drawable[] layers = { dimDrawable, navigationBarDrawable };

                LayerDrawable windowBackground = new LayerDrawable(layers);
                windowBackground.SetLayerInsetTop(1, metrics.HeightPixels);

                window.SetBackgroundDrawable(windowBackground);
            }
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            //base.OnViewCreated(view, savedInstanceState);
        }

        public override void SetupDialog(Dialog dialog, int style)
        {
            base.SetupDialog(dialog, style);

            View contentView = View.Inflate(Context, Resource.Layout.custom_bottom_sheet, null);
            dialog.SetContentView(contentView);

            CoordinatorLayout.LayoutParams layoutParams = (CoordinatorLayout.LayoutParams)((View)contentView.Parent).LayoutParameters;

            CoordinatorLayout.Behavior behavior = layoutParams.Behavior;

            if (behavior != null)
            {
                var bttomCallback = new BottomSheetBehaviorOverride(SetOffsetText, SetStateText);

                ((BottomSheetBehavior)behavior).SetBottomSheetCallback(bttomCallback);
            }

            //mOffsetText = contentView.FindViewById<TextView>(Resource.Id.offsetText);
            //mStateText = contentView.FindViewById<TextView>(Resource.Id.stateText);
        }

        private void SetOffsetText(object sender, OnSlideEventArgs args)
        {
            // may need to do something with runable
            //mOffsetText.Text = GetString("offseeet", args.SlideOffset);
        }

        private void SetStateText(object sender, OnStateChangeEventArgs args)
        {
            //mStateText.SetText(MainActivity.GetStateAsString(args.NewState));
            if (args.NewState == BottomSheetBehavior.StateHidden)
            {
                Dismiss();
            }
        }

        private class RunnableAnonymousInnerClassHelper : Java.Lang.Object, Java.Lang.IRunnable
        {
            private readonly Context outerInstance;
            private AnimationDrawable animDrawable;

            public RunnableAnonymousInnerClassHelper(Context outerInstance, AnimationDrawable animDrawable)
            {
                this.outerInstance = outerInstance;
                this.animDrawable = animDrawable;
            }

            public void Run()
            {
                animDrawable.Start();
            }
        }

        public class BottomSheetBehaviorOverride : BottomSheetBehavior.BottomSheetCallback
        {
            private Action<object, OnSlideEventArgs> setOffsetText;
            private Action<object, OnStateChangeEventArgs> setStateText;

            public BottomSheetBehaviorOverride(Action<object, OnSlideEventArgs> setOffsetText, Action<object, OnStateChangeEventArgs> setStateText)
            {
                this.setOffsetText = setOffsetText;
                this.setStateText = setStateText;
            }

            public override void OnSlide(View bottomSheet, float slideOffset)
            {
                setOffsetText.Invoke(bottomSheet, new OnSlideEventArgs(slideOffset));
            }

            public override void OnStateChanged(View bottomSheet, int newState)
            {
                setStateText.Invoke(bottomSheet, new OnStateChangeEventArgs(newState));
            }
        }
    }
}