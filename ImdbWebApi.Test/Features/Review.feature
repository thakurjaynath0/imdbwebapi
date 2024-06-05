Feature: Review Resource

Scenario: Get all reviews when given movie exists
	Given I am a client
	When I make a GET Request to '/movies/1/reviews'
	Then response code must be '200'
	And response data must look like 'ReviewResponseData/getAllReviewsWhenGivenMovieExists.json'


Scenario: Get all reviews when given movie does not exists
	Given I am a client
	When I make a GET Request to '/movies/3/reviews'
	Then response code must be '404'
	And response data must look like 'ReviewResponseData/getAllReviewsWhenGivenMovieDoesnotExists.json'


Scenario: Get review by id when given movie and review exists
	Given I am a client
	When I make a GET Request to '/movies/1/reviews/1'
	Then response code must be '200'
	And response data must look like 'ReviewResponseData/getReviewByIdWhenGivenMovieAndReviewExists.json'


Scenario: Get review by id when given movie does not exists
	Given I am a client
	When I make a GET Request to '/movies/3/reviews/1'
	Then response code must be '404'
	And response data must look like 'ReviewResponseData/getReviewByIdWhenGivenMovieDoesnotExists.json'


Scenario: Get review by id when given review does not exists
	Given I am a client
	When I make a GET Request to '/movies/1/reviews/2'
	Then response code must be '404'
	And response data must look like 'ReviewResponseData/getReviewByIdWhenGivenReviewDoesnotExists.json'


Scenario: Add reviews with all valid inputs
	Given I am a client
	When I make a POST Request to '/movies/1/reviews' with the following Data 'ReviewRequestData/addReviewWithAllValidInputs.json'
	Then response code must be '201'
	And response data must look like 'ReviewResponseData/addReviewWithAllValidInputs.json'


Scenario Outline: Add reviews with invalid inputs
	Given I am a client
	When I make a POST Request to '/movies/<MovieId>/reviews' with the following Data 'ReviewRequestData/<ReviewRequestData>'
	Then response code must be '<StatusCode>'
	And response data must look like 'ReviewResponseData/<ReviewResponseData>'
Examples: 
	| MovieId | ReviewRequestData                              | StatusCode | ReviewResponseData                             |
	| 3       | addReviewWhenGivenMovieDoesnotExists.json      | 404        | addReviewWhenGivenMovieDoesnotExists.json      |
	| 1       | addReviewWithEmptyMessage.json                 | 400        | addReviewWithEmptyMessage.json                 |
	| 1       | addReviewWithMessageLessThanMinCharacters.json | 400        | addReviewWithMessageLessThanMinCharacters.json |
	| 1       | addReviewWithMessageMoreThanMaxCharacters.json | 400        | addReviewWithMessageMoreThanMaxCharacters.json |


Scenario: Update reviews with all valid inputs
	Given I am a client
	When I make a PUT Request to '/movies/1/reviews/1' with the following Data 'ReviewRequestData/updateReviewWithAllValidInputs.json'
	Then response code must be '200'
	And response data must look like 'ReviewResponseData/updateReviewWithAllValidInputs.json'


Scenario Outline: Update reviews with invalid inputs
	Given I am a client
	When I make a PUT Request to '/movies/<MovieId>/reviews/<Id>' with the following Data 'ReviewRequestData/<ReviewRequestData>'
	Then response code must be '<StatusCode>'
	And response data must look like 'ReviewResponseData/<ReviewResponseData>'
Examples: 
	| MovieId | Id | ReviewRequestData                                 | StatusCode | ReviewResponseData                                |
	| 3       | 1  | updateReviewWhenGivenMovieDoesnotExists.json      | 404        | updateReviewWhenGivenMovieDoesnotExists.json      |
	| 1       | 2  | updateReviewWhenGivenReviewDoesnotExists.json     | 404        | updateReviewWhenGivenReviewDoesnotExists.json     |
	| 1       | 1  | updateReviewWithEmptyMessage.json                 | 400        | updateReviewWithEmptyMessage.json                 |
	| 1       | 1  | updateReviewWithMessageLessThanMinCharacters.json | 400        | updateReviewWithMessageLessThanMinCharacters.json |
	| 1       | 1  | updateReviewWithMessageMoreThanMaxCharacters.json | 400        | updateReviewWithMessageMoreThanMaxCharacters.json |


Scenario: Delete reviews when given movie and review exists
	Given I am a client
	When I make a DELETE Request to '/movies/1/reviews/1'
	Then response code must be '200'
	And response data must look like 'ReviewResponseData/deleteReviewWhenGivenMovieAndReviewExists.json'


Scenario: Delete reviews when given movie does not exists
	Given I am a client
	When I make a DELETE Request to '/movies/3/reviews/1'
	Then response code must be '404'
	And response data must look like 'ReviewResponseData/deleteReviewWhenGivenMovieDoesnotExists.json'


Scenario: Delete reviews when given review does not exists
	Given I am a client
	When I make a DELETE Request to '/movies/1/reviews/2'
	Then response code must be '404'
	And response data must look like 'ReviewResponseData/deleteReviewWhenGivenReviewDoesnotExists.json'
