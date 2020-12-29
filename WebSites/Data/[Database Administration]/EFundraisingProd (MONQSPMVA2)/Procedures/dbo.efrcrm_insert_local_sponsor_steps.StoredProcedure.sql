USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_local_sponsor_steps]    Script Date: 02/14/2014 13:07:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Local_Sponsor_Steps
CREATE PROCEDURE [dbo].[efrcrm_insert_local_sponsor_steps] @Step_Id int OUTPUT, @Description varchar(50) AS
begin

insert into Local_Sponsor_Steps(Description) values(@Description)

select @Step_Id = SCOPE_IDENTITY()

end
GO
