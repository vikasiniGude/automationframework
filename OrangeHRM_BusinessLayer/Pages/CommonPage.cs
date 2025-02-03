using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace OrangeHRM_BusinessLayer.Pages
{
    public class CommonPage
    {
        protected IWebDriver BLDriver;
        public CommonPage(IWebDriver driver)
        {
            BLDriver = driver;
        }
    }
}
