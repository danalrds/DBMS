IF OBJECT_ID('TeamProjects','U') IS NOT NULL
DROP TABLE TeamProjects;
IF OBJECT_ID('Products','U') IS NOT NULL
DROP TABLE Products;
IF OBJECT_ID('Tasks','U') IS NOT NULL
DROP TABLE Tasks;
IF OBJECT_ID('Members','U') IS NOT NULL
DROP TABLE Members;
IF OBJECT_ID('Teams','U') IS NOT NULL
DROP TABLE Teams;
IF OBJECT_ID('Projects','U') IS NOT NULL
DROP TABLE Projects;


CREATE TABLE Projects(pid INT PRIMARY KEY IDENTITY(1,1),
					  name VARCHAR(30));

CREATE TABLE Teams(tid INT PRIMARY KEY IDENTITY(1,1),
					name VARCHAR(30),
					number INT,
					projectid INT FOREIGN KEY REFERENCES Projects(pid));

CREATE TABLE Members(mid INT PRIMARY KEY IDENTITY(1,1),
					name VARCHAR(30),
					role VARCHAR(30),
					tid INT FOREIGN KEY REFERENCES Teams(tid));

CREATE TABLE Tasks(taskid INT PRIMARY KEY IDENTITY(1,1),
				   descr VARCHAR(30),
				   memberid INT FOREIGN KEY REFERENCES Members(mid));

CREATE TABLE Products(productid INT PRIMARY KEY IDENTITY(1,1),
				      name VARCHAR(30));

CREATE TABLE TeamProjects(tid INT FOREIGN KEY REFERENCES Teams(tid),
						  pid INT FOREIGN KEY REFERENCES Products(productid),
						  PRIMARY KEY (tid,pid));


INSERT Projects VALUES ('project1');
INSERT Teams VALUES ('team1', 1, 1), ('team2', 2, 1);
INSERT Members VALUES ('member1', 'role1',1),
						('member2', 'role2', 2),
						('member3', 'role3', 1);
INSERT Tasks VALUES ('descr1', 1), ('descr2', 2), ('descr3', 3);