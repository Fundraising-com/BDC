USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_tags]    Script Date: 02/14/2014 13:08:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Tags
CREATE PROCEDURE [dbo].[efrcrm_update_tags] @Tags_ID int, @Label varchar(100), @Control_Name varchar(100) AS
begin

update Tags set Label=@Label, Control_Name=@Control_Name where Tags_ID=@Tags_ID

end
GO
