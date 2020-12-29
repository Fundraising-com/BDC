USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_efr_lead_activity]    Script Date: 02/14/2014 13:06:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Efr_Lead_Activity
CREATE PROCEDURE [dbo].[efrcrm_insert_efr_lead_activity] @Lead_Activity_Id int OUTPUT, @Lead_Id int, @Lead_Activity_Type_Id int, @Lead_Activity_Date smalldatetime, @Completed_Date smalldatetime, @Comments text AS
begin

insert into Efr_Lead_Activity(Lead_Id, Lead_Activity_Type_Id, Lead_Activity_Date, Completed_Date, Comments) values(@Lead_Id, @Lead_Activity_Type_Id, @Lead_Activity_Date, @Completed_Date, @Comments)

select @Lead_Activity_Id = SCOPE_IDENTITY()

end
GO
