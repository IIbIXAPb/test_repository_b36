using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_repository_b36_nikitin_ps
{
    internal class Test_09_sort_Zone_Names_List
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            //wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]

        public void LitecartGeozones_check()
        {
            // переход на стартовую страницу
            driver.Url = "http://localhost/litecart/admin/";

            //  авторизация на форме админки
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            // пауза интерфейса магазина
            Thread.Sleep(1000);

            // ссылка справочника зон
            driver.Url = "http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones";

            // ссылки на зоны
            var Rows = driver.FindElements(By.XPath("//tr[@class='row']/td[3]/a"));
            List<string> Links = new List<string>();

            // заполнение списка ссылок на зоны
            for (int i = 0; i < Rows.Count(); i++)
            {
                var row = Rows[i];
                Links.Add(row.GetAttribute("href"));
            }

            // проверка сортировок в зонах
            foreach (string link in Links)
            {
                driver.Url = link;

                // поиск элемента доступной зоны на странице
                var Names = driver.FindElements(By.XPath("//table[@class='dataTable']/tbody/tr/td[3]/select/option[@selected='selected']"));
                List<string> ZoneNames = new List<string>();

                // получение названия зоны
                foreach (IWebElement name in Names) if (name.GetAttribute("textContent") != "") ZoneNames.Add(name.GetAttribute("textContent"));

                // зпполнение списка отсортированных названий зон
                List<string> ZoneNamesSorted = new List<string>(ZoneNames);
                ZoneNamesSorted.Sort();

                // проверка на сортировку, совпадают ли списки
                if (ZoneNames.SequenceEqual(ZoneNamesSorted)) Console.WriteLine("Zones are sorted correctly");
                else Console.WriteLine("Zones are not sorted correctly");
            }
        }

        [TearDown]
        public void stop()
        {
            // выход из драйвера
            driver.Quit();
            driver = null;
        }
    }
}
