USE [eFundraisingProd]
GO
/****** Object:  UserDefinedFunction [dbo].[fetchSalesAmount]    Script Date: 02/14/2014 13:09:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  User Defined Function dbo.fetchSalesAmount    Script Date: 2003-02-22 20:35:35 ******/

CREATE function [dbo].[fetchSalesAmount](@nSales_id integer)
returns numeric(15,4)
begin
  declare @nSalesAmount numeric(15,4);
  /* Fait la requête */
  SELECT @nSalesAmount=
  (select sale_amount
    from total_by_sale where
    sales_id = @nSales_id)
  return(@nSalesAmount)
end
GO
