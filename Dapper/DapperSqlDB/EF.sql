CREATE DATABASE CarsDB
GO

USE CarsDB;
GO

CREATE TABLE Cars
(
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[Brand] NVARCHAR(50),
	[Model] NVARCHAR(50),
	[Year] INT,
	[Price] DECIMAL(10,2),
)

SELECT * FROM Cars