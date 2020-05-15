using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AgeCal.Components
{
    public class PillFrame : Frame
    {
        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            var baseMuserment = base.OnMeasure(widthConstraint, heightConstraint);
            if (baseMuserment.Request.Height > 0)
            {
                float cornerRadius = (float)(baseMuserment.Request.Height / 2);
                this.CornerRadius = cornerRadius;
                float minPadding = cornerRadius / 2;
                if (this.Padding.Left < minPadding || this.Padding.Right < minPadding)
                {
                    this.Padding = new Thickness()
                    {
                        Left = Math.Max(this.Padding.Left, minPadding),
                        Right = Math.Max(this.Padding.Right, minPadding),
                        Top = this.Padding.Top,
                        Bottom = this.Padding.Bottom

                    };
                }
                this.HasShadow = true;
                this.BackgroundColor = Color.Black;
                this.Opacity = 0.3;


            }
            return baseMuserment;
        }
    }
}
