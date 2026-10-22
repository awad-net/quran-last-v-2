using System;
using Xamarin.Forms;
using ZadElMuslim;
using ZadElMuslim.iOS;
using Xamarin.Forms.Platform.iOS;
using SlideOverKit.iOS;
using UIKit;
using CoreGraphics;
using Quran;
[assembly: ExportRenderer(typeof(MainPage), typeof(PageImpInterfaceRendereiOS))]
[assembly: ExportRenderer(typeof(Salaty), typeof(PageImpInterfaceRendereiOS))]
[assembly: ExportRenderer(typeof(AzkarPage), typeof(PageImpInterfaceRendereiOS))]
[assembly: ExportRenderer(typeof(About), typeof(PageImpInterfaceRendereiOS))]
[assembly: ExportRenderer(typeof(downloaded), typeof(PageImpInterfaceRendereiOS))]
[assembly: ExportRenderer(typeof(downloadedshioukh), typeof(PageImpInterfaceRendereiOS))]
[assembly: ExportRenderer(typeof(playdownloaded), typeof(PageImpInterfaceRendereiOS))]
[assembly: ExportRenderer(typeof(playquran), typeof(PageImpInterfaceRendereiOS))]
[assembly: ExportRenderer(typeof(qipla), typeof(PageImpInterfaceRendereiOS))]
[assembly: ExportRenderer(typeof(ReadQuran), typeof(PageImpInterfaceRendereiOS))]
[assembly: ExportRenderer(typeof(Salaty), typeof(PageImpInterfaceRendereiOS))]
[assembly: ExportRenderer(typeof(Sebha), typeof(PageImpInterfaceRendereiOS))]
[assembly: ExportRenderer(typeof(Zikr), typeof(PageImpInterfaceRendereiOS))]
namespace ZadElMuslim.iOS
{
    // As your page cannot inherit from MenuContainerPage,
    // You must create a renderer page, copy these codes and rename.
    public class PageImpInterfaceRendereiOS : PageRenderer, ISlideOverKitPageRendereriOS
    {
        public Action<bool> ViewDidAppearEvent { get; set; }

        public Action<VisualElementChangedEventArgs> OnElementChangedEvent { get; set; }

        public Action ViewDidLayoutSubviewsEvent { get; set; }

        public Action<bool> ViewDidDisappearEvent { get; set; }

        public Action<CGSize, IUIViewControllerTransitionCoordinator> ViewWillTransitionToSizeEvent { get; set; }

        public PageImpInterfaceRendereiOS()
        {
            new SlideOverKitiOSHandler().Init(this);
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (OnElementChangedEvent != null)
                OnElementChangedEvent(e);
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            if (ViewDidLayoutSubviewsEvent != null)
                ViewDidLayoutSubviewsEvent();

        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            if (ViewDidAppearEvent != null)
                ViewDidAppearEvent(animated);

        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            if (ViewDidDisappearEvent != null)
                ViewDidDisappearEvent(animated);
        }

        public override void ViewWillTransitionToSize(CGSize toSize, IUIViewControllerTransitionCoordinator coordinator)
        {
            base.ViewWillTransitionToSize(toSize, coordinator);
            if (ViewWillTransitionToSizeEvent != null)
                ViewWillTransitionToSizeEvent(toSize, coordinator);
        }
    }
}