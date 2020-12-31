USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[LoadXMLFromFile]    Script Date: 06/07/2017 09:19:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LoadXMLFromFile]
(
		 @tcFileName		 		 VARCHAR(255),
		 @tcXMLString		 VARCHAR(8000) OUTPUT
) AS
BEGIN
		 -- Scratch variables used in the script
		 DECLARE @retVal INT
		 DECLARE @oXML INT
		 DECLARE @errorSource VARCHAR(8000)
		 DECLARE @errorDescription VARCHAR(8000)
		 DECLARE @loadRetVal INT

		 -- Initialize the XML document
		 EXEC @retVal = sp_OACreate 'MSXML2.DOMDocument', @oXML OUTPUT
		 IF (@retVal <> 0)
		 BEGIN
		 		 -- Trap errors if any
		 		 EXEC sp_OAGetErrorInfo @oXML, @errorSource OUTPUT, @errorDescription OUTPUT
		 		 RAISERROR (@errorDescription, 16, 1)

		 		 -- Release the reference to the COM object
		 		 EXEC sp_OADestroy @oXML
		 		 RETURN
		 END
print 'fff'
		 -- Load the XML into the document
		 EXEC @retVal = sp_OAMethod @oXML, 'load', @loadRetVal OUTPUT, @tcFileName
		 IF (@retVal <> 0)
		 BEGIN
		 		 -- Trap errors if any
		 		 EXEC sp_OAGetErrorInfo @oXML, @errorSource OUTPUT, @errorDescription OUTPUT
		 		 RAISERROR (@errorDescription, 16, 1)

		 		 -- Release the reference to the COM object
		 		 EXEC sp_OADestroy @oXML
		 		 RETURN
		 END
print 'fffwwww'
		 -- Get the loaded XML
		 EXEC @retVal = sp_OAMethod @oXML, 'xml', @tcXMLString OUTPUT
		 IF (@retVal <> 0)
		 BEGIN
		 		 -- Trap errors if any
		 		 EXEC sp_OAGetErrorInfo @oXML, @errorSource OUTPUT, @errorDescription OUTPUT
		 		 RAISERROR (@errorDescription, 16, 1)

		 		 -- Release the reference to the COM object
		 		 EXEC sp_OADestroy @oXML
		 		 RETURN
		 END
print 'sdfdfd'
		 -- Release the reference to the COM object
		 EXEC sp_OADestroy @oXML

END
GO
