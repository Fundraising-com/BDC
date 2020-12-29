USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_rs_threads_by_id]    Script Date: 02/14/2014 13:05:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Rs_threads
CREATE PROCEDURE [dbo].[efrcrm_get_rs_threads_by_id] @Id int AS
begin

select Id, Seq from Rs_threads where Id=@Id

end
GO
