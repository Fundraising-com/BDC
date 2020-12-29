USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_advertisement]    Script Date: 02/14/2014 13:07:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Advertisement
CREATE PROCEDURE [dbo].[efrcrm_update_advertisement] @Advertisement_id int, @Division_id tinyint, @Description varchar(50), @Size float, @Nb_colors int, @Comments varchar(255) AS
begin

update Advertisement set Division_id=@Division_id, Description=@Description, Size=@Size, Nb_colors=@Nb_colors, Comments=@Comments where Advertisement_id=@Advertisement_id

end
GO
