# coding: utf-8
from time import sleep
import unittest
from xml.etree import ElementTree
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
        # 'deviceName': 'Device',
        'deviceName': 'Emulator',
        'deviceIpAddress': '127.0.0.1',
        'locale': 'en-US',
        'debugCodedUI': False,
        'app': r"..\..\Winium.StoreApps.TestApp\AppPackages\Winium.StoreApps.TestApp_1.0.0.0_AnyCPU_Debug_Test"
               r"\Winium.StoreApps.TestApp_1.0.0.0_AnyCPU_Debug.appx"
    }

    def setUp(self):
        self.driver = webdriver.Remote(
            command_executor='http://localhost:9999',
            desired_capabilities=self.desired_capabilities)

    def tearDown(self):
        self.driver.quit()

    def test_page_source(self):
        source = self.driver.page_source
        print(source)
        root = ElementTree.fromstring(source)
        self.assertGreater(sum(1 for _ in root.iterfind('*')), 1)

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
                # 'deviceName': 'Device',
                'deviceName': 'Emulator',
                'debugCodedUI': False,
                'locale': 'en-US',
            })

    def tearDown(self):
        self.driver.quit()

    def test_sample(self):
        # AutomationId for tiles can not be used to find tile directly,
        # but can be used to launch apps by switching to window
        # Actula tile_id is very very very long
        # {36F9FA1C-FDAD-4CF0-99EC-C03771ED741A}:x36f9fa1cyfdady4cf0y99ecyc03771ed741ax:Microsoft.MSCalendar_8wekyb3d8bbwe!x36f9fa1cyfdady4cf0y99ecyc03771ed741ax
        # but all we care about is part after last colon
        self.driver.switch_to.window('_:_:Microsoft.MSCalendar_8wekyb3d8bbwe!x36f9fa1cyfdady4cf0y99ecyc03771ed741ax')

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
        subject.send_keys(u'Winium Coded UI Demo')
        sleep(1)

        # we should have searched for LocationFiled using name or something, but Calendar app uses slightly different
        # classes for location filed in 8.1 and 8.1 Update, searching by class works on both
        location = self.driver.find_elements_by_class_name('TextBox')[1]
        location.send_keys(u'Your computer')
        sleep(1)

        save_btn = find_element(self.driver, By.NAME, "save")

        save_btn.click()
        sleep(2)

        self.driver.close()  # we can close last app opened using switch_to_window


if __name__ == "__main__":
    unittest.main()