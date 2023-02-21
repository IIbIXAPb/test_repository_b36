using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace test_repository_b36_nikitin_ps
{
    internal class Test_12_add_new_products
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            // объявление драйвера браузера
            driver = new ChromeDriver();
        }
        [Test]
        public void Litecart_Products_New_Product()
        {
            // переход на стартовую страницу
            driver.Url = "http://localhost/litecart/admin/";

            //  авторизация на форме админки
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            // пауза интерфейса магазина
            Thread.Sleep(2000);

            // переход: новая позиция магазина
            driver.FindElement(By.XPath("//ul[@id='box-apps-menu']/li[2]/a")).Click();
            driver.FindElement(By.XPath("//td[@id='content']/div[1]/a[2]")).Click();

            // заполнение: основная информация
            // будем на каждого товара добавлять случайное число
            int value = new Random().Next(1000, 9999);
            string prod_name = "name " + value;
            string prod_code = "code " + value;
            string prod_quantity = value.ToString();

            string imgPath = Path.GetFullPath("1-queen-duck.png");

            driver.FindElement(By.XPath("//div[@id='tab-general']/table/tbody/tr[1]/td/label[1]/input")).Click();
            driver.FindElement(By.XPath("//div[@id='tab-general']/table/tbody/tr[2]/td/span/input")).SendKeys(prod_name);
            driver.FindElement(By.XPath("//div[@id='tab-general']/table/tbody/tr[3]/td/input")).SendKeys(prod_code);
            driver.FindElement(By.XPath("//div[@id='tab-general']/table/tbody/tr[4]/td/div/table/tbody/tr[2]/td[1]/input")).Click();
            driver.FindElement(By.XPath("//div[@id='tab-general']/table/tbody/tr[4]/td/div/table/tbody/tr[1]/td[1]/input")).Click();
            driver.FindElement(By.XPath("//div[@id='tab-general']/table/tbody/tr[8]//input")).SendKeys(prod_quantity);

            SelectElement statusSelect = new SelectElement(driver.FindElement(By.XPath("//div[@id='tab-general']/table/tbody/tr[8]//select[@name='sold_out_status_id']")));
            statusSelect.SelectByValue("2");

            // Date Valid From
            driver.FindElement(By.XPath("//div[@id='tab-general']/table/tbody/tr[10]//input")).SendKeys("01012023");
            // Date Valid To
            driver.FindElement(By.XPath("//div[@id='tab-general']/table/tbody/tr[11]//input")).SendKeys("31122023");
            // Upload Images
            driver.FindElement(By.XPath("//div[@id='tab-general']/table/tbody/tr[9]//input")).SendKeys(imgPath);
            driver.FindElement(By.XPath("//div[@id='tab-general']/table/tbody/tr[9]//a")).Click();

            // заполнение: Information
            driver.FindElement(By.XPath("//ul[@class='index']/li[2]/a")).Click();
            Thread.Sleep(2000);

            // выбор поставщика
            SelectElement manufacturer = new SelectElement(driver.FindElement(By.XPath("//select[@name='manufacturer_id']")));
            manufacturer.SelectByValue("1");

            string prod_info_name = "duck " + value;
            string prod_info_description = "description " + value;

            driver.FindElement(By.XPath("//input[@name='keywords']")).SendKeys(prod_info_name);
            driver.FindElement(By.XPath("//input[@name='short_description[en]']")).SendKeys(prod_info_description);
            driver.FindElement(By.XPath("//div[@class='trumbowyg-editor']")).SendKeys("test");
            driver.FindElement(By.XPath("//input[@name='head_title[en]']")).SendKeys("test");
            driver.FindElement(By.XPath("//input[@name='meta_description[en]']")).SendKeys("test");

            // заполнение: Prices
            driver.FindElement(By.XPath("//ul[@class='index']/li[4]/a")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//input[@name='purchase_price']")).SendKeys(prod_quantity);
            SelectElement price = new SelectElement(driver.FindElement(By.XPath("//select[@name='purchase_price_currency_code']")));
            price.SelectByValue("USD");


            driver.FindElement(By.XPath("//input[@name='prices[USD]']")).SendKeys(prod_quantity);
            driver.FindElement(By.XPath("//input[@name='prices[EUR]']")).SendKeys(prod_quantity);

            Thread.Sleep(2000);

            // сохранение нового товара
            driver.FindElement(By.XPath("//button[@name='save']")).Click();
            Thread.Sleep(1000);

            Console.WriteLine("Создание новой позиции:");
            Console.WriteLine("------------------------------");
            Console.WriteLine("Название: " + prod_info_name);
            Console.WriteLine("Описание: " + prod_info_description);
            Console.WriteLine("Цена: " + prod_quantity);
            Console.WriteLine("Код : " + prod_code);
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }

    }
}
