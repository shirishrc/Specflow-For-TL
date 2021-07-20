Feature: APITests - ISS Position(s)

Background: 
	 
	Given I have executed GET request for BaseURL and status should be OK and response should contain Name and ID in response
	 
	Scenario: ISS Satellite by ID and Position and 1 timestamps and no units
		 
		 Given I have called the Satellite Position API with paramters "ID&Positions" and 1 Timestamp(s)
		 When response status for Position API is "OK"
		 Then response should contain 1 Position(s) data

	Scenario: ISS Satellite by ID and Position and 2 timestamps and no units
		 
		 Given I have called the Satellite Position API with paramters "ID&Positions" and 2 Timestamp(s)
		 When response status for Position API is "OK"
		 Then response should contain 2 Position(s) data

	Scenario: ISS Satellite by ID and Position and 9 timestamps and units as kilometers
		 
		 Given I have called the Satellite Position API with paramters "ID&Positions&kilometers" and 9 Timestamp(s)
		 When response status for Position API is "OK"
		 Then response should contain 9 Position(s) data

	Scenario: ISS Satellite by ID and Position and 10 timestamps and units as miles
		 
		 Given I have called the Satellite Position API with paramters "ID&Positions&miles" and 10 Timestamp(s)
		 When response status for Position API is "OK"
		 Then response should contain 10 Position(s) data


	Scenario: ISS Satellite by ID and Position and 11 timestamps and units as miles
		 description: (This should not return responses as it is more than 10 10 timestamps)
		 Given I have called the Satellite Position API with paramters "ID&Positions&miles" and 11 Timestamp(s)
		 When response status for Position API is "BadRequest"
		 Then response should contain 0 Position(s) data
	
	Scenario: ISS Satellite by ID and Postition and NoTimestamps	 
		 Given I have called the Satellite Position API with paramters "ID&Positions" and 0 Timestamp(s)
		 When response status for Position API is "BadRequest"
		 Then response should contain 0 Position(s) data

	Scenario: ISS Satellite by ID and Postition and InvalidTimestamps	 
		 Given I have called the Satellite Position API with paramters "ID&Positions" and InvalidTimestamp(s)
		 When response status for Position API is "BadRequest"
		 Then response should contain 0 Position(s) data