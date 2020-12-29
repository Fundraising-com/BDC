USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_default_consultant_rate]    Script Date: 02/14/2014 13:07:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Default_Consultant_Rate
CREATE PROCEDURE [dbo].[efrcrm_update_default_consultant_rate] @Consultant_ID int, @Promotion_Type_Code varchar(4), @Default_Commission_Rate decimal AS
begin

update Default_Consultant_Rate set Promotion_Type_Code=@Promotion_Type_Code, Default_Commission_Rate=@Default_Commission_Rate where Consultant_ID=@Consultant_ID

end
GO
