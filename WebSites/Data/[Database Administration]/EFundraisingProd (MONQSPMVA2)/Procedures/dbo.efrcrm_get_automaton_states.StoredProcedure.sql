USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_automaton_states]    Script Date: 02/14/2014 13:03:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Automaton_State
CREATE PROCEDURE [dbo].[efrcrm_get_automaton_states] AS
begin

select Automaton_Id, State_Id, Description from Automaton_State

end
GO
