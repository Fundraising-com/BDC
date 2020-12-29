USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_getReceivablePerSale]    Script Date: 02/14/2014 13:08:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
**	Retrieve properties by id's
**
**	dt_getproperties objid, null or '' -- retrieve all properties of the object itself
**	dt_getproperties objid, property -- retrieve the property specified
*/
Create  procedure [dbo].[sp_getReceivablePerSale](@datevalue datetime)
as

begin
SELECT Sales_ID, CAST(SUM(Adjustment_Amount) AS numeric(15, 4)) AS Adjustment_Amount
into #total_adjustment
FROM  dbo.Adjustment
WHERE (Adjustment_Date <= @dateValue)
GROUP BY Sales_ID
end

begin
SELECT dbo.Payment.Sales_ID, SUM(dbo.Payment.Payment_Amount) AS Total_Deposit
into #total_deposit
FROM dbo.Deposit INNER JOIN
                      dbo.Deposit_Item ON dbo.Deposit.Deposit_ID = dbo.Deposit_Item.Deposit_ID INNER JOIN
                      dbo.Payment ON dbo.Deposit_Item.Sales_ID = dbo.Payment.Sales_ID AND dbo.Deposit_Item.Paiement_No = dbo.Payment.Payment_No
WHERE (dbo.Deposit.Deposit_Date <= @dateValue)
GROUP BY dbo.Payment.Sales_ID
end

SELECT     dbo.Sale.Sales_ID, dbo.Sale.Total_Amount - COALESCE (#total_deposit.Total_Deposit, 0) 
                      - COALESCE (#total_adjustment.Adjustment_Amount, 0) AS Total_Receivable, dbo.Sale.Total_Amount, 
                      COALESCE (#total_deposit.Total_Deposit, 0) AS Total_Deposit_On_Sale, COALESCE (#total_adjustment.Adjustment_Amount, 0) 
                      AS Total_Adjustments_On_Sale, dbo.Sale.Client_Sequence_Code, dbo.Sale.Client_ID, dbo.Country.Currency_Code, 
                      dbo.fn_Return_Ageing_By_Sale(dbo.Sale.Sales_ID, @dateValue ) AS Ageing
FROM         dbo.Sale INNER JOIN
                      dbo.Client_Address ON dbo.Sale.Client_Sequence_Code = dbo.Client_Address.Client_Sequence_Code AND 
                      dbo.Sale.Client_ID = dbo.Client_Address.Client_ID INNER JOIN
                      dbo.Country ON dbo.Client_Address.Country_Code = dbo.Country.Country_Code LEFT OUTER JOIN
                      #total_adjustment ON dbo.Sale.Sales_ID = #total_adjustment.Sales_ID LEFT OUTER JOIN
                      #total_deposit ON dbo.Sale.Sales_ID = #total_deposit.Sales_ID
WHERE     (dbo.Sale.Total_Amount - COALESCE (#total_deposit.Total_Deposit, 0) - COALESCE (#total_adjustment.Adjustment_Amount, 0) > 0.01) 
                      AND (dbo.Sale.Actual_Ship_Date IS NOT NULL) AND (dbo.Sale.Box_Return_Date IS NULL) AND (dbo.Sale.Reship_Date IS NULL) AND 
                      (dbo.Client_Address.Address_Type = 'bt') and dbo.sale.sales_date <= @datevalue OR
                      (dbo.Sale.Total_Amount - COALESCE (#total_deposit.Total_Deposit, 0) - COALESCE (#total_adjustment.Adjustment_Amount, 0) > 0.01) 
                      AND (dbo.Sale.Actual_Ship_Date IS NOT NULL) AND (dbo.Sale.Box_Return_Date IS NOT NULL) AND (dbo.Sale.Reship_Date IS NOT NULL) AND 
                      (dbo.Client_Address.Address_Type = 'bt') and dbo.sale.sales_date <= @datevalue



drop table #total_adjustment
drop table #total_deposit
GO
