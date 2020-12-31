USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_reserve_AddressListIDs]    Script Date: 06/07/2017 09:33:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_reserve_AddressListIDs]
 @MinID int
,@MaxID int
AS

DECLARE 
	@AddressListID int,
	@rows int,
	@Max int

SELECT @rows = 0, @AddressListID = @MinID, @Max = @MaxID + 1 ; 

IF (@MinID != 0 AND @MaxID != 0)
begin
	SET IDENTITY_INSERT QSPCanadaCommon.dbo.AddressList ON
	SET NOCOUNT ON
	while( @AddressListID < @Max )
	begin
		-------------------------------------------------
		--  insert a new @AddressListID   ---
		-------------------------------------------------
		INSERT INTO QSPCanadaCommon.dbo.AddressList(ID) VALUES(@AddressListID);
		SELECT @AddressListID = @AddressListID + 1;
		SELECT @rows = @rows + 1;
	end
	SET NOCOUNT OFF
	SET IDENTITY_INSERT QSPCanadaCommon.dbo.AddressList OFF
end
print '--Insert the new AddressListIDs - ' + CAST(@rows AS varchar) + ' rows affected'
GO
