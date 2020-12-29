USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_consultant_address]    Script Date: 02/14/2014 13:06:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Consultant_address
CREATE PROCEDURE [dbo].[efrcrm_insert_consultant_address] @Consultant_address_id int OUTPUT, @Consultant_id int, @Country_code varchar(10), @State_code varchar(10), @Street_address varchar(100), @City varchar(25), @Zip_code varchar(15), @Date_inserted datetime AS
begin

insert into Consultant_address(Consultant_id, Country_code, State_code, Street_address, City, Zip_code, Date_inserted) values(@Consultant_id, @Country_code, @State_code, @Street_address, @City, @Zip_code, @Date_inserted)

select @Consultant_address_id = SCOPE_IDENTITY()

end
GO
