USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_lead_activity_copy]    Script Date: 02/14/2014 13:06:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Lead_Activity_copy
CREATE PROCEDURE [dbo].[efrcrm_insert_lead_activity_copy] @Lead_Activity_Id int OUTPUT, @Lead_Id int, @Lead_Activity_Type_Id int, @Lead_Activity_Date datetime, @Completed_Date datetime, @Comments text AS
begin

insert into Lead_Activity_copy(Lead_Id, Lead_Activity_Type_Id, Lead_Activity_Date, Completed_Date, Comments) values(@Lead_Id, @Lead_Activity_Type_Id, @Lead_Activity_Date, @Completed_Date, @Comments)

select @Lead_Activity_Id = SCOPE_IDENTITY()

end
GO
