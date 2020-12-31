USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[Campaign_IsCombo_Check]    Script Date: 06/07/2017 09:33:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Campaign_IsCombo_Check]

@CampaignId int,
@IsCombo int output

AS
SET NOCOUNT ON
SET @IsCombo = 0

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
			AND A.ProgramID in (1,2,4,20,21,44,49,53,54,55,56,58,59,62,64,65,67,69,70)
			AND A.DeletedTF = 0
			AND OnlineOnly = 0
		
		If @combocount >= 2 
			BEGIN
				SET @IsCombo = 1
			END
	END
	
--Certain standalone programs should be treated as combo in regards to Field supply (Cumulative Prize Chart) generation
Declare @RunningProgram int
SELECT @RunningProgram = isnull(count(*),0)  FROM QSPCanadaCommon..CampaignProgram WHERE CampaignId = @CampaignId AND DeletedTF = 0 AND OnlineOnly = 0 And ProgramID IN (4, 44, 49, 53, 54, 55, 56, 58, 59, 62)
If @RunningProgram > 0 Set @IsCombo = 1

SET NOCOUNT OFF
GO
