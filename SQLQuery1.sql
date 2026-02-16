use Bottles;
CREATE TABLE Bottles (
    Id INT identity(1, 1) PRIMARY KEY,
    Volume INT NOT NULL,
    Description NVARCHAR(255) NULL
);
