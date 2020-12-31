USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_MainProgramsbyCampaign]    Script Date: 06/07/2017 09:21:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_MainProgramsbyCampaign]

           (	  @CampaignID    int  )

RETURNS Varchar(1000)  AS  

BEGIN 


 DECLARE @PROGNAME VARCHAR(500)
 DECLARE @v_name VARCHAR(500)

 DECLARE @rec int

 SET @REC = 0

  Declare Cur_progname Cursor  For
 select case prog.ID WHEN 2 THEN 'Magazine' WHEN 47 THEN 'Magazine' ELSE prog.name END name
 from   QspCanadaCommon..CampaignProgram cp,
        QspCanadaCommon..Program prog
 where  cp.programid = prog.id
 and   cp.campaignid = @CampaignID
 and   cp.DeletedTF = 0
 and	prog.ID IN (1, 2, 4, 44, 47)
order by prog.ID;


	OPEN Cur_progname
	    FETCH NEXT FROM Cur_progname INTO @v_name

	    WHILE @@FETCH_Status = 0
                  

                BEGIN

                       SET @rec = @rec +1

                        IF @REC = 1 

                           BEGIN
                             SET  @PROGNAME  = @v_name
                           END

                        ELSE
                            BEGIN
		     SET  @PROGNAME  = ISNULL(@PROGNAME,'') + ' , ' + @v_name
                            END 

                                     
                   FETCH NEXT FROM Cur_progname INTO @v_name
                END
 
 	CLOSE Cur_progname
	DEALLOCATE Cur_progname


  RETURN @PROGNAME
  
END
GO
