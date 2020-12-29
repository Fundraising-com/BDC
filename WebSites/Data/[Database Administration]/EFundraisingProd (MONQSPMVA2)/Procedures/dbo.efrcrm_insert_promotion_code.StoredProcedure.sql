USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_promotion_code]    Script Date: 02/14/2014 13:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Promotion_Code
CREATE PROCEDURE [dbo].[efrcrm_insert_promotion_code] @Promotion_Code_ID int OUTPUT, @Promotion_Code_Desc varchar(25) AS
begin

insert into Promotion_Code(Promotion_Code_Desc) values(@Promotion_Code_Desc)

select @Promotion_Code_ID = SCOPE_IDENTITY()

end
GO
