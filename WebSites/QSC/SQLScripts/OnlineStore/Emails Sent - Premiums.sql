SET TRANSACTION ISOLATION LEVEL SNAPSHOT

DECLARE @CYStartDate DATETIME = '2016-07-01'
DECLARE @CYEndDate DATETIME = '2017-06-30'
DECLARE @EmailTemplateID INT = 40

SELECT		caFM.Name1 FM,
			caB.SAPAcctNo AccountID,
			caB.Name2 AccountName,
			p.ParticipantID
INTO		#ParticipantsWithPremium
FROM		Portal.Participant p
JOIN		portal.ParticipantCampaign pc ON pc.ParticipantID = p.ParticipantID
JOIN		portal.Campaign cm ON cm.CampaignID = pc.CampaignID
JOIN		Portal.ParticipantEmailmessage pem ON pem.ParticipantID = p.ParticipantID
JOIN		Messaging.Email em 
				ON em.EmailID = pem.EmailID 
				AND EmailTemplateID = @EmailTemplateID
				AND EmailStateCode IN (NULL, 1,7) 
				AND em.DateTimeSent between @CYStartDate and @CYEndDate
JOIN		core.Contract c ON c.ContractID = cm.ContractID
JOIN		core.ContractAddress caB ON caB.ContractID =  c.ContractID AND caB.IsBillTo = 1
JOIN		core.ContractAddress caFM ON caFM.ContractID =  c.ContractID AND caFM.IsSalesPerson = 1
GROUP BY	caFM.Name1,
			caB.SAPAcctNo,
			caB.Name2,
			p.ParticipantID
HAVING		COUNT(DISTINCT em.EmailID) >= 12
ORDER BY	caFM.Name1,
			caB.SAPAcctNo

SELECT		FM,
			AccountID,
			AccountName,
			COUNT(ParticipantID) 'Participants who sent 12+ emails'
FROM		#ParticipantsWithPremium
GROUP BY	FM,
			AccountID,
			AccountName