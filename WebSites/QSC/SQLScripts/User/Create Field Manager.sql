USE QSPCanadaCommon

/*
SELECT		*
FROM		FieldManager
WHERE		FMID < 1806
ORDER BY	FMID DESC
*/

DECLARE @PhoneListID	INT,
		@AddressListID	INT,
		@Firstname		VARCHAR(MAX),
		@Lastname		VARCHAR(MAX),
		@Email			VARCHAR(MAX),
		@FMID			VARCHAR(4),
		@DMID			VARCHAR(4),
		@PhoneTypeID	INT,
		@PhoneType2ID	INT,
		@PhoneNumber	VARCHAR(MAX),
		@Address1		VARCHAR(MAX),
		@Address2		VARCHAR(MAX),
		@City			VARCHAR(MAX),
		@Province		VARCHAR(MAX),
		@PostalCode		VARCHAR(MAX),
		@Zip4			VARCHAR(MAX),
		@Country		VARCHAR(MAX),
		@AddressTypeID	INT,
		@CreatedByID	INT,
		@CreatedByName	VARCHAR(MAX),
		@SAPAcctNo		INT,
		@StartDate		DATETIME

INSERT INTO	PhoneList
VALUES (getdate(), 0)
SELECT		TOP 1
			@PhoneListID = ID
FROM		PhoneList
ORDER BY	ID DESC

INSERT INTO AddressList
VALUES (getdate(), 0)
SELECT		TOP 1
			@AddressListID = ID
FROM		AddressList
ORDER BY	ID DESC

SET	@Firstname = 'Ken'
SET	@Lastname = 'Lamb'
SET	@Email = 'Ken.Lamb@qsp.ca'
SET	@FMID = '1575'
SET	@DMID = '0013'
SET	@PhoneTypeID = 30505
SET	@PhoneType2ID = 30501
SET	@PhoneNumber = '905-704-8026'
SET	@Address1 = '287 Tanbark Road'
SET	@Address2 = ''
SET	@City = 'St. Davids'
SET	@Province = 'ON'
SET	@PostalCode = 'L0S1P0'
SET @Zip4 = NULL
SET	@Country = 'CA'
SET	@AddressTypeID = 54004
SET	@CreatedByID = 612
SET	@CreatedByName = 'jmiles'
SET	@SAPAcctNo = 8031191
SET @StartDate = '2018-08-31'

/*30501	Phone Type - Work
30502	Phone Type - Home
30503	Phone Type - Fax
30504	Phone Type - Other
30505	Phone Type - Main
30506	Phone Type - Pager
30507	Phone Type - Mobile/Cell Phone
30508	Phone Type - Office/Fax
30509	Phone Type - Mailbox
30510	Phone Type - Toll Free Line
30511	Phone Type - Speed Dial
30512	Phone Type - Customer Service
30513	Phone Type - Gift Dept.
30514	Phone Type - Magazine Dept.
30515	Phone Type - Prize Division*/

/* Address
54001	Ship To
54002	Bill To
54003	Secondary
54004	Home --Must use for Field Supply
54005	Supply Address
54006	Contact Address
*/

INSERT INTO FieldManager
VALUES (@FMID, 1, @Country, @PhoneListID, @AddressListID, @Firstname, @Lastname, NULL, @Email, @DMID, 612, getdate(), NULL, 'N', 'EN',0, @SAPAcctNo, @StartDate)
SELECT * FROM FieldManager WHERE FMID = @FMID

INSERT INTO Phone
VALUES (@PhoneTypeID, @PhoneListID, @PhoneNumber, NULL)
INSERT INTO Phone
VALUES (@PhoneType2ID, @PhoneListID, @PhoneNumber, NULL)
SELECT * FROM Phone WHERE PhoneListID = @PhoneListID

INSERT INTO Address
VALUES (@Address1, @Address2, @City, @Province, @PostalCode, @Zip4, @Country, @AddressTypeID, @AddressListID)
SELECT * FROM Address WHERE AddressListID = @AddressListID

DECLARE	@Instance	int
EXEC pr_CUserProfile_CreateUser @Firstname = @Firstname, @Lastname = @Lastname, @FMID = @FMID, @CreatedByID = @CreatedByID, @Instance = @Instance OUTPUT
SELECT	@Instance

INSERT INTO UserPermissions
VALUES (@Instance, 'FieldManager', @CreatedByName, getdate(), @CreatedByName, getdate(), 0)