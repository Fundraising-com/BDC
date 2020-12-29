USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_ar_activity_by_id]    Script Date: 02/14/2014 13:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for AR_Activity
CREATE PROCEDURE [dbo].[efrcrm_get_ar_activity_by_id] @AR_Activity_ID int AS
begin

select AR_Activity_ID, AR_Activity_Type_ID, Sales_ID, AR_Consultant_ID, AR_Activity_Date, Completed_Date, Comments from AR_Activity where AR_Activity_ID=@AR_Activity_ID

end
GO
