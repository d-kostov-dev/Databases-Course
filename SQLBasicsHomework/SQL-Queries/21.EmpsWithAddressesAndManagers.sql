SELECT e.FirstName + ' ' +e.LastName AS EmpName, a.AddressText, m.FirstName + ' '+ m.LastName AS MenagerName
FROM Employees e 
INNER JOIN Employees m 
	ON e.ManagerID = m.EmployeeID
INNER JOIN Addresses a
	ON e.AddressID = a.AddressID