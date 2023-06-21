using System;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace PostATAutomationProject.PageObjects;

class StartPage
{
    IWebDriver driver;
    WebDriverWait wait;
    IConfiguration configuration;

    // Locators
    private By inputSearch_locator => By.XPath(configuration.GetSection("StartPage:Locators:inputSearch_locator").Value);
    private By buttonAcceptCookies_locator => By.XPath(configuration.GetSection("StartPage:Locators:buttonAcceptCookies_locator").Value);
    private By buttonLogin_locator => By.XPath(configuration.GetSection("StartPage:Locators:buttonLogin_locator").Value);
    private By buttonUserContextmenu_locator => By.XPath(configuration.GetSection("StartPage:Locators:buttonUserContextmenu_locator").Value);
    private By buttonUserContextmenuLogout_locator => By.XPath(configuration.GetSection("StartPage:Locators:buttonUserContextmenuLogout_locator").Value);

    public StartPage(IWebDriver driver, IConfiguration configuration)
    {
        this.driver = driver;
        this.configuration = configuration;
        this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
    }

    public String GetPageTitle()
    {
        return driver.Title;
    }
    
    public bool waitTillElementIsClickable(By elementLocator)
    
    {
        try
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(elementLocator));
            return true; 
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
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
        wait.Until(x => ExpectedConditions.InvisibilityOfElementLocated(buttonLogin_locator));
        IWebElement elementButtonLogin = driver.FindElement(buttonLogin_locator);
        IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
        jsExecutor.ExecuteScript("arguments[0].click();", elementButtonLogin);
        Thread.Sleep(1000);
        return new UserLoginPage(driver, configuration);
    }
    
    public void clickLogoutButton()
    {
        var buttonUserContextmenu = driver.FindElement(buttonUserContextmenu_locator);
        wait.Until(x => ExpectedConditions.ElementToBeClickable(buttonUserContextmenu));
        buttonUserContextmenu.Click();
        Thread.Sleep(1000);
        var buttonUserContextmenuLogout = driver.FindElement(buttonUserContextmenuLogout_locator);
        wait.Until(x => ExpectedConditions.ElementToBeClickable(buttonUserContextmenuLogout));
        buttonUserContextmenuLogout.Click();
    }

    public void CheckIfElementExistByLocator(By xPath)
    {
        wait.Until(ExpectedConditions.ElementExists(xPath));
        // Assert that the element exists in the DOM
        bool elementExists = driver.FindElements(xPath).Count > 0;
        Assert.IsTrue(elementExists);
    }
}