USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_payment_batch]    Script Date: 02/14/2014 13:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pavel Tarassov>
-- Create date: <23-02-2010>
-- Description:	<inserts payment batch item>
-- =============================================
CREATE PROCEDURE [dbo].[es_insert_payment_batch]
	@Payment_batch_id int OUTPUT,
	@filename varchar(1024),
	@createdate datetime,
	@confirmation_date datetime,
	@cancelled_date datetime

AS
BEGIN


INSERT INTO [dbo].[payment_batch]
           ([filename]
           ,[createdate]
           ,[confirmation_date]
           ,[cancelled_date])
     VALUES
           (@filename
           ,@createdate
           ,@confirmation_date
           ,@cancelled_date)
select @Payment_batch_id= SCOPE_IDENTITY()
END
GO
