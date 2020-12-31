USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetLabelsforPickList]    Script Date: 06/07/2017 09:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetLabelsforPickList]
	@OrderId		Int,
	@ReportType		Int = 1 -- PackingSlip(2) or PickList(1)

AS
Declare @CALang  	Varchar(2),
	@AccountLang	 Varchar(2),
	@AccountProvince Varchar(2)

Select @CALang = c.Lang, @AccountLang =  Ca.Lang, @AccountProvince = ad.stateProvince
From QSPCanadaCommon.dbo.Campaign c, 
QSPCanadaOrderManagement.dbo.Batch b,QSPCanadaCommon.dbo.CAccount ca, QSPCanadaCommon.dbo.Address ad
Where b.CampaignId=c.Id
and IsNull(b.accountid,b.ShipToAccountId) = ca.Id
And b.OrderId = @OrderId
and ca.AddressListID = ad.AddressListID
and ad.address_type = 54001

If @CALang Is Null  And @AccountLang Is Not Null
Begin
  Set @CALang = @AccountLang
End

--Set @CALang ='FR'

Select (Case @ReportType
	When 1 Then  'PICK LIST'
	Else
		Case     IsNull(@CALang,'EN')
		When 'FR' Then 'LISTE D''EXPÉDITION'
		Else
			'PACKING SLIP'
		End
	End) ReportTitleLabel,

	(Case @ReportType
	When 1 Then  'Participant Name:'
	Else
		Case     IsNull(@CALang,'EN')
		When 'FR' Then 'Nom du participant:'
		Else
			'Participant Name:'
		End
	End) ParticipantLabel,

	(Case @ReportType
	When 1 Then 'Teacher Name:'
	Else
		Case      IsNull(@CALang,'EN')
		When 'FR' Then 'Resp. de classe:'
		Else
			'Teacher Name:'
		End
	End) TeacherLabel,


	(Case @ReportType
	When 1 Then 'Classroom Number:'
	Else
		Case       IsNull(@CALang,'EN')
		When 'FR' Then 'N° de la classe:'
		Else
			'Classroom Number:'
		End
	End) ClassLabel,


	(Case @ReportType
	When 1 Then 'Pick #'
	Else
		Case      IsNull(@CALang,'EN')
		When 'FR' Then 'N° de Sél.'
		Else
			'Pick #'
		End
	End) PickNoLabel,

	(Case @ReportType
	When 1 Then 'Qty to ship/pick'
	Else
		Case      IsNull(@CALang,'EN')
		When 'FR' Then 'Quantité expédiée'
		Else
			'Quantity Shipped'
		End
	End) QtyShpPckLabel,


	(Case @ReportType
	When 1 Then 'Quantity Ordered'
	Else
		Case      IsNull(@CALang,'EN')
		When 'FR' Then 'Qté commandée'
		Else
			'Quantity Ordered'
		End
	End) QtyOrderLabel,


	(Case @ReportType
	When 1 Then 'Catalogue Item Code'
	Else
		Case      IsNull(@CALang,'EN')
		When 'FR' Then 'N° de catalogue de l''article'
		Else
			'Catalogue Item Code'
		End
	End) CatItemCodeLabel,


	(Case @ReportType
	When 1 Then 'SAP Material Code'
	Else
		Case       IsNull(@CALang,'EN')
		When 'FR' Then 'Code de Material de SAP'
		Else
			'SAP Material Code'
		End
	End) OraCodeLabel,

	(Case @ReportType
	When 1 Then 'Item Description'
	Else
		Case      IsNull(@CALang,'EN')
		When 'FR' Then 'Description de l''article'
		Else
			'Item Description'
		End
	End) ItemDescLabel,

	(Case @ReportType
	When 1 Then 'Average Unit Price'
	Else
		Case      IsNull(@CALang,'EN')
		When 'FR' Then ''
		Else
			'Average Unit Price'
		End
	End) UnitPriceLabel,

	(Case @ReportType
	When 1 Then 'Extended Value'
	Else
		Case      IsNull(@CALang,'EN')
		When 'FR' Then ''
		Else
			'Extended Value'
		End
	End) ExtPriceLabel,

	(Case @ReportType
	When 1 Then 'Total $'
	Else
		Case      IsNull(@CALang,'EN')
		When 'FR' Then ''
		Else
			'Total $'
		End
	End) TotalExtPriceLabel,

	(Case @ReportType
	When 1 Then  'of'
	Else
		Case      IsNull(@CALang,'EN')
		When 'FR' Then 'de'
		Else
			'of'
		End
	End) PageOf,


	(Case @ReportType
	When 1 Then 'Report run on: ' --+ Cast(GetDate() as Varchar)
	Else
		Case      IsNull(@CALang,'EN')
		When 'FR' Then 'Rapport exécuté le: '  --+ Cast(GetDate() as Varchar) --NLS for French ?? also Date Created and Received
		Else
			'Report run on: ' --+ Cast(GetDate() as Varchar)
		End
	End) RunOn,

	(Case @ReportType
	When 1 Then 'Ship To Group:'
	Else
		Case      IsNull(@CALang,'EN')
		When 'FR' Then 'Expédier à:'
		Else
			'Ship To Group:'
		End
	End) ShipToAccLabel,

	(Case @ReportType
	When 1 Then 'Bill To Group:'
	Else
		Case     IsNull(@CALang,'EN')
		When 'FR' Then 'Facturer à:'
		Else
			'Bill To Group:'
		End
	End) BillToAccLabel,

	(Case @ReportType
	When 1 Then 'ID:'
	Else
		Case      IsNull(@CALang,'EN')
		When 'FR' Then 'N°:'
		Else
			'ID:'
		End
	End) ShipIdLabel,

	(Case @ReportType
	When 1 Then 'ID:'
	Else
		Case      IsNull(@CALang,'EN')
		When 'FR' Then 'N°:'
		Else
			'ID:'
		End
	End)BillIdLabel,

	(Case @ReportType
	When 1 Then 'Group Contact:'
	Else
		Case     IsNull(@CALang,'EN')
		When 'FR' Then 'Personne-ressource:'
		Else
			'Group Contact:'
		End
	End)GroupContactLabel,

	(Case @ReportType
	When 1 Then 'Telephone:'
	Else
		Case      IsNull(@CALang,'EN')
		When 'FR' Then 'Téléphone:'
		Else
			'Telephone:'
		End
	End)GroupContactTelLabel,

	(Case @ReportType
	When 1 Then 'Field Manager:'
	Else
		Case      IsNull(@CALang,'EN')
		When 'FR' Then 'Rep. QSP:'
		Else
			'Field Manager:'
		End
	End)FMLabel,

	(Case @ReportType
	When 1 Then 'FM ID:'
	Else
		Case      IsNull(@CALang,'EN')
		When 'FR' Then 'N° du Rep. QSP:'
		Else
			'FM ID:'
		End
	End)FMIdLabel,

	(Case @ReportType
	When 1 Then 'Campaign ID:'
	Else
		Case      IsNull(@CALang,'EN')
		When 'FR' Then 'N° de campagne:'
		Else
			'Campaign ID:'
		End
	End)CAIdLabel,


	(Case @ReportType
	When 1 Then 'Order ID:'
	Else
		Case     IsNull(@CALang,'EN')
		When 'FR' Then 'N° de commande:'
		Else
			'Order ID:'
		End
	End)OrderIdLabel,

	(Case @ReportType
	When 1 Then 'Date Received:'
	Else
		Case     IsNull(@CALang,'EN')
		When 'FR' Then 'Reçue à QSP:'
		Else
			'Date Received:'
		End
	End)DateReceivedLabel,

	(Case @ReportType
	When 1 Then 'Date Created:'
	Else
		Case     IsNull(@CALang,'EN')
		When 'FR' Then 'Date expédiée:'
		Else
			'Date Created:'
		End
	End) DateCreatedLabel,

	(Case @ReportType
	When 1 Then 'Replaced Item?'
	Else
		Case      IsNull(@CALang,'EN')
		When 'FR' Then 'Article Remplacé ?'
		Else
			'Replaced Item?'
		End
	End) ReplacedItemLabel,

	(Case @ReportType
	When 1 Then 'CARRIER:'
	Else
		Case      IsNull(@CALang,'EN')
		When 'FR' Then 'transporteur:'
		Else
			'CARRIER:'
		End
	End) CarrierLabel,

	(Case @ReportType
	When 1 Then 'Special Instructions:'
	Else
		Case      IsNull(@CALang,'EN')
		When 'FR' Then 'Instructions spéciales:'
		Else
			'Special Instructions:'
		End
	End) SpecialInstructionsLabel,

	(Case @ReportType
	When 2  Then
		Case    IsNull(@CALang,'EN')
		When 'FR' Then 'QSP Inc. est une filiale de Sélection du Reader''s Digest (Canada)  Ltée.'
		Else
			'QSP Inc. is a subsidiary of The Reader''s Digest Association (Canada) Ltd.'
		End
	Else
		
			'QSP Inc. is a subsidiary of The Reader''s Digest Association (Canada) Ltd.'
	End) RDSubsidaryLabel,

	(Case	IsNull(@AccountProvince, '')
		When 'QC' Then	'33 Prince Street Suite 200'
		Else			'33 Prince Street Suite 200'			
	End) QSPAddress1Label,
	
	(Case    IsNull(@AccountProvince,'')
		When 'QC' Then	'Montreal, QC   H3C 2M7'
		Else			'Montreal, QC   H3C 2M7'
	End) QSPAddress2Label,
	
	(Case @ReportType
	When 2 Then
		Case    IsNull(@CALang,'EN')
		When 'FR' Then 'Facturation De Magasin'
		Else
		'Magazine Billing'
		End
	Else
		'Magazine Billing'
	End)MagazineBillingLabel,

	(Case @ReportType
	When 2 Then
		Case    IsNull(@CALang,'EN')
		When 'FR' Then 'Service à la clientèle'
		Else
		'Customer Service'
		End
	Else
		'Customer Service'
	End)CustomerServiceLabel,

	(Case @ReportType
	When 2 Then
		Case    IsNull(@CALang,'EN')
		When 'FR' Then 'Division Professionnelle'
		Else
		'Prize Division'
		End
	Else
		'Prize Division'
	End)PrizeDivisionLabel,
	
	(Case @ReportType
	When 2 Then
		Case    IsNull(@CALang,'EN')
		When 'FR' Then 'Référence:'
		Else
		'Reference:'
		End
	Else
		'Reference:'
	End)BatchRefLabel,

	(Case @ReportType
	When 2 Then
		Case    IsNull(@CALang,'EN')
		When 'FR' Then 'Commande en attente?'
		Else
		'BackOrder?'
		End
	Else
		'BackOrder?'
	End)BackOrderLabel,
	(Case @ReportType
	When 2 Then
		Case    IsNull(@CALang,'EN')
		When 'FR' Then 'Date De Livraison de Fs:'
		Else
		'FS Delivery Date:'
		End
	Else
		'FS Delivery Date:'
	End)FSDeliveryDate,
	(Case @ReportType
	When 2 Then
		Case    IsNull(@CALang,'EN')
		When 'FR' Then 'Article Embarqué Précédemment?'
		Else
		'Item Shipped Previously?'
		End
	Else
		--'Article Embarqué Précédemment?' 
		'Item Shipped Previously?'
	End)ItemShippedPreviouslyLabel,
	(Case @ReportType
	When 2 Then
		Case    IsNull(@CALang,'EN')
		When 'FR' Then 'Date d''expédiée demandée:'
		Else
		'Requested Ship Date:'
		End
	Else
		'Requested Ship Date:'
	End)RequestedShipDate
GO
