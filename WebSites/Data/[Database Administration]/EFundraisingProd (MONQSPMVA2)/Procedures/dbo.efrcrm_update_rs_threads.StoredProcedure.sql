USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_rs_threads]    Script Date: 02/14/2014 13:08:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Rs_threads
CREATE PROCEDURE [dbo].[efrcrm_update_rs_threads] @Id int, @Seq int AS
begin

update Rs_threads set Seq=@Seq where Id=@Id

end
GO
