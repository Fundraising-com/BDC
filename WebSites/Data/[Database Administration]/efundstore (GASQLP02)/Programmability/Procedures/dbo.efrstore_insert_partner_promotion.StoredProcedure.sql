USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_partner_promotion]    Script Date: 02/14/2014 13:05:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Partner_promotion
CREATE PROCEDURE [dbo].[efrstore_insert_partner_promotion] @Partner_promotion_id int OUTPUT, @Partner_id int, @Promotion_id int, @Create_date datetime AS
begin

insert into Partner_promotion(Partner_id, Promotion_id, Create_date) values(@Partner_id, @Promotion_id, @Create_date)

select @Partner_promotion_id = SCOPE_IDENTITY()

end
GO
