using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace test_repository_b36_nikitin_ps{
    internal class Test_14_admin_open_new_windows
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void LitecartNewWindow()
        {
            // переход на стартовую страницу
            driver.Url = "http://localhost/litecart/admin/";

            //  авторизация на форме админки
            driver.FindElement(By.Name("username")).SendKeys("admin");
            Thread.Sleep(500);
            driver.FindElement(By.Name("password")).SendKeys("admin");
            Thread.Sleep(500);
            driver.FindElement(By.Name("login")).Click();

            // пауза интерфейса магазина
            Thread.Sleep(1000);

            // ссылка справочника стран - список стран
            driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";
            // ссылка справочника стран - добавление
            //driver.Url = "http://localhost/litecart/admin/?app=countries&doc=edit_country";
            driver.FindElement(By.XPath("//td[@id='content']//tr[@class='row']//a")).Click();
            var elements = driver.FindElements(By.XPath("//i[@class='fa fa-external-link']"));

            string windows_start = driver.CurrentWindowHandle;

            Console.WriteLine("Открытие дополнительных окон:");
            Console.WriteLine("------------------------------");

            foreach (var element in elements)
            {
                // пауза интерфейса магазина
                Thread.Sleep(1000);
                ICollection<string> windows_main = driver.WindowHandles;
                // открытие окна справочника
                element.Click();
                // объявление нового окна
                string windows_new = findNewWindow(driver, windows_main);
                // переключение на новое окно
                driver.SwitchTo().Window(windows_new);
                // запись в историю
                Console.WriteLine("Открытие окна: " + windows_new);
                // пауза интерфейса магазина
                Thread.Sleep(2000);
                // закрытие окна
                driver.Close();
                // переход на главное окно теста
                driver.SwitchTo().Window(windows_start);
            }
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }


        // поиск нового окна в браузере тестирования
        static string findNewWindow(IWebDriver driver, ICollection<string> windows_old, int maxRetryCount = 100)
        {
            bool return_value_status;
            string windows_other = null;
            // работа с коллекицией открытых окон
            for (var i = 0; i < maxRetryCount; Thread.Sleep(10), i++)
            {
                ICollection<string> windows_all = driver.WindowHandles;
                return_value_status = (windows_old.Equals(windows_all) ? true : false);

                if (!return_value_status)
                {
                    // проверка каждого окна в корзине окон
                    foreach (var window in windows_all)
                    {
                        if (!windows_old.Contains(window)) windows_other = window;
                        if (windows_other != null) return windows_other;
                    }
                }
            }
            throw new ApplicationException("Новое окно отсутствует");
        }
    }
}
