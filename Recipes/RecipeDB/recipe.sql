CREATE TABLE [dbo].[recipe]
(
	[Id] INT NOT NULL Identity PRIMARY KEY, 
    [name] NVARCHAR(50) NULL, 
    [description] NVARCHAR(MAX) NULL, 
    [steps] NVARCHAR(MAX) NULL, 
    [prepTime] INT NULL, 
    [cookTime] INT NULL, 
    [type] NVARCHAR(50) NULL, 
    
)
