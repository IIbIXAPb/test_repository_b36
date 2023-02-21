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
    internal class Test_11_add_new_user
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
        public void Litecart_Registration_New_User()
        {
            // в рамках тестирования функционала капча отключена
            // так как необходимо соблюдать уникальность каждого пользователя
            // будем на каждого пользователя добавлять случайное число
            int value = new Random().Next(1000, 9999);
            // переход на страницу создания нового пользователя
            driver.Url = "http://localhost/litecart/en/create_account";
            // ожидание открытия
            Thread.Sleep(1000);

            // установка параметров ввода необходимых полей
            var user_Name = driver.FindElement(By.XPath("//input[@name='firstname']"));
            var user_LastName = driver.FindElement(By.XPath("//input[@name='lastname']"));
            var user_Address = driver.FindElement(By.XPath("//input[@name='address1']"));
            var user_Postcode = driver.FindElement(By.XPath("//input[@name='postcode']"));
            var user_Email = driver.FindElement(By.XPath("//input[@name='email']"));
            var user_Phone = driver.FindElement(By.XPath("//input[@name='phone']"));
            var user_Password = driver.FindElement(By.XPath("//input[@name='password']"));
            var user_City = driver.FindElement(By.XPath("//input[@name='city']"));
            var user_confirmPassword = driver.FindElement(By.XPath("//input[@name='confirmed_password']"));
            var user_Country = driver.FindElement(By.XPath("//select[@name='country_code']"));
            var user_State = driver.FindElement(By.XPath("//select[@name='zone_code']"));
            var user_Submit = driver.FindElement(By.XPath("//button[@type='submit']"));

            // указываем пользовательские данные
            string txt_name = "Firstname";
            string txt_lastName = "Lastname";
            string txt_address = "Street name #" + value;
            string txt_postcode = "12345";
            string txt_city = "City";
            string txt_email = "user" + value + "@mail.me";
            string txt_phone = "+7913000" + value;
            string txt_password = "test";

            // заполняем поля пользовательскими данными - поля ввода
            user_Name.SendKeys(txt_name);
            user_LastName.SendKeys(txt_lastName);
            user_Address.SendKeys(txt_address);
            user_Postcode.SendKeys(txt_postcode);
            user_City.SendKeys(txt_city);
            user_Email.SendKeys(txt_email);
            user_Phone.SendKeys(txt_phone);

            // заполняем поля пользовательскими данными - поля выбора страны, области
            SelectElement countrySelect = new SelectElement(user_Country);
            countrySelect.SelectByText("United States");
            Thread.Sleep(3000);
            SelectElement stateSelect = new SelectElement(user_State);
            stateSelect.SelectByText("South Dakota");

            // заполняем поля пользовательскими данными - авторизация пользователя
            user_Password.SendKeys(txt_password);
            user_confirmPassword.SendKeys(txt_password);

            // подтверждение ввода
            user_Submit.Click();
            Thread.Sleep(3000);

            NUnit.Framework.Assert.AreNotEqual(driver.Url, "http://localhost/litecart/en/create_account");

            // выходим из учетной записи пользователя
            var logout = driver.FindElement(By.XPath("//div[@id='box-account']/div/ul/li[4]/a"));
            logout.Click();
            Thread.Sleep(3000);

            // авторизация под новым пользователем
            driver.FindElement(By.XPath("//input[@name='email']")).SendKeys(txt_email);
            driver.FindElement(By.XPath("//input[@name='password']")).SendKeys(txt_password);
            driver.FindElement(By.XPath("//button[@name='login']")).Click();
            Thread.Sleep(3000);

            // выходим из учетной записи пользователя
            logout = driver.FindElement(By.XPath("//div[@id='box-account']/div/ul/li[4]/a"));
            logout.Click();
            Thread.Sleep(3000);

            Console.WriteLine("Создание нового пользователя:");
            Console.WriteLine("------------------------------");
            Console.WriteLine("Имя: " + txt_name);
            Console.WriteLine("Фамилия: " + txt_lastName);
            Console.WriteLine("Логин: " + txt_email);
            Console.WriteLine("Пароль: " + txt_password);
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }

    }
}
