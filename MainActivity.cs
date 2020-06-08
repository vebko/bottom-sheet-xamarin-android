using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Views;

namespace BottomSheetDialogXamarinAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Button btnCustomBottomSheet = FindViewById<Button>(Resource.Id.btnCustomBottomSheet);
            //Button btnDragMe = FindViewById<Button>(Resource.Id.btnDragMe);

            btnCustomBottomSheet.Click += (sender, args) =>
            {
                BottomSheetDialogFragment bottomSheetDialogFragment = new CustomBottomSheet();
                bottomSheetDialogFragment.Show(SupportFragmentManager, bottomSheetDialogFragment.Tag);
            };

            //btnDragMe.Click += (sender, args) =>
            //{
               
            //};

        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}