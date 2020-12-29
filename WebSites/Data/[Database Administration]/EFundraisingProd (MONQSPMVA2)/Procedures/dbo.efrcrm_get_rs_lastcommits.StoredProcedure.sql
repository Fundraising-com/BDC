USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_rs_lastcommits]    Script Date: 02/14/2014 13:05:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Rs_lastcommit
CREATE PROCEDURE [dbo].[efrcrm_get_rs_lastcommits] AS
begin

select Origin, Origin_qid, Secondary_qid, Origin_time, Commit_time from Rs_lastcommit

end
GO
