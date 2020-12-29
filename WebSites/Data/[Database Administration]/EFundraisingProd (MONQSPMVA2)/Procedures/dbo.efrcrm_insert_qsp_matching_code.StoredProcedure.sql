USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_qsp_matching_code]    Script Date: 02/14/2014 13:07:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Qsp_matching_code
CREATE PROCEDURE [dbo].[efrcrm_insert_qsp_matching_code] @Id int OUTPUT, @Group_name varchar(200), @Address varchar(100), @Zip_code varchar(10), @Zzzzz varchar(5), @Nnn varchar(3), @Aa99 varchar(4), @Zzzzzaa99 varchar(9), @Zzzzznnnaa99 varchar(12) AS
begin

insert into Qsp_matching_code(Group_name, Address, Zip_code, Zzzzz, Nnn, Aa99, Zzzzzaa99, Zzzzznnnaa99) values(@Group_name, @Address, @Zip_code, @Zzzzz, @Nnn, @Aa99, @Zzzzzaa99, @Zzzzznnnaa99)

select @Id = SCOPE_IDENTITY()

end
GO
