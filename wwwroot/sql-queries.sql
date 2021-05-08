/* DATA BASE */

SELECT db_name()

use master;

IF EXISTS (SELECT * FROM sys.databases WHERE name = 'Test')
	BEGIN
		DROP DATABASE Test;  
	END
ELSE
	BEGIN
		CREATE DATABASE Test;
	END
GO


/* TABLES */

use Test;

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Persons' AND xtype='U')
	BEGIN
		CREATE TABLE Persons (
			PersonID int,
			LastName varchar(255),
			FirstName varchar(255),
			Address varchar(255),
			City varchar(255),
			Birthdate datetime,
			CreatedAt datetime DEFAULT getdate() NOT NULL
		);
	END
ELSE
	BEGIN
		DROP TABLE Persons;
	END
GO


/*
	constrains
	NOT	NULL
	UNIQUE
	PRIMARY KEY
	FOREIGN KEY
	CHECK
	DEFAULT
*/

ALTER TABLE Persons
ADD Email varchar(255);

ALTER TABLE Persons
DROP COLUMN Email;

CREATE INDEX IX_Persons_FirstName
ON Persons (FirstName);

DROP INDEX Persons.IX_Persons_FirstName;

EXEC sp_helpindex Persons 
EXEC sp_pkeys Persons
EXEC sp_helpconstraint Persons
GO

ALTER TABLE Persons
ALTER COLUMN PersonID int NOT NULL;

ALTER TABLE Persons
ADD PRIMARY KEY (PersonID);

CREATE TABLE Orders (
    OrderID int NOT NULL PRIMARY KEY IDENTITY(1, 1),
    OrderNumber int NOT NULL,
    PersonID int FOREIGN KEY REFERENCES Persons(PersonID)
);

DROP TABLE Orders;


ALTER TABLE Persons DROP COLUMN PersonID

ALTER TABLE Persons ADD PersonID INT IDENTITY(1, 1) NOT NULL PRIMARY KEY;


EXEC sp_helpindex Orders 
EXEC sp_pkeys Orders
EXEC sp_helpconstraint Orders
GO

ALTER TABLE Persons   
DROP CONSTRAINT PK__Persons__AA2FFB85404A7778;

ALTER TABLE Orders   
DROP CONSTRAINT PK__Orders__C3905BAFD98D65E1;

ALTER TABLE Orders   
DROP CONSTRAINT FK__Orders__PersonID__2F10007B;

ALTER TABLE Orders
ADD FOREIGN KEY (PersonID) REFERENCES Persons(PersonID);

ALTER TABLE Orders
ADD PRIMARY KEY (OrderID);




/* DATA MANIPULATIONS */


SELECT * FROM Persons;
 
INSERT INTO Persons (LastName, FirstName, Address, City, Birthdate)
VALUES ('Silva', 'José', 'Address', 'Lages', '1987-01-06T00:00:00');

SELECT * FROM Persons WHERE PersonId = 4;

UPDATE Persons
SET Address = 'Austrália'
WHERE PersonId = 4;

DELETE FROM Persons WHERE PersonId = 5;


SELECT PersonID FROM Persons ORDER BY PersonID  OFFSET 0 ROWS FETCH NEXT 5 ROWS ONLY;



SELECT * FROM Orders;

INSERT INTO Orders (OrderNumber, PersonID)
VALUES (6, 3);


INSERT INTO Orders (OrderNumber)
VALUES (7);



SELECT Persons.FirstName, Orders.OrderID FROM Persons
INNER JOIN Orders ON Orders.PersonID = Persons.PersonID;


SELECT Persons.FirstName, Orders.OrderID FROM Persons
LEFT JOIN Orders ON Orders.PersonID = Persons.PersonID;


SELECT Persons.FirstName, Orders.OrderID FROM Persons
RIGHT JOIN Orders ON Orders.PersonID = Persons.PersonID;


SELECT Persons.FirstName, Orders.OrderID FROM Persons
FULL JOIN Orders ON Orders.PersonID = Persons.PersonID;


SELECT Persons.FirstName, COUNT(Orders.OrderID) AS NumberOfOrders FROM Persons
INNER JOIN Orders ON Orders.PersonID = Persons.PersonID
GROUP BY Persons.FirstName;





/* STORED PROCEDURES */

CREATE PROCEDURE SelectPersons
AS
	SELECT * FROM Persons;
GO;

ALTER PROCEDURE SelectPersons
AS
BEGIN
	SELECT Persons.FirstName, Orders.OrderID FROM Persons
	INNER JOIN Orders ON Orders.PersonID = Persons.PersonID;
END;

EXEC SelectPersons;

SELECT * 
  FROM Test.INFORMATION_SCHEMA.ROUTINES
 WHERE ROUTINE_TYPE = 'PROCEDURE'


 DROP PROCEDURE SelectPersons;