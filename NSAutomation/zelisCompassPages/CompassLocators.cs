using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zelisCompassFrameWorkLib.zelisCompassBase;

namespace zelisCompassPages
{
   public class CompassLocators : CompassBasePage
    {
        [FindsBy(How = How.XPath, Using = "//*[@Name='Claims']")]
        public IWebElement miClaim { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@Name='Payers']")]
        public IWebElement miPayers { get; set; }

      //  [FindsBy(How = How.XPath, Using = "//*[@Name='Claims']")]
        //public IWebElement ddClaimStatus { get; set; }

       
        [FindsBy(How = How.XPath, Using = "(//*[@ClassName='TextBox'])[1]")]
        public IWebElement txtClaimID { get; set; }

        [FindsBy(How = How.XPath, Using = "(//*[@ClassName='TextBox'])[2]")]
        public IWebElement txtPayerClaimID1 { get; set; }

        [FindsBy(How = How.XPath, Using = "(//*[@ClassName='TextBox'])[3]")]
        public IWebElement txtPayerClaimID2 { get; set; }

        [FindsBy(How = How.XPath, Using = "(//*[@ClassName='TextBox'])[4]")]
        public IWebElement txtConfirmation { get; set; }

        [FindsBy(How = How.XPath, Using = "(//*[@ClassName='TextBox'])[5]")]
        public IWebElement txtPatientName { get; set; }

        [FindsBy(How = How.XPath, Using = "(//*[@ClassName='TextBox'])[7]")]
        public IWebElement txtProviderTIN { get; set; }

        [FindsBy(How = How.XPath, Using = "(//*[@ClassName='TextBox'])[9]")]
        public IWebElement txtInvoiceNumber { get; set; }

        [FindsBy(How = How.XPath, Using = "(//*[@ClassName='TextBox'])[10]")]
        public IWebElement txtDOSDateRangeFrom { get; set; }

        [FindsBy(How = How.XPath, Using = "(//*[@ClassName='TextBox'])[11]")]
        public IWebElement txtDOSDateRangeTo { get; set; }

        [FindsBy(How = How.XPath, Using = "(//*[@ClassName='TextBox'])[12]")]
        public IWebElement txtBilledAmountRangeFrom { get; set; }

        [FindsBy(How = How.XPath, Using = "(//*[@ClassName='TextBox'])[13]")]
        public IWebElement txtBilledAmountRangeTo { get; set; }

        [FindsBy(How = How.XPath, Using = "(//*[@ClassName='TextBox'])[14]")]
        public IWebElement txtSearchPayers { get; set; }

        [FindsBy(How = How.XPath, Using = "(//*[@ClassName='TextBox'])[15]")]
        public IWebElement txtSearchNetworks { get; set; }

        [FindsBy(How = How.XPath, Using = "(//*[@ClassName='ComboBox'])[1]")]
        public IWebElement ddClaimStatus { get; set; }

        [FindsBy(How = How.XPath, Using = "(//*[@ClassName='ComboBox'])[2]")]
        public IWebElement ddClaimAge { get; set; }

        [FindsBy(How = How.XPath, Using = "//DataGrid[@ClassName='DataGrid']//DataItem[@ClassName='DataGridRow'][@Name='Zelis.Compass.Domain.Models.Network']//Custom[@ClassName='DataGridCell'][1]")]
        public IList<IWebElement> dgSearchNetworks { get; set; }

        [FindsBy(How = How.XPath, Using = "//DataGrid[@ClassName='DataGrid']//DataItem[@ClassName='DataGridRow'][@Name='Zelis.Compass.Domain.Models.Payer']//Custom[@ClassName='DataGridCell'][1]")]
        public IList<IWebElement> dgSearchPayers { get; set; }


        [FindsBy(How = How.XPath, Using = "//Button[@ClassName='Button'][@Name='Search']")]
        public IWebElement btnSearch { get; set; }

        [FindsBy(How = How.XPath, Using = "//Button[@ClassName='Button'][@Name='Clear All']")]
        public IWebElement btnClearAll { get; set; }


        [FindsBy(How = How.XPath, Using = "//DataGrid[@Name='GridViewDataControl'][@AutomationId='grdClaims']//Custom[@AutomationId='PART_GridViewVirtualizingPanel']//DataItem[@Name='Zelis.Compass.Domain.Models.ClaimList'][starts-with(@AutomationId,'Row_')]")]
        public IList<IWebElement> dgClaimsDetails { get; set; }

        public IWebElement dgClaimsRowDetails(int row)
        {
            return WinDriverContext.CompassCMS.FindElement(By.XPath($"//DataGrid[@Name='GridViewDataControl'][@AutomationId='grdClaims']//Custom[@AutomationId='PART_GridViewVirtualizingPanel']//DataItem[@Name='Zelis.Compass.Domain.Models.ClaimList'][starts-with(@AutomationId,'Row_')][{row}]")); ;
        }

        [FindsBy(How = How.XPath, Using = "//*[contains(@Name,'Line Items')]")]
        public IWebElement tabLineItems { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[contains(@Name,'Claim Notes')]")]
        public IWebElement tabClaimNotes { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[contains(@Name,'Provider Notes')]")]
        public IWebElement tabProviderNotes { get; set; }

        [FindsBy(How = How.XPath, Using = "(//*[contains(@Name,'Invoice')])[2]")]
        public IWebElement tabInvoice { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[contains(@Name,'RBP Repricings')]")]
        public IWebElement tabRBPRepricings { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[contains(@Name,'Benchmarks')]")]
        public IWebElement tabBenchmarks { get; set; }

         [FindsBy(How = How.XPath, Using = "//*[contains(@Name,'Provider Inquiry')]")]
        public IWebElement tabProviderInquiry { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[contains(@Name,'History')]")]
        public IWebElement tabHistory { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[contains(@Name,'Group |')]")]
        public IWebElement tabGroup { get; set; }

        [FindsBy(How = How.XPath, Using = "(//*[@ClassName='ClaimProviderNotesView']//Edit[@ClassName='TextBox'])[1]")]
        public IWebElement txtProviderNotesHeader { get; set; }

        [FindsBy(How = How.XPath, Using = "(//*[@ClassName='ClaimProviderNotesView']//Edit[@ClassName='TextBox'])[2]")]
        public IWebElement txtProviderNotes { get; set; }

        [FindsBy(How = How.XPath, Using = "(//*[@ClassName='ClaimProviderNotesView']//Text[@ClassName='TextBlock'])[4]")]
        public IWebElement lblProviderNotesCount { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@ClassName='ClaimProviderNotesView']//Button[@ClassName='Button'][@Name='Create New']")]
        public IWebElement btnProviderNoteCreateNew { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@ClassName='ClaimProviderNotesView']//Button[@ClassName='Button'][@Name='Save']")]
        public IWebElement btnProviderNoteSave { get; set; }

        [FindsBy(How = How.XPath, Using = "//Button[@ClassName='Button'][@Name='Provider']")]
        public IWebElement btnProviderTab { get; set; }

        [FindsBy(How = How.XPath, Using = "//Button[@ClassName='Button'][@Name='TIN Lookup']")]
        public IWebElement btnTINLookupTab { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@AutomationId='PART_BackButton']")]
        public IWebElement btnBack { get; set; }

    }
}
