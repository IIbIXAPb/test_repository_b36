using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace test_repository_b36_nikitin_ps
{
    internal class Test_10_Iten_Find_Page_Done
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            //driver = new EdgeDriver();
            //driver = new FirefoxDriver();
        }

        [Test]

        public void Litecart_page_check()
        {
            // список главный страниц
            Dictionary<string, string> mainPage = new Dictionary<string, string>();
            Dictionary<string, string> productPage = new Dictionary<string, string>();

            // Сбор данных с главной страницы - проверка данных акционного товара
            driver.Url = "http://localhost/litecart/";

            // создание списков позиций товаров - акция
            var element = driver.FindElement(By.XPath("//div[@id='box-campaigns']/div/ul/li"));
            var full_price = element.FindElement(By.XPath("./a[1]/div[@class='price-wrapper']/s[@class='regular-price']"));
            var sale_price = element.FindElement(By.XPath("./a[1]/div[@class='price-wrapper']/strong[@class='campaign-price']"));

            // получение атрибутов название, цена до акции, цена после акции
            mainPage.Add("name", element.FindElement(By.XPath("./a[1]/div[@class='name']")).GetAttribute("textContent"));
            mainPage.Add("full_price", full_price.GetAttribute("textContent"));
            mainPage.Add("sale_price", sale_price.GetAttribute("textContent"));

            // получение атрибутов товаров - цвет
            var full_price_color = RGB_Number(full_price.GetCssValue("color"));
            var sale_price_color = RGB_Number(sale_price.GetCssValue("color"));
            // получение атрибутов товаров - цена, форматирование
            var full_price_dec = full_price.GetCssValue("text-decoration");
            var sale_price_weight = Int32.Parse(sale_price.GetCssValue("font-weight"));
            var full_price_weight = Int32.Parse(full_price.GetCssValue("font-weight"));

            var sale_price_size = Convert.ToDouble(sale_price.GetCssValue("font-size").Trim(new char[] { 'p', 'x' }).Replace(".", ","));
            var full_price_size = Convert.ToDouble(full_price.GetCssValue("font-size").Trim(new char[] { 'p', 'x' }).Replace(".", ","));

            // получение размера шрифта на увеличение
            NUnit.Framework.Assert.That(sale_price_size, Is.GreaterThan(full_price_size));

            // пауза интерфейса магазина
            Thread.Sleep(1000);

            // пауза интерфейса магазина
            Thread.Sleep(1000);
            // товар из категории, участвующий в акции - прямая ссылка на него
            driver.Url = "http://localhost/litecart/en/rubber-ducks-c-1/subcategory-c-2/yellow-duck-p-1";

            // получение стоимостей товаров
            var full_price_p = driver.FindElement(By.XPath("//div[@class='information']/div/s"));
            var sale_price_p = driver.FindElement(By.XPath("//div[@class='information']/div/strong"));

            var sale_price_size_p = Convert.ToDouble(sale_price_p.GetCssValue("font-size").Trim(new char[] { 'p', 'x' }).Replace(".", ","));
            var full_price_size_p = Convert.ToDouble(full_price_p.GetCssValue("font-size").Trim(new char[] { 'p', 'x' }).Replace(".", ","));

            // заполнение данных товара - название, цена до, цена после акции
            productPage.Add("name", driver.FindElement(By.XPath("//h1")).GetAttribute("textContent"));
            productPage.Add("full_price", full_price_p.GetAttribute("textContent"));
            productPage.Add("sale_price", sale_price_p.GetAttribute("textContent"));

            // заполнение данных товара - цвет, выделение цветом
            var full_price_color_p = RGB_Number(full_price_p.GetCssValue("color"));
            var sale_price_color_p = RGB_Number(sale_price_p.GetCssValue("color"));

            var full_price_dec_p = full_price_p.GetCssValue("text-decoration");
            var sale_price_weight_p = Int32.Parse(sale_price_p.GetCssValue("font-weight"));
            var full_price_weight_p = Int32.Parse(full_price_p.GetCssValue("font-weight"));

            // получение размера шрифта на увеличение
            NUnit.Framework.Assert.That(sale_price_size_p, Is.GreaterThan(full_price_size_p));

            // проверка атрибутов товара с галвной страницы и страницы товара
            NUnit.Framework.Assert.That(mainPage["name"] == productPage["name"]);
            NUnit.Framework.Assert.That(mainPage["full_price"] == productPage["full_price"]);
            NUnit.Framework.Assert.That(mainPage["sale_price"] == productPage["sale_price"]);

            // проверка атрибутов товара с главной страницы и страницы товара - стоимость и формат
            NUnit.Framework.Assert.That((full_price_color[0] == full_price_color[1]) && (full_price_color[1] == full_price_color[2]));
            NUnit.Framework.Assert.That(full_price_dec.Contains("line-through"));
            NUnit.Framework.Assert.That((full_price_color_p[0] == full_price_color_p[1]) && (full_price_color_p[1] == full_price_color_p[2]));
            NUnit.Framework.Assert.That(full_price_dec_p.Contains("line-through"));

            // проверка атрибутов товара с главной страницы и страницы товара -  цена красная и жирная
            NUnit.Framework.Assert.That((sale_price_color[0] != 0) && (sale_price_color[1] == 0) && (sale_price_color[2] == 0));
            NUnit.Framework.Assert.That(sale_price_weight > full_price_weight);
            NUnit.Framework.Assert.That((sale_price_color_p[0] != 0) && (sale_price_color_p[1] == 0) && (sale_price_color_p[2] == 0));
            NUnit.Framework.Assert.That(sale_price_weight_p > full_price_weight_p);

            // пауза интерфейса магазина
            Thread.Sleep(1000);
        }

        // разделение цвета товара на позиции цвета
        public static List<int> RGB_Number(string s)
        {
            char[] separators = new char[] { ' ', ',', ')', '(' };
            string[] subs = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            List<int> result = new List<int>();

            for (int j = 1; j <= 4; j++) result.Add(Int32.Parse(subs[j]));
            return result;
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
