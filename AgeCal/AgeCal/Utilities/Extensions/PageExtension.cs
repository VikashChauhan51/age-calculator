using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AgeCal
{
    public static class PageExtension
    {

        public static TElement FindElementByName<TElement>(this Page page, string name) where TElement : Element
        {
            var container = page as IPageController;
            return FindElementByName<TElement>(container.InternalChildren, name);
        }
        public static TElement FindElementByName<TElement>(this Layout<View> layout, string name) where TElement : Element
        {
            return FindElementByName<TElement>(layout.Children, name);
        }
        private static TElement FindElementByName<TElement>(this IEnumerable<Element> list, string name) where TElement : Element
        {
            if (list == null)
                return null;

            foreach (var child in list)
            {
                try
                {
                    var result = child.FindByName<TElement>(name);
                    if (result != null)
                        return result;
                }
                catch
                {


                }
            }

            return null;
        }
    }
}
