SET TRANSACTION ISOLATION LEVEL SNAPSHOT

DECLARE @CYStartDate DATETIME = '2014-07-01'
DECLARE @CYEndDate DATETIME = '2015-06-30'
DECLARE @EmailTemplateID INT = 40

--Todo exclude emails sent to registrant: And [Messaging].[Email].EmailTo <> [Portal].[RegistrantEmail].emailaddress
--Todo change to what the emails sent report does: EmailStateCode not in (2, 4, 6) --Canceled, Hold, Voided

SELECT	caFM.Name1 FM,
		c.ContractID,
		ca.name1 AccountName,
		p.ParticipantID ParticipantID,
		p.FirstName + p.LastName ParticipantName,
		COUNT(DISTINCT em.EmailID) EmailsSent
FROM Portal.Participant p
JOIN portal.ParticipantCampaign pc ON pc.ParticipantID = p.ParticipantID
JOIN portal.Campaign cm ON cm.CampaignID = pc.CampaignID
JOIN Portal.ParticipantEmailmessage pem ON pem.ParticipantID = p.ParticipantID
JOIN Messaging.Email em 
	ON em.EmailID = pem.EmailID 
	AND EmailTemplateID = @EmailTemplateID
	AND EmailStateCode IN (NULL, 1,7) 
	AND em.DateTimeSent between @CYStartDate and @CYEndDate
JOIN core.Contract c ON c.ContractID = cm.ContractID
JOIN core.ContractAddress ca ON ca.ContractID =  c.ContractID AND ca.IsBillTo = 1
JOIN core.ContractAddress caFM ON caFM.ContractID =  c.ContractID AND caFM.IsSalesPerson = 1
GROUP BY	caFM.Name1,
			c.ContractID,
			ca.name1,
			p.ParticipantID,
			p.FirstName + p.LastName