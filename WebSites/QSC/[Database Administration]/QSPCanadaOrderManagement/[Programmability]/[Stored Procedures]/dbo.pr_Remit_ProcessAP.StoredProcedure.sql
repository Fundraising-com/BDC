USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Remit_ProcessAP]    Script Date: 06/07/2017 09:20:23 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Remit_ProcessAP]

@iRunID int

AS

SET NOCOUNT ON

INSERT INTO QSPOracleInterface..OM_TBL_AP_INVOICES_INTF_BKUP select * from QSPOracleInterface..OM_TBL_AP_INVOICES_INTERFACE
INSERT INTO QSPOracleInterface..OM_TBL_AP_INV_LINES_INTF_BKUP select * from QSPOracleInterface..OM_TBL_AP_INV_LINES_INTERFACE
INSERT INTO QSPOracleInterface..OM_TBL_PO_VENDORS_INTF_BKUP select * from QSPOracleInterface..OM_TBL_PO_VENDORS_INTERFACE

DELETE QSPOracleInterface..OM_TBL_AP_INVOICES_INTERFACE
DELETE QSPOracleInterface..OM_TBL_AP_INV_LINES_INTERFACE
DELETE QSPOracleInterface..OM_TBL_PO_VENDORS_INTERFACE


declare a cursor for select id from remitbatch where  runid = @iRunID 

open a

declare @remitid int
fetch next from a into @remitid
while(@@fetch_status = 0)
begin

print 	@remitid
	declare @result varchar(50)
	exec pr_RemitBatchAPInterface @remitid, @result output	
	print @result
	fetch next from a into @remitid
end
close a
deallocate a

SET NOCOUNT OFF

IF @@error <> 0
	SELECT 1
ELSE 
	SELECT 0
GO
