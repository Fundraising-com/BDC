USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_payment_batches]    Script Date: 02/14/2014 13:06:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[es_get_payment_batches]
	
AS
BEGIN
	SELECT [payment_batch_id]
      ,[filename]
      ,[createdate]
      ,[confirmation_date]
      ,[cancelled_date]
  FROM [dbo].[payment_batch]
END
GO
