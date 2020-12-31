USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[GetCAccountContactInfo]    Script Date: 06/07/2017 09:33:08 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[GetCAccountContactInfo]
	@AccountId int = null,
	@ContactId int = null
as

if (@AccountId is null and @ContactId is null)
begin
	SELECT TypeName FROM CAccountContactType
end
else if @AccountId is not null
begin
	SELECT
		c.Id,
		c.Title,
		c.FirstName,
		c.LastName,
		c.MiddleInitial,
		t.TypeName,
		c.Email
	FROM
		CAccountContact AS c
		join CAccountContactType AS t
			ON c.TypeId = t.Id
	WHERE
		c.AccountId = @AccountId and
		Deleted_TF <> 1
end
else if @ContactId is not null
begin
	SELECT
		Address1,
		Address2,
		City,
		State,
		Zip,
		HomePhone,
		WorkPhone,
		FaxPhone,
		MobilePhone
	FROM CAccountContact
	WHERE
		Id = @ContactId and
		Deleted_TF <> 1
end
GO
