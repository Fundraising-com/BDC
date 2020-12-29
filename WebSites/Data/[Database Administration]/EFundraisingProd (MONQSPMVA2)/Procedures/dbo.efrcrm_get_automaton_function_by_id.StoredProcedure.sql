USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_automaton_function_by_id]    Script Date: 02/14/2014 13:03:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Automaton_Function
CREATE PROCEDURE [dbo].[efrcrm_get_automaton_function_by_id] @Automaton_Function_Id int AS
begin

select Automaton_Function_Id, Function_Name, Description from Automaton_Function where Automaton_Function_Id=@Automaton_Function_Id

end
GO
