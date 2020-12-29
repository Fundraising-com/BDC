USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_territory]    Script Date: 02/14/2014 13:08:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Territory
CREATE PROCEDURE [dbo].[efrcrm_update_territory] @Territory_id smallint, @Territory_name varchar(25) AS
begin

update Territory set Territory_name=@Territory_name where Territory_id=@Territory_id

end
GO
