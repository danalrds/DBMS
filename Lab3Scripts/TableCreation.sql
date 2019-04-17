IF OBJECT_ID('PlanesTechnicians','U') IS NOT NULL
DROP TABLE PlanesTechnicians;
IF OBJECT_ID('Technicians','U') IS NOT NULL
DROP TABLE Technicians;
IF OBJECT_ID('Planes','U') IS NOT NULL
DROP TABLE Planes;

CREATE TABLE Planes(id INT PRIMARY KEY IDENTITY(1,1),
					type VARCHAR(30),
					company VARCHAR(30));
CREATE TABLE Technicians(id INT PRIMARY KEY IDENTITY(1,1),
						name VARCHAR(30));
CREATE TABLE PlanesTechnicians(planeId INT FOREIGN KEY REFERENCES Planes(id),
								techId INT FOREIGN KEY REFERENCES Technicians(id),
								PRIMARY KEY(planeId,techId));