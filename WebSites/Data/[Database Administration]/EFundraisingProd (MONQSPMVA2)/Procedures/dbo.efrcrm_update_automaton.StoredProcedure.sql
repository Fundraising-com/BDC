USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_automaton]    Script Date: 02/14/2014 13:07:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Automaton
CREATE PROCEDURE [dbo].[efrcrm_update_automaton] @Automaton_Id int, @Description varchar(50) AS
begin

update Automaton set Description=@Description where Automaton_Id=@Automaton_Id

end
GO
