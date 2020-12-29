USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_automaton_transition]    Script Date: 02/14/2014 13:06:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Automaton_Transition
CREATE PROCEDURE [dbo].[efrcrm_insert_automaton_transition] @Automaton_Id int OUTPUT, @State_To_Id int, @State_From_Id int, @Description varchar(50) AS
begin

insert into Automaton_Transition(State_To_Id, State_From_Id, Description) values(@State_To_Id, @State_From_Id, @Description)

select @Automaton_Id = SCOPE_IDENTITY()

end
GO
