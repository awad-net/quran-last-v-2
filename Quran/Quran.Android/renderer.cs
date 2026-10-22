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
using SlideOverKit.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Quran.Anroid;

[assembly: ExportRenderer(typeof(Quran.MainPage), typeof(PageImpInterfaceRendererDroid))]
[assembly: ExportRenderer(typeof(Quran.Salaty), typeof(PageImpInterfaceRendererDroid))]
[assembly: ExportRenderer(typeof(Quran.About), typeof(PageImpInterfaceRendererDroid))]
[assembly: ExportRenderer(typeof(Quran.AzkarPage), typeof(PageImpInterfaceRendererDroid))]
[assembly: ExportRenderer(typeof(Quran.downloaded), typeof(PageImpInterfaceRendererDroid))]
[assembly: ExportRenderer(typeof(Quran.downloadedshioukh), typeof(PageImpInterfaceRendererDroid))]
[assembly: ExportRenderer(typeof(Quran.playdownloaded), typeof(PageImpInterfaceRendererDroid))]
[assembly: ExportRenderer(typeof(Quran.playquran), typeof(PageImpInterfaceRendererDroid))]
[assembly: ExportRenderer(typeof(Quran.qipla), typeof(PageImpInterfaceRendererDroid))]
[assembly: ExportRenderer(typeof(Quran.ReadQuran), typeof(PageImpInterfaceRendererDroid))]
[assembly: ExportRenderer(typeof(Quran.Sebha), typeof(PageImpInterfaceRendererDroid))]
[assembly: ExportRenderer(typeof(Quran.Zikr), typeof(PageImpInterfaceRendererDroid))]

namespace Quran.Anroid
{
    // As your page cannot inherit from MenuContainerPage,
    // You must create a renderer page, copy these codes and rename.
    public class PageImpInterfaceRendererDroid : PageRenderer, ISlideOverKitPageRendererDroid
    {
        public Action<ElementChangedEventArgs<Page>> OnElementChangedEvent { get; set; }

        public Action<bool, int, int, int, int> OnLayoutEvent { get; set; }

        public Action<int, int, int, int> OnSizeChangedEvent { get; set; }

        public PageImpInterfaceRendererDroid(Context context) : base(context)
        {
            new SlideOverKitDroidHandler().Init(this, context);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            OnElementChangedEvent?.Invoke(e);
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            OnLayoutEvent?.Invoke(changed, l, t, r, b);
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            OnSizeChangedEvent?.Invoke(w, h, oldw, oldh);
        }
    }
}