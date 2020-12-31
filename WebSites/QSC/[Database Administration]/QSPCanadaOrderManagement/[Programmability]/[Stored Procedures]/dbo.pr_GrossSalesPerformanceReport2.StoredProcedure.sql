USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_GrossSalesPerformanceReport2]    Script Date: 06/07/2017 09:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[pr_GrossSalesPerformanceReport2]   
/***********************************************************************************************************************  
Re-Written March 18, 2006 By MS  
--Added Cookie Dough sales  
--Sales Fig as Used In Invoice  
--Count matches with Other Finance Reports like Overall Sales  
--Modifies to fix bug (Added product 46012) for Sectional Total, removed table variables Feb 03, 2007 MS  
--Removed item counting  fron COD MS Jul 12, 2007  
************************************************************************************************************************/  
@FMID   Varchar(6) = '',  
@DateFrom   DateTime = '01/01/1955',  
@DateTo   DateTime = '01/01/1955',  
@ProvinceCode  Varchar(2) = '',  
@City    Varchar(50) = '',  
@PostalCode   Varchar(7) = '',  
@GroupClassCode  Varchar(7) = '',  
@GroupCodeName  Varchar(7) = '',  
@StaffIndicator   Int = 2,  
@CampaignLanguage  Varchar(2) = '',  
@ProgramsFromCampaign Int = 0,  
@IncentivesPrograms  Int = 0,  
@CatalogCode   Varchar(50) = '',  
@InternetOrders  Int= 0  
As  
  
Set NoCount On  
  
Declare @Cnt   Int  
Declare @StartInt  Int  
Declare @EndInt   Int  
  
Declare @Qualifier Table (OrderQualifierID Int)  
  
--Both Staff and NonStaff  
Select @StartInt = Case IsNull(@StaffIndicator ,0)  
     When 2 Then -1  
     When 1 Then 0   
     When 0 Then -1  
     End,  
 @EndInt = Case IsNull(@StaffIndicator ,0)  
     When 2 Then 2  
     When 1 Then 2  
     When 0 Then 1   
     End   
  
Set @InternetOrders=0  -- Screen does not pass this report Parameter always include internet MS April 4, 2006  
  
If @InternetOrders=1  
Begin  
 --Only Internet  
 Insert  @Qualifier   
 Select instance From QSPcanadacommon..CodeDetail Where instance in ( 39009)  
End  
Else  
Begin  
 --All including internet  
 Insert into @Qualifier   
 Select instance from QSPcanadacommon..CodeDetail Where instance in (39001,39002,39003,39009,39020,39013,39015)  
End   
  
Create Table #AllItems(  
   OrderID   Int,  
   BatchId   Int,  
   Batchdate  DateTime,   
   OrderType  Varchar(20),  
   AccountId  Int,   
   AccountName  Varchar(50),  
   CampaignId  Int,  
   ApprovedStatusDate DateTime,   
   FMID   Int,   
   LastName  Varchar(50),   
   FirstName  Varchar(50),  
   MagSectionId  Int,  
   MagCount  Int,  
   MagSale  Numeric(16,2),  
   GiftSectionId  Int,  
   GiftCount  Int,  
   GiftSale   Numeric(16,2),  
   DoughSectionId  Int,  
   CookieCount  Int,  
   CookieSale  Numeric(16,2),  
   JewelrySectionId  Int,  
   JewelryCount  Int,  
   JewelrySale   Numeric(16,2),     
   CandleSectionId  Int,  
   CandleCount  Int,  
   CandleSale   Numeric(16,2),  
   TrtSectionId  Int,  
   TrtCount  Int,  
   TrtSale   Numeric(16,2),  
   PopcornSectionId  Int,  
   PopcornCount  Int,  
   PopcornSale   Numeric(16,2), 
   EstimatedGross  Numeric(16,2),   
   Participant  Int,  
   StudentInstance  Int,  
   IsStaffOrder  Varchar(1),   
   DateFirstOrder  DateTime  
   )  
  
Create  Table #ItemsSummary (  
   CampaignId  Int,  
   AccountId  Int,   
   AccountName  Varchar(50),  
   ApprovedStatusDate DateTime,   
   FMID   Int,   
   LastName  Varchar(50),   
   FirstName  Varchar(50),  
   EstimatedGross  Numeric(16,2),   
   Participant  Int,  
   DateFirstOrder  DateTime,  
   MagCount  Int,  
   magAmount  Numeric(16,2),  
   GiftCount  Int,  
   GiftAmount  Numeric(16,2),  
   CookieDoughCount Int,  
   CookieDoughAmount Numeric(16,2),  
   JewelryCount Int,  
   JewelryAmount Numeric(16,2),     
   CandleCount Int,  
   CandleAmount Numeric(16,2),
   TrtCount Int,  
   TrtAmount Numeric(16,2), 
   PopcornCount Int,  
   PopcornAmount Numeric(16,2),
    VarianceAmount  Numeric(16,2),  
   VariancePercentage Numeric(12,5),  
   AvgEnrollment  Numeric(16,2),  
   ActualNbrParticipants Int,  
   AvgAmountParticipant Numeric(16,2),  
   ParticipationPercentage Numeric(12,5)    
   )  
  
   
Create  Table #GPSTotalsBYOrder (  
     AccountId  Int,  
     CampaignId  Int,  
      OrderID  Int,  
     MagCount  Numeric(8,2),  
     MagSectionId  Int,  
     MagSale  Numeric(16,2),  
     GiftSectionId  Int,  
     GiftCount  Numeric(8,2),  
     GiftSale  Numeric(16,2),  
     CookieCount  Numeric(8,2),  
     CookieSale  Numeric(16,2),  
     DoughSectionId Int,  
     JewelryCount  Numeric(8,2),  
     JewelrySectionId  Int,  
     JewelrySale Numeric(16,2),            
     CandleCount  Numeric(8,2),  
     CandleSectionId  Int,  
     CandleSale Numeric(16,2),
     TrtCount  Numeric(8,2),  
     TrtSectionId  Int,  
     TrtSale Numeric(16,2), 
    PopcornCount  Int,  
    PopcornSectionId  Int, 
    PopcornSale   Numeric(16,2) 
     )  
  
Create Table #GPSTotalsBYAccountCA(  
     AccountId Int,  
     CampaignId  Int,  
     MagCount  Numeric(8,2),  
     MagSale  Numeric(16,2),  
     GiftCount  Numeric(8,2),  
     GiftSale  Numeric(16,2),  
     CookieCount  Numeric(8,2),  
     CookieSale  Numeric(16,2),  
     JewelryCount  Numeric(8,2),  
     JewelrySale  Numeric(16,2),       
     CandleCount  Numeric(8,2),  
     CandleSale  Numeric(16,2),
     TrtCount  Numeric(8,2),  
     TrtSale  Numeric(16,2),
    PopcornCount Numeric(8,2),  
    PopcornSale Numeric(16,2),
     StudentCount  Numeric(8,2)  
     )  
  
Create Table #ParticipantCountByCA(  
       AccountId  Int,  
       CampaignId  Int,  
       StudentCount  Numeric(8,2)  
     )  
  
Create  Table #ItemsSummaryCopy (  
     CampaignId  Int,  
    AccountId  Int,   
    AccountName  Varchar(50),  
    ApprovedStatusDate DateTime,   
    FMID   Int,   
    LastName  Varchar(50),   
    FirstName  Varchar(50),  
    EstimatedGross  Numeric(16,2),   
    Participant  Int,  
    DateFirstOrder  DateTime,  
    MagCount  Int,  
    MagAmount  Numeric(16,2),  
    GiftCount  Int,  
    GiftAmount  Numeric(16,2),  
    CookieDoughCount Int,  
    CookieDoughAmount Numeric(16,2),  
    JewelryDoughCount Int,  
    JewelryAmount Numeric(16,2),      
    CandleCount Int,  
    CandleAmount Numeric(16,2),
    TrtCount Int,  
    TrtAmount Numeric(16,2),
   PopcornCount Int,  
   PopcornAmount Numeric(16,2),
     VarianceAmount  Numeric(16,2),  
    VariancePercentage Numeric(12,5),  
    AvgEnrollment  Numeric(16,2),  
    ActualNbrParticipants Int,  
    AvgAmountParticipant Numeric(16,2),  
    ParticipationPercentage Numeric(12,5)    
    )  
  
Insert #AllItems  
Select  b.Orderid, b.Id,b.Date,b.OrderTypeCode,  
 a.ID as GroupID,  
 a.Name as GroupName,  
 c.Id as CampaignID,Coalesce(c.ApprovedStatusDate, '1995-01-01') as DateCampaignApproved,  
 fm.FMID,
 fm.LastName,
 fm.FirstName,
 --fmAccountOwner.FMID as FMID,
 --fmAccountOwner.lastname,
 --fmAccountOwner.firstname,  
 sMag.Section_Type_Id MagSectionId,  
 (Case  b.OrderTypeCode  
  When 41003 Then 0  
  When 41004 Then 0  
  Else sMag.Item_Count   
 End) MagCount,  
 (Case  b.OrderTypeCode  
  When 41003 Then 0  
  When 41004 Then 0  
  Else sMag.Total_Tax_Included   
 End) MagSales,  
 sGift.Section_Type_Id GiftSectionId,  
 (Case  b.OrderTypeCode  
  When 41003 Then 0  
  When 41004 Then 0  
  Else sGift.Item_Count   
 End) GiftCount,  
 (Case  b.OrderTypeCode  
  When 41003 Then 0  
  When 41004 Then 0  
  Else sGift.Total_Tax_Included   
 End) GiftSales,  
 sCDough.Section_Type_Id CDoughSectionId,  
 (Case  b.OrderTypeCode  
  When 41003 Then 0  
  When 41004 Then 0  
  Else sCDough.Item_Count  
 End)CDoughCount,  
 (Case  b.OrderTypeCode  
  When 41003 Then 0  
  When 41004 Then 0  
  Else sCDough.Total_Tax_Included   
 End)CDoughNetSales,  
 sJewelry.Section_Type_Id JewelrySectionId,  
 (Case  b.OrderTypeCode  
  When 41003 Then 0  
  When 41004 Then 0  
  Else sJewelry.Item_Count   
 End) JewelryCount,  
 (Case  b.OrderTypeCode  
  When 41003 Then 0  
  When 41004 Then 0  
  Else sJewelry.Total_Tax_Included   
 End) JewelrySales,   
  sCandle.Section_Type_Id CandleSectionId,  
 (Case  b.OrderTypeCode  
  When 41003 Then 0  
  When 41004 Then 0  
  Else sCandle.Item_Count   
 End) CandleCount,  
 (Case  b.OrderTypeCode  
  When 41003 Then 0  
  When 41004 Then 0  
  Else sCandle.Total_Tax_Included   
 End) CandleSales,  
 sTrt.Section_Type_Id TrtSectionId,  
 (Case  b.OrderTypeCode  
  When 41003 Then 0  
  When 41004 Then 0  
  Else sTrt.Item_Count   
 End) TrtCount,  
 (Case  b.OrderTypeCode  
  When 41003 Then 0  
  When 41004 Then 0  
  Else sTrt.Total_Tax_Included   
 End) TrtSales,  
  sPopcorn.Section_Type_Id PopcornSectionId,  
 (Case  b.OrderTypeCode  
  When 41003 Then 0  
  When 41004 Then 0  
  Else sPopcorn.Item_Count   
 End) PopcornCount,  
 (Case  b.OrderTypeCode  
  When 41003 Then 0  
  When 41004 Then 0  
  Else sPopcorn.Total_Tax_Included   
 End) PopcornSales,
 c.EstimatedGross as EstimatedAmountFromCampaign,  
 Case IsNull(c.IsstaffOrder,0) When 0 Then c.NumberOfParticipants When 1 Then c.NumberOfStaff End as CampaignEnrollment,  
 oh.studentInstance,  
 c.isstafforder,  
 Min(b.date)DateFirstOrder  
FROM		CustomerOrderHeader oh (NOLOCK)
JOIN		Batch b (NOLOCK) ON b.ID = oh.OrderBatchID And b.[Date] = oh.OrderBatchDate
JOIN		QSPCanadaCommon..Campaign c (NOLOCK) ON c.ID = b.CampaignID
JOIN		QSPCanadaCommon..FieldManager fm (NOLOCK) ON fm.FMID = c.FMID
JOIN		QSPCanadaCommon..FieldManager dm (NOLOCK) ON dm.FMID = fm.DMID
JOIN		QSPCanadaCommon..FieldManager fmAccountOwner (NOLOCK) ON fmAccountOwner.FMID = QSPCanadaCommon.dbo.UDF_Account_GetFMID(c.BillToAccountID, GETDATE())
JOIN		QSPCanadaCommon..FieldManager dmAccountOwner (NOLOCK) ON dmAccountOwner.FMID = fmAccountOwner.DMID
JOIN		QSPCanadaCommon..CAccount a (NOLOCK) ON a.Id = b.AccountID
JOIN		QSPCanadaCommon..Address ad (NOLOCK) ON ad.addresslistid = a.addresslistid And ad.address_type = 54001  
LEFT JOIN	QSPCanadaFinance.dbo.Invoice i    (NOLOCK) ON b.OrderId=I.Order_Id    
LEFT JOIN	QSPCanadaFinance.dbo.Invoice_Section sMag     (NOLOCK) ON  sMag.Invoice_Id = i.Invoice_Id and sMag.Section_Type_Id=2  
LEFT JOIN	(SELECT g.Invoice_ID, g.Section_Type_ID, SUM(g.Item_Count) Item_Count, SUM(g.Total_Tax_Excluded) Total_Tax_Excluded, SUM(g.Total_Tax_Included) Total_Tax_Included FROM QSPCanadaFinance.dbo.Invoice_Section g (NOLOCK) WHERE g.Section_Type_ID IN (1) GROUP BY g.Invoice_ID, g.Section_Type_ID) sGift ON sGift.Invoice_ID = i.Invoice_ID
LEFT JOIN	(SELECT g.Invoice_ID, g.Section_Type_ID, SUM(g.Item_Count) Item_Count, SUM(g.Total_Tax_Excluded) Total_Tax_Excluded, SUM(g.Total_Tax_Included) Total_Tax_Included FROM QSPCanadaFinance.dbo.Invoice_Section g (NOLOCK) WHERE g.Section_Type_ID IN (11) GROUP BY g.Invoice_ID, g.Section_Type_ID) sJewelry ON sJewelry.Invoice_ID = i.Invoice_ID
--LEFT JOIN	QSPCanadaFinance.dbo.Invoice_Section sJewelry  (NOLOCK) ON  sJewelry.Invoice_Id = i.Invoice_Id and sJewelry.Section_Type_Id IN (11)  
LEFT JOIN	QSPCanadaFinance.dbo.Invoice_Section sCDough   (NOLOCK) ON  sCDough.Invoice_Id = i.Invoice_Id and sCDough.Section_Type_Id = 9  
LEFT JOIN	QSPCanadaFinance.dbo.Invoice_Section sCandle     (NOLOCK) ON  sCandle.Invoice_Id = i.Invoice_Id and sCandle.Section_Type_Id IN (13) 
LEFT JOIN	QSPCanadaFinance.dbo.Invoice_Section sTrt     (NOLOCK) ON  sTrt.Invoice_Id = i.Invoice_Id and sTrt.Section_Type_Id IN (14)   -- from QSPCanadaProduct.dbo.ProgramSectionType
LEFT JOIN	QSPCanadaFinance.dbo.Invoice_Section sPopcorn     (NOLOCK) ON  sPopcorn.Invoice_Id = i.Invoice_Id and sPopcorn.Section_Type_Id IN (10)  
Where		b.StatusInstance <> (40005)  
And			b.OrderQualifierID NOT IN (39006,39014,39011,39012,39004,39017,39018,39019,39022,39023)    
AND			(fm.FMID = ISNULL(@FMID, fm.FMID) OR @FMID = fmAccountOwner.FMID OR dm.FMID = ISNULL(@FMID, dm.FMID) OR @FMID = dmAccountOwner.FMID)
And c.Startdate >=  @DateFrom   
And c.Startdate <=  Case @DateTo  
   When '01/01/1955' Then '01/01/2050'  
   Else @DateTo  
     End  
And ad.StateProvince = Case ISnull(@ProvinceCode,'')  
    When '' Then ad.stateProvince  
    Else @ProvinceCode  
    End  
And ad.City = Case IsNull(@City,'')  
    When '' Then ad.City  
    Else @City  
    End  
And ad.Postal_Code = Case IsNull(@PostalCode,'')  
    When '' Then ad.Postal_Code  
    Else @PostalCode  
    End  
And a.CAccountCodeGroup = Case IsNull(@GroupCodeName,'')  
    When '' Then a.CAccountCodeGroup  
    Else @GroupCodeName  
    End  
And  a.CAccountCodeClass = Case IsNull(@GroupClassCode,'')  
    When '' Then a.CAccountCodeClass  
    Else @GroupClassCode  
    End  
And  c.IsStaffOrder  > @StartInt   
And  c.IsStaffOrder   < @EndInt  
And  c.Lang = Case IsNull(@CampaignLanguage,'')  
    When '' Then c.Lang  
    Else @CampaignLanguage  
    End  
And b.Orderqualifierid in ( Select OrderQualifierID from @Qualifier)  
And Exists (Select cp.ProgramID  
  From QSPCanadaCommon..CampaignProgram cp  
  Where cp.CampaignID = c.ID  
  And cp.DeletedTF = 0  
  And cp.ProgramID = Case IsNull(@ProgramsFromCampaign,0)  
     When 0 Then cp.ProgramID  
     Else @ProgramsFromCampaign  
     End  
  )  
And Exists  
  (Select cp2.ProgramID  
  From QSPCanadaCommon..CampaignProgram cp2  
  Where cp2.CampaignID = c.ID  
  And  cp2.DeletedTF = 0  
  And  cp2.ProgramID = Case IsNull(@IncentivesPrograms,0)  
      When 0 Then cp2.ProgramID  
      Else @IncentivesPrograms  
      End  
  )  
Group By b.id,b.date, b.orderid,a.ID ,  
 a.Name ,  
 c.Id ,c.ApprovedStatusDate, fm.FMID,--fmAccountOwner.FMID,  
 c.EstimatedGross,c.NumberOfParticipants,c.NumberOfStaff,  
 fm.LastName, fm.FirstName,
--fmAccountOwner.lastname,fmAccountOwner.firstname ,   
 sMag.Section_type_Id, sMag.TOTAL_TAX_INCLUDED, sMag.Item_Count,  
 sGift.Section_type_Id,sGift.TOTAL_TAX_INCLUDED,sGift.Item_Count,  
 sCDough.Section_type_Id,sCDough.TOTAL_TAX_INCLUDED,sCDough.Item_Count,  
 sJewelry.Section_type_Id, sJewelry.TOTAL_TAX_INCLUDED, sJewelry.Item_Count,  
 sCandle.Section_type_Id, sCandle.TOTAL_TAX_INCLUDED, sCandle.Item_Count,  
 sTrt.Section_type_Id, sTrt.TOTAL_TAX_INCLUDED, sTrt.Item_Count, 
 sPopcorn.SECTION_TYPE_ID, sPopcorn.TOTAL_TAX_INCLUDED, sPopcorn.Item_Count, 
 b.OrderTypeCode,oh.studentInstance,c.isstafforder  
  
--Create summary record for each campaign  
Insert #ItemsSummary  
Select Distinct CampaignId,AccountId,Accountname,ApprovedStatusDate,  
 FMId,lastName,FirstName,EstimatedGross,Participant,Min(DateFirstOrder),  
 0 as ActualMagUnit,   
 0 as ActualMagAmount ,  
 0 As actualGiftUnit,  
 0 As ActualGiftAmount,   
 0 as actualCookieDough ,  
 0 as ActualCookieDoughAmount,  
 0 as ActualJewelryUnit,  
 0 as ActualJewelryAmount,  
 0 as ActualCandleUnit,  
 0 as ActualCandleAmount, 
 0 as ActualTrtUnit,  
 0 as ActualTrtAmount, 
 0 as ActualPopcornUnit,  
 0 as ActualPopcornAmount, 
 0 as VarianceAmount ,  
 0 as VariancePercentage ,  
 0 as AvgEnrollment  ,  
 0 as ActualNbrParticipants ,  
 0 as AvgAmountParticipant ,  
 0 as ParticipationPercentage   
From #AllItems  
Group By CampaignId,AccountId,Accountname,ApprovedStatusDate,  
  FMId,lastName,FirstName,EstimatedGross,Participant  
  
  
--Insert count and Totals By order   
Insert #GPSTotalsBYOrder  
Select Distinct AccountId,CampaignId,Orderid,   
 MagCount , MagSectionId, MagSale,  
 GiftSectionId,GiftCount,GiftSale,  
 cookieCount,CookieSale,DoughSectionId,  
 JewelryCount , JewelrySectionId, JewelrySale,  
 CandleCount , CandleSectionId, CandleSale,  
 TrtCount , TrtSectionId, TrtSale,
 PopcornCount, PopcornSectionId, PopcornSale  
From #AllItems   
  
--Insert count and Totals By Account and CA  
Insert #GPSTotalsBYAccountCA  
Select AccountId,CampaignId,  
 Sum(MagCount)MagCount, Sum(MagSale) MagSale,   
 Sum(GiftCount)GiftCount, Sum(GiftSale) GiftSale,   
 Sum(cookieCount)CookieCount, Sum(CookieSale) CookieSale,  
 Sum(JewelryCount)JewelryCount, Sum(JewelrySale) JewelrySale,   
 Sum(CandleCount)CandleCount, Sum(CandleSale) CandleSale,   
 Sum(TrtCount)TrtCount, Sum(TrtSale) TrtSale,
 Sum(PopcornCount)PopcornCount, Sum(PopcornSale) PopcornSale,
 0 ActualStudentCount   
From #GPSTotalsBYOrder  
Group by AccountId,CampaignId  
  
--Student count by CA  
Insert #ParticipantCountByCA  
Select AccountId,CampaignId,Count(Distinct StudentInstance) Cnt  
From #AllItems Group by accountid,campaignid  
  
  
Update #GPSTotalsBYAccountCA  
Set #GPSTotalsBYAccountCA.StudentCount=#ParticipantCountByCA.StudentCount  
From #ParticipantCountByCA  
Where #ParticipantCountByCA.Accountid=#GPSTotalsBYAccountCA.Accountid  
And #ParticipantCountByCA.Campaignid=#GPSTotalsBYAccountCA.Campaignid  
  
  
Update #ItemsSummary  
Set MagCount=#GPSTotalsBYAccountCA.MagCount,  
 MagAmount=#GPSTotalsBYAccountCA.MagSale,  
 GiftCount=#GPSTotalsBYAccountCA.GiftCount,  
 GiftAmount=#GPSTotalsBYAccountCA.GiftSale,  
 CookieDoughCount=#GPSTotalsBYAccountCA.CookieCount,  
 CookieDoughAmount=#GPSTotalsBYAccountCA.CookieSale,  
 JewelryCount=#GPSTotalsBYAccountCA.JewelryCount,  
 JewelryAmount=#GPSTotalsBYAccountCA.JewelrySale,  
 CandleCount=#GPSTotalsBYAccountCA.CandleCount,  
 CandleAmount=#GPSTotalsBYAccountCA.CandleSale,   
 TrtCount=#GPSTotalsBYAccountCA.TrtCount,  
 TrtAmount=#GPSTotalsBYAccountCA.TrtSale, 
 PopcornCount=#GPSTotalsBYAccountCA.PopcornCount,  
 PopcornAmount=#GPSTotalsBYAccountCA.PopcornSale, 
 ActualNbrParticipants=#GPSTotalsBYAccountCA.StudentCount  
From #GPSTotalsBYAccountCA  
Where #GPSTotalsBYAccountCA.AccountId=#ItemsSummary.AccountID  
And #GPSTotalsBYAccountCA.CampaignId=#ItemsSummary.CampaignID  
  
  
--Make a copy as Table alias does not work on temp table  
Insert #ItemsSummaryCopy  
Select * From #ItemsSummary  
  
Update #ItemsSummary  
Set  VarianceAmount= (IsNull(#ItemsSummaryCopy.MagAmount,0)+IsNull(#ItemsSummaryCopy.GiftAmount,0)+IsNull(#ItemsSummaryCopy.CookieDoughAmount,0)+IsNull(#ItemsSummaryCopy.JewelryAmount,0)+IsNull(#ItemsSummaryCopy.CandleAmount,0)+IsNull(#ItemsSummaryCopy.TrtAmount,0)+IsNull(#ItemsSummaryCopy.PopcornAmount,0)) - IsNull(#ItemsSummaryCopy.EstimatedGross,0),  
 VariancePercentage= (IsNull(#ItemsSummaryCopy.MagAmount,0)+IsNull(#ItemsSummaryCopy.GiftAmount,0)+IsNull(#ItemsSummaryCopy.CookieDoughAmount,0)+IsNull(#ItemsSummaryCopy.JewelryAmount,0)+IsNull(#ItemsSummaryCopy.CandleAmount,0)+IsNull(#ItemsSummaryCopy.TrtAmount,0)+IsNull(#ItemsSummaryCopy.PopcornAmount,0) -  IsNull(#ItemsSummaryCopy.EstimatedGross,0))/(Case IsNull(#ItemsSummaryCopy.EstimatedGross,0)  
              When 0 Then 1   
              Else #ItemsSummaryCopy.EstimatedGross  
              End),  
 AvgEnrollment=  (IsNull(#ItemsSummaryCopy.MagAmount,0)+IsNull(#ItemsSummaryCopy.GiftAmount,0)+IsNull(#ItemsSummaryCopy.CookieDoughAmount,0)+IsNull(#ItemsSummaryCopy.JewelryAmount,0)+IsNull(#ItemsSummaryCopy.CandleAmount,0)+IsNull(#ItemsSummaryCopy.TrtAmount,0)+IsNull(#ItemsSummaryCopy.PopcornAmount,0))/(Case IsNull(#ItemsSummaryCopy.participant,0)  
          When 0 Then 1   
          Else #ItemsSummaryCopy.participant  
          End ),  
 AvgAmountParticipant= (IsNull(#ItemsSummaryCopy.MagAmount,0)+IsNull(#ItemsSummaryCopy.GiftAmount,0)+IsNull(#ItemsSummaryCopy.CookieDoughAmount,0)+IsNull(#ItemsSummaryCopy.JewelryAmount,0)+IsNull(#ItemsSummaryCopy.CandleAmount,0)+IsNull(#ItemsSummaryCopy.TrtAmount,0)+IsNull(#ItemsSummaryCopy.PopcornAmount,0)) / Case Isnull(#ItemsSummaryCopy.ActualNbrParticipants ,0)  
          When 0 Then 1   
           Else #ItemsSummaryCopy.ActualNbrParticipants  
           End ,  
 ParticipationPercentage= Cast(( Isnull(#ItemsSummaryCopy.ActualNbrParticipants ,0)/ Case IsNull(#ItemsSummaryCopy.participant,0)   
        When 0 Then 1   
        Else (#ItemsSummaryCopy.participant)   
        End)   
        As Numeric(10,2))   
From #ItemsSummaryCopy   
Where #ItemsSummary.campaignId=#ItemsSummaryCopy.campaignId  
And #ItemsSummary.AccountId=#ItemsSummaryCopy.AccountId  
  
Select    CampaignId   CampaignID,   
 AccountId   GroupID,  
 AccountName   GroupName,  
 ApprovedStatusDate  DateCampaignApproved,   
 FMID    FMID,  
 LastName   lastname,   
 FirstName   firstname,  
 Lastname+' '+FirstName   FMName,  
 EstimatedGross   EstimatedAmountFromCampaign,   
 Participant   CampaignEnrollment,  
 DateFirstOrder   DateFirstOrder,  
 ISNULL(MagCount,0)  ActualMagUnits,  
 IsNull(magAmount,0)  ActualMagAmount,  
 IsNull(GiftCount,0)  ActualGiftUnits,  
 Isnull(GiftAmount,0)  ActualGiftAmount,  
 IsNull(CookieDoughCount,0) CookieDoughCount,  
 IsNull(CookieDoughAmount,0) CookieDoughAmount,  
 ISNULL(JewelryCount,0)  ActualJewelryUnits,  
 IsNull(JewelryAmount,0)  ActualJewelryAmount,    
 ISNULL(CandleCount,0)  ActualCandleUnits,  
 IsNull(CandleAmount,0)  ActualCandleAmount,  
 ISNULL(TrtCount,0)  ActualTrtUnits,  
 IsNull(TrtAmount,0)  ActualTrtAmount, 
 ISNULL(PopcornCount,0)  ActualPopcornUnits,  
 IsNull(PopcornAmount,0)  ActualPopcornAmount, 
 IsNull(MagCount,0)+IsNull(GiftCount,0)+IsNull(CookieDoughCount,0)+IsNull(JewelryCount,0)+IsNull(CandleCount,0)+IsNull(TrtCount,0)+IsNull(PopcornCount,0) ActualUnits,  
 IsNull(magAmount,0)+IsNull(GiftAmount,0)+IsNull(CookieDoughAmount,0)+IsNull(JewelryAmount,0)+IsNull(CandleAmount,0)+IsNull(TrtAmount,0)+IsNull(PopcornAmount,0) ActualAmount,  
  VarianceAmount   VarianceAmount,  
 VariancePercentage  VariancePercentage,  
 AvgEnrollment   AvgEnrollment,  
 ActualNbrParticipants  ActualNbrParticipants,  
 AvgAmountParticipant  AvgAmountParticipant,  
 ParticipationPercentage  ParticipationPercentage  
From  #ItemsSummary    
Order By LastName, FirstName, AccountName  
  
  
Drop Table #ItemsSummary  
Drop Table #AllItems  
Drop Table #GPSTotalsBYOrder  
Drop Table #ItemsSummaryCopy  
Drop Table #GPSTotalsBYAccountCA  
Drop Table #ParticipantCountByCA  
GO
