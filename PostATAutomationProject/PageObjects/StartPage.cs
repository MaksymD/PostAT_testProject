using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace PostATAutomationProject.PageObjects;

class StartPage
{
    IWebDriver driver;
    WebDriverWait wait;

    public StartPage(IWebDriver driver)
    {
        this.driver = driver;
        this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
    }

    IWebElement SearchField
    {
        get { return driver.FindElement(By.XPath("(//input[@name = 'suche'])[2]")); }
    }

    public void EnterSerchText(string searchTerm)
    {
        wait.Until(x => SearchField).SendKeys(searchTerm);
        SearchField.SendKeys(Keys.Return);
    }
}
