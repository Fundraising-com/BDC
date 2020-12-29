USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_associate_mentor_commission]    Script Date: 02/14/2014 13:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Associate_mentor_commission
CREATE PROCEDURE [dbo].[efrcrm_update_associate_mentor_commission] @Associate_id int, @Mentor_id int, @Product_class_id tinyint, @Commission_rate float, @Comments varchar(255) AS
begin

update Associate_mentor_commission set Mentor_id=@Mentor_id, Product_class_id=@Product_class_id, Commission_rate=@Commission_rate, Comments=@Comments where Associate_id=@Associate_id

end
GO
