using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using AgeCal.Components;
using AgeCal.Droid.CustomRenderer;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Java.Interop;
using Java.Lang;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace AgeCal.Droid.CustomRenderer
{

    public class HybridWebViewRenderer : ViewRenderer<HybridWebView, Android.Webkit.WebView>
    {
        const string JavascriptFunction = @"function invokeCSharpAction(data){
jsBridge.invokeAction(data);

}

function __parseAnchors__(){

var anchors=document.getElementByTagName('a');
for (var i=0; i<anchors.length;i++){

var a=anchors[i];
a.addEventListener('click',function(e){
try{

var href=e.target.href;
e.preventDefault();
var actionJson={'action':'any','value':href};
invokeCSharpAction(JSON.stringify(actionJson));
}
catch(e){}
});
}
}
";
        Context _context;
        JavascriptWebViewClient _jsWebViewClient;
        public HybridWebViewRenderer(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    var hybridWebView = (HybridWebView)sender;
                    switch (e.PropertyName)
                    {
                        case nameof(HybridWebView.JsFunc):
                            if (!string.IsNullOrEmpty(hybridWebView.JsFunc))
                            {
                                if (_jsWebViewClient.IsLoading)
                                {
                                    _jsWebViewClient.hybridWebView = hybridWebView;
                                }
                                else
                                {
                                    if (hybridWebView.PendingJsFuncs != null)
                                    {
                                        foreach (var jsFunc in hybridWebView.PendingJsFuncs)
                                        {
                                            Control.EvaluateJavascript($"javascript:{jsFunc}", null);
                                        }
                                        hybridWebView.PendingJsFuncs.Clear();
                                    }
                                }
                            }
                            break;
                        case nameof(HybridWebView.Uri):
                            if (!string.IsNullOrEmpty(hybridWebView.Uri))
                            {
                                _jsWebViewClient.IsLoading = true;
                                Control.LoadUrl($"file:///android_asset/{hybridWebView.Uri}");
                            }
                            break;
                        case nameof(HybridWebView.Url):
                            if (!string.IsNullOrEmpty(hybridWebView.Url))
                            {
                                _jsWebViewClient.IsLoading = true;
                                Control.LoadUrl(hybridWebView.Url);
                            }
                            break;
                        case nameof(HybridWebView.Source):
                            if (!string.IsNullOrEmpty(hybridWebView.Source))
                            {
                                _jsWebViewClient.IsLoading = true;
                                Control.LoadDataWithBaseURL($"file:///android_asset/", hybridWebView.Source, "text/html", "utf-8", "about:blank");
                            }
                            break;
                    }
                }
                catch
                {

                }

            });
        }
        protected override void OnElementChanged(ElementChangedEventArgs<HybridWebView> e)
        {
            base.OnElementChanged(e);

            if (Control==null)
            {
                var webView = new Android.Webkit.WebView(_context);
                webView.Settings.JavaScriptEnabled = true;
                _jsWebViewClient = new JavascriptWebViewClient($"javascript: {JavascriptFunction}");
                webView.SetWebViewClient(_jsWebViewClient);
                SetNativeControl(webView);
            }
            if (e.OldElement != null)
            {
                Control.RemoveJavascriptInterface("jsBridge");
                var hybridWebView = e.OldElement as HybridWebView;
                hybridWebView?.Cleanup();
            }
            if (e.NewElement != null)
            {
                Control.AddJavascriptInterface(new JSBridge(this), "jsBridge");
                if (!string.IsNullOrEmpty(e.NewElement.Uri))
                {
                    Control.LoadUrl($"file:///android_asset/{e.NewElement.Uri}");
                }
                else if (!string.IsNullOrEmpty(e.NewElement.Url))
                {
                    Control.LoadUrl(e.NewElement.Url);
                }
                else if (!string.IsNullOrEmpty(e.NewElement.Source))
                {
                    Control.LoadDataWithBaseURL($"file:///android_asset/", e.NewElement.Source, "text/html", "utf-8", "about:blank");
                }
                
            }
        }

        protected override void Dispose(bool disposing)
        {
             
            base.Dispose(disposing);
        }
    }
    public class JavascriptWebViewClient : WebViewClient
    {
        string _javascript;
        public bool IsLoading { get; set; }
        public HybridWebView hybridWebView;

        public JavascriptWebViewClient(string javascript)
        {
            _javascript = javascript;
            IsLoading = true;
        }

        public override void OnPageStarted(Android.Webkit.WebView view, string url, Bitmap favicon)
        {
            IsLoading = true;
            base.OnPageStarted(view, url, favicon);
        }

        public override void OnPageFinished(Android.Webkit.WebView view, string url)
        {
            base.OnPageFinished(view, url);
            view.EvaluateJavascript(_javascript, null);
            view.EvaluateJavascript("javascript:__parseAnchors__();", null);
            if (hybridWebView != null && hybridWebView.PendingJsFuncs != null && hybridWebView.PendingJsFuncs.Count > 0)
            {
                foreach (var jsFunc in hybridWebView.PendingJsFuncs)
                {
                    view.EvaluateJavascript($"javascript:{jsFunc}", null);
                }
                hybridWebView.PendingJsFuncs.Clear();
            }
            IsLoading = false;
        }


    }
 

    public class JSBridge : Java.Lang.Object
    {
        readonly WeakReference<HybridWebViewRenderer> hybridWebViewRenderer;

        public JSBridge(HybridWebViewRenderer hybridRenderer)
        {
            hybridWebViewRenderer = new WeakReference<HybridWebViewRenderer>(hybridRenderer);
        }

        [JavascriptInterface]
        [Export("invokeAction")]
        public void InvokeAction(string data)
        {
            HybridWebViewRenderer hybridRenderer;

            if (hybridWebViewRenderer != null && hybridWebViewRenderer.TryGetTarget(out hybridRenderer))
            {
                hybridRenderer.Element.InvokeAction(data);
            }
        }
    }
}