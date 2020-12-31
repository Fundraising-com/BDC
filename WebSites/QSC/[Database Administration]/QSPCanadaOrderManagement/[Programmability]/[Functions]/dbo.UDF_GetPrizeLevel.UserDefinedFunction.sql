USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_GetPrizeLevel]    Script Date: 06/07/2017 09:21:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_GetPrizeLevel]

	( @StudentInstance int, @OrderID int )

RETURNS Varchar(1)  AS  
BEGIN 

  DECLARE  @V_LevelCode VARCHAR(100)

         

 SELECT @V_LevelCode = Substring(MAX(productcode),3,1)
 from qspcanadaordermanagement..customerorderdetail  as cod,qspcanadaordermanagement..customerorderheader as coh,qspcanadaordermanagement..batch as batch
 where batch.id = coh.orderbatchid
 and batch.date = coh.orderbatchdate
 and coh.instance = cod.customerorderheaderinstance
 and batch.orderid  = @OrderID
 and coh.StudentInstance  = @StudentInstance
 and cod.producttype  in ( 46008,46013,46014,46015) 


  RETURN @V_LevelCode
  
END
GO
