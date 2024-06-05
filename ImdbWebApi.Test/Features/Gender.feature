Feature: Gender Resource

Scenario: Get all genders
	Given I am a client
	When I make a GET Request to '/genders'
	Then response code must be '200'
	And response data must look like 'GenderResponseData/getAllGenders.json'


Scenario: Get gender by id when gender with given id exists
	Given I am a client
	When I make a GET Request to '/genders/1'
	Then response code must be '200'
	And response data must look like 'GenderResponseData/getGenderByIdWhenGivenGenderExists.json'


Scenario: Get gender by id when gender with given id does not exits
	Given I am a client
	When I make a GET Request to '/genders/4'
	Then response code must be '404'
	And response data must look like 'GenderResponseData/getGenderByIdWhenGivenGenderDoesnotExists.json'


Scenario: Add gender with all valid inputs
	Given I am a client
	When I make a POST Request to '/genders' with the following Data 'GenderRequestData/addGenderWithAllValidDetails.json'
	Then response code must be '201'
	And response data must look like 'GenderResponseData/addGenderWithAllValidDetails.json'


Scenario Outline: Add gender with invalid inputs
	Given I am a client
	When I make a POST Request to '/genders' with the following Data 'GenderRequestData/<GenderRequestData>'
	Then response code must be '<StatusCode>'
	And response data must look like 'GenderResponseData/<GenderResponseData>'
Examples: 
	| GenderRequestData                           | StatusCode | GenderResponseData                          |
	| addGenderWithEmptyName.json                 | 400        | addGenderWithEmptyName.json                 |
	| addGenderWithNameLessThanMinCharacters.json | 400        | addGenderWithNameLessThanMinCharacters.json |
	| addGenderWithNameMoreThanMaxCharacters.json | 400        | addGenderWithNameMoreThanMaxCharacters.json |


Scenario: Update gender with all valid inputs
	Given I am a client
	When I make a PUT Request to '/genders/1' with the following Data 'GenderRequestData/updateGenderWithAllValidDetails.json'
	Then response code must be '200'
	And response data must look like 'GenderResponseData/updateGenderWithAllValidDetails.json'

Scenario Outline: Update gender with invalid inputs
	Given I am a client
	When I make a PUT Request to '/genders/<Id>' with the following Data 'GenderRequestData/<GenderRequestData>'
	Then response code must be '<StatusCode>'
	And response data must look like 'GenderResponseData/<GenderResponseData>'
Examples: 
	| Id | GenderRequestData                              | StatusCode | GenderResponseData                             |
	| 4  | updateGenderWhenGivenGenderDoesnotExists.json  | 404        | updateGenderWhenGivenGenderDoesnotExists.json  |
	| 1  | updateGenderWithEmptyName.json                 | 400        | updateGenderWithEmptyName.json                 |
	| 1  | updateGenderWithNameLessThanMinCharacters.json | 400        | updateGenderWithNameLessThanMinCharacters.json |
	| 1  | updateGenderWithNameMoreThanMaxCharacters.json | 400        | updateGenderWithNameMoreThanMaxCharacters.json |


Scenario: Delete gender when the gender with given id exists and is not linked
	Given I am a client
	When I make a DELETE Request to '/genders/2'
	Then response code must be '200'
	And response data must look like 'GenderResponseData/deleteGenderWhenGivenGenderExistsAndNotLinked.json'


Scenario: Delete gender when the gender with given id exists and is linked
	Given I am a client
	When I make a DELETE Request to '/genders/1'
	Then response code must be '400'
	And response data must look like 'GenderResponseData/deleteGenderWhenGivenGenderExistsAndLinked.json'


Scenario: Delete gender when the gender with given id does not exists
	Given I am a client
	When I make a DELETE Request to '/genders/4'
	Then response code must be '404'
	And response data must look like 'GenderResponseData/deleteGenderWhenGivenGenderExists.json'
