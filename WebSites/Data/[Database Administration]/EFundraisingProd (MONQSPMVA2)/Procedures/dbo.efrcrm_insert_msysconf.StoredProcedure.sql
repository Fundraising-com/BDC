USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_msysconf]    Script Date: 02/14/2014 13:07:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for MSysConf
CREATE PROCEDURE [dbo].[efrcrm_insert_msysconf] @Config int OUTPUT, @CHValue varchar(255), @NValue int, @Comments varchar(255) AS
begin

insert into MSysConf(CHValue, NValue, Comments) values(@CHValue, @NValue, @Comments)

select @Config = SCOPE_IDENTITY()

end
GO
