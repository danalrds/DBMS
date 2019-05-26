IF OBJECT_ID('Comments','U') IS NOT NULL
DROP TABLE Comments;
IF OBJECT_ID('Posts','U') IS NOT NULL
DROP TABLE Posts;
IF OBJECT_ID('Likes','U') IS NOT NULL
DROP TABLE Likes;
IF OBJECT_ID('Pages','U') IS NOT NULL
DROP TABLE Pages;
IF OBJECT_ID('Categories','U') IS NOT NULL
DROP TABLE Categories;
IF OBJECT_ID('Users','U') IS NOT NULL
DROP TABLE Users;



CREATE TABLE Users(uid INT PRIMARY KEY IDENTITY(1,1),
				 name VARCHAR(30),
				 city VARCHAR(30), 
				 dob DATE);
CREATE TABLE Categories(cid INT PRIMARY KEY IDENTITY(1,1),
						name VARCHAR(30),
						descr VARCHAR(30));
CREATE TABLE Pages(pid INT PRIMARY KEY IDENTITY(1,1),
					name VARCHAR(30),
					cid INT FOREIGN KEY REFERENCES Categories(cid));
CREATE TABLE Likes(uid INT FOREIGN KEY REFERENCES Users(uid),
					pid INT FOREIGN KEY REFERENCES Pages(pid),
					ldate DATE,
					PRIMARY KEY(uid,pid));
CREATE TABLE Posts(postid INT PRIMARY KEY IDENTITY(1,1),
					ptext VARCHAR(300),
					pdate DATE,
					nrshares INT,
					uid INT FOREIGN KEY REFERENCES Users(uid));

CREATE TABLE Comments(comid INT PRIMARY KEY IDENTITY(1,1),
				      ctext VARCHAR(300),
					  cdate DATE,
					  topcomment BIT,
					  postid INT FOREIGN KEY REFERENCES Posts(postid));
			
			
INSERT Users VALUES ('name1', 'city1', '2019-04-05'),	
					('name2', 'city1', '2019-04-05'),	
					('name3', 'city1', '2019-04-05');

INSERT Posts VALUES ('text1', '2019-08-05',4,1);




