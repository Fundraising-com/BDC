USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[view_list_state_code_coast]    Script Date: 02/14/2014 13:02:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE VIEW [dbo].[view_list_state_code_coast]
AS
SELECT
	State_Code
	, State_Name
	, CASE 
		WHEN Time_Zone_Difference >= -1 THEN 'EAST COAST' 
		ELSE 'WEST COAST' 
	  END AS Coast
FROM	dbo.State
GO
