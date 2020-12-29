USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_automaton]    Script Date: 02/14/2014 13:06:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Automaton
CREATE PROCEDURE [dbo].[efrcrm_insert_automaton] @Automaton_Id int OUTPUT, @Description varchar(50) AS
begin

insert into Automaton(Description) values(@Description)

select @Automaton_Id = SCOPE_IDENTITY()

end
GO
