USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[GetProgramsbyCampaign]    Script Date: 06/07/2017 09:33:09 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetProgramsbyCampaign]
@p_campaignid int, @p_program_name varchar(500) output
As

 DECLARE @PROGNAME VARCHAR(500)
 DECLARE @v_name VARCHAR(500)
 declare @rec int
set @rec = 0

  Declare Cur_progname Cursor  For
 select prog.name
 from   QspCanadaCommon..CampaignProgram cp,
        QspCanadaCommon..Program prog
 where  cp.programid = prog.id
 and   cp.campaignid = @p_campaignid
 and   cp.DeletedTF = 0
 AND	cp.OnlineOnly = 0
order by prog.name;


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


  set @p_program_name = @PROGNAME
GO
