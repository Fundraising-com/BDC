USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GroupSummaryReportHeader]    Script Date: 06/07/2017 09:19:38 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GroupSummaryReportHeader] @OrderId 	Int,
						    	@BatchId 	Int,
							@BatchDate	DateTime
 As
Select Top 1
	b.OrderID, 
	Convert(Varchar(10), 
	b.DateReceived, 101) As DateReceived, 
	Convert(Varchar(10), b.DateCreated, 101) As DateCreated, 
             b.AccountID As BillAccountId, 
	a.Name As BillAccountName,
	AdBill.Street1 As BillAccountAddress1, 
	AdBill.Street2 As billAccountAddress2, 
	AdBill.City As billAccountCity,
	AdBill.stateProvince As BillAccountState, 
	AdBill.postal_code As BillPCode, 
	b.ContactFirstName, 
	b.ContactLastName, 
	b.ContactPhone, 
	b.CampaignID, 
	c.Lang, 
	f.FmId, 
	f.FirstName, 
	f.LastName, 
	dbo.UDF_GetHomeRoomCount  (@OrderId ,@BatchId,@BatchDate,'CLASS') HomeRoomCount,
	dbo.UDF_GetHomeRoomCount  (@OrderId ,@BatchId,@BatchDate,'STUDENT') StudentCount,
	(Case IsNull(b.ShipToAccountID,0) 
	  When 0 Then  b.AccountID
	  Else
		b.ShipToAccountID
	  End) As ShipAccountId, 

	(Case IsNull(Shipa.Name, '')
	When '' Then a.Name
	Else 
		Shipa.Name
	End) As ShipAccountName, 

	(Case IsNull((bShip.ContactFirstName + ' ' + bShip.ContactLastName),'')
	When '' Then b.ContactFirstName+' '+ b.ContactLastName
	Else
		bShip.ContactFirstName + ' ' + bShip.ContactLastName
	End) As ShipAccountContcat, 

	(Case IsNull(bShip.ContactPhone,'')
	When '' Then  b.ContactPhone
	Else
		bShip.ContactPhone
	End)  As ShipAccountContcatPhone, 

             --QSPCanadaCommon.dbo.Program.Name As IncentiveProgram, 
	--dbo.GetIncentiveProgramName (@OrderId , @BatchId , @BatchDate ) As IncentiveProgram, 
	QSPCanadaOrderManagement.dbo.UDF_ProgramsbyCampaign(c.Id) As IncentiveProgram,  --Unigistix  need to know all programs rather than Incentive prog only MS Aug 11, 05
	(Case IsNull(AdShip.street1,'') 
	When '' Then  AdBill.street1
	Else
		AdShip.street1
	End) As ShipAccountAddress1, 

	(Case IsNull(AdShip.street2,'') 
	When '' Then  AdBill.street2
	Else
		AdShip.street2 
	End)As ShipAccountAddress2, 

	(Case IsNull(AdShip.city, '') 
	When '' Then AdBill.city 
	Else
		AdShip.city 
	End) As ShipAccountCity, 

	(Case IsNull(AdShip.stateProvince, '') 
	When '' Then AdBill.stateProvince 
	Else
		AdShip.stateProvince 
	End) As ShipAccountState, 

	(Case IsNull(AdShip.postal_code, '') 
	When '' Then AdBill.postal_code
	Else
		AdShip.postal_code 
	End)As ShipPCode
From     QSPCanadaCommon.dbo.CAccount Shipa 
	Left Outer Join   QSPCanadaCommon.dbo.Address AdShip ON Shipa.AddressListID = AdShip.AddressListID And AdShip.address_type = 54001 
	Right Outer Join QSPCanadaCommon.dbo.CampaignProgram 
	Inner Join QSPCanadaCommon.dbo.Campaign c ON QSPCanadaCommon.dbo.CampaignProgram.CampaignID = c.ID 
	Inner Join QSPCanadaCommon.dbo.Program ON QSPCanadaCommon.dbo.CampaignProgram.ProgramID = QSPCanadaCommon.dbo.Program.ID 
	Right Outer Join QSPCanadaOrderManagement.dbo.Batch b ON c.ID = b.CampaignID ON Shipa.Id = b.ShipToAccountID 
	Left Outer Join  QSPCanadaOrderManagement.dbo.Batch bShip ON b.ShipToAccountID = bShip.AccountID And b.ID = bShip.ID And b.[Date] = bShip.[Date] 
	Left Outer Join  QSPCanadaCommon.dbo.Address AdBill 
	Right Outer Join QSPCanadaCommon.dbo.CAccount a ON AdBill.AddressListID = a.AddressListID And AdBill.address_type = 54002 ON  b.AccountID = a.Id 
	Left Outer Join  QSPCanadaCommon.dbo.FieldManager f ON c.FMID = f.FMID
Where   (b.OrderID=@OrderId)
And(b.Id = @BatchId)
And (b.Date = @BatchDate)
--And  (QSPCanadaCommon.dbo.Program.Id <> 1) -- Magazine program
-- We want to print Summary reports for all program except Magazine program  
--And  (QSPCanadaCommon.dbo.Program.ProgramTypeID = 36002) -- Incentive
GO
