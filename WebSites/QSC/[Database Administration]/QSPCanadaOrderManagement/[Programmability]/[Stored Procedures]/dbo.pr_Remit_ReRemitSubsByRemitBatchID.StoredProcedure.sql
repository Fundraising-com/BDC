USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Remit_ReRemitSubsByRemitBatchID]    Script Date: 06/07/2017 09:20:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
--  JMRF - 08/03/2010
-- =============================================

CREATE PROCEDURE [dbo].[pr_Remit_ReRemitSubsByRemitBatchID] 

@RunID INT

AS

BEGIN

      DECLARE @ProductCode VARCHAR(15)

     DECLARE product_cursor CURSOR FOR
        SELECT codrh.titlecode
        FROM   qspcanadaordermanagement..customerorderdetailremithistory codrh
               JOIN qspcanadaordermanagement..remitbatch rb
                 ON rb.id = codrh.remitbatchid
        WHERE  rb.runid = @RunID
        GROUP  BY codrh.titlecode
        ORDER  BY codrh.titlecode

      OPEN product_cursor

      FETCH NEXT FROM product_cursor INTO @ProductCode;

      WHILE @@FETCH_STATUS = 0
        BEGIN
            EXEC qspcanadaordermanagement.dbo.pr_Remit_ReRemitSubsByProductandRemitBatchID
              	@Product_Code = @ProductCode,
				@RunIDFrom = @RunID,
				@RunIDTo = @RunID,
				@AlreadyRemittedToPublisher = 1,
				@ReRemitSubs = 1,
				@RemitInactiveMagSubs = 1,
				@ReRemitCancels = 1,
				@ReRemitChadds = 1

            FETCH NEXT FROM product_cursor INTO @ProductCode;
        END

      CLOSE product_cursor;
      DEALLOCATE product_cursor;
  END
GO
