USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_automaton_transition_functions]    Script Date: 02/14/2014 13:03:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Automaton_Transition_Function
CREATE PROCEDURE [dbo].[efrcrm_get_automaton_transition_functions] AS
begin

select Automaton_Id, State_To_Id, State_From_Id, Automaton_Function_Id, Sequence from Automaton_Transition_Function

end
GO
