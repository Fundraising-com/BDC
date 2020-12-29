USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_lead_qualification_type]    Script Date: 02/14/2014 13:08:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Lead_Qualification_Type
CREATE PROCEDURE [dbo].[efrcrm_update_lead_qualification_type] @Lead_Qualification_Type_ID int, @Description varchar(200), @Weight int AS
begin

update Lead_Qualification_Type set Description=@Description, Weight=@Weight where Lead_Qualification_Type_ID=@Lead_Qualification_Type_ID

end
GO
