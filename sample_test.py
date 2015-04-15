# coding: utf-8
from time import sleep
import unittest
from selenium import webdriver
from selenium.common.exceptions import TimeoutException
from selenium.webdriver.common.by import By
from selenium.webdriver.support.wait import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC


def find_element(driver, by, value):
    """
    :rtype: selenium.webdriver.remote.webelement.WebElement
    """
    return WebDriverWait(driver, 10).until(EC.presence_of_element_located((by, value)))


class Test(unittest.TestCase):
    def setUp(self):
        self.driver = webdriver.Remote(
            command_executor='http://localhost:9999',
            desired_capabilities={
                'deviceName': 'Emulator',
                'debugCodedUI': False,
                'deviceIpAddress': '10.54.7.139',
                'locale': 'en-US'
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

        # accept permisson alert if any
        try:
            accept_btn = find_element(self.driver, By.NAME, "allow")
            accept_btn.click()
        except TimeoutException:
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

    def test_winium_test_app(self):
        text_box = self.driver.find_element_by_tag_name("TextBox")
        print text_box.text

        button = self.driver.find_element_by_id("MagicId")
        button.click()

        print text_box.text

        buttons = self.driver.find_elements_by_class_name("TextBox")
        print [b.text for b in buttons]

        list_view = self.driver.find_element_by_id("TopPanel")
        buttons = list_view.find_elements_by_class_name("TextBox")
        print [b.text for b in buttons]


if __name__ == "__main__":
    unittest.main()