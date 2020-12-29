USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_local_sponsor_activity]    Script Date: 02/14/2014 13:07:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Local_Sponsor_Activity
CREATE PROCEDURE [dbo].[efrcrm_insert_local_sponsor_activity] @Local_Sponsor_Activity_ID int OUTPUT, @Local_Sponsor_Activity_Type_ID int, @Sales_ID int, @Sponsor_Consultant_ID int, @Local_Sponsor_Activity_Date smalldatetime, @Completed_Date smalldatetime, @Comments varchar(255), @Brand_ID int, @Local_Sponsor_ID int AS
begin

insert into Local_Sponsor_Activity(Local_Sponsor_Activity_Type_ID, Sales_ID, Sponsor_Consultant_ID, Local_Sponsor_Activity_Date, Completed_Date, Comments, Brand_ID, Local_Sponsor_ID) values(@Local_Sponsor_Activity_Type_ID, @Sales_ID, @Sponsor_Consultant_ID, @Local_Sponsor_Activity_Date, @Completed_Date, @Comments, @Brand_ID, @Local_Sponsor_ID)

select @Local_Sponsor_Activity_ID = SCOPE_IDENTITY()

end
GO
