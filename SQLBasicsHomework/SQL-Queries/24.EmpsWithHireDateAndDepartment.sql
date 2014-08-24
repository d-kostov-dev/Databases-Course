SELECT e.FirstName, e.LastName, e.HireDate, d.Name
FROM Employees e
INNER JOIN Departments d
	ON e.DepartmentID = d.DepartmentID
		WHERE d.Name = 'Sales' 
		OR d.Name = 'Finance'
        AND YEAR(e.HireDate) >= 1995
		AND YEAR(e.HireDate) <= 2005