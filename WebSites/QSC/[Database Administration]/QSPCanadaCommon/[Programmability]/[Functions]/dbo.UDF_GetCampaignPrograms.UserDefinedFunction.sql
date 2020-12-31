USE [QSPCanadaCommon]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_GetCampaignPrograms]    Script Date: 06/07/2017 09:33:38 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE Function [dbo].[UDF_GetCampaignPrograms](@CampaignID int)
Returns Varchar(200)
As
Begin


declare @programs varchar(200)
declare @abr varchar(200)
declare @id int
declare @progid int
set @programs=''
declare aSetProg  cursor for select id, programid from QSPCanadaCommon..CampaignProgram cp,QSPCanadaCommon..Campaign c
	where  C.ID = CampaignID		
		and cp.DeletedTF=0
		AND cp.OnlineOnly = 0
		and c.id = @CampaignID
open aSetProg
fetch next from aSetProg into @id, @progid
WHILE(@@fetch_status <> -1)
begin

	select @abr = Abr from qspcanadacommon..Program where 
			ID = @ProgID	
	set  @programs   = @programs + ' ' + @abr

	fetch next from aSetProg into @id, @progid

end
close aSetProg
deallocate aSetProg

Return @programs

End
GO
