create database ATMBankDB;
use ATMBankDB

-------------------1   Role------------
create table Roles(
RoleId int  primary key identity(1,1),
RoleName Nvarchar(50) not null Unique,
Description Nvarchar(200),
CreatedDate DateTime Default GEtDate()
);


------------- 2   Branches--------------

create table Branches(
BranchId int primary key identity(1,1),
BranchName nvarchar(100) not null,
BranchCode Nvarchar(20),
IFSCCode Nvarchar(20) unique,
Address Nvarchar(300),
City Nvarchar(50),
State Nvarchar(50),
PinCode varchar(20),
IsActive Bit Default 1,
CreateDate DateTime Default GEtdate()
);

------------------- 3 Customer-----------------------
create table Customers(
CustomerId int primary key identity(1,1),
CustomerCode varchar(20) unique,
FirstName varchar(30) not null,
LastName varchar(30) not null,
Gender nvarchar(10),
DOB Date,
MobileNumber varchar(10) unique not null,
Email nvarchar(100) unique,
AadharNo varchar(12) unique,
PanNo varchar(10) unique,
Address nvarchar(300),
City varchar(50),
State varchar(50),
Pincode varchar(6),
IsActive Bit Default 1,
CreateDate DateTime Default GetDate()
);

--------------------------- 4   Account---------

Create Table Accounts(
AccountId int Primary key identity(1,1),
CustomerId int constraint fky_customerid references Customers(CustomerId)  not null,
BranchId int constraint fky_Branchid references Branches(BranchId) not null,
AccountNumber Bigint unique not null,
AccountType Nvarchar(30),
Balance Decimal(18,2) Default 0,
Status Nvarchar(20)Default 'Active',
OpenDate DateTime Default GetDate(),
);

--------------------------5 Users --------------
Create Table Users(
UserId int Primary Key identity(1,1),
UserName Nvarchar(100) unique not null,
PasswordHash nvarchar(max) not null,
RoleId int constraint fky_RoleId references Roles(RoleId) not null,
CustomerId int constraint fky_CustomerIdu references Customers(CustomerId),
IsActive Bit Default 1,
LastLogin DateTime null,
CreateDate DateTime Default Getdate()
);

---------------------6 RefreshTokens----------

create table RefreshTokens(
RefreshTokenId int primary key identity(1,1),
UserId int constraint fky_UserIdR references Users(UserId) not null,
Token Nvarchar(max),
ExpiryDate DateTime,
IsRevoked Bit Default 0,
CreateDate DateTime Default getDate(),
);


---------------------------- 7 AtmCards -------------
create table AtmCards(
CardId int Primary Key identity(1,1),
AccountID int constraint fky_AccountId references Accounts(AccountId) not null,
CardNumber Bigint unique,
Cvv Char(3),
ExpiryDate Date,
PinHash Nvarchar(Max),
DailyLimit Decimal(18,2),
CardStatus nvarchar(20) default 'Active',
CreateDate DAteTime Default GetDate()
);



-------------------- 8    Beneficiaries -----------

Create Table Beneficiaries(
BeneficiaryId int primary key identity(1,1),
AccountId int constraint fky_AccountIdB references Accounts(AccountId) not null,
BeneficiaryName nvarchar(100),
BeneficiaryAccount bigint,
IFSCCode nvarchar(20),
NickName nvarchar(50),
CreateDate DateTime Default GetDate()
);


------------------------------ 9 Transactions ------------
create table Transactions(
TransactionId int primary key identity (1,1),
AccountId int constraint fky_AccountIdT references Accounts(AccountId),
TransactionType Nvarchar(30),
Amount Decimal(18,2),
Description Nvarchar(3000),
ReferenceNumber Nvarchar(50),
TransactionDate Datetime default GetDate()
);

---------------------  10   AuditLogs  --------------------------
create table AuditLogs(
AuditId bigint primary key identity(1,1),
UserId int constraint fky_UseridA references Users(UserId),
ActionName nvarchar(200),
TableName Nvarchar(100),
RecordId int,
ActionDate datetime default Getdate(),
IpAddress nvarchar(100)
);


------------------- 11 LoneType  ---------------
create table LoanTypes(
LoneTypeId int primary key identity(1,1),
LoneTypeName nvarchar(100),
IntresteRate Decimal(5,2),
MaxAmount Decimal(18,2)
);


----------------- 12 Loans-------------------

create table Loans
(
LoanId int primary key identity(1,1),
CustomerId int constraint fky_CustomerIdL references Customers(CustomerId) not null,
LoanType int constraint fky_LoneType references LoneTypes(LoneTypeId) not null,
LoanAmount Decimal(18,2),
EMI Decimal(18,2),
DurationMonths Int,
LoanStatus Nvarchar(30),
ApplyDate DateTime Default GetDate()
);




