USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_priority_by_id]    Script Date: 02/14/2014 13:05:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Priority
CREATE PROCEDURE [dbo].[efrcrm_get_priority_by_id] @Priority_ID int AS
begin

select Priority_ID, Description, Color_Code from Priority where Priority_ID=@Priority_ID

end
GO
