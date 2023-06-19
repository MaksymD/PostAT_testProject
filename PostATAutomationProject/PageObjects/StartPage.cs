using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace PostATAutomationProject.PageObjects;

class StartPage
{
    IWebDriver driver;
    WebDriverWait wait;

    // Locators
    private readonly By inputSearch_locator = By.XPath("(//input[@name = 'suche'])[2]");
    private readonly By buttonAcceptCookies_locator = By.XPath("//button[@id = 'onetrust-accept-btn-handler']");
    private readonly By buttonLogin_locator =
        By.XPath("(//span[contains(text(), 'Einloggen / Registrieren')]//ancestor::button)[1]");

    public StartPage(IWebDriver driver)
    {
        this.driver = driver;
        this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
    }

    public String GetPageTitle()
    {
        return driver.Title;
    }

    public void EnterSearchText(string searchTerm)
    {
        var searchInput = driver.FindElement(inputSearch_locator);
        wait.Until(x => searchInput).SendKeys(searchTerm);
        searchInput.SendKeys(Keys.Return);
        Thread.Sleep(500);
    }

    public void AcceptCookie()
    {
        var buttonAcceptCookies = driver.FindElement(buttonAcceptCookies_locator);
        wait.Until(x => buttonAcceptCookies).Click();
        Thread.Sleep(1000);
    }

    public UserLoginPage selectLoginInHeaderbar()
    {
        var buttonLogin = driver.FindElement(buttonLogin_locator);
        wait.Until(x => ExpectedConditions.ElementToBeClickable(buttonLogin));
        buttonLogin.Click();
        Thread.Sleep(1000);
        return new UserLoginPage(driver);
    }
}