USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_lead_qualification_type]    Script Date: 02/14/2014 13:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Lead_Qualification_Type
CREATE PROCEDURE [dbo].[efrcrm_insert_lead_qualification_type] @Lead_Qualification_Type_ID int OUTPUT, @Description varchar(200), @Weight int AS
begin

insert into Lead_Qualification_Type(Description, Weight) values(@Description, @Weight)

select @Lead_Qualification_Type_ID = SCOPE_IDENTITY()

end
GO
