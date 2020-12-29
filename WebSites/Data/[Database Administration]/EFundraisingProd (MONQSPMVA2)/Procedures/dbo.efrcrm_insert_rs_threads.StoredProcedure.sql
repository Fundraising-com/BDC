USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_rs_threads]    Script Date: 02/14/2014 13:07:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Rs_threads
CREATE PROCEDURE [dbo].[efrcrm_insert_rs_threads] @Id int OUTPUT, @Seq int AS
begin

insert into Rs_threads(Seq) values(@Seq)

select @Id = SCOPE_IDENTITY()

end
GO
