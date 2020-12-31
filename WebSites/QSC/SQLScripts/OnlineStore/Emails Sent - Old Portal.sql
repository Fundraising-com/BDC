
/*All active supporters with the number of Participants who have sent 12 or more emails*/

DECLARE @dstart_date datetime, @dend_date datetime, @EmailCount int
SET @dstart_date = '07/01/2013'
SET @dend_date = '6/30/2014'

SELECT		act.fulf_account_id AccountID,
			act.account_name,
			OP.Online_Participant_Id,
			OP.First_Name + ' ' + OP.Last_Name ParticipantName,
			ce.ID EmailID,
			ce.DateSent EmailDateSent
FROM		QSPEcommerce.dbo.Online_Participant OP WITH (NOLOCK)
JOIN		QSPEcommerce.dbo.CustomerEmail CE  WITH (NOLOCK) ON OP.online_participant_id = CE.ParticipantID
JOIN		QSPFulfillment.dbo.Supporter S WITH (NOLOCK) ON S.supporter_id = CE.x_sender_supporter_id
LEFT JOIN	QSPFulfillment.dbo.account act WITH (NOLOCK) ON OP.account_id = act.account_id 
LEFT JOIN	QSPCanadaCommon.dbo.FieldManager FSM WITH (NOLOCK)  ON FSM.FMID = act.FM_ID
JOIN		QSPFulfillment.dbo.Account_Attribute AA WITH (NOLOCK) ON AA.Account_Id = act.Account_Id
JOIN		QSPFulfillment..Organization O WITH (NOLOCK) ON act.Organization_Id = O.Organization_Id
WHERE		ce.DateSent BETWEEN @dStart_Date AND @dEnd_Date
AND			O.business_division_id = 2
AND			IsNull(CE.IsDeleted, 0) = 0
AND			IsNull(CE.Bounced, 0) = 0
and			ce.templateemailid = 6
and			fulf_account_id in (10161,9963,9100,9025,7629,9114)
order by	ce.datesent