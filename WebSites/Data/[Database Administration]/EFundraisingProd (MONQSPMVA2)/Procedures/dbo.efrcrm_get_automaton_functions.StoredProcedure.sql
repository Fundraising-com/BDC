USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_automaton_functions]    Script Date: 02/14/2014 13:03:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Automaton_Function
CREATE PROCEDURE [dbo].[efrcrm_get_automaton_functions] AS
begin

select Automaton_Function_Id, Function_Name, Description from Automaton_Function

end
GO
