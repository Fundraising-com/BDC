USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_getCollectionForFM]    Script Date: 02/14/2014 13:08:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  PROCEDURE [dbo].[sp_getCollectionForFM](@dateFrom datetime, @dateTo dateTime) AS
begin
	SELECT dbo.Country.Currency_Code, dbo.fn_Return_Ageing_By_Sale(dbo.Sale.Sales_ID, @dateTo) AS Ageing, 
               SUM(dbo.Payment.Payment_Amount) AS Collection
               FROM         dbo.Sale INNER JOIN
                      dbo.Client_Address ON dbo.Sale.Client_Sequence_Code = dbo.Client_Address.Client_Sequence_Code AND 
                      dbo.Sale.Client_ID = dbo.Client_Address.Client_ID INNER JOIN
                      dbo.Country ON dbo.Client_Address.Country_Code = dbo.Country.Country_Code INNER JOIN
                      dbo.Payment ON dbo.Sale.Sales_ID = dbo.Payment.Sales_ID INNER JOIN
                      dbo.Consultant ON dbo.Sale.Consultant_ID = dbo.Consultant.Consultant_ID
WHERE     (dbo.Client_Address.Address_Type = 'bt') AND (dbo.Sale.Sales_Date <= @dateTo) AND 
                      (dbo.Payment.Payment_Entry_Date BETWEEN CONVERT(DATETIME, @dateFrom, 102) AND CONVERT(DATETIME, @dateTo, 
                      102))AND (dbo.Consultant.Is_Fm = 1) OR
                      (dbo.Client_Address.Address_Type = 'bt') AND (dbo.Sale.Sales_Date <= @dateTo) AND (dbo.Payment.Payment_Entry_Date BETWEEN 
                      CONVERT(DATETIME, @dateFrom, 102) AND CONVERT(DATETIME, @dateTo, 102)) AND (dbo.Consultant.Is_Fm = 1)
GROUP BY dbo.Country.Currency_Code, dbo.fn_Return_Ageing_By_Sale(dbo.Sale.Sales_ID, @dateTo)
ORDER BY dbo.fn_Return_Ageing_By_Sale(dbo.Sale.Sales_ID, @dateTo)
end
GO
