USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_getReceivablePerAgeing]    Script Date: 02/14/2014 13:08:57 ******/
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
CREATE   procedure [dbo].[sp_getReceivablePerAgeing](@datevalue datetime)
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

SELECT      SUM(dbo.Sale.Total_Amount - COALESCE (dbo.#total_deposit.Total_Deposit, 0) 
                      - COALESCE (dbo.#total_adjustment.Adjustment_Amount, 0)) AS Total_Receivable, dbo.Country.Currency_Code, 
                      dbo.fn_Return_Ageing_By_Sale(dbo.Sale.Sales_ID, @datevalue) AS Ageing
FROM         dbo.Sale INNER JOIN
                      dbo.Client_Address ON dbo.Sale.Client_Sequence_Code = dbo.Client_Address.Client_Sequence_Code AND 
                      dbo.Sale.Client_ID = dbo.Client_Address.Client_ID INNER JOIN
                      dbo.Country ON dbo.Client_Address.Country_Code = dbo.Country.Country_Code LEFT OUTER JOIN
                      dbo.#total_adjustment ON dbo.Sale.Sales_ID = dbo.#total_adjustment.Sales_ID LEFT OUTER JOIN
                      dbo.#total_deposit ON dbo.Sale.Sales_ID = dbo.#total_deposit.Sales_ID
WHERE     (dbo.Sale.Actual_Ship_Date IS NOT NULL) AND (dbo.Sale.Box_Return_Date IS NULL) AND (dbo.Sale.Reship_Date IS NULL) AND 
                      (dbo.Client_Address.Address_Type = 'bt') AND (dbo.Sale.Sales_Date <= ' 2002 - 01 - 01') OR
                      (dbo.Sale.Actual_Ship_Date IS NOT NULL) AND (dbo.Sale.Box_Return_Date IS NOT NULL) AND (dbo.Sale.Reship_Date IS NOT NULL) AND 
                      (dbo.Client_Address.Address_Type = 'bt') AND (dbo.Sale.Sales_Date <= ' 2002 - 01 - 01')
GROUP BY dbo.Country.Currency_Code, dbo.fn_Return_Ageing_By_Sale(dbo.Sale.Sales_ID, @datevalue)
HAVING      (SUM(dbo.Sale.Total_Amount - COALESCE (dbo.#total_deposit.Total_Deposit, 0) - COALESCE (dbo.#total_adjustment.Adjustment_Amount, 0)) 
                      > 0.01) OR
                      (SUM(dbo.Sale.Total_Amount - COALESCE (dbo.#total_deposit.Total_Deposit, 0) - COALESCE (dbo.#total_adjustment.Adjustment_Amount, 0)) 
                      > 0.01)
ORDER BY dbo.fn_Return_Ageing_By_Sale(dbo.Sale.Sales_ID, @datevalue)


drop table #total_adjustment
drop table #total_deposit
GO
