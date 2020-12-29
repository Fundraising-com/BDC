USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_automaton_transition]    Script Date: 02/14/2014 13:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Automaton_Transition
CREATE PROCEDURE [dbo].[efrcrm_update_automaton_transition] @Automaton_Id int, @State_To_Id int, @State_From_Id int, @Description varchar(50) AS
begin

update Automaton_Transition set State_To_Id=@State_To_Id, State_From_Id=@State_From_Id, Description=@Description where Automaton_Id=@Automaton_Id

end
GO
