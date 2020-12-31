USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[InvoiceGenerated_List_ByFM]    Script Date: 06/07/2017 09:17:24 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[InvoiceGenerated_List_ByFM]  @FmID Varchar(4) AS

SET NOCOUNT ON
BEGIN

DECLARE @Now DateTime
DECLARE @StartDate DateTime

SET @Now = CONVERT(DateTime,CONVERT(Varchar(10),GETDATE(),101))
SET @StartDate =  @Now  -7

--SELECT @StartDate,@Now

SELECT @StartDate StartDate, @Now EndDate,
 	DM.DMID,DM.Firstname DMFname,DM.LastNAme DMLname,
	F.FMID, F.Firstname FMFname,F.LastNAme FMLname,
	A.Id AccountID,A.Name AccountName,
	b.CampaignID,CASE C.IsStaffOrder WHEN 1 THEN 'Staff' ELSE 'Non Staff' END CAType,
	QSPCanadaOrderManagement.dbo.UDF_ProgramsbyCampaign(B.CampaignID)Programs,
	b.OrderID,SUBSTRING(CD.Description,1,5) Description,
	I.Invoice_ID,CONVERT(Varchar(10),I.DATETIME_CREATED,101)DATETIME_CREATED,
	
	SUM(ISNULL(CASE WHEN invSec.SECTION_TYPE_ID IN (2,8) THEN invSec.Item_Count ELSE 0 END,0)) MagItemCount,
	SUM(ISNULL(CASE WHEN invSec.SECTION_TYPE_ID IN (2,8) THEN invSec.TOTAL_TAX_INCLUDED ELSE 0.00 END,0)) MagGross,
	SUM(ISNULL(CASE WHEN invSec.SECTION_TYPE_ID IN (2,8) THEN invSec.DUE_AMOUNT ELSE 0.00 END,0)) MagAmountDue,
	SUM(ISNULL(CASE WHEN invSec.SECTION_TYPE_ID NOT IN (2,8) THEN invSec.Item_Count ELSE 0 END,0)) NonMagItemCount,
	SUM(ISNULL(CASE WHEN invSec.SECTION_TYPE_ID NOT IN (2,8) THEN invSec.TOTAL_TAX_INCLUDED ELSE 0.00 END,0)) NonMagGross,
	SUM(ISNULL(CASE WHEN invSec.SECTION_TYPE_ID NOT IN (2,8) THEN invSec.DUE_AMOUNT ELSE 0.00 END,0)) NonMagAmountDue,
	Sum(ISNULL(invSec.DUE_AMOUNT,0)) TotalAmountDue
FROM 	QSPCanadaOrderManagement.dbo.batch B, 
	QSPCanadaCommon.dbo.Campaign C,
	QSPcanadaCommon.dbo.Caccount A,
	QSPCanadaCommon.dbo.Fieldmanager F,
	QSPCanadaCommon.dbo.Fieldmanager DM,
	QSPCanadaCommon.dbo.CodeDetail CD,
	QSPCanadaFinance.dbo.Invoice I
	LEFT JOIN QSPCanadaFinance.dbo.Invoice_Section invSec ON I.Invoice_ID = invSec.Invoice_ID and invSec.Section_Type_ID IN (1, 2, 6, 7, 8, 9, 10, 11, 12)
WHERE B.OrderID=I.Order_ID
AND B.CampaignID=C.ID
AND C.FMID=F.FMID
AND F.DMID=DM.FMID
AND C.BillToAccountID = A.Id
AND CD.Instance =OrderQualifierID
--AND I.Invoice_id in(98997,94062,100109,97892,93713)
AND B.OrderQualifierID IN (39001,39002,39003,39006)
AND  C.FMID = IsNull(@FmID,C.FMID)
AND CONVERT(DateTime,CONVERT(Varchar(10),I.DATETIME_CREATED,101)) BETWEEN @StartDate AND @Now
GROUP BY I.Invoice_ID,
	I.DateTime_Created,
	b.OrderID,CD.Description,
	A.Id,A.Name,
	b.CampaignID,C.IsStaffOrder,
	F.FMID, F.Firstname,F.LastNAme,DM.DMID,DM.Firstname,DM.LastNAme
ORDER by DM.DMID,F.FMID,A.Name,b.CampaignID,CD.Description,b.OrderID,I.Invoice_ID


SET NOCOUNT OFF
END
GO
