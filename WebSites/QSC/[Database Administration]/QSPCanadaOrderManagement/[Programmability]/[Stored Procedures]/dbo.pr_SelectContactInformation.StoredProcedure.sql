USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectContactInformation]    Script Date: 06/07/2017 09:20:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_SelectContactInformation] 

@iOrderID int= 0

AS

select  contactfirstname as FirstName,
           contactlastname as LastName,
           contactphone as Phone,
           'N/A' as Fax,
           contactemail as Email
    from batch
 where orderid=@iOrderID
GO
