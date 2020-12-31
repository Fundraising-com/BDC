USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[HomeRoomSummaryReportDetail_New]    Script Date: 06/07/2017 09:19:38 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  Procedure [dbo].[HomeRoomSummaryReportDetail_New]   @OrderId 	Int
						      
						
AS
DECLARE @RoomTotals TABLE  (
	ID 			Int Identity,
	AccountAddress1 	Varchar(50), 
	AccountAddress2	Varchar(50),
	AccountCity		Varchar(50), 
	AccountProvince	Varchar(50), 
	AccountPcode		Varchar(10),
	ClassRoom		Varchar(50),
	TeacherFirstName 	Varchar(50),
	TeacherLastName  	Varchar(50),
	AccountId	 	Int,
	Accountname	 	Varchar(50),
	PhoneNumber	 	Varchar(15),
	FirstName	 	Varchar(50),
	Lastname	 	Varchar(50),
	ProgramList	 	Varchar(200),
	CampaignId	 	Int,
	Lang		 	Varchar(10),
	FMid			Int,
	FMFName		Varchar(50),
	FMLName		Varchar(50),
	StudentFName	Varchar(50),
	StudentLName	Varchar(50),
	MagQuantityReg	 Int,
	MagAmountReg	 	Numeric(10,2),
	MagQuantityOnline 	Int,
	MagamountOnline	Numeric(10,2),
	GiftQuantity 		Int,
	TotalGiftItemPrice 	Numeric(10,2),
	LvlAQuantity		Int,
	LvlBQuantity		Int,
	LvlCQuantity		Int,
	LvlDQuantity		Int,
	LvlEQuantity		Int,
	LvlFQuantity		Int,
	LvlGQuantity		Int,
	RogersPremiumCount 	Int,
	RDPremiumCount	Int,
	QSPAddress1label   	Varchar(100),
	QSPAddress2Label   	Varchar(100),
	QSPPhoneLabel	   	Varchar(50),
	RunOn		   	Varchar(20),
	GroupTelLabel	   	Varchar(50),
	IncentProglabel	   	Varchar(50),
	CAIdLabel	   	Varchar(25),
	ShipToAccLabel	   	Varchar(50),
	OrderIdlabel	   	Varchar(25),
	FMIdLabel	   	Varchar(25),
	FMLabel		Varchar(25),
	RoomNolabel	   	Varchar(25),
	ChocolateLabel	   	Varchar(25),
	MagazineItemLabel  	Varchar(25),
	OnlineItemLabel	   	Varchar(25),
	TotalItemsLabel    	Varchar(25),
	RDPrizesLabel	   	Varchar(25),
	NonRDPrizeslabel   	Varchar(50),
	MagazineIncentivelabel	Varchar(50),
	PageOf		   	Varchar(10),
	QTYLabel	   	Varchar(10),
	AttnLabel	   	Varchar(30)
	)

DECLARE @Distinctteacher TABLE ( 
	Id 			Int Identity,
	ClassRoom 		Varchar(50),
	TeacherLastname 	Varchar(50)
	)

DECLARE @RoomCount 	Int
DECLARE @RowNum 		Int
DECLARE @TeacherLname 	Varchar(50)
DECLARE @Class		Varchar(50)


INSERT @RoomTotals
SELECT 
	AccountAddress1, 
	AccountAddress2,
	AccountCity, 
	AccountProvince, 
	AccountPcode,
	ClassRoom,
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
	FMid,
	FMFName,
	FMLName,
	StudentFName,
	StudentLName,
	SUM(MagQuantityReg)	MagQuantityReg,
	SUM(MagAmountReg)	MagAmountReg,
	SUM(MagQuantityOnline) MagQuantityOnline,
	SUM(MagamountOnline)	MagamountOnline,
	SUM(GiftQuantity)	GiftQuantity,
	SUM(TotalGiftItemPrice)	TotalGiftItemPrice,
	SUM(LvlAQuantity)	LvlAQuantity,
	SUM(LvlBQuantity)	LvlBQuantity,
	SUM(LvlCQuantity)	LvlCQuantity,
	SUM(LvlDQuantity)	LvlDQuantity,
	SUM(LvlEQuantity)	LvlEQuantity,
	SUM(LvlFQuantity)	LvlFQuantity,
	SUM(LvlGQuantity)	LvlGQuantity,
	SUM(RogersPremiumCount)	RogersPremiumCount,
	SUM(RDPremiumCount)		RDPremiumCount,
	QSPAddress1label,
	QSPAddress2Label,
	QSPPhoneLabel,
	RunOn,
	GroupTelLabel,
	IncentProglabel,
	CAIdLabel,
	ShipToAccLabel,
	OrderIdlabel,
	FMIdLabel,
	FMLabel,
	RoomNolabel,
	ChocolateLabel,
	MagazineItemLabel,
	OnlineItemLabel,
	TotalItemsLabel,
	RDPrizesLabel,
	NonRDPrizeslabel,
	MagazineIncentivelabel,
	PageOf,
	QTYLabel,
	AttnLabel
FROM [QSPCanadaOrderManagement].[dbo].[UDF_SummaryReportDetail](@OrderId)
GROUP BY TeacherLastName,TeacherFirstName,ClassRoom,AccountId,Accountname, 
	AccountAddress1, 
	AccountAddress2,
	AccountCity, 
	AccountProvince, 
	AccountPcode,
	PhoneNumber,
	FirstName,
	Lastname,
	ProgramList,
	CampaignId,
	Lang,
	FMid,
	FMFName,
	FMLName,
	StudentFName,
	StudentLName,
	QSPAddress1label,
	QSPAddress2Label,
	QSPPhoneLabel,
	RunOn,
	GroupTelLabel,
	IncentProglabel,
	CAIdLabel,
	ShipToAccLabel,
	OrderIdlabel,
	FMIdLabel,
	FMLabel,
	RoomNolabel,
	ChocolateLabel,
	MagazineItemLabel,
	OnlineItemLabel,
	TotalItemsLabel,
	RDPrizesLabel,
	NonRDPrizeslabel,
	MagazineIncentivelabel,
	PageOf,
	QTYLabel,
	AttnLabel

INSERT @DistinctTeacher
SELECT Distinct ClassRoom,TeacherLastname 
FROM @RoomTotals
WHERE Classroom <> '00'


SELECT @RoomCount=  COUNT(ClassRoom),@RowNum=MAX(Id) FROM @DistinctTeacher

WHILE @RoomCount > 0 
BEGIN
	SELECT @Class=ClassRoom,@TeacherLname=TeacherLastname FROM @DistinctTeacher
	WHERE Id=@RowNum 

	-- If teacher has two or more class room 
	UPDATE @RoomTotals
	SET ClassRoom = @Class
	WHERE TeacherLastName = @TeacherLname
	AND ClassRoom <> @Class

SET @RoomCount=@RoomCount-1
SET @RowNum = @RowNum -1
END


SELECT * FROM @RoomTotals ORDER BY TeacherLastName,StudentLName,StudentFName
GO
