# coding: utf-8
from time import sleep
import unittest
from selenium import webdriver
from selenium.common.exceptions import NoSuchElementException
from selenium.webdriver.common.by import By
from selenium.webdriver.support.wait import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC


def find_element(driver, by, value):
    """
    :rtype: selenium.webdriver.remote.webelement.WebElement
    """
    return WebDriverWait(driver, 5).until(EC.presence_of_element_located((by, value)))


class TestApp(unittest.TestCase):
    """
    To run tests on connected (and unlocked for development) device:
    1. Set 'deviceName' to 'Device',
    2. Set 'deviceIpAddress' to IP address of device (you can find it in device wi-fi settings)
    """
    desired_capabilities = {
        'deviceName': 'Emulator',
        'deviceIpAddress': '10.54.4.128',
        'locale': 'en-US',
        'app': r"..\..\..\Winium.StoreApps.TestApp\AppPackages\Winium.StoreApps.TestApp_1.0.0.0_AnyCPU_Debug_Test"
               r"\Winium.StoreApps.TestApp_1.0.0.0_AnyCPU_Debug.appx"
    }

    def setUp(self):
        self.driver = webdriver.Remote(
            command_executor='http://localhost:9999',
            desired_capabilities=self.desired_capabilities)

    def tearDown(self):
        self.driver.quit()

    def test_sample_app(self):

        text_box = self.driver.find_element_by_tag_name("TextBox")
        print(text_box.text)

        button = self.driver.find_element_by_id("MagicId")
        button.click()

        first_text = text_box.text

        button.click()

        second_text = text_box.text

        self.assertNotEqual(first_text, second_text)

        buttons = self.driver.find_elements_by_class_name("TextBox")
        print([b.text for b in buttons])

        list_view = self.driver.find_element_by_id("TopPanel")
        buttons = list_view.find_elements_by_class_name("TextBox")
        print([b.text for b in buttons])


class TestStandardCalendar(unittest.TestCase):
    def setUp(self):
        self.driver = webdriver.Remote(
            command_executor='http://localhost:9999',
            desired_capabilities={
                'deviceName': 'Device',
                # 'debugCodedUI': True,
                'deviceIpAddress': '10.54.4.128',
                'locale': 'en-US',
            })

    def tearDown(self):
        self.driver.quit()

    def test_sample(self):
        # FIXME for some reason tiles can not be found by its full AutomationId returned by tile.Current.AutomationId
        calendar_tile_id = '{36F9FA1C-FDAD-4CF0-99EC-C03771ED741A}'  # lets do partial match manually
        tiles = self.driver.find_elements_by_class_name('')
        for tile in tiles:
            tile_id = tile.get_attribute('AutomationIdProperty')
            if tile_id.startswith(calendar_tile_id):
                tile.click()
                break
        else:
            pass

        # accept permisson alert if any
        try:
            accept_btn = self.driver.find_element_by_name("allow")
            accept_btn.click()
        except NoSuchElementException:
            pass

        # now we are in calendar app
        new_btn = find_element(self.driver, By.NAME, "new")
        new_btn.click()
        sleep(1)  # it all happens fast, lets add sleeps

        subject = find_element(self.driver, By.ID, "EditCardSubjectFieldSimplified")
        subject.send_keys(u'Презентация')
        sleep(1)

        location = find_element(self.driver, By.ID, "EditCardLocationFieldSimplified")
        location.send_keys(u'12.12 Библиотека')
        sleep(1)

        save_btn = find_element(self.driver, By.NAME, "save")

        save_btn.click()
        sleep(1)


if __name__ == "__main__":
    unittest.main()