﻿Feature: Login

This is Login file for http://eaapp.somee.com/

@login
Scenario: This is login with valid credentials
	Given I am on th login page
	When I click on the login link
	And I enter username as "pirorox107@lesotica.com" and password as "Test@1234"
	And I clicked on login button
	Then I should be looged in
	Then I clicked on logout