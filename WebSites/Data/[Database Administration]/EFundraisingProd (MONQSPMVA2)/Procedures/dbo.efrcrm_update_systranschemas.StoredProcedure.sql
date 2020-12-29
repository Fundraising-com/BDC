USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_systranschemas]    Script Date: 02/14/2014 13:08:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Systranschemas
CREATE PROCEDURE [dbo].[efrcrm_update_systranschemas] @Tabid int, @Startlsn binary, @Endlsn binary AS
begin

update Systranschemas set Startlsn=@Startlsn, Endlsn=@Endlsn where Tabid=@Tabid

end
GO
