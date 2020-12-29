USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efr_get_lead_activity]    Script Date: 02/14/2014 13:03:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efr_get_lead_activity] 
	@lead_activity_id int
AS

SELECT
	lead_activity_id,
	lead_id,
	lead_activity_type_id,
	lead_activity_date,
	completed_date,
	comments
FROM lead_activity
WHERE
	lead_activity_id = @lead_activity_id
GO
