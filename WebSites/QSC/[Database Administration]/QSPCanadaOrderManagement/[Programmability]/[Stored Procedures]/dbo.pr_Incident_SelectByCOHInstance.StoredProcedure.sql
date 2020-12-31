USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Incident_SelectByCOHInstance]    Script Date: 06/07/2017 09:20:04 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Incident_SelectByCOHInstance]
	@iCustomerOrderHeaderInstance int,
	@iTransID int = 0
 AS

BEGIN

select	inc.IncidentInstance,
	iac.ActionInstance,
	inc.UserIDCreated,
	cup.UserName as UserNameCreated,
	inc.DateCreated,
	inc.ProblemCodeInstance,
	inc.TransID,
	CommunicationSourceInstance ,
	inc.StatusInstance,
	inc.Comments as IncidentComments,       
	coc.description as CommunicationChannel,
	coso.description as communicationsource,
	ins.description as incidentstatus, 
	b.OrderID ,
	iac.Comments as IncidentActionComments,
	act.Description as ActionDescription,
	inc.CustomerOrderHeaderInstance,
	inc.TransID,
	iac.Instance,
	inc.refertoincidentinstance

FROM		IncidentAction iac
JOIN		Incident inc ON inc.IncidentInstance = iac.IncidentInstance
LEFT JOIN	QSPCanadaCommon..CUserProfile cup ON cup.Instance = inc.UserIDCreated
LEFT JOIN	CommunicationChannel coc ON coc.Instance = inc.CommunicationChannelInstance
LEFT JOIN	CommunicationSource coso ON coso.Instance =  inc.CommunicationSourceInstance
LEFT JOIN	IncidentStatus ins ON ins.instance = inc.StatusInstance
JOIN		CustomerOrderHeader coh ON coh.instance =  inc.CustomerOrderHeaderInstance
JOIN		Batch b ON b.ID = coh.OrderBatchid AND b.[Date] = coh.OrderBatchDate
LEFT JOIN	[Action] act ON act.instance =  iac.ActionInstance
WHERE		inc.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
AND			inc.TransID = @iTransID
ORDER BY	inc.DateCreated desc

END
GO
