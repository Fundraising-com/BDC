USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_sport_association]    Script Date: 02/14/2014 13:07:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Sport_Association
CREATE PROCEDURE [dbo].[efrcrm_insert_sport_association] @Sport_Association_Id int OUTPUT, @Sport_Ass_Desc varchar(50) AS
begin

insert into Sport_Association(Sport_Ass_Desc) values(@Sport_Ass_Desc)

select @Sport_Association_Id = SCOPE_IDENTITY()

end
GO
