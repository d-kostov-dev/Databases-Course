SELECT 
	FirstName,
	LastName,
	Salary
FROM Employees
WHERE Salary = (
	SELECT MIN(Salary)
	FROM Employees
)
--------------------------------------------
SELECT 
	FirstName,
	LastName,
	Salary
FROM Employees
WHERE Salary < (
	SELECT MIN(Salary) * 1.1
	FROM Employees
)
--------------------------------------------
SELECT e.FirstName, e.LastName, e.Salary, d.Name
FROM Employees e
 INNER JOIN Departments d
	ON e.DepartmentID = d.DepartmentID
WHERE Salary = (
	SELECT MIN(Salary)
	FROM Employees c
	WHERE c.DepartmentID = d.DepartmentID
)
--------------------------------------------
SELECT AVG(Salary)
FROM Employees
WHERE DepartmentID = 1
--------------------------------------------
SELECT AVG(e.Salary)
FROM Employees e
	INNER JOIN Departments d
		ON e.DepartmentID = d.DepartmentID
WHERE d.Name = 'Sales'
--------------------------------------------
SELECT COUNT(*)
FROM Employees e
	INNER JOIN Departments d
		ON e.DepartmentID = d.DepartmentID
WHERE d.Name = 'Sales'
--------------------------------------------
SELECT COUNT(e.ManagerID)
FROM Employees e

SELECT COUNT(*)
FROM Employees e
WHERE e.ManagerID IS NOT NULL
--------------------------------------------
SELECT COUNT(*) - COUNT(ManagerID)
FROM Employees

SELECT COUNT(*)
FROM Employees
WHERE ManagerID IS NULL
--------------------------------------------
SELECT d.Name, AVG(e.Salary) AS [Average Salary]
FROM Departments d
	INNER JOIN Employees e
		ON e.DepartmentID = d.DepartmentID
GROUP BY d.Name
--------------------------------------------
SELECT COUNT(*), d.Name, t.Name
FROM Employees e
	INNER JOIN Departments d
		ON e.DepartmentID = d.DepartmentID
	INNER JOIN Addresses a
		ON e.AddressID = a.AddressID
	INNER JOIN Towns t
		ON a.TownID = t.TownID
GROUP BY d.Name, t.Name
ORDER BY d.Name
--------------------------------------------
SELECT e.FirstName, e.LastName
FROM Employees e
WHERE (
	SELECT COUNT(*)
	FROM Employees
	WHERE ManagerID = e.EmployeeID
) = 5
--------------------------------------------
SELECT e.FirstName, ISNULL(m.FirstName, '(no manager)') AS [Manager Name]
FROM Employees e
	LEFT JOIN Employees m
		ON e.ManagerID = m.EmployeeID
--------------------------------------------
SELECT FirstName, LastName
FROM Employees
WHERE LEN(LastName) = 5
--------------------------------------------
SELECT CONVERT(varchar, GETDATE(), 113)
--------------------------------------------
CREATE TABLE Users ( 
	UserID int IDENTITY,
	UserName nvarchar(100) NOT NULL, 
	UserPassword nvarchar(20) NULL, 
	FullName nvarchar(100) NOT NULL, 
	LastLogin datetime NULL,
	CONSTRAINT PK_Users PRIMARY KEY(UserID),
	CONSTRAINT uc_UserName UNIQUE(UserName),
	CONSTRAINT lc_PasswordLength CHECK (DATALENGTH(UserPassword) > 5)
)
--------------------------------------------
CREATE VIEW [V_Users] AS 
SELECT * 
FROM Users
WHERE LastLogin > GETDATE() - 1

GO

SELECT *
FROM V_Users
--------------------------------------------
CREATE TABLE Groups ( 
	GroupID int IDENTITY,
	Name nvarchar(100) NOT NULL, 
	CONSTRAINT PK_GroupID PRIMARY KEY(GroupID),
	CONSTRAINT UC_Name UNIQUE(Name)
)
--------------------------------------------
ALTER TABLE Users ADD GroupID int
							
GO

INSERT INTO 
	Groups
VALUES 
('Administrators'),
('Writers'),
('Coders'),
('Designers')


INSERT INTO 
	Users
VALUES 
('Peshkata','123456','Pesho Goshov','',1),
('Gosho','123456','Gosho Peshov','',1),
('Kencho','123456','Kencho Ivailov','',3),
('Niksun','123456','Kosta Nikolov','',4)

GO

ALTER TABLE Users 
ADD CONSTRAINT FK_Users_Groups
	FOREIGN KEY (GroupID) 
	REFERENCES Groups(GroupID)
--------------------------------------------
BEGIN TRANSACTION
	ALTER TABLE Users
	DROP CONSTRAINT uc_UserName

	INSERT INTO Users(UserName, UserPassword, FullName)
	SELECT 
		SUBSTRING(FirstName, 1, 1 ) + LastName AS UserName,
		SUBSTRING(FirstName, 1, 1) + LastName AS UserPassword,
		FirstName + ' ' + LastName AS FullName
	FROM Employees
COMMIT
--------------------------------------------
ALTER TABLE Users 
ALTER COLUMN UserPassword NVARCHAR(50) NULL

GO

UPDATE Users
SET UserPassword = NULL
WHERE LastLogin < CONVERT(datetime, '10.03.2010')
OR LastLogin IS NULL
--------------------------------------------
DELETE FROM Users
WHERE UserPassword IS NULL
--------------------------------------------
SELECT AVG(e.Salary), e.JobTitle, d.Name
FROM Employees e
	INNER JOIN Departments d
		ON e.DepartmentID = d.DepartmentID
GROUP BY d.Name, e.JobTitle
ORDER BY d.Name
--------------------------------------------
SELECT MIN(e.Salary), e.JobTitle, d.Name, e.FirstName
FROM Employees e
	INNER JOIN Departments d
		ON e.DepartmentID = d.DepartmentID
GROUP BY e.FirstName, e.JobTitle, d.Name
ORDER BY d.Name
--------------------------------------------
SELECT TOP 1 COUNT(*) AS EmpCount, t.Name
FROM Employees e
	INNER JOIN Addresses a
		ON e.AddressID = a.AddressID
	INNER JOIN Towns t
		ON a.TownID = t.TownID
GROUP BY t.Name
ORDER BY EmpCount DESC
--------------------------------------------
CREATE TABLE WorkHours ( 
	ID int IDENTITY NOT NULL,
	EmpID int NOT NULL, 
	Task nvarchar(100) NOT NULL, 
	Comments ntext NULL, 
	TaskHours int NULL,
	TaskDate date NULL
	CONSTRAINT PK_WorkHoursID 
		PRIMARY KEY(ID)
	CONSTRAINT FK_WorkHours_Users
		FOREIGN KEY (EmpID) 
		REFERENCES Employees(EmployeeID)
)

CREATE TABLE WorkHoursLogs ( 
	ID int IDENTITY NOT NULL, 
	OldState ntext NULL, 
	NewState ntext NULL,
	Command nvarchar(10)
	CONSTRAINT PK_LogId
		PRIMARY KEY(ID)
)

CREATE TRIGGER tr_WorkHoursUpdate 
	ON WorkHours
	FOR INSERT, UPDATE
AS
	DECLARE @TaskID int;
	SET @TaskID = (SELECT i.ID FROM inserted i)

	INSERT INTO WorkHoursLogs 
		(OldState, NewState)
	VALUES (
		(SELECT 
			CONVERT(nvarchar, ID) + '\n' +
			CONVERT(nvarchar, EmpID) + '\n' +
			Task + '\n' +
			CONVERT(nvarchar, TaskDate) + '\n' +
			CONVERT(nvarchar, TaskHours) + '\n' AS OldState
		FROM deleted
		WHERE ID = @TaskID),

		(SELECT 
			CONVERT(nvarchar, ID) + '\n' +
			CONVERT(nvarchar, EmpID) + '\n' +
			Task + '\n' +
			CONVERT(nvarchar, TaskDate) + '\n' +
			CONVERT(nvarchar, TaskHours) + '\n' AS NewState
		FROM inserted)

	)
	
CREATE TRIGGER tr_WorkHoursDelete
	ON WorkHours
	FOR DELETE
AS
	DECLARE @TaskID int;
	SET @TaskID = (SELECT i.ID FROM inserted i)

	INSERT INTO WorkHoursLogs 
		(OldState)
	VALUES (
		(SELECT 
			CONVERT(nvarchar, ID) + '\n' +
			CONVERT(nvarchar, EmpID) + '\n' +
			Task + '\n' +
			CONVERT(nvarchar, TaskDate) + '\n' +
			CONVERT(nvarchar, TaskHours) + '\n' AS OldState
		FROM WorkHours
		WHERE ID = @TaskID)
	)
	
INSERT INTO
	WorkHours
VALUES
	(1,'Pesho','Pesho text',5,GETDATE())

UPDATE 
	WorkHours
SET 
	Task = 'Ivan'
WHERE 
	Task = 'Pesho'
--------------------------------------------
BEGIN TRAN DeleteEmployees
 
	DECLARE @DepID int 
	DECLARE @ManagerId int

	SET @DepID = (
		SELECT DepartmentID FROM Departments
		WHERE Name = 'Sales'
	)

	SET @ManagerId = (
		SELECT ManagerID
		FROM Departments WHERE DepartmentID = @DepID
	)
 
	DELETE FROM 
		Employees
	WHERE 
		DepartmentID = @DepID 
	AND 
		EmployeeID != @ManagerId
 
ROLLBACK TRAN DeleteEmployees
--------------------------------------------
BEGIN TRAN
 
	DROP TABLE EmployeesProjects
 
ROLLBACK TRAN
--------------------------------------------
BEGIN TRAN
 
	CREATE TABLE #TempProjects(
			EmployeeID int NOT NULL,
			ProjectID int NOT NULL,
			PRIMARY KEY (EmployeeID, ProjectID)
	)
	 
	GO
	 
	INSERT INTO #TempProjects 
		(EmployeeID, ProjectID)
	SELECT EmployeeID, ProjectID 
	FROM EmployeesProjects
	 
	SELECT * 
	FROM #TempProjects
	 
	DROP TABLE EmployeesProjects
	 
	GO
	 
	CREATE TABLE EmployeesProjects(
			EmployeeID int NOT NULL,
			ProjectID int NOT NULL,
			PRIMARY KEY (EmployeeID, ProjectID)
	)
	 
	GO
	 
	INSERT INTO EmployeesProjects 
		(EmployeeID, ProjectID)
	SELECT EmployeeID, ProjectID 
	FROM #TempProjects
	 
	SELECT EmployeeID, ProjectID 
	FROM EmployeesProjects

COMMIT TRANSACTION