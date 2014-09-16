--3. Get each employee’s full name (first name + “ “ + last name), 
--project’s name, 
--department’s name, 
--starting and ending date for each employee in project. 
--Additionally get the number of all reports, which time of reporting is between the start and end date. 
--Sort the results first by the employee id, then by the project id. (This query is slow, be patient!)

USE [CompanyEmployees]
GO

SELECT 
	e.FirstName + ' ' + e.LastName AS FullName, 
	p.Name AS ProjectName, 
	d.Name AS DepartmentName, 
	ep.StartDate, 
	ep.EndDate,
	SUBSTRING((SELECT ',' +  CONVERT(nvarchar, r.Id) 
	FROM Reports r
	WHERE r.EmployeeId = e.Id
	AND r.ReportDate > ep.StartDate
	AND r.ReportDate < ep.EndDate
	FOR XML PATH('')), 2, 8000) AS Reports
FROM Employees e
	INNER JOIN EmployeeProjects ep
	ON e.Id = ep.EmployeeId
	INNER JOIN Projects p
	ON ep.ProjectId = p.Id
	INNER JOIN Departments d
	ON e.DepartmentId = d.Id
ORDER BY e.Id, p.Id
