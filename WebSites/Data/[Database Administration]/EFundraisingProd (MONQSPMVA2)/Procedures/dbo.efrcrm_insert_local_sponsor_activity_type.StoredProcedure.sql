USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_local_sponsor_activity_type]    Script Date: 02/14/2014 13:07:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Local_Sponsor_Activity_Type
CREATE PROCEDURE [dbo].[efrcrm_insert_local_sponsor_activity_type] @Local_Sponsor_Activity_Type_Id int OUTPUT, @Description varchar(50) AS
begin

insert into Local_Sponsor_Activity_Type(Description) values(@Description)

select @Local_Sponsor_Activity_Type_Id = SCOPE_IDENTITY()

end
GO
