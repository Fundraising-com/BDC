USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_GetInternetOrderItems]    Script Date: 06/07/2017 09:21:03 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[UDF_GetInternetOrderItems]

	( @OrderID int,@StudentInstance int )

RETURNS int  AS  
BEGIN 

-- SS -  20 oct 2004
-- return the count of all items sold on Internet by a student
--used in the Participant Listing Report proc

  DECLARE  @Total_Internet_Items int

 Select @Total_Internet_Items = count(cod.ProductCode)
 From  QspCanadaOrderManagement.dbo.customerorderdetail cod,
          QspCanadaOrderManagement.dbo.customerorderheader coh,
          QspCanadaOrderManagement.dbo.batch as batch
 where batch.id = coh.orderbatchid
 and batch.date = coh.orderbatchdate
 and coh.instance = cod.customerorderheaderinstance
 and COD.StatusInstance <>  506 -- Voided Due To Error 
and coh.StudentInstance = @StudentInstance
 and batch.orderid in ( select distinct OnlineOrderId
 From   OnlineOrderMappingTable
 where  LandedOrderId  = @OrderID
   and  StudentInstance  = @StudentInstance )  
and coh.StudentInstance = @StudentInstance

  RETURN @Total_Internet_Items
  
END
GO
