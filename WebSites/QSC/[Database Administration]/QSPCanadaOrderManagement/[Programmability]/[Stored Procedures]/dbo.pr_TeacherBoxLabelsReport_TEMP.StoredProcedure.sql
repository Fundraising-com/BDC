USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_TeacherBoxLabelsReport_TEMP]    Script Date: 06/07/2017 09:20:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_TeacherBoxLabelsReport_TEMP]
@OrderID int,@TeacherID int , @TotalLabels int
AS
SET NOCOUNT ON

-- SS, Sep, 2004
-- used in .net Teacher Box labels report

Declare @V_Total_Labels int, @OddEven varchar(4) 

 Create table #HowManyLabels (id int)

 IF @TotalLabels = null OR @TotalLabels  = ''
   BEGIN 
      SET @TotalLabels = 1
   END 


   Select @V_Total_Labels = Ceiling(Cast(@TotalLabels as numeric(10,2))/2)     -- reduce half of the labels coz report is showing 2 labels in one line

  IF Cast(Right(@TotalLabels,1) as int) in (1,3,5,7,9)
     Begin
       set @OddEven = 'ODD'  -- in odd cases the last line right label will not be printed , in even cases it will be printed
     End
  ELSE
     BEGIN
        Set @OddEven = 'EVEN' 
     END


 WHILE (@V_Total_Labels <> 0) 
   
  begin
    Insert into #HowManyLabels values (1)  
    set @V_Total_Labels = @V_Total_Labels - 1
  end 

 Select distinct upper(tch.LastName) as TeacherLastName,
	upper(tch.Title) as TeacherTitle,
	tch.Classroom,
	batch.OrderID,
	batch.CampaignID,
	ca.ShiptoAccountID,
	upper(ac.Name) as AccountName,
	ad.Street1 as Address1,
	ad.Street2 as Address2,
	upper(ad.City) as City,
	upper(ad.StateProvince) as State ,
	ad.Postal_Code as Zip,
	@OddEven as OddEven
 From 	QSPCanadaCommon..Campaign 				as ca,
	QSPCanadaCommon..CAccount 				as ac,
	QSPCanadaCommon..Address 				as ad,
	QSPCanadaOrderManagement..CustomerOrderHeader 		as coh,
	QSPCanadaOrderManagement..Student 			as stu,
	QSPCanadaOrderManagement..Teacher 			as tch,
	QSPCanadaOrderManagement..Batch 			as batch,
	#HowManyLabels						as hml
 Where
   batch.CampaignID 		  = ca.ID
   and coh.OrderBatchID    	  = Batch.id
   and coh.OrderBatchDate  	  = batch.Date
   and coh.StudentInstance 	  = stu.instance
   and stu.teacherinstance  	  = tch.instance
   and ca.ShiptoAccountID	  = ac.ID
   and ac.AddressListID		  = ad.AddressListID
   and ad.Address_Type 		  = 54001 --shipto address type
   and batch.OrderID 		  = @OrderID
   and stu.teacherinstance	  	  = isnull(@TeacherID,  stu.teacherinstance ) 
   and hml.id  			  = 1
 Order By   TeacherLastName

  drop table #HowManyLabels
GO
