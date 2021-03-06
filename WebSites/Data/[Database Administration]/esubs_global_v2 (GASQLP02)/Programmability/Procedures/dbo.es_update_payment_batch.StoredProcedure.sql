USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_payment_batch]    Script Date: 02/14/2014 13:07:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pavel Tarassov>
-- Create date: <02-26-2010>
-- Description:	<upadtes payment batch table>
-- =============================================
CREATE PROCEDURE [dbo].[es_update_payment_batch] 
	@payment_batch_id int,
	@filename varchar(1024),
    @createdate datetime,
    @confirmation_date datetime,
    @cancelled_date datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.


    UPDATE [dbo].[payment_batch]
   SET [filename] = @filename
      ,[createdate] = @createdate
      ,[confirmation_date] = @confirmation_date
      ,[cancelled_date] = @cancelled_date
 WHERE payment_batch_id = @payment_batch_id
END
GO
