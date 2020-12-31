USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GroupSummaryReportDetail]    Script Date: 06/07/2017 09:19:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------

CREATE  Procedure [dbo].[GroupSummaryReportDetail]             @OrderId   Int,  
                 @ReportRequestID  Int  
              
        
AS  
SET NOCOUNT ON  
  
Begin  
  
DECLARE @RoomTotals TABLE  (  
 ID    Int Identity,  
 AccountAddress1  Varchar(50),   
 AccountAddress2 Varchar(50),  
 AccountCity  Varchar(50),   
 AccountProvince Varchar(50),   
 AccountPcode  Varchar(10),  
 ClassRoom  Varchar(50),  
 RoomCount  Int,  
 TeacherInstance Int,  
 TeacherFirstName  Varchar(50),  
 TeacherLastName   Varchar(50),  
 AccountId   Int,  
 Accountname   Varchar(50),  
 PhoneNumber   Varchar(15),  
 FirstName   Varchar(50),  
 Lastname   Varchar(50),  
 ProgramList   Varchar(200),  
 CampaignId   Int,  
 Lang    Varchar(10),  
 IsOnline   Varchar(1),  
 FMid   Varchar(10),  
 FMFName  Varchar(50),  
 FMLName  Varchar(50),  
 MagQuantityReg  Int,  
 MagAmountReg   Numeric(10,2),  
 MagProfitReg		Numeric(10,2),
 MagQuantityOnline  Int,  
 MagamountOnline Numeric(10,2), 
 MagProfitOnline		Numeric(10,2),	
 GiftQuantityReg   Int,  
 GiftAmountReg  Numeric(10,2),  
 GiftProfitReg		Numeric(10,2),
 GiftQuantityOnline   Int,  
 GiftAmountOnline  Numeric(10,2),  
 GiftProfitOnline		Numeric(10,2),	
 CookieDoughQuantityReg 		Int,
 CookieDoughAmountReg 	Numeric(10,2),
 CookieDoughProfitReg		Numeric(10,2),
 CookieDoughQuantityOnline 		Int,
 CookieDoughAmountOnline 	Numeric(10,2),
 CookieDoughProfitOnline		Numeric(10,2),	
 TrtQuantityReg 		Int,
 TrtAmountReg 	Numeric(10,2),	
 TRTProfitReg		Numeric(10,2),
 TrtQuantityOnline 		Int,
 TrtAmountOnline 	Numeric(10,2),	
 TRTProfitOnline		Numeric(10,2),	
 LvlAQuantity  Int,  
 LvlBQuantity  Int,  
 LvlCQuantity  Int,  
 LvlDQuantity  Int,  
 LvlEQuantity  Int,  
 LvlFQuantity  Int,  
 LvlGQuantity  Int,  
 LvlHQuantity  Int,  
 LvlIQuantity  Int,  
 LvlJQuantity  Int,  
 LvlKQuantity  Int,  
 RogersPremiumCount  Int,  
 RDPremiumCount  Int,  
 Logo    Varchar(30),  
 People_12Issues  Int,  
 People_22Issues  Int,  
 People_53Issues  Int ,
 SortOrder	Varchar(50) 
 )  
  
INSERT @RoomTotals  
SELECT   
 AccountAddress1,   
 AccountAddress2,  
 AccountCity,   
 AccountProvince,   
 AccountPcode,  
 ClassRoom,  
 RoomCount,  
 TeacherInstance,  
 TeacherFirstName,  
 TeacherLastName,  
 AccountId,  
 Accountname,  
 PhoneNumber,  
 FirstName,  
 Lastname,  
 ProgramList,  
 CampaignId,  
 Lang,  
 IsOnline,  
 FMid,  
 FMFName,  
 FMLName,  
 MagQuantityReg,  
 MagAmountReg,  
 MagProfitReg,
 MagQuantityOnline,  
 MagamountOnline, 
 MagProfitOnline,
 GiftQuantityReg,
 GiftAmountReg,
 GiftProfitReg,
 GiftQuantityOnline,
 GiftAmountOnline,
 GiftProfitOnline,
 CookieDoughQuantityReg,
 CookieDoughAmountReg,
 CookieDoughProfitReg,
 CookieDoughQuantityOnline,
 CookieDoughAmountOnline,
 CookieDoughProfitOnline,
 TrtQuantityReg,
 TrtAmountReg,
 TrtProfitReg,
 TrtQuantityOnline,
 TrtAmountOnline,
 TrtProfitOnline,
 LvlAQuantity,  
 LvlBQuantity,  
 LvlCQuantity,  
 LvlDQuantity,  
 LvlEQuantity,  
 LvlFQuantity,  
 LvlGQuantity,  
 LvlHQuantity,  
 LvlIQuantity,  
 LvlJQuantity,  
 LvlKQuantity,  
 RogersPremiumCount,  
 RDPremiumCount,  
 'logo_qsp_fr.gif',  
 0,  
 0,  
 0,
 SortOrder
   
FROM [QSPCanadaOrderManagement].[dbo].[UDF_SummaryReportDetail](@OrderId)   
  
DECLARE @CaID Int  
DECLARE @lang Varchar(10)  
DECLARE @ProgramList Varchar(500)  
DECLARE @PhoneNumber Varchar(20)  
DECLARE @CleanPhoneNumber Varchar(20)  
  
SELECT TOP 1 @CaID=CampaignId, @lang=lang, @PhoneNumber=PhoneNumber FROM @RoomTotals  
SELECT @ProgramList = dbo.UDF_ProgramsbyCampaignLang(@CaID,@lang)  
  
SELECT @CleanPhoneNumber = QSPCanadaFinance.dbo.UDF_CleanPhoneNumber(@PhoneNumber,'-')   
  
UPDATE @RoomTotals  
SET  ProgramList= @ProgramList,  
 PhoneNumber = @CleanPhoneNumber  
  
/*  
UPDATE rt  
SET  rt.People_12Issues = dbo.UDF_GetPeopleNbrOfIssuesByTeacherID( @OrderId, 12, t1.TeacherInstance),  
  rt.People_22Issues = dbo.UDF_GetPeopleNbrOfIssuesByTeacherID(@OrderId, 22, t1.TeacherInstance),  
  rt.People_53Issues = dbo.UDF_GetPeopleNbrOfIssuesByTeacherID(@OrderId, 53, t1.TeacherInstance)  
FROM @RoomTotals rt,  
(    
 SELECT MIN(ID) AS MinID,  
   TeacherInstance  
 FROM @RoomTotals  
 GROUP BY TeacherInstance    
)t1 WHERE rt.TeacherInstance = t1.TeacherInstance AND rt.ID = t1.MinID  
*/  
  
  
  
  
SELECT * FROM @RoomTotals  
ORDER BY  SortOrder--dbo.[UDF_RemoveTitle](TeacherLastName)--Classroom,TeacherInstance --TeacherLastName  
  
--------- DDS Update  
IF @@RowCount = 0   
Begin  
         Insert into QspCanadaCommon.dbo.SystemErrorLog   
    ( ErrorDate,OrderID,CampaignID,ProcName,Desc1,Desc2,IsReviewed,IsFixed)   
         values ( getdate(),@OrderID,Null, 'GroupSummaryReportDetail','Zero RowCount',null,0,0 ) End  
  
End  -- If IsRegular  
  
--Saqib- April 2005 - To update data driven subscription support tables  
IF @ReportRequestID >= 1  -- if the value is greater than 0  it means the report is called from a data driven subscription  
BEGIN  
   UPDATE Qspcanadaordermanagement.dbo.ReportRequestBatch_GroupSummaryReport  
   Set  RunDateStart = Getdate()    Where [id]  = @ReportRequestID  
END
GO
