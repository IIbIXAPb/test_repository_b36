using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace test_repository_b36_nikitin_ps
{
    internal class Test_13_add_product_to_shopping_cart
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            // объявление драйвера браузера
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]

        public void LitecartCart()
        {
            int prod_insert = 0;
            int prod_delete = 0;

            // переход на стартовую страницу магазина
            driver.Url = "http://localhost/litecart/";

            // добавление товаров в корзину пользователя
            for (int i = 0; i < 5; i++)
            {
                driver.FindElement(By.XPath($"//ul[@class='listing-wrapper products']/li[{i + 1}]/a[1]")).Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlContains("duck"));

                var quanity = (Int32.Parse(driver.FindElement(By.XPath("//span[@class='quantity']")).GetAttribute("textContent")) + 1).ToString();
                try
                {
                    SelectElement statusSelect = new SelectElement(driver.FindElement(By.XPath("//select[@name='options[Size]']")));
                    statusSelect.SelectByValue("Small");
                }
                catch (NoSuchElementException)
                { }
                Thread.Sleep(1000);

                // нажатие на кнопку добавления в корзину
                driver.FindElement(By.XPath("//button[@name='add_cart_product']")).Click();
                // ожидание изменения количества товаров в корзине
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.XPath("//span[@class='quantity']")), quanity));

                // увеличение счетчика
                prod_insert++;

                // переход на стартовую страницу магазина
                driver.Url = "http://localhost/litecart/";
            }

            // переход в корзину и удаление товаров из корзины
            driver.FindElement(By.XPath("//div[@id='cart']/a[@class='link']")).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlContains("checkout"));
            IWebElement? element = null;
            // получение количества товаров в корзине
            var cnt = driver.FindElements(By.XPath("//table[@class='dataTable rounded-corners']//td[@class='item']")).Count;

            // запуск цикла удаления товаров в  корзине
            for (int j = 0; j < cnt; j++)
            {
                var ourItem = driver.FindElement(By.XPath("//div[@style='display: inline-block;']//a")).GetAttribute("textContent");
                var items = driver.FindElements(By.XPath("//table[@class='dataTable rounded-corners']//td[@class='item']"));
                foreach (var item in items)
                {
                    if (item.GetAttribute("textContent") == ourItem) element = item;
                }
                driver.FindElement(By.XPath("//button[@value='Remove']")).Click();
                // ожидание изменения
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(element));
            }

            // ожидание изменения в корзине
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//em")));
            Thread.Sleep(1000);
            // переход на стартовую страницу магазина
            driver.Url = "http://localhost/litecart/";
            Thread.Sleep(1000);

            Console.WriteLine("Проверка добавления товаров:");
            Console.WriteLine("------------------------------");
            Console.WriteLine("Добавлено товаров: " + prod_insert);
        }


        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
