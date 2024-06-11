-- ------------------------------------------------------------------------------
-- ---------------------------- CREATE DATABASE ----------------------------------
-- ------------------------------------------------------------------------------
CREATE SCHEMA IF NOT EXISTS iskorecords;

-- Switch to the iskorecords schema
USE iskorecords;

-- ------------------------------------------------------------------------------
-- ----------------------------- CREATE TABLES -----------------------------------
-- ------------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS student_db (
    studentNo INT PRIMARY KEY,
    firstName VARCHAR(100) NOT NULL,
    middleName VARCHAR(100) NOT NULL,
    lastName VARCHAR(100) NOT NULL,
    yearLevel INT NOT NULL,
    section VARCHAR(10) NOT NULL
);



