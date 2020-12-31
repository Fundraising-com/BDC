USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[Remit_RegenerateCheque_Complete]    Script Date: 06/07/2017 09:17:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
--	JMRF - 08/03/2010
-- =============================================
CREATE PROCEDURE [dbo].[Remit_RegenerateCheque_Complete]
@RunID INT

AS
BEGIN
      DECLARE @ProductCode VARCHAR(15)

      DECLARE product_cursor CURSOR FOR
--        SELECT codrh.remitcode--,cd.description,*
--        FROM   qspcanadaordermanagement..customerorderdetailremithistory codrh
--               JOIN qspcanadaordermanagement..remitbatch rb
--                 ON rb.id = codrh.remitbatchid
--               JOIN [QSPCanadaCommon].[dbo].[CodeDetail] cd
--                 ON codrh.status = cd.instance
--        WHERE  rb.runid = @RunID --AND codrh.status=42???
--        GROUP  BY codrh.remitcode,
--                  cd.DESCRIPTION
--        ORDER  BY codrh.remitcode,
--                  cd.DESCRIPTION
        SELECT apc.remitcode
        FROM   [QSPCanadaFinance].dbo.ap_cheque_remit apc
        WHERE  apc.RemitBatchId = @RunID
        GROUP  BY apc.remitcode
        ORDER  BY apc.remitcode

      OPEN product_cursor

      FETCH NEXT FROM product_cursor INTO @ProductCode;

      WHILE @@FETCH_STATUS = 0
        BEGIN
            EXEC QSPCanadaFinance.[dbo].[Remit_RegenerateCheque]
              @RunID,
              @ProductCode
            FETCH NEXT FROM product_cursor INTO @ProductCode;
        END

      CLOSE product_cursor;
      DEALLOCATE product_cursor;

--To push this batch execute switch to the Remit user and run this:
--EXEC QSPCanadaFinance..AP_Remit_CreateBatch

END
GO
