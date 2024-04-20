CREATE DATABASE FYP_Streamliner;
USE FYP_Streamliner;
-- drop database FYP_Streamliner;

-- Users Table
CREATE TABLE Users (
    UserID INT PRIMARY KEY AUTO_INCREMENT,
    UserName VARCHAR(25) NOT NULL,
    Email VARCHAR(50) NOT NULL UNIQUE,
    UserType ENUM('Administrator', 'Supervisor', 'Student') NOT NULL,
    Password VARCHAR(50) NOT NULL,
    FirstName VARCHAR(20) NOT NULL,
    LastName VARCHAR(20) NOT NULL,
    CNIC VARCHAR(13) NOT NULL UNIQUE,
    DOB DATE NOT NULL,
    PhoneNumber VARCHAR(11)
);
-- Supervisors Table
CREATE TABLE Supervisors (
    FacultyNumber VARCHAR(14) PRIMARY KEY,
    UserID INT UNIQUE NOT NULL,
    Department ENUM('Computing', 'Electrical Engineering', 'Management') NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);


-- Admins Table
CREATE TABLE Admins (
    AdminNumber VARCHAR(14) PRIMARY KEY,
    UserID INT UNIQUE NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
-- Projects Table
CREATE TABLE Projects (
    ProjectID  VARCHAR(40) PRIMARY KEY,
    ProjectName VARCHAR(100) NOT NULL,
    Description TEXT,
    Status ENUM('Active', 'Inactive', 'Completed') DEFAULT 'Active',
    FacultyNumber VARCHAR(14),
    image_path VARCHAR(50),
    rating INT,
    FOREIGN KEY (FacultyNumber) REFERENCES Supervisors(FacultyNumber)
);

-- Students Table
CREATE TABLE Students (
    RollNumber VARCHAR(18) PRIMARY KEY,
    UserID INT UNIQUE NOT NULL,
    BatchNumber INT NOT NULL,
    Campus ENUM('Islamabad', 'Karachi', 'Lahore', 'Peshawar', 'Chiniot-Faisalabad') NOT NULL,
    Department ENUM('Computing', 'Electrical Engineering', 'Management') NOT NULL,
    Degree ENUM('AI', 'CS', 'DS', 'CY', 'SE', 'FinTech', 'BA', 'ANF', 'EE', 'CE') NOT NULL,
    Program ENUM('Bachelor\'s', 'Master\'s') NOT NULL,
    ParentsPhoneNumber VARCHAR(20) NOT NULL,
    ProjectID VARCHAR(40),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (ProjectID) REFERENCES Projects(ProjectID)
);

CREATE TABLE Requests(
	StudentID VARCHAR(25),
    FacultyName VARCHAR(25),
    FOREIGN KEY (StudentID) REFERENCES Students(RollNumber),
    FOREIGN KEY (FacultyName) REFERENCES Users(UserName)
);

SELECT RollNumber FROM Users AS u
INNER JOIN Students as s 
ON s.UserID=u.UserID
WHERE u.UserName="MuhammadHani";

SELECT FacultyNumber FROM Supervisors AS s
INNER JOIN Users as u on s.UserID=u.UserID
WHERE u.UserName="ArshadIslam";


CREATE TABLE Feedback (
    FeedbackID INT PRIMARY KEY AUTO_INCREMENT,
    ProjectID  VARCHAR(40),
    RollNumber VARCHAR(18),
    SupervisorFacultyNumber VARCHAR(14),
    Content TEXT NOT NULL,
    Evaluation ENUM('Excellent', 'Good', 'Fair', 'Poor'),
    FOREIGN KEY (ProjectID) REFERENCES Projects(ProjectID),
    FOREIGN KEY (RollNumber) REFERENCES Students(RollNumber),
    FOREIGN KEY (SupervisorFacultyNumber) REFERENCES Supervisors(FacultyNumber)
);


INSERT INTO Users (UserName, Email, UserType, Password, FirstName, LastName, CNIC, DOB)
VALUES ('Admin', 'admin@university.edu', 'Administrator', 'password123', 'Admin', 'User', '1234567890123', '1990-01-01');

INSERT INTO Users (UserName, Email, UserType, Password, FirstName, LastName, CNIC, DOB)
VALUES ('Admin', 'superadmin@university.edu', 'Administrator', 'superSecure', 'Super', 'Admin', '3456789012345', '1985-12-31');


INSERT INTO Users (UserName, Email, UserType, Password, FirstName, LastName, CNIC, DOB)
VALUES ('ArshadIslam', 'arshadislam@university.edu', 'Supervisor', '12342', 'Arshad', 'Islam', '8765321098360', '1978-03-09');
INSERT INTO Users (UserName, Email, UserType, Password, FirstName, LastName, CNIC, DOB)
VALUES ('MehreenAlam', 'mehreenalam@university.edu', 'Supervisor', '12345', 'Mehreen', 'Alam', '8765321098760', '1978-03-09');
INSERT INTO Users (UserName, Email, UserType, Password, FirstName, LastName, CNIC, DOB)
VALUES ('SidraKhalid', 'sidra@university.edu', 'Supervisor', '12345', 'Sidra', 'Khalid', '8765432108761', '1978-03-09');
INSERT INTO Users (UserName, Email, UserType, Password, FirstName, LastName, CNIC, DOB)
VALUES ('OwaiseIdrees', 'owaise@university.edu', 'Supervisor', '12345', 'Owaise', 'Idrees', '8765421098762', '1978-03-09');
INSERT INTO Users (UserName, Email, UserType, Password, FirstName, LastName, CNIC, DOB)
VALUES ('SaadSalman', 'saad@university.edu', 'Supervisor', '12345', 'Saad', 'Salman', '8765432109763', '1978-03-09');
INSERT INTO Users (UserName, Email, UserType, Password, FirstName, LastName, CNIC, DOB)
VALUES ('MuhammadHani', 'hani1@university.edu', 'Student', '1234', 'Muhammad', 'Hani', '012345679011', '2002-10-26');
INSERT INTO Users (UserName, Email, UserType, Password, FirstName, LastName, CNIC, DOB)
VALUES ('SachalMustafa', 'sachal1@university.edu', 'Student', '1234', 'Sachal', 'Mustafa', '012356789012', '2002-10-26');
INSERT INTO Users (UserName, Email, UserType, Password, FirstName, LastName, CNIC, DOB)
VALUES ('HadeedRahuf', 'hadeed1@university.edu', 'Student', '1234', 'Hadeed', 'Rahuf', '012345689013', '2002-10-26');

INSERT INTO Students (RollNumber, UserID, BatchNumber, Campus, Department, Degree, Program, ParentsPhoneNumber, ProjectID)
VALUES ('21I-2595', (SELECT UserID FROM Users WHERE UserName = 'MuhammadHani'), 2021, 'Islamabad', 'Computing', 'CS', 'Bachelor\'s', '03123456789', NULL);
INSERT INTO Students (RollNumber, UserID, BatchNumber, Campus, Department, Degree, Program, ParentsPhoneNumber, ProjectID)
VALUES ('21I-0412', (SELECT UserID FROM Users WHERE UserName = 'SachalMustafa'), 2021, 'Islamabad', 'Computing', 'CS', 'Bachelor\'s', '02123456789', NULL);
INSERT INTO Students (RollNumber, UserID, BatchNumber, Campus, Department, Degree, Program, ParentsPhoneNumber, ProjectID)
VALUES ('21I-0813', (SELECT UserID FROM Users WHERE UserName = 'HadeedRahuf'), 2021, 'Islamabad', 'Computing', 'CS', 'Bachelor\'s', '05123456789', NULL);

INSERT INTO Supervisors (FacultyNumber, UserID, Department)
VALUES ('A1734', (SELECT UserID FROM Users WHERE UserName = 'ArshadIslam'), 'Computing');
INSERT INTO Supervisors (FacultyNumber, UserID, Department)
VALUES ('F1734', (SELECT UserID FROM Users WHERE UserName = 'MehreenAlam'), 'Computing');
INSERT INTO Supervisors (FacultyNumber, UserID, Department)
VALUES ('G7678', (SELECT UserID FROM Users WHERE UserName = 'SidraKhalid'), 'Computing');
INSERT INTO Supervisors (FacultyNumber, UserID, Department)
VALUES ('E6678', (SELECT UserID FROM Users WHERE UserName = 'OwaiseIdrees'), 'Computing');
INSERT INTO Supervisors (FacultyNumber, UserID, Department)
VALUES ('D5578', (SELECT UserID FROM Users WHERE UserName = 'SaadSalman'), 'Computing');

SELECT FacultyNumber FROM Supervisors as s INNER JOIN Users as u ON s.UserID=u.UserID WHERE u.UserName="MehreenAlam";

SELECT * FROM Supervisors;

select ProjectName, Description, Status, image_path from Projects as p
inner join Students as s on s.ProjectID=p.ProjectID
inner join Supervisors as sp on sp.FacultyNumber=p.FacultyNumber
inner join  Users as u on s.UserID=u.UserID
where u.UserName="MuhammadHani";

select FirstName, LastName from Projects as p
inner join Supervisors as s on s.FacultyNumber=p.FacultyNumber
inner join Users as u on u.UserID=s.UserID
where p.ProjectName="Kumon";


select ProjectName, Description, Status from Projects;

select * from Projects;

select * from Students;

Select * from Users;

SELECT ProjectName, Description, FirstName, LastName from Projects as p
inner join Supervisors as s on p.FacultyNumber=s.FacultyNumber
inner join Users as u on u.UserID=s.UserID
inner join Students as st on st.UserID=u.UserID
where u.UserName="MuhammadHani";


INSERT INTO Projects VALUES ("f2803c41-33c6-4e57-b70b-8e9608949d4d","FMS","FYP Management System","Active","E6678");

Select RollNumber From students as s INNER JOIN Users as u on s.UserID=u.UserID WHERE u.Username="Student123";

UPDATE Students SET ProjectID="132aedd4-5158-44b8-aa84-94c6cf8502cd" WHERE RollNumber IN ("21I-2595");

UPDATE Projects SET rating = 1;

SELECT * FROM Projects;

SELECT s.RollNumber FROM Students AS s INNER JOIN Users AS u ON s.UserID = u.UserID WHERE u.UserName = "MuhammadHani";

SELECT UserName
FROM Projects as p 
INNER JOIN Supervisors AS s ON p.FacultyNumber = s.FacultyNumber 
INNER JOIN Users AS u ON u.UserID = s.UserID 
GROUP BY UserName 
HAVING COUNT(UserName) < 9;
