USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_lead_conditions]    Script Date: 02/14/2014 13:07:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Lead_Conditions
CREATE PROCEDURE [dbo].[efrcrm_insert_lead_conditions] @Condition_ID int OUTPUT, @Description varchar(2000) AS
begin

insert into Lead_Conditions(Description) values(@Description)

select @Condition_ID = SCOPE_IDENTITY()

end
GO
