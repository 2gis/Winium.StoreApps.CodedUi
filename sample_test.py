# coding: utf-8
import unittest
from selenium import webdriver


def sample():
    driver = webdriver.Remote(
        command_executor='http://localhost:9999',
        desired_capabilities={
            'deviceName': 'iPhone 6',
        })

    text_box = driver.find_element_by_tag_name("TextBox")
    print text_box.text

    button = driver.find_element_by_id("MagicId")
    button.click()

    print text_box.text

    driver.close()

if __name__ == "__main__":
    sample()