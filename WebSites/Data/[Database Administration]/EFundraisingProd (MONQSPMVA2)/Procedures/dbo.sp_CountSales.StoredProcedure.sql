USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_CountSales]    Script Date: 02/14/2014 13:08:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  Stored Procedure dbo.sp_CountSales    Script Date: 2003-02-22 20:34:55 ******/

create procedure [dbo].[sp_CountSales]
/* ( @parameter_name datatype [= default] [output], ... ) */
as
begin
  select* from Sale
end
GO
