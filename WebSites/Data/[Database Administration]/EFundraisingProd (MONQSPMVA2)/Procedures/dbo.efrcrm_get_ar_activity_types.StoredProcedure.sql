USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_ar_activity_types]    Script Date: 02/14/2014 13:03:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for AR_Activity_Type
CREATE PROCEDURE [dbo].[efrcrm_get_ar_activity_types] AS
begin

select AR_Activity_Type_Id, Description from AR_Activity_Type

end
GO
