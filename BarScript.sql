IF OBJECT_ID('OrderDetails','U') IS NOT NULL
DROP TABLE OrderDetails;
IF OBJECT_ID('Orders','U') IS NOT NULL
DROP TABLE Orders;
IF OBJECT_ID('Drinks','U') IS NOT NULL
DROP TABLE Drinks;
IF OBJECT_ID('Categories','U') IS NOT NULL
DROP TABLE Categories;
IF OBJECT_ID('Bartables','U') IS NOT NULL
DROP TABLE Bartables;
IF OBJECT_ID('Waiters','U') IS NOT NULL
DROP TABLE Waiters;


CREATE TABLE Waiters(wid INT PRIMARY KEY IDENTITY(1,1),
					name VARCHAR(30),
					address VARCHAR(30),
					city VARCHAR(30),
					hireDate DATE);

CREATE TABLE Bartables(btid INT PRIMARY KEY IDENTITY(1,1),
						name VARCHAR(30),
						nrChairs INT);

CREATE TABLE Categories(cid INT PRIMARY KEY IDENTITY(1,1),
					  name VARCHAR(30));


CREATE TABLE Drinks(did INT PRIMARY KEY IDENTITY(1,1),
					name VARCHAR(30),
					descr VARCHAR(30),
					categoryId INT FOREIGN KEY REFERENCES Categories(cid)); 

CREATE TABLE Orders(oid INT PRIMARY KEY IDENTITY(1,1),
					date DATE,
					waiterId INT FOREIGN KEY REFERENCES Waiters(wid),
					tableId INT FOREIGN KEY REFERENCES BarTables(btid));

CREATE TABLE OrderDetails(oid INT FOREIGN KEY REFERENCES Orders(oid),
							did INT FOREIGN KEY REFERENCES Drinks(did),
							quantity INT,
							PRIMARY KEY(oid,did));


INSERT Categories VALUES ('category1'), ('category2'), ('category3');
INSERT Drinks VALUES ('vodka', 'desc1', 1), ('whiskey','desc2', 2 ), ('scotch', 'desc3', 3);


SELECT * FROM Drinks;