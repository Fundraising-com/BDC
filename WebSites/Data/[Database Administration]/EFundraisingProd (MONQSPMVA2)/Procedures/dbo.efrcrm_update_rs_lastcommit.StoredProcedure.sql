USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_rs_lastcommit]    Script Date: 02/14/2014 13:08:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Rs_lastcommit
CREATE PROCEDURE [dbo].[efrcrm_update_rs_lastcommit] @Origin int, @Origin_qid binary, @Secondary_qid binary, @Origin_time datetime, @Commit_time datetime AS
begin

update Rs_lastcommit set Origin_qid=@Origin_qid, Secondary_qid=@Secondary_qid, Origin_time=@Origin_time, Commit_time=@Commit_time where Origin=@Origin

end
GO
