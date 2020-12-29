USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_automaton_state]    Script Date: 02/14/2014 13:06:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Automaton_State
CREATE PROCEDURE [dbo].[efrcrm_insert_automaton_state] @Automaton_Id int OUTPUT, @State_Id int, @Description varchar(50) AS
begin

insert into Automaton_State(State_Id, Description) values(@State_Id, @Description)

select @Automaton_Id = SCOPE_IDENTITY()

end
GO
