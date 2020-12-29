USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_associate_mentor]    Script Date: 02/14/2014 13:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Associate_Mentor
CREATE PROCEDURE [dbo].[efrcrm_update_associate_mentor] @Associate_ID int, @Mentor_ID int, @Start_Date datetime, @End_Date datetime AS
begin

update Associate_Mentor set Mentor_ID=@Mentor_ID, Start_Date=@Start_Date, End_Date=@End_Date where Associate_ID=@Associate_ID

end
GO
