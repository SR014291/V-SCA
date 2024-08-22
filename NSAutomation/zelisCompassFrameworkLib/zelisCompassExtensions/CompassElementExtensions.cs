using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zelisCompassFrameWorkLib.zelisCompassBase;
using zelisCompassFrameWorkLib.zelisHelpers;
using System.Collections;

namespace zelisCompassFrameWorkLib.zelisCompassExtensions
{
    public static class CompassElementExtensions
    {
        
        public static string GetSelectedDropDown(this IWebElement element)
        {
            SelectElement DropDownList = new SelectElement(element);
            return DropDownList.AllSelectedOptions.First().ToString();
        }
        public static IList<IWebElement> GetSelectedListOptions(this IWebElement element)
        {
            SelectElement DropDownList = new SelectElement(element);
            
            return DropDownList.Options;
        }



        public static void SelectDropDownList(this IWebElement element, string value)
        {
            SelectElement DropDownList = new SelectElement(element);
            DropDownList.SelectByText(value);
        }
        public static void AssertElementPresent(this IWebElement element)
        {
            if (!IsElementPresent(element))
                throw new Exception(string.Format("Element Not Present Exception"));
        }

        public static void Hover(this IWebElement mouseElement)
        {
            Actions Mouse = new Actions(WinDriverContext.CompassCMS);

            Mouse.MoveToElement(mouseElement).Perform();
        }

        public static bool IsElementPresent(IWebElement element)
        {
            try
            {
                bool ele = element.Displayed;
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }

        public static List<string> FetchColumnData(IWebElement datagrid, string columnname)
        {
            List<string> data = new List<string>();
            var dgServiceInfoIteams = datagrid.FindElements(By.TagName("DataItem"));

            foreach (var DataIteam in dgServiceInfoIteams)
            {
                
                if (DataIteam.GetAttribute("Name").StartsWith(columnname))
                {
                    data.Add(DataIteam.Text);
                }
            }
            return data;
        }

        public static void AddValueToDG(IWebElement datagrid, string columnname, int rowno, String billingcode)
        { 
            var dgServiceInfoIteams = datagrid.FindElements(By.TagName("DataItem"));

            foreach (var DataIteam in dgServiceInfoIteams)
            {
                
                if (DataIteam.GetAttribute("Name").Contains($"{columnname} Row {rowno}"))
                {
                    LogHelper.Write("--- ADD Data to -- " + DataIteam.GetAttribute("Name"));
                    DataIteam.Click();
                    DataIteam.SendKeys(billingcode);
                    break;
                }
            }             
        }
        
        public static Dictionary<string, string> FetchRowData(IWebElement datagridRow, int rowno)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            var dgServiceInfoIteams = datagridRow.FindElements(By.TagName("DataItem"));

            foreach (var DataIteam in dgServiceInfoIteams)
            {
                if (!DataIteam.GetAttribute("Name").StartsWith(" Row"))
                {
                    string key = DataIteam.GetAttribute("Name").Replace($" Row {rowno}", "");
                    data.Add(key, DataIteam.Text);
                                        
                }
            }
            return data;
        }
        
        public static void SelectRadioButton(IWebElement radiobutton)
        {
            if (!radiobutton.Selected)
            {
                radiobutton.Click();
            }
        }

        public static void pageLoad(IWebDriver driver)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("return document.readyState").Equals("complete");
        }

        public static void enterText(this IWebElement element, string value)
        {
            element.Clear();
            element.SendKeys(value);
        }

        public static string getElementText(this IWebElement element)
        {
            return element.Text;
        }

        public static void Click(this IWebDriver driver, IWebElement element)
        {
            element.Click();
            pageLoad(driver);
        }

        public static void ClickJS(this IWebDriver driver, IWebElement element)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", element);
        }

        public static void scrollIntoView(this IWebDriver driver, IWebElement element)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public static void ClickMoveToElement(this IWebDriver driver, IWebElement element)
        {
            Actions act = new Actions(driver);
            act.MoveToElement(element).Click().Build().Perform();
        }

        public static void doubleClick(this IWebDriver driver, IWebElement element)
        {
            Actions act = new Actions(driver);
            act.MoveToElement(element).DoubleClick().Build().Perform();
        }

        public static void dragAndDrop(this IWebDriver driver, IWebElement sourceElement, IWebElement targetElement)
        {
            Actions act = new Actions(driver);
            act.DragAndDrop(sourceElement, targetElement).Build().Perform();
        }

        public static string getDropdownSelectedText(IWebElement element)
        {
            SelectElement select = new SelectElement(element);
            return select.SelectedOption.Text;
        }

        public static void checkboxToggledOn(this IWebElement element)
        {
            if (!element.Selected)
            {
                element.Click();
            }
        }

        public static void checkboxToggledOff(this IWebElement element)
        {
            if (element.Selected)
            {
                element.Click();
            }
        }

        public static bool IsElementSelected(IWebElement element)
        {
            try
            {
                bool ele = element.Selected;
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
    }
}
