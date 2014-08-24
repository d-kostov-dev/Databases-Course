SELECT e.FirstName, m.FirstName
FROM Employees m
RIGHT JOIN Employees e
	ON e.ManagerID = m.EmployeeID

SELECT e.FirstName, m.FirstName
FROM Employees e
LEFT JOIN Employees m
	ON e.ManagerID = m.EmployeeID