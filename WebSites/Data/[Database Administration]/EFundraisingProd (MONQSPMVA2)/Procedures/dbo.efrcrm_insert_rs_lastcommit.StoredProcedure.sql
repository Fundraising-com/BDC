USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_rs_lastcommit]    Script Date: 02/14/2014 13:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Rs_lastcommit
CREATE PROCEDURE [dbo].[efrcrm_insert_rs_lastcommit] @Origin int OUTPUT, @Origin_qid binary, @Secondary_qid binary, @Origin_time datetime, @Commit_time datetime AS
begin

insert into Rs_lastcommit(Origin_qid, Secondary_qid, Origin_time, Commit_time) values(@Origin_qid, @Secondary_qid, @Origin_time, @Commit_time)

select @Origin = SCOPE_IDENTITY()

end
GO
