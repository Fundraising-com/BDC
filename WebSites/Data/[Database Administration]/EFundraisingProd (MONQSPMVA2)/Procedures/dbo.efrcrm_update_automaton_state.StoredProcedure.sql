USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_automaton_state]    Script Date: 02/14/2014 13:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Automaton_State
CREATE PROCEDURE [dbo].[efrcrm_update_automaton_state] @Automaton_Id int, @State_Id int, @Description varchar(50) AS
begin

update Automaton_State set State_Id=@State_Id, Description=@Description where Automaton_Id=@Automaton_Id

end
GO
