USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_advertisment_type]    Script Date: 02/14/2014 13:07:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Advertisment_Type
CREATE PROCEDURE [dbo].[efrcrm_update_advertisment_type] @Advertisment_Type_ID int, @Description varchar(200) AS
begin

update Advertisment_Type set Description=@Description where Advertisment_Type_ID=@Advertisment_Type_ID

end
GO
