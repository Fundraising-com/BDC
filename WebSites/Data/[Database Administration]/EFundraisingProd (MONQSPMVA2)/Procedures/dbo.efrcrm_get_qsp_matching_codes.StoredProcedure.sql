USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_qsp_matching_codes]    Script Date: 02/14/2014 13:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Qsp_matching_code
CREATE PROCEDURE [dbo].[efrcrm_get_qsp_matching_codes] AS
begin

select Id, Group_name, Address, Zip_code, Zzzzz, Nnn, Aa99, Zzzzzaa99, Zzzzznnnaa99 from Qsp_matching_code

end
GO
