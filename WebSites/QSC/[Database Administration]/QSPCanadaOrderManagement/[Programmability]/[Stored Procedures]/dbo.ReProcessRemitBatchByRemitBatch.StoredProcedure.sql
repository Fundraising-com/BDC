USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[ReProcessRemitBatchByRemitBatch]    Script Date: 06/07/2017 09:20:46 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[ReProcessRemitBatchByRemitBatch] 

@rbid int

AS

update remitbatch set status=42000 where status=42001 and id=@rbid
delete from orderoutput where remitbatchid in (select id from remitbatch where id=@rbid)

update customerorderdetailremithistory set status=42000 where status=42001 and remitbatchid in (select id from remitbatch where id=@rbid)
update customerorderdetailremithistory set status=42002 where status=42003 and remitbatchid in (select id from remitbatch where id=@rbid)
update customerorderdetailremithistory set status=42006 where status=42007 and remitbatchid in (select id from remitbatch where id=@rbid)

exec ProcessRemitBatchByRemitBatchID @rbid
GO
