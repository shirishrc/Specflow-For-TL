Feature: APITests - ISS Tles

Background: 
	 
	 Given I have executed GET request for BaseURL and status should be OK and response should contain Name and ID in response

    Scenario: ISS Satellite by Tles with JSON format response
		 
		Given I have called the Satellite API by "IDTles"
		When response status is OK
		Then response should contain TLE data
		
	Scenario: ISS Satellite by Tles with Text Format
		 
		Given I have called the Satellite API by "IDTles-TextParameter"
		When response status is OK
		Then response should contain TLE data in text format
	