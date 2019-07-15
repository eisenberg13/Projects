declare @recipe int;

Insert Recipe (name, description, steps, prepTime, cookTime, type)
    values ('Steak','describe1', 'step1', 10, 30, 'Meat')
Select @recipe = @@IDENTITY
Insert Ingredients (name, quantity, unit, recipeId) Values ('Steak', 10, 'lbs', @recipe)
Insert Ingredients (name, quantity, unit, recipeId) Values ('Salt', 2, 'tsp', @recipe)
Insert Ingredients (name, quantity, unit, recipeId) Values ('Pepper', 1, 'tsp', @recipe)

Insert Recipe (name, description, steps, prepTime, cookTime, type)
    values ('Stuffed Shells','describe2', 'step2', 7, 25, 'Dairy')
Select @recipe = @@IDENTITY
Insert Ingredients (name, quantity, unit, recipeId) Values ('Riccota Cheese', 3, 'cups', @recipe)
Insert Ingredients (name, quantity, unit, recipeId) Values ('Shell Noodles', 1, 'pack', @recipe)
Insert Ingredients (name, quantity, unit, recipeId) Values ('Marinara Sauce', 1, 'jar', @recipe)

Insert Recipe (name, description, steps, prepTime, cookTime, type)
    values ('Cedar Plank Salmon','describe3', 'step3', 12, 20 , 'Parve')
Select @recipe = @@IDENTITY
Insert Ingredients (name, quantity, unit, recipeId) Values ('Salmon', 2, 'lbs', @recipe)
Insert Ingredients (name, quantity, unit, recipeId) Values ('Olive oil', 3, 'tbsp', @recipe)
Insert Ingredients (name, quantity, unit, recipeId) Values ('Chyanne Pepper', 2, 'tbsp', @recipe)