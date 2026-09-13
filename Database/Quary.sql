use ATMBankDB
select * from Accounts

select * from Customers

select AccountNumber,Balance from Accounts where AccountNumber=1000000001;

select AccountNumber,Balance from Accounts where AccountNumber=1000000002;

select * from Transactions  order by TransactionId desc;