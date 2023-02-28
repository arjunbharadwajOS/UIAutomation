using NUnit.Framework.Interfaces;

namespace EverlightTestAutomation.Startup
{
    public enum TestOutcome
    {
        passed,
        failed
    }

    [Binding]
        public class Hooks
        {
            private readonly ScenarioContext _scenarioContext;
            private readonly ConfigurationDriver _configurationDriver;
            private IWebDriver _driver;
            private static bool uiTests = false;
            private AllureLifecycle _allureLifecycle;

            public Hooks(ScenarioContext scenarioContext)
            {
                _scenarioContext = scenarioContext;
                _configurationDriver = new ConfigurationDriver();
                _allureLifecycle = AllureLifecycle.Instance;
            }

            [OneTimeSetUp]
            public void SetupForAllure()
            {
                Environment.CurrentDirectory = Path.GetDirectoryName(GetType().Assembly.Location);
            }

            [StepDefinition(@"Step is '(.*)'")]
            public void StepResultIs(TestOutcome outcome)
            {
                switch (outcome)
                {
                    case TestOutcome.passed:
                        break;
                    case TestOutcome.failed:
                        throw new Exception("This test is failed",
                            new InvalidOperationException("Internal message",
                                new ArgumentException("One more message")));
                    default:
                        throw new ArgumentException("value is not supported");
                }
            }

            [StepDefinition("Step with attachment")]
            public void StepWithAttach()
            {
                var path = Guid.NewGuid().ToString();
                File.WriteAllText(path, "hi there");
                _allureLifecycle.AddAttachment(path);
            }

            [BeforeFeature]
            public static void beforeFeature()
            {  
                if (FeatureContext.Current.FeatureInfo.Title.Contains("UI"))
                {
                   uiTests = true;
                }
                Directory.CreateDirectory(Path.Combine("..", "..", "TestResults"));
            }

            [AfterFeature]
            public static void afterFeature()
            {
                uiTests = false;
            }
                   
            [BeforeScenario(Order = 0)]
            public void BeforeScenario()
            {
                if (uiTests)
                {
                    initializeDriver();
                }
                
            }

            public void initializeDriver()
            {
                string br = _configurationDriver.Browser;
                string browser = Environment.GetEnvironmentVariable("Test_Browser");
                switch (browser)
                {
                    case "Chrome":
                        _driver = new ChromeDriver();
                        break;
                    case "Firefox":
                        _driver = new FirefoxDriver();
                        break;
                    case "Edge":
                        _driver = new EdgeDriver();
                        break;
                    default:
                        _driver = new ChromeDriver();
                        break;
                }

                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                _driver.Manage().Window.Maximize();
                _scenarioContext.ScenarioContainer.RegisterInstanceAs(_driver);
            }


            [AfterScenario]
            public void AfterScenario()
            { 
                 if (uiTests)
                 {
                     if (_scenarioContext.TestError != null)
                     {
                        _driver.TakeScreenshot().SaveAsFile(Path.Combine("..", "..", "TestResults", $"{_scenarioContext.ScenarioInfo.Title}.png"), ScreenshotImageFormat.Png);
                        _allureLifecycle.AddAttachment(Path.Combine("..", "..", "TestResults", $"{_scenarioContext.ScenarioInfo.Title}.png"));
                     }

                     _driver?.Quit();
                 }
            }   

           

    }
    
}
