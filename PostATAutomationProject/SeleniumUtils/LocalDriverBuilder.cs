using System;
using PostATAutomationProject.SeleniumUtils.Wrappers;
using OpenQA.Selenium;
using PostATAutomationProject.SeleniumUtils;

namespace PostATAutomationProject.SeleniumUtils;

public class LocalDriverBuilder
{
    private readonly WebDriverFactory factory;

    public LocalDriverBuilder() : this(new WebDriverFactory())
    {
        
    }


internal LocalDriverBuilder(WebDriverFactory factory)
    {
        this.factory = factory;
    }

    public virtual IWebDriver Launch(string browserTarget, string startingURL)
    {
        var driver = CreateWebDriver(browserTarget);
        driver.Navigate().GoToUrl(startingURL);
        return driver;
    }

    private IWebDriver CreateWebDriver(string browserTarget)
    {
        switch (browserTarget)
        {
            case BrowserTarget.Chrome:
                return factory.CreateLocalChromeDriver();
            case BrowserTarget.Firefox:
                return factory.CreateLocalFirefoxDriver();
            default:
                throw new NotSupportedException($"{browserTarget} is not supported browser type.");
        }
    }
}