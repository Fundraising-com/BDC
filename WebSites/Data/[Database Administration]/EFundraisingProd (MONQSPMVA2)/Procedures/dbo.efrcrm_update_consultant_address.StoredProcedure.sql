USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_consultant_address]    Script Date: 02/14/2014 13:07:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Consultant_address
CREATE PROCEDURE [dbo].[efrcrm_update_consultant_address] @Consultant_address_id smallint, @Consultant_id int, @Country_code varchar(10), @State_code varchar(10), @Street_address varchar(100), @City varchar(25), @Zip_code varchar(15), @Date_inserted datetime AS
begin

update Consultant_address set Consultant_id=@Consultant_id, Country_code=@Country_code, State_code=@State_code, Street_address=@Street_address, City=@City, Zip_code=@Zip_code, Date_inserted=@Date_inserted where Consultant_address_id=@Consultant_address_id

end
GO
