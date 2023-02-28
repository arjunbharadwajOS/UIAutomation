namespace EverlightTestAutomation.Pages
{
    public class HomePage : PageObject
    {
        private readonly IWebDriver _driver;
        
        public HomePage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        public void clickHyperlink(string hyperlinkText)
        {
            try
            {
                for (int i = 0; i < hyperLinkElements.Count; i++)
                {
                    if (hyperLinkElements[i].Text.Equals(hyperlinkText, StringComparison.OrdinalIgnoreCase))
                    {
                        hyperLinkElements[i].Click();
                        break;
                    }
                }
                log.Info(hyperlinkText + " is Clicked");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
            
        }

        public IList<IWebElement> hyperLinkElements => _driver.FindElements(By.TagName("a"));



    }
}
