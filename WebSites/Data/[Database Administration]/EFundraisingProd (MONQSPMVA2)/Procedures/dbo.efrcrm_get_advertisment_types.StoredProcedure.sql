USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_advertisment_types]    Script Date: 02/14/2014 13:03:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Advertisment_Type
CREATE PROCEDURE [dbo].[efrcrm_get_advertisment_types] AS
begin

select Advertisment_Type_ID, Description from Advertisment_Type

end
GO
