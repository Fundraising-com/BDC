USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_advertisment_type_by_id]    Script Date: 02/14/2014 13:03:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Advertisment_Type
CREATE PROCEDURE [dbo].[efrcrm_get_advertisment_type_by_id] @Advertisment_Type_ID int AS
begin

select Advertisment_Type_ID, Description from Advertisment_Type where Advertisment_Type_ID=@Advertisment_Type_ID

end
GO
