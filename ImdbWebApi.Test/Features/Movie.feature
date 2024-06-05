Feature: Movie Resource

Scenario: Get all movies for given year of release
	Given I am a client
	When I make a GET Request to '/movies?year=2002'
	Then response code must be '200'
	And response data must look like 'MovieResponseData/getAllMoviesGivenYear.json'


Scenario: Get movie by id when movie with given id exists
	Given I am a client
	When I make a GET Request to '/movies/1'
	Then response code must be '200'
	And response data must look like 'MovieResponseData/getMovieByIdWhenGivenMovieExists.json'


Scenario: Get movie by id when movie with given id does not exists
	Given I am a client
	When I make a GET Request to '/movies/3'
	Then response code must be '404'
	And response data must look like 'MovieResponseData/getMovieByIdWhenGivenMovieDoesnotExists.json'

Scenario: Add movie with all valid inputs
	Given I am a client
	When I make a POST Request to '/movies' with the following Data 'MovieRequestData/addMovieWithAllValidInputs.json'
	Then response code must be '201'
	And response data must look like 'MovieResponseData/addMovieWithAllValidInputs.json'


Scenario Outline: Add movie with invalid inputs
	Given I am a client
	When I make a POST Request to '/movies' with the following Data 'MovieRequestData/<MovieRequestData>'
	Then response code must be '<StatusCode>'
	And response data must look like 'MovieResponseData/<MovieResponseData>'
Examples: 
	| MovieRequestData                              | StatusCode | MovieResponseData                             |
	| addMovieWithEmptyName.json                    | 400        | addMovieWithEmptyName.json                    |
	| addMovieWithNameLessThanMinCharacters.json    | 400        | addMovieWithNameLessThanMinCharacters.json    |
	| addMovieWithNameMoreThanMaxCharacters.json    | 400        | addMovieWithNameMoreThanMaxCharacters.json    |
	| addMovieWithYearOfReleaseLessThanMinYear.json | 400        | addMovieWithYearOfReleaseLessThanMinYear.json |
	| addMovieWithYearOfReleaseMoreThanMaxYear.json | 400        | addMovieWithYearOfReleaseMoreThanMaxYear.json |
	| addMovieWithEmptyPlot.json                    | 400        | addMovieWithEmptyPlot.json                    |
	| addMovieWithPlotLessThanMinCharacters.json    | 400        | addMovieWithPlotLessThanMinCharacters.json    |
	| addMovieWithPlotMoreThanMaxCharacters.json    | 400        | addMovieWithPlotMoreThanMaxCharacters.json    |
	| addMovieWithInvalidProducerId.json            | 404        | addMovieWithInvalidProducerId.json            |
	| addMovieWithNoActors.json                     | 400        | addMovieWithNoActors.json                     |
	| addMovieWithInvalidActorIds.json              | 404        | addMovieWithInvalidActorIds.json              |
	| addMovieWithNoGenres.json                     | 400        | addMovieWithNoGenres.json                     |
	| addMovieWithInvalidGenreIds.json              | 404        | addMovieWithInvalidGenreIds.json              |
	| addMovieWithEmptyCoverImage.json              | 400        | addMovieWithEmptyCoverImage.json              |


Scenario: Update movie with all valid inputs
	Given I am a client
	When I make a PUT Request to '/movies/1' with the following Data 'MovieRequestData/updateMovieWithAllValidInputs.json'
	Then response code must be '200'
	And response data must look like 'MovieResponseData/updateMovieWithAllValidInputs.json'


Scenario Outline: Update movie with invalid inputs
	Given I am a client
	When I make a PUT Request to '/movies/<Id>' with the following Data 'MovieRequestData/<MovieRequestData>'
	Then response code must be '<StatusCode>'
	And response data must look like 'MovieResponseData/<MovieResponseData>'
Examples: 
	| Id | MovieRequestData                                 | StatusCode | MovieResponseData                                |
	| 3  | updateMovieWhenGivenMovieDoesnotExists.json      | 404        | updateMovieWhenGivenMovieDoesnotExists.json      |
	| 1  | updateMovieWithEmptyName.json                    | 400        | updateMovieWithEmptyName.json                    |
	| 1  | updateMovieWithNameLessThanMinCharacters.json    | 400        | updateMovieWithNameLessThanMinCharacters.json    |
	| 1  | updateMovieWithNameMoreThanMaxCharacters.json    | 400        | updateMovieWithNameMoreThanMaxCharacters.json    |
	| 1  | updateMovieWithYearOfReleaseLessThanMinYear.json | 400        | updateMovieWithYearOfReleaseLessThanMinYear.json |
	| 1  | updateMovieWithYearOfReleaseMoreThanMaxYear.json | 400        | updateMovieWithYearOfReleaseMoreThanMaxYear.json |
	| 1  | updateMovieWithEmptyPlot.json                    | 400        | updateMovieWithEmptyPlot.json                    |
	| 1  | updateMovieWithPlotLessThanMinCharacters.json    | 400        | updateMovieWithPlotLessThanMinCharacters.json    |
	| 1  | updateMovieWithPlotMoreThanMaxCharacters.json    | 400        | updateMovieWithPlotMoreThanMaxCharacters.json    |
	| 1  | updateMovieWithInvalidProducerId.json            | 404        | updateMovieWithInvalidProducerId.json            |
	| 1  | updateMovieWithNoActors.json                     | 400        | updateMovieWithNoActors.json                     |
	| 1  | updateMovieWithInvalidActorIds.json              | 404        | updateMovieWithInvalidActorIds.json              |
	| 1  | updateMovieWithNoGenres.json                     | 400        | updateMovieWithNoGenres.json                     |
	| 1  | updateMovieWithInvalidGenreIds.json              | 404        | updateMovieWithInvalidGenreIds.json              |
	| 1  | updateMovieWithEmptyCoverImage.json              | 400        | updateMovieWithEmptyCoverImage.json              |
	


Scenario: Delete movie when movie with given id exists and is not linked
	Given I am a client
	When I make a DELETE Request to '/movies/2'
	Then response code must be '200'
	And response data must look like 'MovieResponseData/deleteMovieWhenGivenMovieExistsAndNotLinked.json'


Scenario: Delete movie when movie with given id exists and is linked
	Given I am a client
	When I make a DELETE Request to '/movies/1'
	Then response code must be '400'
	And response data must look like 'MovieResponseData/deletedMovieWhenGivenMovieExistsAndLinked.json'


Scenario: Delete movie when movie with given id does not exists
	Given I am a client
	When I make a DELETE Request to '/movies/3'
	Then response code must be '404'
	And response data must look like 'MovieResponseData/deletedMovieWhenGivenMovieDoesnotExists.json'
