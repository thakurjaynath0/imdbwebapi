Feature: Actor Resource

Scenario: Get all actors
	Given I am a client
	When I make a GET Request to '/actors'
	Then response code must be '200'
	And response data must look like 'ActorResponseData/getAllActors.json'


Scenario: Get actor by id when actor with given id exists
	Given I am a client
	When I make a GET Request to '/actors/1'
	Then response code must be '200'
	And response data must look like 'ActorResponseData/getActorByIdWhenGivenActorExists.json'


Scenario: Get actor by id when actor with given id does not exists
	Given I am a client
	When I make a GET Request to '/actors/4'
	Then response code must be '404'
	And response data must look like 'ActorResponseData/getActorByIdWhenGivenActorDoesnotExists.json'


Scenario: Add actor with all valid inputs
	Given I am a client
	When I make a POST Request to '/actors' with the following Data 'ActorRequestData/addActorWithAllValidDetails.json'
	Then response code must be '201'
	And response data must look like 'ActorResponseData/addActorWithAllValidDetails.json'


Scenario Outline: Add actor with invalid inputs
	Given I am a client
	When I make a POST Request to '/actors' with the following Data 'ActorRequestData/<ActorRequestData>'
	Then response code must be '<StatusCode>'
	And response data must look like 'ActorResponseData/<ActorResponseData>'
Examples: 
	| ActorRequestData                           | StatusCode | ActorResponseData                          |
	| addActorWithEmptyName.json                 | 400        | addActorWithEmptyName.json                 |
	| addActorWithNameLessThanMinCharacters.json | 400        | addActorWithNameLessThanMinCharacters.json |
	| addActorWithNameMoreThanMaxCharacters.json | 400        | addActorWithNameMoreThanMaxCharacters.json |
	| addActorWithEmptyBio.json                  | 400        | addActorWithEmptyBio.json                  |
	| addActorWithBioLessThanMinCharacters.json  | 400        | addActorWithBioLessThanMinCharacters.json  |
	| addActorWithBioMoreThanMaxCharacters.json  | 400        | addActorWithBioMoreThanMaxCharacters.json  |
	| addActorWithInvalidDateFormatForDOB.json   | 400        | addActorWithInvalidDateFormatForDOB.json   |
	| addActorWithMaxDOB.json                    | 400        | addActorWithMaxDOB.json                    |
	| addActorWithInvalidGenderId.json           | 404        | addActorWithInvalidGenderId.json           |

Scenario: Update actor with all valid inputs
	Given I am a client
	When I make a PUT Request to '/actors/1' with the following Data 'ActorRequestData/updateActorWithAllValidDetails.json'
	Then response code must be '200'
	And response data must look like 'ActorResponseData/updateActorWithAllValidDetails.json'


Scenario Outline: Update actor with invalid inputs
	Given I am a client
	When I make a PUT Request to '/actors/<Id>' with the following Data 'ActorRequestData/<ActorRequestData>'
	Then response code must be '<StatusCode>'
	And response data must look like 'ActorResponseData/<ActorResponseData>'
Examples: 
	| Id | ActorRequestData                              | StatusCode | ActorResponseData                             |
	| 4  | updateActorWhenGivenActorDoesnotExists.json   | 404        | updateActorWhenGivenActorDoesnotExists.json   |
	| 1  | updateActorWithEmptyName.json                 | 400        | updateActorWithEmptyName.json                 |
	| 1  | updateActorWithNameLessThanMinCharacters.json | 400        | updateActorWithNameLessThanMinCharacters.json |
	| 1  | updateActorWithNameMoreThanMaxCharacters.json | 400        | updateActorWithNameMoreThanMaxCharacters.json |
	| 1  | updateActorWithEmptyBio.json                  | 400        | updateActorWithEmptyBio.json                  |
	| 1  | updateActorWithBioLessThanMinCharacters.json  | 400        | updateActorWithBioLessThanMinCharacters.json  |
	| 1  | updateActorWithBioMoreThanMaxCharacters.json  | 400        | updateActorWithBioMoreThanMaxCharacters.json  |
	| 1  | updateActorWithInvalidDateFormatForDOB.json   | 400        | updateActorWithInvalidDateFormatForDOB.json   |
	| 1  | updateActorWithMaxDOB.json                    | 400        | updateActorWithMaxDOB.json                    |
	| 1  | updateActorWithInvalidGenderId.json           | 404        | updateActorWithInvalidGenderId.json           |


Scenario: Delete actor when actor with given id exists and is not linked
	Given I am a client
	When I make a DELETE Request to '/actors/3'
	Then response code must be '200'
	And response data must look like 'ActorResponseData/deleteActorWhenGivenActorExistsAndNotLinked.json'


Scenario: Delete actor when actor with given id exists and is linked
	Given I am a client
	When I make a DELETE Request to '/actors/2'
	Then response code must be '400'
	And response data must look like 'ActorResponseData/deleteActorWhenGivenActorExistsAndLinked.json'

Scenario: Delete actor when actor with given id does not exists
	Given I am a client
	When I make a DELETE Request to '/actors/4'
	Then response code must be '404'
	And response data must look like 'ActorResponseData/deleteActorWhenGivenActorDoesnotExists.json'