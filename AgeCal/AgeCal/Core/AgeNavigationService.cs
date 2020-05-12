using AgeCal.ViewModels;
using AgeCal.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AgeCal.Core
{

    public class AgeNavigationService : IAgeNavigationService
    {
        private Dictionary<string, Type> _pages = new Dictionary<string, Type>();
        private NavigationPage _mainPage;
        private string _currentPageKey;
        public List<Type> RootPages => new List<Type>
        {
            typeof(MainPage)
        };

        public string CurrentPageKey => _currentPageKey;

        public INavigation Navigation => this._mainPage.CurrentPage.Navigation;

        public AgeNavigationService(NavigationPage page)
        {
            this._mainPage = page;
        }

        public string GetKey(Type page) => page.ToString();

        public string GetKey<TViewModel>() => typeof(TViewModel).ToString();
        public void BackToRoot()
        {
            Device.BeginInvokeOnMainThread(() =>
            {

                Navigation.PopToRootAsync();

            });
        }
        public void GoBack()
        {
            Device.BeginInvokeOnMainThread(() =>
            {

                Navigation.PopAsync();

            });
        }

        public void NavigateTo<TViewModel>() => GoTo(GetKey<TViewModel>(), null);
        public void NavigateTo<TViewModel>(object parameters) => GoTo(GetKey<TViewModel>(), parameters);

        public void NavigateTo(string pageKey) => GoTo(pageKey, null);

        public void NavigateTo(string pageKey, object parameter) => GoTo(pageKey, parameter);

        public void NavigateToWeb(string url) => Device.OpenUri(new Uri(url));

        public void RegisterPage<TViewModel, TPage>() where TViewModel : class => RegisterPage(GetKey<TViewModel>(), typeof(TPage));

        public void RemovePage<TViewModel>() => RemovePage(GetKey<TViewModel>());

        public void RemovePage(string pageKey)
        {
            if (_pages.ContainsKey(pageKey))
            {
                Device.BeginInvokeOnMainThread(() =>
                {

                    Type t = _pages[pageKey];
                    if (Navigation.NavigationStack != null)
                    {
                        foreach (Page page in Navigation.NavigationStack)
                        {
                            if (page.GetType() == t && page != _mainPage.CurrentPage)
                            {
                                try
                                {
                                    Navigation.RemovePage(page);
                                    break;
                                }
                                catch (Exception e)
                                {


                                }
                            }
                        }
                    }
                });
            }
        }

        public void RegisterPage(string key, Type page)
        {
            if (!_pages.ContainsKey(key))
                _pages.Add(key, page);
        }
        private void GoTo(string pageKey, object parameters)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var destination = await CreatePageInstance(pageKey);
                Page page = destination.Item1;
                if (destination.Item2 != null)
                    parameters = destination.Item2;
                Type pageType = page?.GetType();
                if (page != null)
                {
                    if (_mainPage.CurrentPage == null || _mainPage.CurrentPage.GetType() != pageType)
                    {
                        if (page is AgeContentPage && ((AgeContentPage)page).ViewModel != null)
                        {
                            //
                        }
                        if (RootPages.Contains(pageType))
                        {
                            var navigationStack = Navigation.NavigationStack;
                            Navigation.InsertPageBefore(page, navigationStack[0]);
                            await Navigation.PopToRootAsync(false);
                        }
                        else
                        {
                            await Navigation.PushAsync(page);
                        }
                    }
                }
            });
        }

        private async Task<Tuple<Page, object>> CreatePageInstance(string pageKey)
        {

            Page page = null;
            object parms = null;
            if (_pages.ContainsKey(pageKey))
            {
                page = (Page)Activator.CreateInstance(_pages[pageKey]);

                BaseViewModel pageViewModel = ((dynamic)page).ViewModel;
                if (pageViewModel != null)
                {
                    var reRoute = await pageViewModel.ShouldReRoute();
                    if (!string.IsNullOrEmpty(reRoute.Item1) && _pages.ContainsKey(reRoute.Item1))
                    {
                        page = (Page)Activator.CreateInstance(_pages[reRoute.Item1]);
                        parms = reRoute.Item2;
                    }

                }
            }
            return new Tuple<Page, object>(page, parms);
        }



    }
}
