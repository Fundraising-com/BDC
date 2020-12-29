USE [eFundraisingProd]
GO
/****** Object:  UserDefinedFunction [dbo].[fct_CheckIfInAR]    Script Date: 02/14/2014 13:09:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Cette fonction vérifie si une vente est en AR
Entrée: Le sales_ID
Sortie: Si elle est en AR ou non

Nicolas Désy
2 juin 2003
*/

create  FUNCTION [dbo].[fct_CheckIfInAR] (@SaleID int) RETURNS bit
AS
BEGIN
	DECLARE @Amount money;	-- Montant total à vérifier
	DECLARE @PaymentTotal money;	-- Somme des paiements de la vente
	DECLARE @AdjustmentTotal money;	-- Somme des ajustements de la vente
	DECLARE @SaleAmount money;	-- Montant dans le champ "Total_Amount" dépendamment si elle est box return et reshipped
	DECLARE @IsInAR bit;	-- Retour

	-- Calcul de la somme des paiements de la vente
	SELECT @PaymentTotal = (
		SELECT     
			SUM(dbo.Payment.Payment_Amount) AS SumOfPayments
	        FROM          
			dbo.Payment RIGHT OUTER JOIN dbo.Sale 
				ON dbo.Payment.Sales_ID = dbo.Sale.Sales_ID
		WHERE
			Sale.Sales_ID = @SaleID
	        GROUP BY 
	            	dbo.Sale.Sales_ID);
	-- Pas de paiement = 0
	IF @PaymentTotal IS NULL
		SET @PaymentTotal = 0;

	-- Calcul de la somme des ajustements	
	SELECT @AdjustmentTotal = (
		SELECT     
			SUM(dbo.Adjustment.Adjustment_Amount) AS SumOfAdjustments
	        FROM          
			dbo.Adjustment RIGHT OUTER JOIN dbo.Sale 
				ON dbo.Adjustment.Sales_ID = dbo.Sale.Sales_ID
		WHERE
			Sale.Sales_ID = @SaleID
	        GROUP BY 
			dbo.Sale.Sales_ID)
	-- Pas d'ajustement = 0
	IF @AdjustmentTotal IS NULL
		SET @AdjustmentTotal = 0;

	
	-- Si on a un box return date sans reship date, alors le montant de la vente est de 0;
	SELECT @SaleAmount = (SELECT (CASE dbo.Sale.Box_Return_Date WHEN NULL THEN Total_Amount ELSE (CASE dbo.Sale.Reship_Date WHEN NULL THEN 0 ELSE dbo.Sale.Total_Amount END) END) As Amount FROM dbo.Sale WHERE Sale.Sales_ID = @SaleID)

	-- Calcul de vérification si on est en AR
	SET @Amount = @SaleAmount - @AdjustmentTotal - @PaymentTotal;

	-- Plus qu'un dollar --> AR
	IF @Amount > 1
		SET @IsInAR = 1;
	ELSE
		SET @IsInAR = 0;
	
RETURN @IsInAR;
END
GO
