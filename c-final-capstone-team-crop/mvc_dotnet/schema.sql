
-- Switch to the system (aka master) database
USE master;
GO

-- Delete the CropPlannerDB Database (IF EXISTS)
IF EXISTS(select * from sys.databases where name='CropPlannerDB')
DROP DATABASE CropPlannerDB;
GO

-- Create a new CropPlannerDB Database
CREATE DATABASE CropPlannerDB;
GO

-- Switch to the CropPlannerDB Database
USE CropPlannerDB
GO

BEGIN TRANSACTION;

CREATE TABLE users
(
	id			int			identity(1,1),
	username	varchar(50)	not null,
	password	varchar(50)	not null,
	salt		varchar(50)	not null,
	role		varchar(50)	default('user'),

	constraint pk_users primary key (id)
);


COMMIT TRANSACTION;