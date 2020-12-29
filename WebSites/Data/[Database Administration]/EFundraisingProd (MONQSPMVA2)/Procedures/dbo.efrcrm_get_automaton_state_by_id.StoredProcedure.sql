USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_automaton_state_by_id]    Script Date: 02/14/2014 13:03:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Automaton_State
CREATE PROCEDURE [dbo].[efrcrm_get_automaton_state_by_id] @Automaton_Id int AS
begin

select Automaton_Id, State_Id, Description from Automaton_State where Automaton_Id=@Automaton_Id

end
GO
