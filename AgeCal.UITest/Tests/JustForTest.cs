using AgeCal.UITest.Utilities.TestData;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;

namespace AgeCal.UITest.Tests
{
    //[TestFixtureSource(typeof(AppFixtureData), "TestParms")]
    [TestFixture(Platform.Android)]
    public class JustForTest : BaseTest
    {
        public JustForTest(Platform platform) : base(platform)
        {

        }

        [SetUp]
        public void BeforeEachTest()
        {
            //start app
            StartApp(Xamarin.UITest.Configuration.AppDataMode.Clear);
        }

        [TearDown]
        public void AfterEachTest()
        {
             
        }

        [Test]
        public void Repl()
        {
            app.Repl();
        }
    }
}
