USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[ClearOutOrder]    Script Date: 06/07/2017 09:17:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[ClearOutOrder]
	@orderid int
as
declare @invoiceid int

select @invoiceid=invoice_id from invoice where order_id = @orderid
select * from invoice where order_id = @orderid
select * from invoice_section where invoice_id= @invoiceid



select * from gl_entry where invoice_id =@invoiceid
GO
