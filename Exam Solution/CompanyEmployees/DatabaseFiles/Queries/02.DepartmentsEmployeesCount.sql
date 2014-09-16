--2. Get all departments and how many employees there are in each one. 
--Sort the result by the number of employees in descending order.

USE [CompanyEmployees]
GO

SELECT d.Name, COUNT(e.Id) AS EmployeesCount
FROM Departments d
	INNER JOIN Employees e
	ON d.Id = e.DepartmentId
GROUP BY d.Name
ORDER BY EmployeesCount DESC

--Version 2
USE [CompanyEmployees]
GO

SELECT d.Name, (SELECT COUNT(*) FROM Employees WHERE DepartmentId = d.Id) AS EmployeesCount
FROM Departments d
ORDER BY EmployeesCount DESC
