# CEVA
CEVA Logistics Developer Coding Test

Coding Test
Test 1: .Net Core/Spring Boot:
Create a .Net Core Web or Spring Boot REST API, which accepts the below array as JSON
request and perform the following tasks
1. Create directory “Users”, If not exist at specified path in a config file
a. Create Sub Directory “IN”.
2. Store the Request as JSON file in “IN” directory” created above

Results:
The .Net Core WebAPI project: Test1 
The .Net xUnit Test project: Test1.Tests
The test results screenshot: Test1 Test Results.jpg


Test 2: JavaScript:
Without using any FOR loops, Create a new 2D Array from the “activity[]” array below with
the Employee ID as the key and the value is an array of activities for each employee.
For this you can submit your response via. https://stackblitz.com/ and send us the url

create an empty HTML file with JavaScript function to group Employee ID from “activity[]” array: Test2.html

Test 3 SQL:
Provide the full set of SQLs and output for the following:
1) Create a new table called “model”, and insert the following records, with an
auto-incrementing key called “ID”
2) write SQL statements to remove any records with duplicate make and model
combinations from the table while keeping the record with the highest ID.

Create sql file for this question: Test3.sql
1) Create table query , and insert into query
2) use select Max(ID) group by Make, Model to get all highest ID of each make and model combination, then delete the ID not in the select result