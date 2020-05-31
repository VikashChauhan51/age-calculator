using NUnit.Framework;
using System.Collections;
using Xamarin.UITest;

namespace AgeCal.UITest.Utilities.TestData
{
    public class AppFixtureData
    {
        public static IEnumerable TestParms
        {
            get
            {
                yield return new TestFixtureData(Platform.Android);
                yield return new TestFixtureData(Platform.iOS);
            }
        }
    }
}
