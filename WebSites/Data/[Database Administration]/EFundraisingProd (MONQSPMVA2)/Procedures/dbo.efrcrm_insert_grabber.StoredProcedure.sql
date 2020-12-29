USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_grabber]    Script Date: 02/14/2014 13:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Grabber
CREATE PROCEDURE [dbo].[efrcrm_insert_grabber] @Grabber_Id int OUTPUT, @Grabber_Desc varchar(100) AS
begin

insert into Grabber(Grabber_Desc) values(@Grabber_Desc)

select @Grabber_Id = SCOPE_IDENTITY()

end
GO
