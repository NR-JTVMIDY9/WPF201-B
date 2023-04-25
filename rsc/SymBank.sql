
CREATE DATABASE SymBank;

GO ---------------------------------------------------------------

USE SymBank;

GO ---------------------------------------------------------------

CREATE TABLE dbo.Accounts(
	Code		INT				NOT NULL,
	Type		INT				NOT NULL,
	Name		NVARCHAR(30)	NOT NULL,
	ZipCode		NVARCHAR( 5)	NOT NULL,
	Creator		NVARCHAR(30)	NOT NULL,
	Created		DATETIME		NOT NULL,
	Updated		DATETIME		NOT NULL,
	Balance		MONEY			NOT NULL,
	Picture		IMAGE			NULL
);

GO ---------------------------------------------------------------

CREATE TABLE dbo.Transactions(
	Code		INT				NOT NULL IDENTITY(10000000, 1),
	Type		INT				NOT NULL,
	Source		INT				NOT NULL,
	Target		INT				NOT NULL,
	Amount		MONEY			NOT NULL,
	Creator		NVARCHAR(30)	NOT NULL,
	Created		DATETIME		NOT NULL
);

GO ---------------------------------------------------------------

ALTER TABLE dbo.Accounts
	ADD CONSTRAINT PK_Accounts
		PRIMARY KEY (Code);

GO ---------------------------------------------------------------

ALTER TABLE dbo.Accounts
	ADD CONSTRAINT CC_Accounts_Name
		CHECK (LEN(Name) > 0);

GO ---------------------------------------------------------------

ALTER TABLE dbo.Accounts
ADD CONSTRAINT CC_Accounts_ZipCode
		CHECK (ZipCode LIKE '[0-9][0-9][0-9][0-9][0-9]');

GO ---------------------------------------------------------------

ALTER TABLE dbo.Accounts
	ADD CONSTRAINT CC_Accounts_Balance
		CHECK (Balance >= 0);
 
GO ---------------------------------------------------------------

ALTER TABLE dbo.Transactions
	ADD CONSTRAINT PK_Transactions
		PRIMARY KEY (Code);

GO ---------------------------------------------------------------

ALTER TABLE dbo.Transactions
	ADD CONSTRAINT FK_Transactions_Source
		FOREIGN KEY (Source) REFERENCES Accounts (Code);

GO ---------------------------------------------------------------

ALTER TABLE dbo.Transactions
	ADD CONSTRAINT FK_Transactions_Target
		FOREIGN KEY (Target) REFERENCES Accounts (Code);


GO ---------------------------------------------------------------

ALTER TABLE dbo.Transactions
	ADD CONSTRAINT CC_Transactions_Amount
		CHECK (Amount > 0);

GO ---------------------------------------------------------------

CREATE PROCEDURE dbo.AccountAdd(
	@Code	INT,
	@Type	INT,
	@Name	NVARCHAR(30),
	@ZipCode	NVARCHAR( 5),
	@Creator	NVARCHAR(30),
	@Created	DATETIME,
	@Balance	MONEY,
	@Picture	IMAGE
)
AS
	IF @Balance < 100 BEGIN
		RAISERROR('Opening balance below minimum.', 16, 1)
		RETURN
	END
	IF EXISTS(SELECT * FROM dbo.Accounts
		WHERE Code = @Code) BEGIN
		RAISERROR('Account code must be unique.', 16, 1)
		RETURN
	END
	BEGIN TRANSACTION
	INSERT INTO dbo.Accounts (
		Code,
		Type,
		Name,
		ZipCode,
		Creator,
		Created,
		Updated,
		Balance,
		Picture)
	VALUES (
		@Code,
		@Type,
		@Name,
		@ZipCode, 
		@Creator,
		@Created,
		@Created,
		@Balance,
		@Picture
	)
	IF @@ERROR <> 0 BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END
	INSERT INTO dbo.Transactions (
		Type,
		Source,
		Target,
		Amount,
		Creator,
		Created)
	VALUES (
		0,
		@Code,
		@Code,
		@Balance,
		@Creator,
		@Created
	)
	IF @@ERROR <> 0 BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END
	COMMIT TRANSACTION
	RETURN

GO ---------------------------------------------------------------

CREATE PROCEDURE dbo.AccountDebit(
	@Source		INT,
	@Amount		MONEY,
	@Creator	NVARCHAR(30),
	@Created	DATETIME,
	@Code		INT	OUTPUT
)
AS
	IF @Amount <= 0 BEGIN
		RAISERROR('Invalid debit amount.',16,1)
		RETURN
	END
	BEGIN TRANSACTION
	UPDATE Accounts SET
		Balance = Balance + @Amount,
		Updated = @Created WHERE Code = @Source
	IF @@ROWCOUNT = 0 BEGIN
		RAISERROR('Invalid account code.',16,1)
		ROLLBACK TRANSACTION
		RETURN
	END
	INSERT INTO Transactions (
		Type,
		Source,
		Target,
		Amount,
		Creator,
		Created)
	VALUES (
		1,
		@Source,
		@Source,
		@Amount,
		@Creator,
		@Created
	)
	IF @@ERROR <> 0 BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END
	SET @Code = @@IDENTITY
	COMMIT TRANSACTION
	RETURN

GO ---------------------------------------------------------------

CREATE PROCEDURE dbo.AccountCredit(
	@Source		INT,
	@Amount		MONEY,
	@Creator	NVARCHAR(30),
	@Created	DATETIME,
	@Code		INT	OUTPUT
)
AS
	IF @Amount <= 0 BEGIN
		RAISERROR('Invalid credit amount.',16,1)
		RETURN
	END
	BEGIN TRANSACTION
	DECLARE @Balance MONEY
	SELECT @Balance = Balance
		FROM Accounts (HOLDLOCK)
		WHERE Code = @Source
	IF @@ROWCOUNT = 0 BEGIN
		RAISERROR('Invalid account code.',16,1)
		ROLLBACK TRANSACTION
		RETURN
	END
	IF @Amount > @Balance BEGIN
		RAISERROR('Insufficient funds.',16,1)
		ROLLBACK TRANSACTION
		RETURN
	END
	UPDATE Accounts SET
		Balance = Balance - @Amount,
		Updated = @Created WHERE Code = @Source
	INSERT INTO Transactions (
		Type,
		Source,
		Target,
		Amount,
		Creator,
		Created)
	VALUES (
		2,
		@Source,
		@Source,
		@Amount,
		@Creator,
		@Created
	)
	IF @@ERROR <> 0 BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END
	SET @Code = @@IDENTITY
	COMMIT TRANSACTION
	RETURN

GO ---------------------------------------------------------------

CREATE PROCEDURE dbo.AccountTransfer(
	@Source	INT,
	@Target	INT,
	@Amount	MONEY,
	@Creator	NVARCHAR(30),
	@Created	DATETIME,
	@Code	INT	OUTPUT
)
AS
	IF @Source = @Target BEGIN
		RAISERROR('Cannot transfer to same account.',16,1)
		RETURN
	END
	IF @Amount <= 0 BEGIN
		RAISERROR('Invalid transfer amount.',16,1)
		RETURN
	END
	BEGIN TRANSACTION
	DECLARE @Balance MONEY
	SELECT @Balance = Balance
		FROM Accounts (HOLDLOCK)
		WHERE Code = @Source
	IF @@ROWCOUNT = 0 BEGIN
		RAISERROR('Invalid source account code.',16,1)
		ROLLBACK TRANSACTION
		RETURN
	END
	IF @Amount > @Balance BEGIN
		ROLLBACK TRANSACTION
		RAISERROR('Insufficient funds.',16,1)
		RETURN
	END
	UPDATE Accounts SET
		Balance = Balance + @Amount,
		Updated = @Created WHERE Code = @Target
	IF @@ROWCOUNT = 0 BEGIN
		ROLLBACK TRANSACTION
		RAISERROR('Invalid target account code.',16,1)
		RETURN
	END
	UPDATE Accounts SET
		Balance = Balance - @Amount,
		Updated = @Created WHERE Code = @Source
	INSERT INTO Transactions (
		Type,
		Source,
		Target,
		Amount,
		Created,
		Creator)
	VALUES (
		3,
		@Source,
		@Target,
		@Amount,
		@Created,
		@Creator
	)
	IF @@ERROR <> 0 BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END
	SET @Code = @@IDENTITY
	COMMIT TRANSACTION
	RETURN

GO ---------------------------------------------------------------
