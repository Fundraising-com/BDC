USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_automaton_function]    Script Date: 02/14/2014 13:06:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Automaton_Function
CREATE PROCEDURE [dbo].[efrcrm_insert_automaton_function] @Automaton_Function_Id int OUTPUT, @Function_Name varchar(50), @Description varchar(50) AS
begin

insert into Automaton_Function(Function_Name, Description) values(@Function_Name, @Description)

select @Automaton_Function_Id = SCOPE_IDENTITY()

end
GO
