USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectCampaignsForCustomerServiceOrdersStatement]    Script Date: 06/07/2017 09:20:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[pr_SelectCampaignsForCustomerServiceOrdersStatement]

@dFrom	datetime	= '01/01/1955',
@dTo	datetime	= '01/01/1955'

AS

DECLARE 	@SelectFrom	Varchar(8000),
		@Where	Varchar(8000),
		@DateFrom	DateTime,
		@DateTo	DateTime

SET @DateFrom = @dFrom
SET @DateTo = @dTo

SET @SelectFrom='
SELECT	A.ID as AccountID, 
	B.CampaignID,
	'''' AS Name,
	C.FMID,
	'''' AS LastName,
	'''' AS FirstName,
	'''' AS Lang,
	NULL AS Amount
FROM	QSPcanadaOrdermanagement.dbo.Batch B	 	        (NOLOCK)
	LEFT JOIN QSPCanadaCommon.dbo.CAccount A 	        (NOLOCK) ON B.AccountID =A.Id
	LEFT JOIN QSPCanadaCommon.dbo.Phone P 		        (NOLOCK) ON A.phonelistId = P.PhoneListid AND P.Type=30505  --Main
	LEFT JOIN QSPCanadaCommon.dbo.AddressList AL 	        (NOLOCK) ON A.AddressListID = AL.ID
	LEFT JOIN QSPCanadaCommon.dbo.Address AdShip 	        (NOLOCK) ON AL.ID = AdShip.AddressListID AND AdShip.Address_Type = 54002
 	LEFT JOIN QSPCanadaCommon.dbo.TaxApplicableTax TAT      (NOLOCK) ON AdShip.StateProvince = TAT.Province_Code and TAT.Tax_Id =1 and tat.SECTION_TYPE_ID=2
	LEFT JOIN QSPCanadaCommon.dbo.Tax GST		        (NOLOCK) ON GST.Tax_Id = TAT.Tax_Id 
	LEFT JOIN QSPCanadaCommon.dbo.TaxApplicableTax TATPST	(NOLOCK) ON AdShip.StateProvince = TATPST.Province_Code and TATPST.Tax_Id NOT IN(1,2,4,5)and TATPST.SECTION_TYPE_ID=2
	LEFT JOIN QSPCanadaCommon.dbo.Tax PST			(NOLOCK) ON PST.Tax_Id = TATPST.Tax_Id
	LEFT JOIN QSPCanadaCommon.dbo.TaxApplicableTax TATHST	(NOLOCK) ON AdShip.StateProvince = TATHST.Province_Code and TATHST.Tax_Id IN(2,4,5)and TATHST.SECTION_TYPE_ID=2
	LEFT JOIN QSPCanadaCommon.dbo.Tax HST			(NOLOCK) ON HST.Tax_Id = TATHST.Tax_Id,
	QSPCanadaCommon.dbo.Campaign C	 	   	        (NOLOCK)
	LEFT JOIN QSPCanadaCommon.dbo.Contact Cont 	        (NOLOCK) ON Cont.ID = C.ShipToCampaignContactID
	LEFT JOIN QSPCanadaCommon.dbo.CampaignProgram CP        (NOLOCK) ON (CP.CampaignId=C.Id AND CP.ProgramId IN(1,2) AND CP.DeletedTF=0),
	QSPCanadaCommon.dbo.FieldManager FM 	 	        (NOLOCK),
	QSPCanadaOrdermanagement.dbo.CustomerOrderHeader H      (NOLOCK)
	LEFT JOIN QSPCanadaOrderManagement.dbo.CustomerPaymentHeader CPH      (NOLOCK) ON CPH.CustomerOrderHeaderInstance = H.Instance 
	LEFT JOIN QSPCanadaOrderManagement.dbo.CreditCardPayment CCP 	      (NOLOCK) ON CCP.CustomerPaymentHeaderInstance = CPH.Instance ,
	QSPCanadaOrdermanagement.dbo.CustomerOrderDetail D      (NOLOCK)
	LEFT JOIN QSPCanadaOrderManagement.dbo.CustomerOrderDetailRemitHistory RH on  (d.customerorderheaderinstance =rh.CustomerOrderHeaderInstance and d.TransID=rh.transid and rh.Status in (42000,42001,42004)),
	QSPCanadaOrderManagement.dbo.Student St		        (NOLOCK) '

SET @Where= '
WHERE 	B.Campaignid=C.[Id]
AND	C.IsStaffOrder = 0
AND     C.FMID = FM.FMID
AND 	B.[ID] = H.OrderBatchId
AND	B.[DATE]= H.OrderBatchDate
AND	D.CustomerOrderHeaderInstance = H.Instance
AND	St.Instance = H.StudentInstance 
AND 	D.DelFlag=0	
AND 	((H.PaymentMethodInstance <> 50002 AND CCP.StatusInstance = 19000 )
	OR
	(H.PaymentMethodInstance = 50002))
AND	B.OrderQualifierId IN(39015,39013) 
AND  ((RH.DateChanged BETWEEN  '''+CONVERT(nvarchar, @DateFrom,101) +'''  AND '''+ CONVERT(nvarchar, @DateTo,101) +'''
	AND RH.Status IN (42000, 42001, 42004))
OR    (D.CreationDate BETWEEN  '''+CONVERT(nvarchar, @DateFrom,101) +'''  AND '''+ CONVERT(nvarchar, @DateTo,101) +'''
	AND D.StatusInstance =508)) --BHE
'

SET @SelectFrom= @SelectFrom+' '+@Where + ' GROUP BY A.ID, B.CampaignID, C.FMID '

SET @SelectFrom = @SelectFrom + ' ORDER BY B.CampaignID '


EXEC (@SelectFrom)
GO
