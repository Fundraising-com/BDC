USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_automaton_transitions]    Script Date: 02/14/2014 13:03:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Automaton_Transition
CREATE PROCEDURE [dbo].[efrcrm_get_automaton_transitions] AS
begin

select Automaton_Id, State_To_Id, State_From_Id, Description from Automaton_Transition

end
GO
