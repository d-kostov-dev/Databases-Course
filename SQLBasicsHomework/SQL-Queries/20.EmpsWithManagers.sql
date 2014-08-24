SELECT e.FirstName + ' ' + e.LastName + ' is managed by ' + m.FirstName + ' ' + m.LastName AS EmpAndManager
FROM Employees e
INNER JOIN Employees m
	ON e.ManagerID = m.EmployeeID

SELECT e.FirstName, e.LastName, m.FirstName, m.LastName
FROM Employees e
INNER JOIN Employees m
	ON e.ManagerID = m.EmployeeID