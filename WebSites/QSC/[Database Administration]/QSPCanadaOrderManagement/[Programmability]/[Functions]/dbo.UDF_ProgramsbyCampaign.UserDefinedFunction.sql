USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_ProgramsbyCampaign]    Script Date: 06/07/2017 09:21:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_ProgramsbyCampaign]

           (	  @CampaignID    int  )

RETURNS Varchar(1000)  AS  

BEGIN 


 DECLARE @PROGNAME VARCHAR(500)
 DECLARE @v_name VARCHAR(500)

 DECLARE @rec int

 SET @REC = 0

  Declare Cur_progname Cursor  For
 select prog.name
 from   QspCanadaCommon..CampaignProgram cp,
        QspCanadaCommon..Program prog,
		QSPCanadaCommon..Campaign c
 where  cp.programid = prog.id
 and	c.ID = cp.CampaignID
 and   cp.campaignid = @CampaignID
 and   cp.DeletedTF = 0
 AND   (cp.OnlineOnly = 0 OR c.OnlineOnlyPrograms = 1)
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
