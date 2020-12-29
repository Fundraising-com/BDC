USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_ar_activitys]    Script Date: 02/14/2014 13:03:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for AR_Activity
CREATE PROCEDURE [dbo].[efrcrm_get_ar_activitys] AS
begin

select AR_Activity_ID, AR_Activity_Type_ID, Sales_ID, AR_Consultant_ID, AR_Activity_Date, Completed_Date, Comments from AR_Activity

end
GO
