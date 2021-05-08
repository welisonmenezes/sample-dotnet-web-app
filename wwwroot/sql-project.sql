

USE Test;



/* CRIANDO TABELAS */

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
GO

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
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Categories' AND xtype='U')
	BEGIN
		CREATE TABLE Categories (
			CategoryID INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
			Name VARCHAR(255) NOT NULL
		);
	END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='CategoriesPosts' AND xtype='U')
	BEGIN
		CREATE TABLE CategoriesPosts (
			PostID INT FOREIGN KEY REFERENCES Posts(PostID) ON DELETE CASCADE,
			CategoryID INT FOREIGN KEY REFERENCES Categories(CategoryID)
		);
	END
GO



DROP TABLE CategoriesPosts;
DROP TABLE Categories;
DROP TABLE Posts;
DROP TABLE Users;




/* INSERINDO DADOS */

INSERT INTO Users (Name, Email, Password)
VALUES ('Welison Menezes', 'welisonmenezes@gmail.com', '123456');

INSERT INTO Posts (Title, Content, UserID)
VALUES ('Post Title Here', 'Post Content Here', 1);

INSERT INTO Categories (Name)
VALUES ('Tecnologia');

INSERT INTO CategoriesPosts (PostID, CategoryID)
VALUES (1, 1);




/* ATUALIZANDO DADOS */

UPDATE Users
SET Name = 'José Silva', Email = 'jose@email.com', Password = '654321'
WHERE UserID = 1;

UPDATE Posts
SET Title = 'New Title Here', Content = 'New Content Here', IsActive = 1
WHERE PostID = 1;

UPDATE Categories
SET Name = 'Programação'
WHERE CategoryID = 1;




/* DELETANDO DADOS */

DELETE FROM Posts WHERE PostId = 1;




/* SELECT DATA */

SELECT * FROM Users;

SELECT * FROM Users WHERE UserID = 1;

SELECT * FROM Posts ORDER BY CreatedAt;

SELECT * FROM Posts WHERE PostID = 1;

SELECT * FROM Categories ORDER BY Name;

SELECT * FROM Categories WHERE CategoryID = 1;




/* FILTERING DATA */

SELECT * FROM Posts WHERE IsActive = 0;

SELECT Posts.* FROM Posts
INNER JOIN CategoriesPosts ON Posts.PostID = CategoriesPosts.PostID
WHERE CategoriesPosts.CategoryID = 1;

SELECT Categories.* FROM Categories
INNER JOIN CategoriesPosts ON Categories.CategoryID = CategoriesPosts.CategoryID
WHERE CategoriesPosts.PostID = 1;

SELECT * FROM Posts WHERE Title LIKE '%Tit%' OR Content LIKE '%Tit%';




/* PAGINATING DATA */

SELECT * FROM Users ORDER BY UserID  
OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY;

SELECT * FROM Posts ORDER BY PostID  
OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY;

SELECT * FROM Categories ORDER BY CategoryID  
OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY;

SELECT Posts.* FROM Posts
INNER JOIN CategoriesPosts ON Posts.PostID = CategoriesPosts.PostID
WHERE CategoriesPosts.CategoryID = 1
ORDER BY Posts.PostID  
OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY;

SELECT * FROM Posts WHERE Title LIKE '%Tit%' OR Content LIKE '%Tit%'
ORDER BY Posts.PostID  
OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY;