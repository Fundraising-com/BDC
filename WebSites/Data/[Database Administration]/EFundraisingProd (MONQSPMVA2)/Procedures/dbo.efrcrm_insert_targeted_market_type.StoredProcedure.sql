USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_targeted_market_type]    Script Date: 02/14/2014 13:07:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Targeted_market_type
CREATE PROCEDURE [dbo].[efrcrm_insert_targeted_market_type] @Targeted_market_type_id int OUTPUT, @Description varchar(50), @Decision_maker bit, @Group_type_id tinyint, @Comments varchar(255) AS
begin

insert into Targeted_market_type(Description, Decision_maker, Group_type_id, Comments) values(@Description, @Decision_maker, @Group_type_id, @Comments)

select @Targeted_market_type_id = SCOPE_IDENTITY()

end
GO
