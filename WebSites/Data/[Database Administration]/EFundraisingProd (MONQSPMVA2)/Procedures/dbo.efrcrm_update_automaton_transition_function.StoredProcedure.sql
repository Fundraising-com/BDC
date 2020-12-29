USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_automaton_transition_function]    Script Date: 02/14/2014 13:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Automaton_Transition_Function
CREATE PROCEDURE [dbo].[efrcrm_update_automaton_transition_function] @Automaton_Id int, @State_To_Id int, @State_From_Id int, @Automaton_Function_Id int, @Sequence int AS
begin

update Automaton_Transition_Function set State_To_Id=@State_To_Id, State_From_Id=@State_From_Id, Automaton_Function_Id=@Automaton_Function_Id, Sequence=@Sequence where Automaton_Id=@Automaton_Id

end
GO
