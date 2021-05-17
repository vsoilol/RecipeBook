CREATE DATABASE "RecipeBookDb";
Use "RecipeBookDb";

CREATE TABLE "Ingredient"
(
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(20) NOT NULL,
    Weight REAL NOT NULL,
);

CREATE TABLE "Recipe"
(
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(20) NOT NULL,
    CookingTime INT NOT NULL,
    CookingTemperature TIME NOT NULL,
    Category NVARCHAR(30) NOT NULL,
    ImagePath NVARCHAR(50) NOT NULL,
	Description NVARCHAR(100) NULL,
	SequenceActions NVARCHAR(300) NULL,
);

CREATE TABLE "RecipeIngredient"
(
	IngredientId INT NULL,
	RecipeId INT NULL,
	FOREIGN KEY (IngredientId)  REFERENCES "Ingredient" (Id),
	FOREIGN KEY (RecipeId)  REFERENCES "Recipe" (Id)
);


