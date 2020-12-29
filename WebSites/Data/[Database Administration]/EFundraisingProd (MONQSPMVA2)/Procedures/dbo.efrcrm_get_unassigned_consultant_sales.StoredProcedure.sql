USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_unassigned_consultant_sales]    Script Date: 02/14/2014 13:06:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Unassigned_Consultant_Sale
CREATE PROCEDURE [dbo].[efrcrm_get_unassigned_consultant_sales] AS
begin

select Unassignation_ID, Sale_ID, Old_Consultant_ID, New_Consultant_ID, Unassigned_Date from Unassigned_Consultant_Sale

end
GO
