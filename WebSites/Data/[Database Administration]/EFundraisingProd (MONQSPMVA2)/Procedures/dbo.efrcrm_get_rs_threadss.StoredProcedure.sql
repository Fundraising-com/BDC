USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_rs_threadss]    Script Date: 02/14/2014 13:05:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Rs_threads
CREATE PROCEDURE [dbo].[efrcrm_get_rs_threadss] AS
begin

select Id, Seq from Rs_threads

end
GO
