using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Core
{
    public enum AgeNavigationType { NONE, BACK, CLOSE }
    public interface IAgeNavigationService : INavigationService
    {
        List<Type> RootPages { get; }
        void NavigateToWeb(string url);
        void RegisterPage<TViewModel, TPage>() where TViewModel : class;
        string GetKey(Type page);
        string GetKey<TViewModel>();
        void RemovePage<TViewModel>();
        void RemovePage(string pageKey);
        void NavigateTo<TViewModel>(object parameters);
        void NavigateTo<TViewModel>();
        void BackToRoot();
    }
}
