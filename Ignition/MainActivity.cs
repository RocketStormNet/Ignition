using Android.App;
using Android.Widget;
using Android.OS;
using Plugin.Messaging;
using Android.Telephony;
using Android;
using Android.Content.PM;

namespace Ignition
{
    [Activity(Label = "Ignition", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        const int RequestSmsId = 0;

        readonly string[] PermissionsSms =
        {
          Manifest.Permission.SendSms
        };

        const string permission = Manifest.Permission.SendSms;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            LinearLayout llayout = FindViewById<LinearLayout>(Resource.Id.llout);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate
            {
                // Deprecated
                // SmsManager.Default.SendTextMessage("YOUR_NUMBER", null, "Something!", null, null);

                // Checking SMS Send Android permission
                if (CheckSelfPermission(permission) == (int)Permission.Granted)
                {
                    var smsMessenger = CrossMessaging.Current.SmsMessenger;
                    if (smsMessenger.CanSendSmsInBackground)
                        smsMessenger.SendSmsInBackground("YOUR_NUMBER", "YOUR_TEXT");
                    button.Text = "SMS was sent!";
                } else
                {
                    // Asking for permission if it is not granted
                    RequestPermissions(PermissionsSms, RequestSmsId);
                    Toast.MakeText(this, "SMS permission granted!", ToastLength.Long).Show();
                }
            };
        }
    }
}

