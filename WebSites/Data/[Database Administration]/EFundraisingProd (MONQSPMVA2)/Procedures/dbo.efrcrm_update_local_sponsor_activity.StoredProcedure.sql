USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_local_sponsor_activity]    Script Date: 02/14/2014 13:08:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Local_Sponsor_Activity
CREATE PROCEDURE [dbo].[efrcrm_update_local_sponsor_activity] @Local_Sponsor_Activity_ID int, @Local_Sponsor_Activity_Type_ID int, @Sales_ID int, @Sponsor_Consultant_ID int, @Local_Sponsor_Activity_Date smalldatetime, @Completed_Date smalldatetime, @Comments varchar(255), @Brand_ID int, @Local_Sponsor_ID int AS
begin

update Local_Sponsor_Activity set Local_Sponsor_Activity_Type_ID=@Local_Sponsor_Activity_Type_ID, Sales_ID=@Sales_ID, Sponsor_Consultant_ID=@Sponsor_Consultant_ID, Local_Sponsor_Activity_Date=@Local_Sponsor_Activity_Date, Completed_Date=@Completed_Date, Comments=@Comments, Brand_ID=@Brand_ID, Local_Sponsor_ID=@Local_Sponsor_ID where Local_Sponsor_Activity_ID=@Local_Sponsor_Activity_ID

end
GO
