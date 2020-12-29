USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_unassigned_consultant]    Script Date: 02/14/2014 13:07:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Unassigned_Consultant
CREATE PROCEDURE [dbo].[efrcrm_insert_unassigned_consultant] @Lead_ID int OUTPUT, @Old_Consultant_ID int, @New_Consultant_ID int, @Unassigned_Date datetime, @Unassignation_ID int AS
begin

insert into Unassigned_Consultant(Old_Consultant_ID, New_Consultant_ID, Unassigned_Date, Unassignation_ID) values(@Old_Consultant_ID, @New_Consultant_ID, @Unassigned_Date, @Unassignation_ID)

select @Lead_ID = SCOPE_IDENTITY()

end
GO
