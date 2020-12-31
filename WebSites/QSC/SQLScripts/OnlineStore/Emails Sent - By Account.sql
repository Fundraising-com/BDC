SET TRANSACTION ISOLATION LEVEL SNAPSHOT

DECLARE @CYStartDate DATETIME = '2014-07-01'
DECLARE @CYEndDate DATETIME = '2015-06-30'
DECLARE @EmailTemplateID INT = 40

SELECT		caFM.Name1 FM,
			caB.SAPAcctNo AccountID,
			caB.name1 AccountName,
			COUNT(DISTINCT em.EmailID) EmailsSent
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
			caB.name1
ORDER BY	caFM.Name1,
			caB.SAPAcctNo