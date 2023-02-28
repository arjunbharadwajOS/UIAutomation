namespace EverlightTestAutomation.Pages
{
    public class CreateNewOrderPage : PageObject
    {
        private readonly IWebDriver _driver;

        public CreateNewOrderPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        public void sendMRNText(string mrn)
        {
            try { 

                mnrElement.SendKeys(mrn);
                log.Info(mnrElement.Text + " text entered in Textfield");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
        }

        public void sendFirstNameText(string firstName)
        {
            try
            {
                firstNameElement.SendKeys(firstName);
                log.Info(firstNameElement.Text + " text entered in Textfield");
            } 
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
}

        public void sendLastNameText(string lastName)
        {
            try { 
                lastNameElement.SendKeys(lastName);
                log.Info(lastNameElement.Text + " text entered in Textfield");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
        }

        public void accessionNumberText(string accessionNumber)
        {
            try { 
                accessionElement.SendKeys(accessionNumber);
                log.Info(accessionElement.Text + " text entered in Textfield");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
        }

        public void clearAccessionNumberText()
        {
            try { 
                accessionElement.Clear();
                log.Info("accessionElement Text is Cleared");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
        }

        public IList<IWebElement> getOrgCodeDropDown()
        {
            try
            {
                SelectElement orgCodeDropDown = new SelectElement(orgCodeElement);
                log.Info(orgCodeDropDown.Options + " Select Dropdown values");
                return orgCodeDropDown.Options;

            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                return null;

            }
        }

        public IList<IWebElement> getSiteDropDown()
        {
            try { 
                SelectElement siteIdDropDown = new SelectElement(siteIdElement);
                log.Info(siteIdDropDown.Options + " Select Dropdown values");
                return siteIdDropDown.Options;
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                return null;

            }
        }

        public IList<IWebElement> getModalityDropDown()
        {
            try
            { 
            SelectElement modalityDropDown = new SelectElement(modalityElement);
            log.Info(modalityDropDown.Options + " Select Dropdown values");
            return modalityDropDown.Options;
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                return null;

            }
        }

        public void selectOrgCodeDropDown(string orgCode)
        {
            try { 
                SelectElement orgCodeDropDown = new SelectElement(orgCodeElement);
                log.Info(orgCodeDropDown.Options + " Dropdown text is selected");
                orgCodeDropDown.SelectByValue(orgCode);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
        }

        public void selectSiteDropDown(int siteIdValue)
        {
            try { 
                SelectElement siteIdDropDown = new SelectElement(siteIdElement);
                log.Info(siteIdDropDown.Options + " Dropdown text is selected");
                siteIdDropDown.SelectByValue(siteIdValue.ToString());
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
}

        public void selectModalityDropDown(string modality)
        {
            try { 
                SelectElement modalityDropDown = new SelectElement(modalityElement);
                log.Info(modalityDropDown.Options + " Dropdown text is selected");
                modalityDropDown.SelectByValue(modality);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
        }

        public void studyDateTimeText(string studyDateTime)
        {
            try { 
                studyDateTimeElement.SendKeys(studyDateTime);
                log.Info(studyDateTimeElement.Text + " text entered in Textfield");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
        }

        public void submitButton()
        {
            try
            {
                submitElement.Submit();
                log.Info(submitElement.Text + " text is Clicked");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
        }

        public IWebElement mnrElement => _driver.FindElement(By.Id("mrn"));

        public IWebElement firstNameElement => _driver.FindElement(By.Id("first-name"));

        public IWebElement lastNameElement => _driver.FindElement(By.Id("last-name"));

        public IWebElement accessionElement => _driver.FindElement(By.Id("accession-number"));

        public IWebElement orgCodeElement => _driver.FindElement(By.XPath("//*[@formcontrolname='orgCode']"));

        public IWebElement siteIdElement => _driver.FindElement(By.Id("site-id"));

        public IWebElement modalityElement => _driver.FindElement(By.Id("modality"));

        public IWebElement studyDateTimeElement => _driver.FindElement(By.Id("study-date-time"));

        public IWebElement submitElement => _driver.FindElement(By.XPath("//*[contains(text(),'Submit')]"));

        public IList<IWebElement> textErrorMsges => _driver.FindElements(By.XPath("//*[@class='text-danger']"));

    }
}
