using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace test_repository_b36_nikitin_ps
{
    internal class Test_4_2_stikers
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
        public void LitecartCheckStikers()
        {
            // переход на стартовую страницу
            driver.Url = "http://localhost/litecart/";

            // создание элементов списков товаров
            By Popular = By.Id("box-most-popular");
            By Campaigns = By.Id("box-campaigns");
            By Latest = By.Id("box-latest-products");

            // обработка списка популярных
            var elements = driver.FindElement(Popular);
            var PopEl = elements.FindElement(By.TagName("ul")).FindElements(By.TagName("li"));

            for (int i = 0; i < PopEl.Count(); i++)
            {
                var PopElStickerCheck = PopEl[i].FindElements(By.ClassName("sticker"));
                // проверка отсутствия стикера
                if (PopElStickerCheck.Count() != 1) Console.WriteLine("Popular: sticker error");
                else if (PopElStickerCheck.Count() == 1) Console.WriteLine("Popular: sticker yes");

            }

            // обработка списка участвующих в акции
            elements = driver.FindElement(Campaigns);
            var CampEl = elements.FindElement(By.TagName("ul")).FindElements(By.TagName("li"));

            for (int i = 0; i < CampEl.Count(); i++)
            {
                var CampElStickerCheck = CampEl[i].FindElements(By.ClassName("sticker"));
                // проверка отсутствия стикера
                if (CampElStickerCheck.Count() != 1) Console.WriteLine("Campaigns: sticker error");
                else if (CampElStickerCheck.Count() == 1) Console.WriteLine("Popular: sticker yes");

            }

            // обработка списка с последними экземплярами
            elements = driver.FindElement(Latest);
            var LatestEl = elements.FindElement(By.TagName("ul")).FindElements(By.TagName("li"));

            for (int i = 0; i < LatestEl.Count(); i++)
            {
                var LatestElStickerCheck = LatestEl[i].FindElements(By.ClassName("sticker"));
                // проверка отсутствия стикера
                if (LatestElStickerCheck.Count() != 1) Console.WriteLine("Latest: sticker error");
                else if (LatestElStickerCheck.Count() == 1) Console.WriteLine("Popular: sticker yes");

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
