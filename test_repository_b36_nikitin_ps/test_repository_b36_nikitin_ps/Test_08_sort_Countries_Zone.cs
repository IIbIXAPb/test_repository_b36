using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace test_repository_b36_nikitin_ps
{
    internal class Test_08_sort_Countries_Zone
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            // активация браузера
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        [Test]

        public void LitecartCountries()
        {
            // переход на стартовую страницу
            driver.Url = "http://localhost/litecart/admin/";

            //  авторизация на форме админки
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            // пауза интерфейса магазина
            Thread.Sleep(1000);

            // ссылка справочника стран
            driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";

            var Rows = driver.FindElements(By.XPath("//table[@class='dataTable']/tbody/tr[@class='row']"));
            //создание списков обработки тестов
            List<string> Countries = new List<string>();
            List<string> Links = new List<string>();

            // заполнение списков
            for (int k = 0; k < Rows.Count(); k++)
            {
                // страны
                var row = Rows[k];
                var country = row.FindElement(By.XPath("./td[5]/a")).GetAttribute("textContent");
                Countries.Add(country);
                // временные зоны
                var zone = row.FindElement(By.XPath("./td[6]")).GetAttribute("textContent");
                if (zone != "0") Links.Add(row.FindElement(By.XPath("./td[5]/a")).GetAttribute("href"));

            }

            // сортировка стран магазина
            List<string> CountriesSorted = new List<string>(Countries);
            CountriesSorted.Sort();

            // проверка двух списков на предмет сортировки
            if (Countries.SequenceEqual(CountriesSorted)) Console.WriteLine("Countries are sorted correctly");
            else Console.WriteLine("Countries are not sorted correctly");


            // Проверяем сортировку в зонах
            foreach (string link in Links)
            {
                driver.Url = link;

                var Names = driver.FindElements(By.XPath("//table[@id='table-zones']/tbody/tr/td[3]"));
                List<string> ZoneNames = new List<string>();

                foreach (IWebElement name in Names) if (name.GetAttribute("textContent") != "") ZoneNames.Add(name.GetAttribute("textContent"));

                // создание сортировочного списка
                List<string> ZoneNamesSorted = new List<string>(ZoneNames);
                ZoneNamesSorted.Sort();
                // сравнение списков сортировки
                if (ZoneNames.SequenceEqual(ZoneNamesSorted)) Console.WriteLine("Zones are sorted correctly");
                else Console.WriteLine("Zones are not sorted correctly");
            }
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
