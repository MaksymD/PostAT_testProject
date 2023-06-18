using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace PostATAutomationProject.SeleniumUtils.Wrappers;

internal class WebDriverFactory
{
    public virtual IWebDriver CreateLocalChromeDriver()
    {
        ChromeOptions options = new ChromeOptions();
        options.SetLoggingPreference(LogType.Browser, LogLevel.All);
        return new ChromeDriver(options);
    }
    
    public virtual IWebDriver CreateLocalFirefoxDriver()
    {
        FirefoxOptions options = new FirefoxOptions();
        options.SetLoggingPreference(LogType.Browser, LogLevel.All);
        return new FirefoxDriver(options);
    }
}