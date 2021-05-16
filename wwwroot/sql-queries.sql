
SELECT DB_NAME();

USE Test;

/* CREATE TABLES */

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Users' AND xtype='U')
BEGIN
	CREATE TABLE Users (
		UserID INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
		Name VARCHAR(255) NOT NULL,
		Email VARCHAR(255) UNIQUE NOT NULL,
		Password VARCHAR(255) NOT NULL,
		Avatar VARCHAR(255),
		CreatedAt DATETIME DEFAULT GETDATE() NOT NULL
	);
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Posts' AND xtype='U')
BEGIN
	CREATE TABLE Posts (
		PostID INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
		Title VARCHAR(255) NOT NULL,
		Content TEXT NOT NULL,
		IsActive BIT DEFAULT 0,
		Image VARCHAR(255),
		CreatedAt DATETIME DEFAULT GETDATE() NOT NULL,
		UserID INT FOREIGN KEY REFERENCES Users(UserID)
	);
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Categories' AND xtype='U')
BEGIN
	CREATE TABLE Categories (
		CategoryID INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
		Name VARCHAR(255) UNIQUE NOT NULL,
	);
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='CategoriesPosts' AND xtype='U')
BEGIN
	CREATE TABLE CategoriesPosts (
		CategoryID INT NOT NULL FOREIGN KEY REFERENCES Categories(CategoryID),
		PostID INT NOT NULL FOREIGN KEY REFERENCES Posts(PostID) ON DELETE CASCADE,
	);
END



/* INSERT DATA */

INSERT INTO Users (Name, Email, Password)
VALUES ('Welison', 'welison@test.com', '123456');

INSERT INTO Posts (Title, Content, UserID)
VALUES ('Title here', 'Content here', 1);

INSERT INTO Categories (Name)
VALUES ('Technology');

INSERT INTO CategoriesPosts (CategoryID, PostID)
VALUES (1, 1);



/* UPDATE DATA */

UPDATE Users
SET Name='José', Email='jose@email.com', Password='654321'
WHERE UserID = 1;

UPDATE Posts
SET Title='New Title'
WHERE PostID = 3;

UPDATE Categories
SET Name='Database'
WHERE CategoryID = 1;



/* DELETE DATA */

DELETE FROM Posts WHERE PostID = 3;

DELETE FROM CategoriesPosts WHERE CategoryID = 1 AND PostID = 3;



/* SELECT DATA */

SELECT * FROM Users;

SELECT * FROM Users WHERE UserID =1;

SELECT * FROM Posts;

SELECT * FROM Posts WHERE PostID =1;

SELECT * FROM Categories;

SELECT * FROM Categories WHERE CategoryID =1;



/* FILTER THE DATA */

SELECT * FROM Posts WHERE IsActive = 0;

SELECT Posts.* FROM Posts
INNER JOIN CategoriesPosts ON Posts.PostID = CategoriesPosts.PostID
WHERE CategoriesPosts.CategoryID = 1;

SELECT Categories.* FROM Categories
INNER JOIN CategoriesPosts ON Categories.CategoryID = CategoriesPosts.CategoryID
WHERE CategoriesPosts.PostID = 3;

SELECT * FROM Posts WHERE Title LIKE '%XXX%' OR Content LIKE '%XXX%';

SELECT * FROM Posts ORDER BY PostID
OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY;

SELECT Posts.* FROM Posts
INNER JOIN CategoriesPosts ON Posts.PostID = CategoriesPosts.PostID
WHERE CategoriesPosts.CategoryID = 1
ORDER BY PostID
OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY;

SELECT * FROM Posts WHERE Title 
LIKE '%XXX%' OR Content LIKE '%here%'
ORDER BY PostID
OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY;