USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_automaton_by_id]    Script Date: 02/14/2014 13:03:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Automaton
CREATE PROCEDURE [dbo].[efrcrm_get_automaton_by_id] @Automaton_Id int AS
begin

select Automaton_Id, Description from Automaton where Automaton_Id=@Automaton_Id

end
GO
