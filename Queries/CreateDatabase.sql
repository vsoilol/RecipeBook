CREATE DATABASE "RecipeBookDb";
Use "RecipeBookDb";

CREATE TABLE "Ingredient"
(
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(20) NOT NULL,
    Weight REAL NOT NULL,
);

CREATE TABLE "Category"
(
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(30) NOT NULL
);

CREATE TABLE "Recipe"
(
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(20) NOT NULL,
    CookingTime TIME NOT NULL,
    CookingTemperature INT NOT NULL,
    CategoryId INT NOT NULL,
    ImageData VARBINARY(MAX) NOT NULL,
	Description NVARCHAR(50) NULL,
	SequenceActions NVARCHAR(100) NULL,
    FOREIGN KEY (CategoryId)  REFERENCES "Category" (Id)
);

CREATE TABLE "RecipeIngredient"
(
	IngredientId INT NOT NULL,
	RecipeId INT NOT NULL,
	FOREIGN KEY (IngredientId)  REFERENCES "Ingredient" (Id),
	FOREIGN KEY (RecipeId)  REFERENCES "Recipe" (Id)
);


