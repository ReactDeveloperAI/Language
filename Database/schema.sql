-- ============================================
-- Multilingual Menu Application - Database Schema
-- Database: MultilingualMenuDb
-- SQL Server 2019+
-- ============================================

-- Create Database
-- Note: Execute this manually if the database doesn't exist
-- CREATE DATABASE MultilingualMenuDb;
-- GO
-- USE MultilingualMenuDb;
-- GO

-- ============================================
-- Table: Languages
-- Stores supported languages in the system
-- ============================================
CREATE TABLE Languages (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Code NVARCHAR(10) NOT NULL UNIQUE,
    Name NVARCHAR(100) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT CK_Languages_Code CHECK (LEN(Code) >= 2)
);

-- ============================================
-- Table: Menus
-- Stores menu items with default content
-- ============================================
CREATE TABLE Menus (
    Id INT PRIMARY KEY IDENTITY(1,1),
    DefaultName NVARCHAR(200) NOT NULL,
    DefaultDescription NVARCHAR(1000) NULL
);

-- ============================================
-- Table: MenuTranslations
-- Stores translations for menu items
-- ============================================
CREATE TABLE MenuTranslations (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Language NVARCHAR(10) NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(1000) NULL,
    MenuId INT NOT NULL,
    CONSTRAINT FK_MenuTranslations_Menus FOREIGN KEY (MenuId) 
        REFERENCES Menus(Id) ON DELETE CASCADE
);

-- Create indexes for better query performance
CREATE INDEX IX_MenuTranslations_MenuId ON MenuTranslations(MenuId);
CREATE INDEX IX_MenuTranslations_Language ON MenuTranslations(Language);
CREATE INDEX IX_Languages_Code ON Languages(Code);

-- ============================================
-- Seed Data: Default Languages
-- ============================================
INSERT INTO Languages (Code, Name, IsActive) VALUES ('en', 'English', 1);
INSERT INTO Languages (Code, Name, IsActive) VALUES ('tr', 'Türkçe', 1);
INSERT INTO Languages (Code, Name, IsActive) VALUES ('es', 'Español', 1);

-- ============================================
-- Sample Data: Menu Items with Translations
-- ============================================

-- Sample Menu Item 1: Pizza
INSERT INTO Menus (DefaultName, DefaultDescription) 
VALUES ('Pizza', 'Delicious Italian pizza with various toppings');

DECLARE @PizzaId INT = SCOPE_IDENTITY();

INSERT INTO MenuTranslations (Language, Name, Description, MenuId) 
VALUES ('tr', 'Pizza', 'Çeşitli malzemelerle İtalyan pizzası', @PizzaId);

INSERT INTO MenuTranslations (Language, Name, Description, MenuId) 
VALUES ('es', 'Pizza', 'Deliciosa pizza italiana con varios ingredientes', @PizzaId);

-- Sample Menu Item 2: Pasta
INSERT INTO Menus (DefaultName, DefaultDescription) 
VALUES ('Pasta', 'Fresh homemade pasta with authentic Italian sauces');

DECLARE @PastaId INT = SCOPE_IDENTITY();

INSERT INTO MenuTranslations (Language, Name, Description, MenuId) 
VALUES ('tr', 'Makarna', 'Otantik İtalyan soslarıyla ev yapımı taze makarna', @PastaId);

INSERT INTO MenuTranslations (Language, Name, Description, MenuId) 
VALUES ('es', 'Pasta', 'Pasta casera fresca con auténticas salsas italianas', @PastaId);

-- Sample Menu Item 3: Salad
INSERT INTO Menus (DefaultName, DefaultDescription) 
VALUES ('Caesar Salad', 'Fresh romaine lettuce with Caesar dressing and croutons');

DECLARE @SaladId INT = SCOPE_IDENTITY();

INSERT INTO MenuTranslations (Language, Name, Description, MenuId) 
VALUES ('tr', 'Sezar Salata', 'Sezar sosu ve kruton ile taze marul', @SaladId);

INSERT INTO MenuTranslations (Language, Name, Description, MenuId) 
VALUES ('es', 'Ensalada César', 'Lechuga romana fresca con aderezo César y crutones', @SaladId);

-- ============================================
-- Verification Queries
-- ============================================

-- Check all tables
SELECT 'Languages' AS TableName, COUNT(*) AS RecordCount FROM Languages
UNION ALL
SELECT 'Menus', COUNT(*) FROM Menus
UNION ALL
SELECT 'MenuTranslations', COUNT(*) FROM MenuTranslations;

-- View all menus with their translations
SELECT 
    m.Id AS MenuId,
    m.DefaultName,
    m.DefaultDescription,
    mt.Language,
    mt.Name AS TranslatedName,
    mt.Description AS TranslatedDescription
FROM Menus m
LEFT JOIN MenuTranslations mt ON m.Id = mt.MenuId
ORDER BY m.Id, mt.Language;
