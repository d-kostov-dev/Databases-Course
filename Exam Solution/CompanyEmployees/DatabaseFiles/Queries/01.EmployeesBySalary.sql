--1. Get the full name (first name + “ “ + last name)  of every employee and its salary, 
--for each employee with salary between $100 000 and $150 000, inclusive. 
--Sort the results by salary in ascending order.

USE [CompanyEmployees]
GO

SELECT FirstName + ' ' + LastName AS FullName, Salary
FROM Employees
WHERE Salary > 100000
AND Salary < 150000
ORDER BY Salary