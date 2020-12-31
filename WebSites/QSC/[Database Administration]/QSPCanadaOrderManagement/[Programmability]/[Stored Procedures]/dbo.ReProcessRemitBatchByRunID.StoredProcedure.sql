USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[ReProcessRemitBatchByRunID]    Script Date: 06/07/2017 09:20:46 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[ReProcessRemitBatchByRunID] 

@runid int,
@ReturnCode bit output

AS
update remitbatch set status=42000 where status=42001 and runid=@runid
delete from orderoutput where remitbatchid in (select id from remitbatch where runid=@runid)

update customerorderdetailremithistory set status=42000 where status=42001 and remitbatchid in (select id from remitbatch where runid=@runid)
update customerorderdetailremithistory set status=42002 where status=42003 and remitbatchid in (select id from remitbatch where runid=@runid)
update customerorderdetailremithistory set status=42006 where status=42007 and remitbatchid in (select id from remitbatch where runid=@runid)

exec ProcessRemitBatch @runid, @ReturnCode output
GO
