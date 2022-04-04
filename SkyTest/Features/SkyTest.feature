Feature: This feature will make sure that the shop page is navigable and usable.

@skytest_Deals
Scenario:  User navigates to shop page
	Given  I am on the home page
	When I navigate to ‘Deals’
	Then the user should be on the https://www.sky.com/deals page

@skytest_Login
Scenario Outline: User sees tiles on the shop page
Given I am on the home page
When I try to sign in with invalid <Email>
Then I should see a box with the text ‘Create your My Sky password’

Examples:
| Email       |
| xopx@yyy.com |
@skytest_dealsprice
Scenario: User sees a list of deals on the deals page
Given I am on the ‘https://www.sky.com/deals‘ page
Then I see a list of deals with a price to it 


@search
Scenario: The search show the results that are determined by editorial, as well
as generic searches.
Given I am on the home page
When I search <Searchdata> in the search bar
Then I should see <Searchdata> an editorial section. 

Examples:
| Searchdata |
| sky        |