USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_automaton_function]    Script Date: 02/14/2014 13:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Automaton_Function
CREATE PROCEDURE [dbo].[efrcrm_update_automaton_function] @Automaton_Function_Id int, @Function_Name varchar(50), @Description varchar(50) AS
begin

update Automaton_Function set Function_Name=@Function_Name, Description=@Description where Automaton_Function_Id=@Automaton_Function_Id

end
GO
