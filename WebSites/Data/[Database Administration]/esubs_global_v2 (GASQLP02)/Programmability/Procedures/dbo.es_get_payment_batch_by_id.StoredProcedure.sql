USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_payment_batch_by_id]    Script Date: 02/14/2014 13:06:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pavel Tarassov>
-- Create date: <13-02-2010>
-- Description:	<Gets payment_batch_by_id>
-- =============================================
CREATE PROCEDURE [dbo].[es_get_payment_batch_by_id] @Payment_batch_id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.


    SELECT [payment_batch_id]
      ,[filename]
      ,[createdate]
      ,[confirmation_date]
      ,[cancelled_date]
  FROM [dbo].[payment_batch] with (nolock)
	WHERE payment_batch_id = @Payment_batch_id 
END
GO
