IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'CreditCard'))

BEGIN
	CREATE TABLE CreditCard
	(
		Number int PRIMARY KEY,
		ExpiryDate date,
		CardHolder varchar(255),
		EmployeeId int FOREIGN KEY REFERENCES Employees(EmployeeID)
	)
END