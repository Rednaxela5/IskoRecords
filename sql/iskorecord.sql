-- ------------------------------------------------------------------------------
-- ---------------------------- CREATE DATABASE ----------------------------------
-- ------------------------------------------------------------------------------
CREATE SCHEMA IF NOT EXISTS db_iskorecords;

-- Switch to the iskorecords schema
USE db_iskorecords;

-- ------------------------------------------------------------------------------
-- ----------------------------- CREATE TABLES -----------------------------------
-- ------------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS tb_student (
    studentID INT PRIMARY KEY,
    firstName VARCHAR(100) NOT NULL,
    middleName VARCHAR(100) NOT NULL,
    lastName VARCHAR(100) NOT NULL,
    yearLevel INT NOT NULL,
    section VARCHAR(10) NOT NULL
);

-- INSERT DATA INTO THE TABLE
INSERT INTO tb_student (
    studentID, firstName, middleName, lastName, yearLevel, section
)
VALUES 
    (1, 'Alexander', 'Sanchez', 'Porlares', 3, '1N'),
    (2, 'Tim', 'Toyota', 'Bergling', 3, '1N'),
    (3, 'Ariana', 'Sanchez', 'Grande', 3, '3N');
