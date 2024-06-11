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
    studentNo INT PRIMARY KEY,
    firstName VARCHAR(100) NOT NULL,
    middleName VARCHAR(100) NOT NULL,
    lastName VARCHAR(100) NOT NULL,
    yearLevel INT NOT NULL,
    section VARCHAR(10) NOT NULL
);

-- INSERT DATA INTO THE TABLE
INSERT INTO tb_student (
    studentNo, firstName, middleName, lastName, yearLevel, section
)
VALUES 
    (01, 'Alexander', 'Sanchez', 'Porlares', 3, '1N'),
    (02, 'John', 'Doe', 'Smith', 2, '2'),
    (03, 'Jane', 'Doe', 'Johnson', 4, '3');




-- STORED PROCEDURE TO UPDATE THE STUDENT RECORD
DELIMITER //

CREATE PROCEDURE UpdateStudentRecords(
    IN p_studentNo INT,
    IN p_firstName VARCHAR(100),
    IN p_middleName VARCHAR(100),
    IN p_lastName VARCHAR(100),
    IN p_yearLevel INT,
    IN p_section VARCHAR(10)
)
BEGIN
    UPDATE tb_student
    SET 
        firstName = p_firstName,
        middleName = p_middleName,
        lastName = p_lastName,
        yearLevel = p_yearLevel,
        section = p_section
    WHERE studentNo = p_studentNo;
END //

DELIMITER ;

-- STORED PROCEDURE TO DELETE A STUDENT RECORD
DELIMITER //

CREATE PROCEDURE DeleteStudentRecord(
    IN p_studentNo INT
)
BEGIN
    DELETE FROM tb_student
    WHERE studentNo = p_studentNo;
END //

DELIMITER ;