USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_alias_states]    Script Date: 02/14/2014 13:03:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Alias_State
CREATE PROCEDURE [dbo].[efrcrm_get_alias_states] AS
begin

select Input_State_Code, State_Code from Alias_State

end
GO
