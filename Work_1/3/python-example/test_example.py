import pytest
from selenium import webdriver
from selenium.webdriver.support.wait import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.common.by import By


@pytest.fixture
def driver(request):
    wd = webdriver.Chrome()
    print(wd.capabilities)
    request.addfinalizer(wd.quit)
    return wd

def test_example(driver):
    driver.get("http://localhost/litecart/en/")
    driver.find_element(By.NAME, "email").send_keys("admin")
    driver.find_element(By.NAME, "password").send_keys("admin")
    driver.find_element(By.NAME, "login").click()
    WebDriverWait(driver, 10).until(EC.title_is("Login | My Store"))
