using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AgeCal.Components
{
    public abstract class BaseTopNavigationView : ContentView
    {
        public static readonly BindableProperty HasBackButtonProperty = BindableProperty.Create(
            nameof(HasBackButton),
            typeof(bool),
            typeof(BaseTopNavigationView),
            null,
            propertyChanged: (bindable, oldV, newV) => ((BaseTopNavigationView)bindable).UpdateHasBackButton((bool)oldV, (bool)newV));
        public bool HasBackButton { get { return (bool)GetValue(HasBackButtonProperty); } set { SetValue(HasBackButtonProperty, value); } }

        protected virtual void UpdateHasBackButton(bool oldV, bool newV)
        {
            HasBackButton = newV;
        }

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(BaseTopNavigationView),
            null,
            propertyChanged: (bindable, oldV, newV) => ((BaseTopNavigationView)bindable).UpdateTitle((string)oldV, (string)newV));
        public string Title { get { return (string)GetValue(TitleProperty); } set { SetValue(TitleProperty, value); } }

        protected virtual void UpdateTitle(string oldV, string newV)
        {
            Title = newV;
        }
        public static readonly BindableProperty SubTitleProperty = BindableProperty.Create(
           nameof(SubTitle),
           typeof(string),
           typeof(BaseTopNavigationView),
           null,
           propertyChanged: (bindable, oldV, newV) => ((BaseTopNavigationView)bindable).UpdateSubTitle((string)oldV, (string)newV));
        public string SubTitle { get { return (string)GetValue(SubTitleProperty); } set { SetValue(SubTitleProperty, value); } }

        protected virtual void UpdateSubTitle(string oldV, string newV)
        {
            SubTitle = newV;
        }


        public static readonly BindableProperty TitleIconProperty = BindableProperty.Create(
            nameof(TitleIcon),
            typeof(string),
            typeof(BaseTopNavigationView),
            null,
            propertyChanged: (bindable, oldV, newV) => ((BaseTopNavigationView)bindable).UpdateTitleIcon((string)oldV, (string)newV));
        public string TitleIcon { get { return (string)GetValue(TitleIconProperty); } set { SetValue(TitleIconProperty, value); } }

        protected virtual void UpdateTitleIcon(string oldV, string newV)
        {
            TitleIcon = newV;
        }
        public static readonly BindableProperty ButtonPressedProperty = BindableProperty.Create(
            nameof(ButtonPressed),
            typeof(ICommand),
            typeof(BaseTopNavigationView),
            null,
            propertyChanged: (bindable, oldV, newV) => ((BaseTopNavigationView)bindable).UpdateButtonPressed((ICommand)oldV, (ICommand)newV));
        public ICommand ButtonPressed { get { return (ICommand)GetValue(ButtonPressedProperty); } set { SetValue(ButtonPressedProperty, value); } }

        protected virtual void UpdateButtonPressed(ICommand oldV, ICommand newV)
        {
            ButtonPressed = newV;
        }
        public virtual void Dispose()
        {

        }
    }
}
