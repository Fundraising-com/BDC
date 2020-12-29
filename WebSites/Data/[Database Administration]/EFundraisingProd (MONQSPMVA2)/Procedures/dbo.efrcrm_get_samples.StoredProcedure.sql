USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_samples]    Script Date: 02/14/2014 13:06:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Pavel Tarassov >
-- Create date: <28-01-2010>
-- Description:	<Get all active samples>
-- =============================================

-- Generate get stored proc for Subdivision
CREATE PROCEDURE [dbo].[efrcrm_get_samples] AS
begin
SELECT [SampleID]
      ,[SampleName]
      ,[Description]
      ,[Active]
  FROM [eFundraisingProd].[dbo].[Sample]
WHERE Active = 1
end
GO
