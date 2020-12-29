USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_advertisement]    Script Date: 02/14/2014 13:06:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Advertisement
CREATE PROCEDURE [dbo].[efrcrm_insert_advertisement] @Advertisement_id int OUTPUT, @Division_id tinyint, @Description varchar(50), @Size float, @Nb_colors int, @Comments varchar(255) AS
begin

insert into Advertisement(Division_id, Description, Size, Nb_colors, Comments) values(@Division_id, @Description, @Size, @Nb_colors, @Comments)

select @Advertisement_id = SCOPE_IDENTITY()

end
GO
