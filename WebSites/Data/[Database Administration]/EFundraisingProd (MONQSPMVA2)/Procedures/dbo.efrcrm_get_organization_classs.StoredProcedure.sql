USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_organization_classs]    Script Date: 02/14/2014 13:05:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Organization_Class
CREATE PROCEDURE [dbo].[efrcrm_get_organization_classs] AS
begin

select Organization_Class_Code, Description, Accept_PO, Is_Distributor from Organization_Class

end
GO
