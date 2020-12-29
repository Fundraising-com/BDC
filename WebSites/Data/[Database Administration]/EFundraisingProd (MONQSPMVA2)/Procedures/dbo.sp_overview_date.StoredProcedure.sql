USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_overview_date]    Script Date: 02/14/2014 13:09:03 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE   PROCEDURE [dbo].[sp_overview_date] (@date_from DATETIME = null, @date_to DATETIME = null, @Partner_ID INT = 8) AS

CREATE TABLE #tbout(Partrner_ID INT,
        Promotion_ID INT,
        Description VARCHAR(50),
        CountOfLeadID INT,
        CountOfSalesID INT,
        SumOfBrochure DECIMAL(38,4),
        SumOfCandies DECIMAL(38,4),
        SumOfScratchcard DECIMAL(38,4))

DECLARE @Partrner_ID INT,
        @Promotion_ID INT,
        @Description VARCHAR(50),
        @CountOfLeadID INT,
        @CountOfSalesID INT,
        @SumOfBrochure DECIMAL(38,4),
        @SumOfCandies DECIMAL(38,4),
        @SumOfScratchcard DECIMAL(38,4)

SELECT Promotion.Partner_ID,
       Promotion.Promotion_ID,
       Promotion.Description,
       SUM(CASE WHEN Product_Class.Description = 'Brochure' THEN Sales_Item.Sales_Amount ELSE 0 END) AS SumOfBrochure,
       SUM(CASE WHEN Product_Class.Description = 'Candies' THEN Sales_Item.Sales_Amount ELSE 0 END) AS SumOfCandies,
       SUM(CASE WHEN Product_Class.Description = 'Scratchcard' THEN Sales_Item.Sales_Amount ELSE 0 END) AS SumOfScratchcard
  INTO #tb1
  FROM Promotion
       JOIN Lead ON Lead.Promotion_ID = Promotion.Promotion_ID
       LEFT JOIN Client ON Client.Lead_ID = Lead.Lead_ID
       JOIN Sale ON Sale.Client_ID = Client.Client_ID and Sale.Client_Sequence_Code = Client.Client_Sequence_Code
       JOIN Sales_Item ON Sales_Item.Sales_ID = Sale.Sales_ID
       JOIN Scratch_Book ON Scratch_Book.Scratch_Book_ID = Sales_Item.Scratch_Book_ID
       JOIN Product_Class ON Product_Class.Product_Class_ID = Scratch_Book.Product_Class_ID
 WHERE Promotion.Partner_ID = @Partner_ID
   AND Sale.Actual_Ship_Date IS NOT NULL
   AND ((Sale.Reship_Date IS NULL AND Sale.Box_Return_Date IS NULL)
         OR
        (Sale.Reship_Date IS NOT NULL AND Sale.Box_Return_Date IS NOT NULL))
   AND Lead.Lead_Entry_Date BETWEEN isnull(@date_from, Lead.Lead_Entry_Date) AND isnull(@date_to, Lead.Lead_Entry_Date)
 GROUP BY Promotion.Partner_ID,
          Promotion.Promotion_ID,
          Promotion.Description

DECLARE c1 CURSOR FOR SELECT * FROM #tb1 ORDER BY Partner_ID, Promotion_ID
OPEN c1

FETCH NEXT FROM c1 INTO @Partrner_ID, @Promotion_ID, @Description, @SumOfBrochure, @SumOfCandies, @SumOfScratchcard
WHILE @@FETCH_STATUS = 0
BEGIN
    SELECT @CountOfLeadID = COUNT(*)
        FROM Lead, Promotion
      WHERE Lead.Promotion_ID = Promotion.Promotion_ID
        AND Lead.Lead_Entry_Date BETWEEN isnull(@date_from, Lead.Lead_Entry_Date) AND isnull(@date_to, Lead.Lead_Entry_Date) 
        AND Promotion.Partner_ID = @Partner_ID
        AND Promotion.Promotion_ID = @Promotion_ID

    SELECT @CountOfSalesID = COUNT(*)
      FROM Sale, Client, Lead, Promotion
     WHERE Sale.Client_ID = Client.Client_ID
       AND Sale.Client_Sequence_Code = Client.Client_Sequence_Code
       AND Client.Lead_ID = Lead.Lead_ID
       AND Lead.Promotion_ID = Promotion.Promotion_ID
       AND Lead.Lead_Entry_Date BETWEEN isnull(@date_from, Lead.Lead_Entry_Date) AND isnull(@date_to, Lead.Lead_Entry_Date)
       AND Promotion.Partner_ID = @Partner_ID
       AND Promotion.Promotion_ID = @Promotion_ID

    INSERT INTO #tbout VALUES(@Partrner_ID, @Promotion_ID, @Description, @CountOfLeadID, @CountOfSalesID, @SumOfBrochure, @SumOfCandies, @SumOfScratchcard)

    FETCH NEXT FROM c1 INTO @Partrner_ID, @Promotion_ID, @Description, @SumOfBrochure, @SumOfCandies, @SumOfScratchcard
END

CLOSE c1
DEALLOCATE c1

SELECT * FROM #tbout
GO
