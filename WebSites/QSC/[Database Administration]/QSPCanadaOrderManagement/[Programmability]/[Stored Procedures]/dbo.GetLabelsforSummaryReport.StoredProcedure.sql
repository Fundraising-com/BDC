USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetLabelsforSummaryReport]    Script Date: 06/07/2017 09:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetLabelsforSummaryReport]
	@OrderID		Int, @BatchId Int, @BatchDate DateTime
	

AS
Select  --(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
	When 'FR' Then 'RAPPORT SOMMAIRE DU GROUPE'
	Else
		 'GROUP SUMMARY REPORT'
	End) ReportTitleLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
	When 'FR' Then 'RAPPORT SOMMAIRE DE LA CLASSE'
	Else
		 'HOMEROOM SUMMARY REPORT'
	End) HRReportTitleLabel,

	(Case IsNull(c.Lang,'EN')
	When 'FR' Then '* LES VENTES EN LIGNE comprennent les élèves qui ont (uniquement) effectué des ventes confirmées en ligne à QSP.ca. Les renseignements sur les ventes en ligne des élèves se trouvent sur les relevés de vente à QSP.ca. Le RELEVÉ DE LA CLASSE inclut les élèves qui effectuent des ventes courantes et en ligne.'
	Else
		 '* ONLINE SALES includes students who have (only) sold and confirmed sales online at QSP.ca. Student''s Online sales detail is found in the detailed sales reports at QSP.ca. Students with online and regular sales are included in their HOMEROOM SUMMARY.'
	End) OnlineGroupSummaryText,

	
	(Case IsNull(c.Lang,'EN')
	When 'FR' Then '* L’élève qui a effectué des ventes courantes durant la campagne et des ventes en ligne.  On tient compte de toutes les ventes pour calculer les primes avec exactitude.'
	Else
		 '* Student that has recorded both regular campaign and on-line sales. All sales are reflected to calculate prizes accurately'
	End) OnlineHRSummaryText,



	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
		When 'FR' Then 'Nombre de classes:'
		Else
			'Number of Homerooms:'
	End) HomeRoomLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
		When 'FR' Then 'Nombre de participants:'
		Else
			'Number of Participants:'
	End) ParticipantLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
		When 'FR' Then 'Programmes:'
		Else
			'Programs:'
	End) IncentProgLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
		When 'FR' Then 'Expédier à:'
		Else
			'Ship To Group:'
	End) ShipToAccLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
		When 'FR' Then 'Facturer à:'
		Else
			'Bill To Group:'
	End) BillToAccLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
		When 'FR' Then 'N°:'
		Else
			'ID:'
	End) ShipBillIdLabel,
	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
		When 'FR' Then 'Personne-ressource:'
		Else
			'Group Contact:'
	End)GroupContactLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
		When 'FR' Then 'Téléphone:'
		Else
			'Telephone:'
	End)GroupContactTelLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
		When 'FR' Then 'Rep. QSP:'
		Else
			'Field Manager:'
	End)FMLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
		When 'FR' Then 'N° du Rep. QSP:'
		Else
			'FM ID:'
	End)FMIdLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
		When 'FR' Then 'N° de campagne:'
		Else
			'Campaign ID:'
	End)CAIdLabel,


	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
		When 'FR' Then 'N° de commande:'
		Else
			'Order ID:'
	End)OrderIdLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
		When 'FR' Then 'Reçue à QSP:'
		Else
			'Date Received:'
	End)DateReceivedLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
		When 'FR' Then 'Date expédiée:'
		Else
			'Date Created:'
	End) DateCreatedLabel,

	
	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
		When 'FR' Then 'de'
		Else
			'of'
	End) PageOf,


	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
		When 'FR' Then 'Rapport exécuté le: '  --+ Cast(GetDate() as Varchar) --NLS for French ?? also Date Created and Received
		Else
			'Report run on: ' --+ Cast(GetDate() as Varchar)
	End) RunOn,


	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
		When 'FR' Then 'QSP Inc. est une filiale de Sélection du Reader''s Digest (Canada)  Ltée.'
		Else
			'QSP Inc. is a subsidiary of The Reader''s Digest Association (Canada) Ltd.'
	End) RDSubsidaryLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
		When 'FR' Then '33 Prince Street Suite 200'
		Else
			'695 Riddell Road'
			
	End) QSPAddress1Label,
	
	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
		When 'FR' Then 'Montreal, QC   H3C 2M7'
		Else
			'Orangeville, ON   L9W 4Z5'
	End) QSPAddress2Label,
	
	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
	When 'FR' Then 'Facturation De Magasin'
	Else
		'Magazine Billing'
	End)MagazineBillingLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
	When 'FR' Then 'Service à la clientèle'
	Else
		'Customer Service'
	End)CustomerServiceLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
	When 'FR' Then 'Division Professionnelle'
	Else
		'Prize Division'
	End)PrizeDivisionLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
	When 'FR' Then 'MERCI DE CHOISIR QSP!'
	Else
		'THANK YOU FOR CHOOSING QSP!'
	End)ThankYouLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
	When 'FR' Then  '*** IMPORTANT, CONSERVEZ CE DOCUMENT POUR VOS DOSSIERS ***'
	Else
		'*** IMPORTANT, KEEP THIS DOCUMENT FOR YOUR FILES ***'
	End)ImportantLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
	When 'FR' Then 'Cadeau'
	Else
		'Gift'
	End)ChocolateLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
	When 'FR' Then 'Articles - Magazines'
	Else
		'Magazine Items'
	End)MagazineItemLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
	When 'FR' Then 'Total des articles'
	Else
		'Total Items'
	End)TotalItemsLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
	When 'FR' Then 'Qté'
	Else
		'Qty'
	End)QtyLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
	When 'FR' Then 'Niveaux D''Incitations De Magasin'
	Else
		'Magazine Incentives Levels'
	End)MagazineIncentiveLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
	When 'FR' Then 'N° de la classe'
	Else
		'Room  No.  '
	End)RoomNoLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
	When 'FR' Then 'Nom du responsable (Nom, Initiale, Prénom)'
	Else
		'Homeroom Leader Name (Last, Init, First)'
	End)HRLeaderNameLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
	When 'FR' Then 'Note: Le niveau de récompenses atteint pour cette commande a été ou sera expédié avec la commande # '
	Else
		'Note: The incentives level attained for this order has been or will be shipped as part of order # '
	End)OrderIncentLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
	When 'FR' Then 'Prix RD'
	Else
		'Reader'+''''+'s'+'  '+'Digest Prizes'
	End)RDPrizesLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
	When 'FR' Then 'Prix Flare Maclean & Chatelaine'
	Else
		'Chatelaine, Maclean & Flare Prizes'
	End)NonRDPrizesLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
	When 'FR' Then 'Nom du responsable de classe:'
	Else
		'Homeroom Leader Last Name:'
	End)HRLeaderLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
	When 'FR' Then 'Numéro de la classe:'
	Else
		'Classroom Number:'
	End)HRClassroomNoLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
	When 'FR' Then 'Nom de participant (Nom, Prénom)'
	Else
		'Participant Name (Last, First)'
	End)HRParticipantNameLabel,

	--(Case 'FR'  -- (Case IsNull(c.Lang,'EN')
	(Case IsNull(c.Lang,'EN')
	When 'FR' Then 'N° du part.'
	Else
		'Part. #'
	End)HRParticipantNoLabel
From  	QSPCanadaCommon.dbo.Campaign c,
	QSPCanadaOrderManagement.dbo.batch b
Where  c.Id   = b.Campaignid 
And b.orderId = @OrderID
And b.id=@BatchId
And b.date = @batchDate
GO
