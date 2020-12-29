USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_promotion_group]    Script Date: 02/14/2014 13:07:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Promotion_Group
CREATE PROCEDURE [dbo].[efrcrm_insert_promotion_group] @Promo_Group_ID int OUTPUT, @Description nvarchar(100) AS
begin

insert into Promotion_Group(Description) values(@Description)

select @Promo_Group_ID = SCOPE_IDENTITY()

end
GO
