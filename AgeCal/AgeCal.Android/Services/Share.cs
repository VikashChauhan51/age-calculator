using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgeCal.Interfaces;
using Android.App;
using Android.Content;


namespace AgeCal.Droid.Services
{
    public class Share: IShare
    {
        private readonly Context _context;
        public Share()
        {
            _context = Android.App.Application.Context;
        }

        public async Task Show(string title, string message, string filePath)
        {
            var extension = filePath.Substring(filePath.LastIndexOf(".") + 1).ToLower();
            var contentType = string.Empty;

            await Task.Delay(100);
            // You can manually map more ContentTypes here if you want.
            switch (extension)
            {
                case "pdf":
                    contentType = "application/pdf";
                    break;
                case "png":
                    contentType = "image/png";
                    break;
                default:
                    contentType = "application/octetstream";
                    break;
            }

            var intent = new Intent(Intent.ActionSend);
            intent.SetType(contentType);
            intent.PutExtra(Intent.ExtraStream, new Uri("file://" + filePath).AbsolutePath);
            intent.PutExtra(Intent.ExtraText, string.Empty);
            intent.PutExtra(Intent.ExtraSubject, message ?? string.Empty);

            var chooserIntent = Intent.CreateChooser(intent, title ?? string.Empty);
            chooserIntent.SetFlags(ActivityFlags.ClearTop);
            chooserIntent.SetFlags(ActivityFlags.NewTask);
            _context.StartActivity(chooserIntent);
        }

        public async Task Show(string title, string content)
        {

            await Task.Delay(100);
            var intent = new Intent(Intent.ActionSend);
            intent.SetType("application/text");
            intent.PutExtra(Intent.ExtraText, string.Empty);
            intent.PutExtra(Intent.ExtraSubject, content ?? string.Empty);

            var chooserIntent = Intent.CreateChooser(intent, title ?? string.Empty);
            chooserIntent.SetFlags(ActivityFlags.ClearTop);
            chooserIntent.SetFlags(ActivityFlags.NewTask);
            _context.StartActivity(chooserIntent);
        }
    }
}