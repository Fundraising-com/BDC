USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_unassigned_consultant_sale]    Script Date: 02/14/2014 13:07:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Unassigned_Consultant_Sale
CREATE PROCEDURE [dbo].[efrcrm_insert_unassigned_consultant_sale] @Unassignation_ID int OUTPUT, @Sale_ID int, @Old_Consultant_ID int, @New_Consultant_ID int, @Unassigned_Date datetime AS
begin

insert into Unassigned_Consultant_Sale(Sale_ID, Old_Consultant_ID, New_Consultant_ID, Unassigned_Date) values(@Sale_ID, @Old_Consultant_ID, @New_Consultant_ID, @Unassigned_Date)

select @Unassignation_ID = SCOPE_IDENTITY()

end
GO
