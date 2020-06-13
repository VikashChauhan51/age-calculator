using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AgeCal.Components
{
    public class HybridWebView : View
    {
        Action<string> action;

        public static readonly BindableProperty UriProperty = BindableProperty.Create(
            propertyName: nameof(Uri),
            returnType: typeof(string),
            declaringType: typeof(HybridWebView),
            defaultValue: default(string));

        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }

        public static readonly BindableProperty UrlProperty = BindableProperty.Create(
            propertyName: nameof(Url),
            returnType: typeof(string),
            declaringType: typeof(HybridWebView),
            defaultValue: default(string));

        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
            propertyName: nameof(Source),
            returnType: typeof(string),
            declaringType: typeof(HybridWebView),
            defaultValue: default(string));

        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public static readonly BindableProperty JsFuncProperty = BindableProperty.Create(
            propertyName: nameof(JsFunc),
            returnType: typeof(string),
            declaringType: typeof(HybridWebView),
            defaultValue: default(string));

        public string JsFunc
        {
            get { return (string)GetValue(JsFuncProperty); }
            set
            {
                SetValue(JsFuncProperty, value);
                PendingJsFuncs.Add(JsFunc);
            }
        }

        public List<string> PendingJsFuncs = new List<string>();
        public void RegisterAction(Action<string> callback)
        {
            action = callback;
        }

        public void Cleanup()
        {
            action = null;
        }

        public void InvokeAction(string data)
        {
            if (action == null || data == null)
            {
                return;
            }
            action.Invoke(data);
        }

        public void EvalJsFunc(string jsFunc)
        {
            JsFunc = jsFunc;
        }
    }
}
