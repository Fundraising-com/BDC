USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_IsCampaignCombo]    Script Date: 06/07/2017 09:21:04 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[UDF_IsCampaignCombo]

           (	  @CampaignID    int  )

RETURNS int

 AS  

BEGIN 

Declare @IsCombo int 
Set @IsCombo  = 0 

Declare @campaigncnt int
Declare @combocount int

--- First Test For More Than 1 Program
SELECT  @campaigncnt=isnull(count(*),0)  FROM QSPCanadaCommon..CampaignProgram WHERE CampaignId = @CampaignId AND DeletedTF = 0 AND OnlineOnly = 0

If @campaigncnt > 1
	BEGIN
	
		--- SEE IF AT LEAST 1 IS GIFT
		SELECT
			@combocount=isnull(count(*),0)
		FROM
			QSPCanadaCommon..CampaignProgram A
			INNER JOIN QSPCanadaCommon..Program B ON A.ProgramId = B.Id
		WHERE
			A.CampaignId = @CampaignId
			AND A.ProgramID in (1,2,4,20,21,44,49,53,54,55,56,58,59,62)
			AND A.DeletedTF = 0
			AND OnlineOnly = 0
		
		If @combocount >= 2 
			BEGIN
				SET @IsCombo = 1
			END
	END

/* Select top 1  @Is_Combo = 1
 From qspcanadacommon..campaignprogram cp1, qspcanadaordermanagement.dbo.batch batch1
 where batch1.campaignid = cp1.campaignid
 and  programid  = 2 
and cp1.deletedtf <>1 
 and batch1.campaignid  = @CampaignID
 and exists (  Select 1
	      From qspcanadacommon..campaignprogram cp2, qspcanadaordermanagement.dbo.batch batch2
	      where batch2.campaignid = cp2.campaignid
              	and batch2.campaignid  = @CampaignID
		and cp2.deletedtf <>1 
	      	and  programid  in (27, 4)  )    

  SET @Is_Combo = Isnull(@Is_Combo,0)  -- important, dont exclude
*/

RETURN @IsCombo
  
END
GO
