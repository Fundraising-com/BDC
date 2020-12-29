USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_associate_mentor]    Script Date: 02/14/2014 13:06:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Associate_Mentor
CREATE PROCEDURE [dbo].[efrcrm_insert_associate_mentor] @Associate_ID int OUTPUT, @Mentor_ID int, @Start_Date datetime, @End_Date datetime AS
begin

insert into Associate_Mentor(Mentor_ID, Start_Date, End_Date) values(@Mentor_ID, @Start_Date, @End_Date)

select @Associate_ID = SCOPE_IDENTITY()

end
GO
