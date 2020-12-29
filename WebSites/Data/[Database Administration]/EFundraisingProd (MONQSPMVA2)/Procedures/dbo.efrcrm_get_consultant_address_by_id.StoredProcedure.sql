USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_consultant_address_by_id]    Script Date: 02/14/2014 13:04:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Consultant_address
CREATE PROCEDURE [dbo].[efrcrm_get_consultant_address_by_id] @Consultant_address_id int AS
begin

select Consultant_address_id, Consultant_id, Country_code, State_code, Street_address, City, Zip_code, Date_inserted from Consultant_address where Consultant_address_id=@Consultant_address_id

end
GO
