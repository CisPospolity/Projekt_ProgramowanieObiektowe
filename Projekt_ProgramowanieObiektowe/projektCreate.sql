CREATE DATABASE Projekt_PO;

GO

USE Projekt_PO;

GO

CREATE TABLE Products(
productID int IDENTITY(1,1) PRIMARY KEY,
productName varchar(50) NOT NULL,
productPrice NUMERIC(5,2) NOT NULL,
discount NUMERIC(5,2) DEFAULT 0,

/*CHECK (discount < medicinePrice OR discount = NULL)*/
);
CREATE TABLE Receipts(
receiptID int IDENTITY(1,1) PRIMARY KEY,
purchaseTime datetime NOT NULL
);

CREATE TABLE ProductsOnReceipt(
porID int IDENTITY(1,1) PRIMARY KEY,
receiptID int FOREIGN KEY REFERENCES Receipts(receiptID),
productID int FOREIGN KEY REFERENCES Products(productID),
amount NUMERIC(7,4) NOT NULL DEFAULT 1,
discount NUMERIC(7,2) NOT NULL DEFAULT 0,
sumPrice NUMERIC(7,2) NOT NULL DEFAULT 0
);

CREATE TABLE Storage(
storageID int IDENTITY(1,1) PRIMARY KEY,
productID int UNIQUE FOREIGN KEY REFERENCES  Products(productID),
amount NUMERIC(7,4) NOT NULL DEFAULT 1
);

GO


INSERT INTO Products(productName, productPrice) VALUES
('Czekolada', 7),
('Ananas', 12.99),
('Jab³ko', 3.99),
('Czipsy', 5)

INSERT INTO Receipts(purchateTime) VALUES
('2022-04-28'),
('2022-01-01'),
(getdate())

INSERT INTO ProductsOnReceipt(receiptID,productID,amount, sumPrice) VALUES
(1,1,2,14),
(1,3,1.608,6.42),
(2,4,4,20),
(2,1,1,7),
(3,1,1,7),
(3,2,1,12.99),
(3,3,1,3.99),
(3,4,1,12.99)

INSERT INTO Storage(productID, amount) VALUES
(1,50),
(2,100),
(3,20)