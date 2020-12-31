USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_reserve_PhoneListIDs]    Script Date: 06/07/2017 09:33:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_reserve_PhoneListIDs]
 @MinID int
,@MaxID int
AS

DECLARE 
	@PhoneListID int,
	@rows int,
	@Max int

SELECT @rows = 0, @PhoneListID = @MinID, @Max = @MaxID + 1 ; 

IF (@MinID != 0 AND @MaxID != 0)
begin
	SET IDENTITY_INSERT QSPCanadaCommon.dbo.PhoneList ON
	SET NOCOUNT ON
	while( @PhoneListID < @Max )
	begin
		-------------------------------------------------
		--  insert a new PhoneListID   ---
		-------------------------------------------------
		INSERT INTO QSPCanadaCommon.dbo.PhoneList(ID) VALUES(@PhoneListID);
		SELECT @PhoneListID = @PhoneListID + 1;
		SELECT @rows = @rows + 1;
	end
	SET NOCOUNT OFF
	SET IDENTITY_INSERT QSPCanadaCommon.dbo.PhoneList OFF
end
print '--Insert the new PhoneListIDs - ' + CAST(@rows AS varchar) + ' rows affected'
GO
