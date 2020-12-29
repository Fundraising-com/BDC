USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_associate_mentor_commission]    Script Date: 02/14/2014 13:06:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Associate_mentor_commission
CREATE PROCEDURE [dbo].[efrcrm_insert_associate_mentor_commission] @Associate_id int OUTPUT, @Mentor_id int, @Product_class_id tinyint, @Commission_rate float, @Comments varchar(255) AS
begin

insert into Associate_mentor_commission(Mentor_id, Product_class_id, Commission_rate, Comments) values(@Mentor_id, @Product_class_id, @Commission_rate, @Comments)

select @Associate_id = SCOPE_IDENTITY()

end
GO
