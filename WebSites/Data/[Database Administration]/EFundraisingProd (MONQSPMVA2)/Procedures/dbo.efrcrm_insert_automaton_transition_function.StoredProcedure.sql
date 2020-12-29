USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_automaton_transition_function]    Script Date: 02/14/2014 13:06:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Automaton_Transition_Function
CREATE PROCEDURE [dbo].[efrcrm_insert_automaton_transition_function] @Automaton_Id int OUTPUT, @State_To_Id int, @State_From_Id int, @Automaton_Function_Id int, @Sequence int AS
begin

insert into Automaton_Transition_Function(State_To_Id, State_From_Id, Automaton_Function_Id, Sequence) values(@State_To_Id, @State_From_Id, @Automaton_Function_Id, @Sequence)

select @Automaton_Id = SCOPE_IDENTITY()

end
GO
