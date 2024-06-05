Feature: Genre Resource

Scenario: Get all genres
	Given I am a client
	When I make a GET Request to '/genres'
	Then response code must be '200'
	And response data must look like 'GenreResponseData/getAllGenres.json'


Scenario: Get genre by id when genre with given id exists
	Given I am a client
	When I make a GET Request to '/genres/1'
	Then response code must be '200'
	And response data must look like 'GenreResponseData/getGenreByIdWhenGivenGenreExists.json'


Scenario: Get genre by id when genre with given id does not exits
	Given I am a client
	When I make a GET Request to '/genres/4'
	Then response code must be '404'
	And response data must look like 'GenreResponseData/getGenreByIdWhenGivenGenreDoesnotExists.json'


Scenario: Add genre with all valid inputs
	Given I am a client
	When I make a POST Request to '/genres' with the following Data 'GenreRequestData/addGenreWithAllValidDetails.json'
	Then response code must be '201'
	And response data must look like 'GenreResponseData/addGenreWithAllValidDetails.json'


Scenario Outline: Add genre with invalid inputs
	Given I am a client
	When I make a POST Request to '/genres' with the following Data 'GenreRequestData/<GenreRequestData>'
	Then response code must be '<StatusCode>'
	And response data must look like 'GenreResponseData/<GenreResponseData>'
Examples: 
	| GenreRequestData                           | StatusCode | GenreResponseData                          |
	| addGenreWithEmptyName.json                 | 400        | addGenreWithEmptyName.json                 |
	| addGenreWithNameLessThanMinCharacters.json | 400        | addGenreWithNameLessThanMinCharacters.json |
	| addGenreWithNameMoreThanMaxCharacters.json | 400        | addGenreWithNameMoreThanMaxCharacters.json |


Scenario: Update genre with all valid inputs
	Given I am a client
	When I make a PUT Request to '/genres/1' with the following Data 'GenreRequestData/updateGenreWithAllValidDetails.json'
	Then response code must be '200'
	And response data must look like 'GenreResponseData/updateGenreWithAllValidDetails.json'


Scenario Outline: Update genre with invalid inputs
	Given I am a client
	When I make a PUT Request to '/genres/<Id>' with the following Data 'GenreRequestData/<GenreRequestData>'
	Then response code must be '<StatusCode>'
	And response data must look like 'GenreResponseData/<GenreResponseData>'
Examples: 
	| Id | GenreRequestData                              | StatusCode | GenreResponseData                             |
	| 4  | updateGenreWhenGivenGenreDoesnotExists.json   | 404        | updateGenreWhenGivenGenreDoesnotExists.json   |
	| 1  | updateGenreWithEmptyName.json                 | 400        | updateGenreWithEmptyName.json                 |
	| 1  | updateGenreWithNameLessThanMinCharacters.json | 400        | updateGenreWithNameLessThanMinCharacters.json |
	| 1  | updateGenreWithNameMoreThanMaxCharacters.json | 400        | updateGenreWithNameMoreThanMaxCharacters.json |


Scenario: Delete genre when the genre with given id exists and is not linked
	Given I am a client
	When I make a DELETE Request to '/genres/3'
	Then response code must be '200'
	And response data must look like 'GenreResponseData/deleteGenreWhenGivenGenreExistsAndNotLinked.json'


Scenario: Delete genre when the genre with given id exists and is linked
	Given I am a client
	When I make a DELETE Request to '/genres/1'
	Then response code must be '400'
	And response data must look like 'GenreResponseData/deleteGenreWhenGivenGenreExistsAndLinked.json'


Scenario: Delete genre when the genre with given id does not exists
	Given I am a client
	When I make a DELETE Request to '/genres/4'
	Then response code must be '404'
	And response data must look like 'GenreResponseData/deleteGenreWhenGivenGenreExists.json'
