Feature: Producer Resource

Scenario: Get all producers
	Given I am a client
	When I make a GET Request to '/producers'
	Then response code must be '200'
	And response data must look like 'ProducerResponseData/getAllProducers.json'


Scenario: Get producer by id when producer with given id exists
	Given I am a client
	When I make a GET Request to '/producers/1'
	Then response code must be '200'
	And response data must look like 'ProducerResponseData/getProducerByIdWhenGivenProducerExists.json'


Scenario: Get producer by id when producer with given id does not exists
	Given I am a client
	When I make a GET Request to '/producers/4'
	Then response code must be '404'
	And response data must look like 'ProducerResponseData/getProducerByIdWhenGivenProducerDoesnotExists.json'


Scenario: Add producer with all valid inputs
	Given I am a client
	When I make a POST Request to '/producers' with the following Data 'ProducerRequestData/addProducerWithAllValidDetails.json'
	Then response code must be '201'
	And response data must look like 'ProducerResponseData/addProducerWithAllValidDetails.json'


Scenario Outline: Add producer with invalid inputs
	Given I am a client
	When I make a POST Request to '/producers' with the following Data 'ProducerRequestData/<ProducerRequestData>'
	Then response code must be '<StatusCode>'
	And response data must look like 'ProducerResponseData/<ProducerResponseData>'
Examples: 
	| ProducerRequestData                           | StatusCode | ProducerResponseData                          |
	| addProducerWithEmptyName.json                 | 400        | addProducerWithEmptyName.json                 |
	| addProducerWithNameLessThanMinCharacters.json | 400        | addProducerWithNameLessThanMinCharacters.json |
	| addProducerWithNameMoreThanMaxCharacters.json | 400        | addProducerWithNameMoreThanMaxCharacters.json |
	| addProducerWithEmptyBio.json                  | 400        | addProducerWithEmptyBio.json                  |
	| addProducerWithBioLessThanMinCharacters.json  | 400        | addProducerWithBioLessThanMinCharacters.json  |
	| addProducerWithBioMoreThanMaxCharacters.json  | 400        | addProducerWithBioMoreThanMaxCharacters.json  |
	| addProducerWithInvalidDateFormatForDOB.json   | 400        | addProducerWithInvalidDateFormatForDOB.json   |
	| addProducerWithMaxDOB.json                    | 400        | addProducerWithMaxDOB.json                    |
	| addProducerWithInvalidGenderId.json           | 404        | addProducerWithInvalidGenderId.json           |

Scenario: Update producer with all valid inputs
	Given I am a client
	When I make a PUT Request to '/producers/1' with the following Data 'ProducerRequestData/updateProducerWithAllValidDetails.json'
	Then response code must be '200'
	And response data must look like 'ProducerResponseData/updateProducerWithAllValidDetails.json'


Scenario Outline: Update producer with invalid inputs
	Given I am a client
	When I make a PUT Request to '/producers/<Id>' with the following Data 'ProducerRequestData/<ProducerRequestData>'
	Then response code must be '<StatusCode>'
	And response data must look like 'ProducerResponseData/<ProducerResponseData>'
Examples: 
	| Id | ProducerRequestData                               | StatusCode | ProducerResponseData                              |
	| 4  | updateProducerWhenGivenProducerDoesnotExists.json | 404        | updateProducerWhenGivenProducerDoesnotExists.json |
	| 1  | updateProducerWithEmptyName.json                  | 400        | updateProducerWithEmptyName.json                  |
	| 1  | updateProducerWithNameLessThanMinCharacters.json  | 400        | updateProducerWithNameLessThanMinCharacters.json  |
	| 1  | updateProducerWithNameMoreThanMaxCharacters.json  | 400        | updateProducerWithNameMoreThanMaxCharacters.json  |
	| 1  | updateProducerWithEmptyBio.json                   | 400        | updateProducerWithEmptyBio.json                   |
	| 1  | updateProducerWithBioLessThanMinCharacters.json   | 400        | updateProducerWithBioLessThanMinCharacters.json   |
	| 1  | updateProducerWithBioMoreThanMaxCharacters.json   | 400        | updateProducerWithBioMoreThanMaxCharacters.json   |
	| 1  | updateProducerWithInvalidDateFormatForDOB.json    | 400        | updateProducerWithInvalidDateFormatForDOB.json    |
	| 1  | updateProducerWithMaxDOB.json                     | 400        | updateProducerWithMaxDOB.json                     |
	| 1  | updateProducerWithInvalidGenderId.json            | 404        | updateProducerWithInvalidGenderId.json            |


Scenario: Delete producer when producer with given id exists and is not linked
	Given I am a client
	When I make a DELETE Request to '/producers/3'
	Then response code must be '200'
	And response data must look like 'ProducerResponseData/deleteProducerWhenGivenProducerExistsAndNotLinked.json'


Scenario: Delete producer when producer with given id exists and is linked
	Given I am a client
	When I make a DELETE Request to '/producers/1'
	Then response code must be '400'
	And response data must look like 'ProducerResponseData/deleteProducerWhenGivenProducerExistsAndLinked.json'

Scenario: Delete producer when producer with given id does not exists
	Given I am a client
	When I make a DELETE Request to '/producers/4'
	Then response code must be '404'
	And response data must look like 'ProducerResponseData/deleteProducerWhenGivenProducerDoesnotExists.json'