﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace Automation_CoreLayer.DriverUtils
{
    public class EdgeDriverFactory : IwebDriverFactory
    {
        public IWebDriver CreateDriver()
        {
            return new EdgeDriver();
        }
    }
}
