USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_GetDistributionCentreNamesByOrder]    Script Date: 06/07/2017 09:21:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[UDF_GetDistributionCentreNamesByOrder]

           (	  @pOrderID    int  )

RETURNS Varchar(1000)  AS  

BEGIN 


 DECLARE @v_name VARCHAR(500)
 DECLARE @v_wh VARCHAR(500)

 DECLARE @rec int

 SET @REC = 0

  Declare c1 Cursor  For
  select distinct [Name]
  from QSPCanadaOrderManagement.dbo.BatchDistributionCenter bdc,
       QSPCanadaOrderManagement.dbo.DistributionCenter dc,
       QSPCanadaOrderManagement.dbo.Batch batch	
  where bdc.batchID = batch.id
  and bdc.BatchDate  = batch.date
  and bdc.distributionCenterId  = dc.ID
  and orderid  = @pOrderID 


	OPEN c1
	    FETCH NEXT FROM c1 INTO @v_name

	    WHILE @@FETCH_Status = 0
                  

                BEGIN

                       SET @rec = @rec +1

                        IF @REC = 1 

                           BEGIN
                             SET  @v_wh  = @v_name
                           END

                        ELSE
                            BEGIN
		     SET  @v_wh  = ISNULL(@v_wh,'') + ' , ' + @v_name
                            END 

                                     
                   FETCH NEXT FROM c1 INTO @v_name
                END
 
 	CLOSE c1
	DEALLOCATE c1


  RETURN @v_wh
  
END
GO
