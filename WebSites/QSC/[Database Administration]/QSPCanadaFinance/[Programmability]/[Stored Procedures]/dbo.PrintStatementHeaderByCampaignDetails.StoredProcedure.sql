USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[PrintStatementHeaderByCampaignDetails]    Script Date: 06/07/2017 09:17:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PrintStatementHeaderByCampaignDetails]

	@CampaignID		INT,
	@Realtime		BIT,
	@DateTo			DATETIME, 
	@StatementID	INT = NULL
	
AS

SET NOCOUNT ON

IF @Realtime = CONVERT(BIT, 1)
	BEGIN

		SELECT	CONVERT(VARCHAR(10), ISNULL(DATEADD(DAY, -1, @DateTo), GETDATE()), 120) AS StatementDate,
				AccountID,
				CampaignID,
				IsStaffCampaign,
				Lang,
				CampaignPrograms,
				FMID,
				FMFirstName,
				FMLastName,
				PaymentTerms,
				CorpAttn,
				CorpAddress1,
				CorpAddress2,
				CorpCity,
				CorpProvince,
				CorpPostalCode,
				CorpPhoneNumber,
				CorpGSTNumber,
				CorpQSTNumber,
				AccountName,
				AccountContactFirstName,
				AccountContactLastName,
				AccountAddress1,
				AccountAddress2,
				AccountCity,
				AccountProvince,
				AccountPostalCode,
				AccountZip4,
				AccountPhoneNumber
		FROM	dbo.UDF_Statement_GetHeader(@CampaignID)

	END
ELSE
	BEGIN
		IF ISNULL(@StatementID, 0) > 0
			BEGIN

				SELECT	CONVERT(VARCHAR(10), StatementDate, 120) AS StatementDate,
						AccountID,
						CampaignID,
						IsStaffCampaign,
						Lang,
						CampaignPrograms,
						FMID,
						FMFirstName,
						FMLastName,
						PaymentTerms,
						CorpAttn,
						CorpAddress1,
						CorpAddress2,
						CorpCity,
						CorpProvince,
						CorpPostalCode,
						CorpPhoneNumber,
						CorpGSTNumber,
						CorpQSTNumber,
						AccountName,
						AccountContactFirstName,
						AccountContactLastName,
						AccountAddress1,
						AccountAddress2,
						AccountCity,
						AccountProvince,
						AccountPostalCode,
						AccountZip4,
						AccountPhoneNumber
				FROM	[Statement] stat
				WHERE	stat.StatementID = @StatementID
				
			END
		ELSE
			BEGIN

				SELECT	CONVERT(VARCHAR(10), StatementDate, 120) AS StatementDate,
						AccountID,
						CampaignID,
						IsStaffCampaign,
						Lang,
						CampaignPrograms,
						FMID,
						FMFirstName,
						FMLastName,
						PaymentTerms,
						CorpAttn,
						CorpAddress1,
						CorpAddress2,
						CorpCity,
						CorpProvince,
						CorpPostalCode,
						CorpPhoneNumber,
						CorpGSTNumber,
						CorpQSTNumber,
						AccountName,
						AccountContactFirstName,
						AccountContactLastName,
						AccountAddress1,
						AccountAddress2,
						AccountCity,
						AccountProvince,
						AccountPostalCode,
						AccountZip4,
						AccountPhoneNumber
				FROM	[Statement] stat
				WHERE	stat.StatementID IN	(SELECT	MAX(StatementID)
											FROM	[Statement]
											WHERE	CampaignID = @CampaignID)
			END
	END
	
SET NOCOUNT OFF
GO
