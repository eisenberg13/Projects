CREATE TABLE [dbo].[ingredients]
(
	[Id] INT NOT NULL Identity PRIMARY KEY, 
    [name] NVARCHAR(50) NULL, 
    [quantity] DECIMAL(8, 1) NULL DEFAULT 1.0, 
    [unit] NVARCHAR(20) NULL DEFAULT '', 
    [recipeId] INT NULL, 
    CONSTRAINT [FK_ingredients_recipe] FOREIGN KEY (recipeId) REFERENCES recipe(Id)
)
