-------------------Insert master date--------


------   Roles-------------

insert into Roles(RoleName,Description)
values('Admin','System Administrator'),('Manager','Bank Manager'),('Customer','Bank Customer')

select * from Roles


-------------- Branchs ------------

insert into Branches
(
BranchName,
BranchCode,
IFSCCode,
Address,
City,
State,
PinCode)
values
('Pune Branch','PUN001','BANK0001','Shivaji Nagar','Pune','Maharashtra','411001'),
('Mumbai Branch','MUM001','BANk0002','Andheri','Mumbai','Maharashtra','400001'),
('Solapur Branch','SOL001','BANK0003','Railway Line','Solapur','Maharashta','400001'),
('Sangola Branch','SAN001','BANK0004','Main Rode','Sangola','Magharashtra','413307')

select * from Branches




--------------------------- LoanTypes-----------


insert into LoanTypes
(
LoneTypeName,
IntresteRate,
MaxAmount)
values
('Home Loan', 8.50,5000000),
('Car Loan',9.00,1500000),
('Education Loan',6.75,2000000),
('Personal Loan',11.50,1000000)
select * from LoanTypes


INSERT INTO Customers
(
CustomerCode,
FirstName,
LastName,
Gender,
DOB,
MobileNumber,
Email,
AadharNo,
PANNo,
Address,
City,
State,
Pincode
)
VALUES
(
'CUST001',
'Sachin',
'Kale',
'Male',
'2000-01-01',
'9876543210',
'sachin@gmail.com',
'123456789012',
'ABCDE1234F',
'Sangola',
'Sangola',
'Maharashtra',
'413307'
);

select * from Customers