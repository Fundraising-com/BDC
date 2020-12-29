USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_lead_activity_type]    Script Date: 02/14/2014 13:06:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Lead_Activity_Type
CREATE PROCEDURE [dbo].[efrcrm_insert_lead_activity_type] @Lead_Activity_Type_Id int OUTPUT, @Description varchar(50), @Priority int AS
begin

insert into Lead_Activity_Type(Description, Priority) values(@Description, @Priority)

select @Lead_Activity_Type_Id = SCOPE_IDENTITY()

end
GO
