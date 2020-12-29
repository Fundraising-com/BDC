USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_default_consultant_rate]    Script Date: 02/14/2014 13:06:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Default_Consultant_Rate
CREATE PROCEDURE [dbo].[efrcrm_insert_default_consultant_rate] @Consultant_ID int OUTPUT, @Promotion_Type_Code varchar(4), @Default_Commission_Rate decimal AS
begin

insert into Default_Consultant_Rate(Promotion_Type_Code, Default_Commission_Rate) values(@Promotion_Type_Code, @Default_Commission_Rate)

select @Consultant_ID = SCOPE_IDENTITY()

end
GO
