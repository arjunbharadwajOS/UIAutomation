namespace EverlightTestAutomation.Pages
{
    public class OrdersPage : PageObject
    {
        private readonly IWebDriver _driver;

        private int AccessionNumber = 0, OrgCode = 0, SiteName = 0, PatientMRN = 0, PatientName = 0, Modality = 0, StudyDateTime = 0, Status = 0, initialRowCount = 0;

        public OrdersPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        public void clickCreateNew()
        {
            try
            {
                clickCreateNewElement.Click();
                log.Info(clickCreateNewElement.Text + " is Clicked");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
        }

        public string getOrderPageHeader()
        {
            try
            {
                log.Info(getPageHeader.Text + " is Clicked");
                return getPageHeader.Text;
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                return null;
            }

            
        }    

        public void getColumnHeaderText()
        {
            try
            {
                for (int i = 0; i < columnHeaders.Count; i++)
                {
                    log.Info(columnHeaders[i].Text + " text is returned");

                    if (columnHeaders[i].Text.ToString().Equals("Accession Number", StringComparison.OrdinalIgnoreCase))
                    {
                        AccessionNumber = i;
                    }
                    else if (columnHeaders[i].Text.ToString().Equals("Org Code", StringComparison.OrdinalIgnoreCase))
                    {
                        OrgCode = i;
                    }
                    else if (columnHeaders[i].Text.ToString().Equals("Site Name", StringComparison.OrdinalIgnoreCase))
                    {
                        SiteName = i;
                    }
                    else if (columnHeaders[i].Text.ToString().Equals("Patient MRN", StringComparison.OrdinalIgnoreCase))
                    {
                        PatientMRN = i;
                    }
                    else if (columnHeaders[i].Text.ToString().Equals("Patient Name", StringComparison.OrdinalIgnoreCase))
                    {
                        PatientName = i;
                    }
                    else if (columnHeaders[i].Text.ToString().Equals("Modality", StringComparison.OrdinalIgnoreCase))
                    {
                        Modality = i;
                    }
                    else if (columnHeaders[i].Text.ToString().Equals("Study DateTime", StringComparison.OrdinalIgnoreCase))
                    {
                        StudyDateTime = i;
                    }
                    else if (columnHeaders[i].Text.ToString().Equals("Status", StringComparison.OrdinalIgnoreCase))
                    {
                        Status = i;
                    }
                }
               
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                
            }
           
        }

        public void validateCreatedOrder(string mrn, string firstName, string lastName, string accessionNumber, string orgCode, string siteId, string modality)
        {
            try
            {
                getColumnHeaderText();

                string statusText = string.Empty;

                for (int i = 0; i < tableRowList.Count; i++)
                {
                    log.Info(tableRowList[i].Text + " text is returned");

                    if (tableRowList[i].FindElements(By.TagName("td"))[AccessionNumber].Text.ToString().Equals(accessionNumber))
                    {
                        Assert.IsTrue(true, accessionNumber);
                        statusText = tableRowList[i].FindElements(By.TagName("td"))[Status].Text.ToString();

                        foreach (DictionaryEntry de in StaticModels.Status())
                        {
                            if (de.Key.ToString().Equals(statusText, StringComparison.OrdinalIgnoreCase))
                            {
                                Assert.IsTrue(true, statusText);
                                break;
                            }
                        }

                        foreach (DictionaryEntry de in StaticModels.SiteIdList_Org1())
                        {
                            if (de.Key.Equals(siteId))
                            {
                                try
                                {
                                    Assert.AreEqual(de.Value, tableRowList[i].FindElements(By.TagName("td"))[SiteName].Text.ToString());
                                }
                                catch (Exception)
                                {
                                    Assert.IsTrue(true, tableRowList[i].FindElements(By.TagName("td"))[SiteName].Text.ToString());
                                }
                                break;
                            }
                        }

                        foreach (DictionaryEntry de in StaticModels.OrgCodeList())
                        {
                            if (de.Key.Equals(orgCode))
                            {
                                Assert.AreEqual(de.Key, tableRowList[i].FindElements(By.TagName("td"))[OrgCode].Text.ToString());
                                break;
                            }
                        }

                        Assert.AreEqual(tableRowList[i].FindElements(By.TagName("td"))[PatientName].Text.ToString(), firstName + " " + lastName);
                    
                        Assert.AreEqual(tableRowList[i].FindElements(By.TagName("td"))[PatientMRN].Text.ToString(), mrn);
                    
                        Assert.AreEqual(tableRowList[i].FindElements(By.TagName("td"))[Modality].Text.ToString(),modality);
                    }
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);

            }
        }

        public void verifyOrdersFromTable()
        {
            try
            {
                getColumnHeaderText();

                initialRowCount = tableRowList.Count;

                Assert.Greater(initialRowCount, 0);
                log.Info("Order table has records " + initialRowCount);

                for (int i = 0; i < tableRowList.Count; i++)
                {
                    Assert.IsNotNull(tableRowList[i].FindElements(By.TagName("td"))[AccessionNumber].Text.ToString());
                    Assert.IsNotNull(tableRowList[i].FindElements(By.TagName("td"))[Status].Text.ToString());
                    Assert.IsNotNull(tableRowList[i].FindElements(By.TagName("td"))[OrgCode].Text.ToString());
                    Assert.IsNotNull(tableRowList[i].FindElements(By.TagName("td"))[SiteName].Text.ToString());
                    Assert.IsNotNull(tableRowList[i].FindElements(By.TagName("td"))[PatientName].Text.ToString());
                    Assert.IsNotNull(tableRowList[i].FindElements(By.TagName("td"))[PatientMRN].Text.ToString());
                    Assert.IsNotNull(tableRowList[i].FindElements(By.TagName("td"))[StudyDateTime].Text.ToString());
                    Assert.IsNotNull(tableRowList[i].FindElements(By.TagName("td"))[Modality].Text.ToString());
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);

            }
        }

        public void deleteExistingOrder()
        {
            bool deleteSuccessful = false;

            try
            {
                log.Info("Order table has records " + initialRowCount);

                if (tableRowList.Count > 0)
                {
                   _driver.Navigate().Refresh();

                   deleteButtonList[0].Click();

                   Thread.Sleep(5000);

                   Assert.IsTrue(true, _driver.SwitchTo().Alert().Text.ToString()); ;

                   _driver.SwitchTo().Alert().Accept();

                   deleteSuccessful = true;

                   log.Info("Delete record is successful " + deleteSuccessful);

                   Thread.Sleep(5000);
                }

                if (deleteSuccessful)
                {
                    Assert.IsTrue(deleteSuccessful, "Delete Order is successful");
                    Assert.Greater(initialRowCount, tableRowList.Count);
                }
                else
                {
                    Assert.IsTrue(deleteSuccessful, "Delete Order is unsuccessful");
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);

            }
}

        public void deleteCreatedOrder(string mrn, string accessionNumber)
        {
            bool deleteSuccessful = true;

            try
            {
                log.Info("Order table has records " + initialRowCount);

                for (int i = 0; i < tableRowList.Count; i++)
                {
                    if (tableRowList[i].FindElements(By.TagName("td"))[AccessionNumber].Text.ToString().Equals(accessionNumber))
                    {
                        _driver.Navigate().Refresh();

                        deleteButtonList[i].Click();

                        Thread.Sleep(5000);

                        Assert.IsTrue(true, _driver.SwitchTo().Alert().Text.ToString()); ;

                        _driver.SwitchTo().Alert().Accept();

                        log.Info("Delete record is successful " + deleteSuccessful);

                        Thread.Sleep(5000);
                    }
                }

                for (int i = 0; i < tableRowList.Count; i++)
                {
                    if (tableRowList[i].FindElements(By.TagName("td"))[AccessionNumber].Text.ToString().Equals(accessionNumber))
                    {
                        deleteSuccessful = false;
                        log.Error("Delete record is unsuccessful " + deleteSuccessful);
                    }
                }

                if (deleteSuccessful)
                {
                    Assert.IsTrue(deleteSuccessful, "Delete Order is successful");
                }
                else
                {
                    Assert.IsTrue(deleteSuccessful, "Delete Order is unsuccessful");
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);

            }
        }
        
        public IWebElement clickCreateNewElement => _driver.FindElement(By.XPath("//*[contains(text(),'Create New')]"));

        public IWebElement getPageHeader => _driver.FindElement(By.Id("tableLabel"));

        public IList<IWebElement> deleteButtonList => _driver.FindElements(By.XPath("//*[contains(@class,'cursor-pointer')]"));

        public IList<IWebElement> columnHeaders => _driver.FindElements(By.XPath("//table[@aria-labelledby='tableLabel']//th"));

        public IList<IWebElement> tableRowList => _driver.FindElements(By.XPath("//table[@aria-labelledby='tableLabel']/tbody/tr"));


    }
}
