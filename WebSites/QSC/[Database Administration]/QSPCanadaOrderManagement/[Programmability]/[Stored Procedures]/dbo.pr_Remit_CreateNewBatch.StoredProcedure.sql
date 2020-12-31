USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Remit_CreateNewBatch]    Script Date: 06/07/2017 09:20:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Remit_CreateNewBatch]

AS

DECLARE @iRunID int
SELECT	@iRunID = max(runid) + 1 from remitbatch

UPDATE  remitbatch 
SET 	runid=@iRunID 
WHERE 	status=42000 
AND 	runid is null


declare @fulfill varchar(20)
declare @maxid int

declare aNewBatch cursor for select fulfillmenthousenbr from remitbatch where status=42000 and runid=@iRunID
open aNewBatch

fetch next from aNewBatch into @fulfill
while(@@fetch_status <> -1 )
begin
	print @fulfill

	create table #tempinst
	(
		 NextInstance int
	)
	insert into #tempinst exec InsertNextInstance 17

	select @maxid = NextInstance from #tempinst
	drop table #tempinst
	insert RemitBatch(ID, Date, Status, FulfillmentHouseNbr, UserIDChanged,DateChanged) 
		values(@maxid, '1/1/95', 42000,@fulfill,'ADMI',GetDate())

	fetch next from aNewBatch into @fulfill
end
close aNewBatch
deallocate aNewBatch


SELECT @iRunID
GO
