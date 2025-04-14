CREATE DATABASE SISDB;
GO

USE SISDB;
GO
CREATE TABLE Students (
    student_id INT IDENTITY(1,1) PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    date_of_birth DATE,
    email VARCHAR(100),
    phone_number VARCHAR(20)
);
CREATE TABLE Courses (
    course_id INT IDENTITY(1,1) PRIMARY KEY,
    course_name VARCHAR(100),
    course_code VARCHAR(20)
);
CREATE TABLE Teacher (
    teacher_id INT IDENTITY(1,1) PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    email VARCHAR(100)
);
CREATE TABLE Enrollments (
    enrollment_id INT IDENTITY(1,1) PRIMARY KEY,
    student_id INT FOREIGN KEY REFERENCES Students(student_id),
    course_id INT FOREIGN KEY REFERENCES Courses(course_id),
    enrollment_date DATE
);
CREATE TABLE Payments (
    payment_id INT IDENTITY(1,1) PRIMARY KEY,
    student_id INT FOREIGN KEY REFERENCES Students(student_id),
    amount DECIMAL(10,2),
    payment_date DATE
);
-- Sample Students
INSERT INTO Students (first_name, last_name, date_of_birth, email, phone_number)
VALUES
('John', 'Doe', '2001-05-12', 'john@example.com', '9999999999'),
('Jane', 'Smith', '2002-08-21', 'jane@example.com', '8888888888');

-- Sample Courses
INSERT INTO Courses (course_name, course_code)
VALUES
('Mathematics 101', 'MATH101'),
('Physics 101', 'PHY101');

-- Sample Teachers
INSERT INTO Teacher (first_name, last_name, email)
VALUES
('Robert', 'Brown', 'robert@uni.com'),
('Anna', 'White', 'anna@uni.com');
