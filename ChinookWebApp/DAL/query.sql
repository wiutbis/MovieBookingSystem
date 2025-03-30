USE Chinook;
GO

SELECT EmployeeId, FirstName, LastName, Title, ReportsTo, BirthDate
FROM Employee;
GO

CREATE PROCEDURE usp_GetAllEmployees
AS
BEGIN
    SELECT EmployeeId, FirstName, LastName, Title, ReportsTo, BirthDate
    FROM Employee;
END;
GO

EXEC usp_GetAllEmployees;
GO

CREATE PROCEDURE usp_GetEmployeeById
    @EmployeeId INT
AS
BEGIN
    SELECT EmployeeId, FirstName, LastName, Title, ReportsTo, BirthDate
    FROM Employee
    WHERE EmployeeId = @EmployeeId;
END;

EXEC usp_GetEmployeeById 1;
GO

CREATE PROCEDURE usp_CreateEmployee
    @FirstName NVARCHAR(20),
    @LastName NVARCHAR(20),
    @Title NVARCHAR(30),
    @ReportsTo INT,
    @BirthDate DATETIME
AS
BEGIN
    INSERT INTO Employee
        (FirstName, LastName, Title, ReportsTo, BirthDate)
    VALUES(@FirstName, @LastName, @Title, @ReportsTo, @BirthDate);
END;
GO

EXEC usp_CreateEmployee 'John', 'Doe', 'Manager', 2, '1980-01-01';
GO

CREATE PROCEDURE usp_UpdateEmployee
    @EmployeeId INT,
    @FirstName NVARCHAR(20),
    @LastName NVARCHAR(20),
    @Title NVARCHAR(30),
    @ReportsTo INT,
    @BirthDate DATETIME
AS
BEGIN
    UPDATE Employee
    SET FirstName = @FirstName, 
        LastName = @LastName, 
        Title = @Title, 
        ReportsTo = @ReportsTo, 
        BirthDate = @BirthDate
    WHERE EmployeeId = @EmployeeId
END;
GO

EXEC usp_UpdateEmployee 12, 'Jane', 'Doe', 'Assistant Manager', 2, '1979-01-01';
GO

CREATE PROCEDURE usp_DeleteEmployee
    @EmployeeId INT
AS
BEGIN
    DELETE 
    FROM Employee
    WHERE EmployeeId = @EmployeeId;
END;


EXEC usp_DeleteEmployee 12;
GO

EXEC usp_GetAllEmployees;
GO