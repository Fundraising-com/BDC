USE [QSPCanadaCommon]
GO

DECLARE @Firstname VARCHAR(50),
		@Lastname VARCHAR(50)

SET	@Firstname = 'Sabrinnea'
SET @Lastname = 'Gero'

DECLARE	@return_value int,
		@Instance int

EXEC	@return_value = [dbo].[pr_CUserProfile_CreateUser]
		@Firstname = @Firstname,
		@Lastname = @Lastname,
		@FMID = N'9999',
		@CreatedByID = 612,
		@Instance = @Instance OUTPUT

SELECT	@Instance as N'@Instance'

SELECT	'Return Value' = @return_value


--select * from cuserprofile where firstname = 'johanna'

--select * from userpermissions where profileID = 868

--insert into userpermissions
--values (@Instance, 'CAccountInsert', 'jmiles', getdate(), 'jmiles', getdate(), 0)
insert into userpermissions
values (@Instance, 'CustomerService', 'jmiles', getdate(), 'jmiles', getdate(), 0)
insert into userpermissions
values (@Instance, 'Finance', 'jmiles', getdate(), 'jmiles', getdate(), 0)
insert into userpermissions
values (@Instance, 'HomeOffice', 'jmiles', getdate(), 'jmiles', getdate(), 0)
insert into userpermissions
values (@Instance, 'OrderMgt', 'jmiles', getdate(), 'jmiles', getdate(), 0)
