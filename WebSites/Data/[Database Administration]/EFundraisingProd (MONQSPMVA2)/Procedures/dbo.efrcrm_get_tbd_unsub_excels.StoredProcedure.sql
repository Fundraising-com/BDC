USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_tbd_unsub_excels]    Script Date: 02/14/2014 13:06:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Tbd_unsub_excel
CREATE PROCEDURE [dbo].[efrcrm_get_tbd_unsub_excels] AS
begin

select Email, Method, Source , [Transaction date] from Tbd_unsub_excel

end
GO
