USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_automatons]    Script Date: 02/14/2014 13:03:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Automaton
CREATE PROCEDURE [dbo].[efrcrm_get_automatons] AS
begin

select Automaton_Id, Description from Automaton

end
GO
