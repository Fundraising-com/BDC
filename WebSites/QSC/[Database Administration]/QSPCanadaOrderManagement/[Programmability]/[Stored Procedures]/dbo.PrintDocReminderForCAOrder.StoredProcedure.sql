USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[PrintDocReminderForCAOrder]    Script Date: 06/07/2017 09:20:44 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[PrintDocReminderForCAOrder]
AS

DECLARE @count INT
DECLARE @Message VARCHAR(500)

SELECT  @count=count(*) 
FROM	 QspCanadaOrderManagement.dbo.Batch A
WHERE A.OrderID IN (Select BatchOrderId from QspCanadaOrderManagement.dbo.ReportRequestBatch 
           	                     Where IsPrinted = 0 and IsQSPPrint = 1
		        And createdate <= dateadd(mi,-20,getdate())
		      )
AND A.OrderTypeCode in (41001,41008)
AND A.statusInstance <> 40005
AND A.OrderQualifierID In (39001,39002,39003)


	IF IsNull(@count,0)> 0	BEGIN
	    SET @Message = 'There are '+CONVERT(VARCHAR(5),@count) +' orders available for printing.'
	    EXEC QSPCanadaCommon..Send_EMail  'CheckPrintDocument@qsp.com','michelle.staniforth@rd.com', 'Reminder: Campaign/Group Order ready for printing',@Message

	END
GO
