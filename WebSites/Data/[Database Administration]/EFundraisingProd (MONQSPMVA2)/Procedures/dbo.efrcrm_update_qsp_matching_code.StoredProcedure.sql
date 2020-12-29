USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_qsp_matching_code]    Script Date: 02/14/2014 13:08:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Qsp_matching_code
CREATE PROCEDURE [dbo].[efrcrm_update_qsp_matching_code] @Id int, @Group_name varchar(200), @Address varchar(100), @Zip_code varchar(10), @Zzzzz varchar(5), @Nnn varchar(3), @Aa99 varchar(4), @Zzzzzaa99 varchar(9), @Zzzzznnnaa99 varchar(12) AS
begin

update Qsp_matching_code set Group_name=@Group_name, Address=@Address, Zip_code=@Zip_code, Zzzzz=@Zzzzz, Nnn=@Nnn, Aa99=@Aa99, Zzzzzaa99=@Zzzzzaa99, Zzzzznnnaa99=@Zzzzznnnaa99 where Id=@Id

end
GO
