namespace EverlightTestAutomation.Pages
{
        public abstract class PageObject
        {
            private readonly IWebDriver _driver;
            public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            public PageObject(IWebDriver driver)
            {
               _driver = driver;
            }

            public void Navigate(string url)
            {
               _driver.Navigate().GoToUrl(url);
            }

            public void AssertTitle(string title)
            {
                string pageTitle = _driver.Title;
                pageTitle.Should().Be(title);
            }
        }
}
