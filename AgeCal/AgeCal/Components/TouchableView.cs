using AgeCal.Effects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AgeCal.Components
{
    public class TouchableView : ContentView
    {
        protected TouchEffect effect;
        public event EventHandler Clicked;
        ~TouchableView()
        {
            if (effect != null)
            {
                effect.TouchAction -= TouchActionHandler;
            }
        }

        public static readonly BindableProperty ClickedProperty = BindableProperty.Create(
           nameof(ClickCommand),
           typeof(ICommand),
           typeof(TouchableView),
           null,
           propertyChanged: (bindable, oldV, newV) => ((TouchableView)bindable).UpdateButtonPressed((ICommand)oldV, (ICommand)newV));
        public ICommand ClickCommand { get { return (ICommand)GetValue(ClickedProperty); } set { SetValue(ClickedProperty, value); } }

        protected virtual void UpdateButtonPressed(ICommand oldV, ICommand newV)
        {
            ClickCommand = newV;
        }
        public static readonly BindableProperty ClickedParameterProperty = BindableProperty.Create(
           nameof(ClickedParameter),
           typeof(object),
           typeof(TouchableView),
           null,
           propertyChanged: (bindable, oldV, newV) => ((TouchableView)bindable).UpdateClickedParameter(oldV, newV));
        public object ClickedParameter { get { return GetValue(ClickedProperty); } set { SetValue(ClickedProperty, value); } }

        protected virtual void UpdateClickedParameter(object oldV, object newV)
        {
            ClickedParameter = newV;
        }
        private Layout _touchableLayout;
        protected Layout TouchableLayout
        {
            get { return _touchableLayout; }
            set
            {

                _touchableLayout = value;
                _touchableLayout.Effects.Clear();

                if (effect == null)
                {
                    effect = new TouchEffect() { Capture = true };
                    effect.TouchAction += TouchActionHandler;
                }
                _touchableLayout.Effects.Add(effect);
            }
        }

        public virtual void TouchActionHandler(object sender, TouchActionEventArgs args)
        {
            switch (args.Type)
            {
                case TouchActionType.Entered:
                    Device.BeginInvokeOnMainThread(() => this.Opacity = 1.0);
                    break;
                case TouchActionType.Pressed:
                    Device.BeginInvokeOnMainThread(() => this.Opacity = 0.75);
                    break;
                case TouchActionType.Moved:
                    Device.BeginInvokeOnMainThread(() => this.Opacity = 1.0);
                    break;
                case TouchActionType.Released:
                    Device.BeginInvokeOnMainThread(() => this.Opacity = 1.0);
                    Clicked?.Invoke(this, args);
                    ClickCommand?.Execute(ClickedParameter ?? BindingContext);
                    break;
                case TouchActionType.Exited:
                    Device.BeginInvokeOnMainThread(() => this.Opacity = 1.0);
                    break;
                case TouchActionType.Cancelled:
                    Device.BeginInvokeOnMainThread(() => this.Opacity = 1.0);
                    break;

            }
        }

    }
}
