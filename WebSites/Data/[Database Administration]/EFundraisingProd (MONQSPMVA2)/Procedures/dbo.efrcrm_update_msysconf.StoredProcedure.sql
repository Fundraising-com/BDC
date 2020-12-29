USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_msysconf]    Script Date: 02/14/2014 13:08:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for MSysConf
CREATE PROCEDURE [dbo].[efrcrm_update_msysconf] @Config smallint, @CHValue varchar(255), @NValue int, @Comments varchar(255) AS
begin

update MSysConf set CHValue=@CHValue, NValue=@NValue, Comments=@Comments where Config=@Config

end
GO
