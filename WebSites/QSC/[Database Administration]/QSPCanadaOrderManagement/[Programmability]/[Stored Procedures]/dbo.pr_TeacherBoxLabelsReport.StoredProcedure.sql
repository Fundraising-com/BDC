USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_TeacherBoxLabelsReport]    Script Date: 06/07/2017 09:20:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_TeacherBoxLabelsReport]  
@ReportRequestID int = 0, @OrderID int, @TeacherID int , @TotalLabels int, @ShipmentGroupID int = null 
AS
SET NOCOUNT ON

-- SS, Sep, 2004
-- used in .net Teacher Box labels report

Declare @V_Total_Labels int, @OddEven varchar(4) 

 Create table #HowManyLabels (id int)

 IF @TotalLabels = null OR @TotalLabels  = ''
   BEGIN 
      SET @TotalLabels = 2 -- doc control and warehouse people said atleast 2 are needed
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

SELECT		DISTINCT 
			upper(dbo.[UDF_RemoveTitle](t.LastName)) as TeacherLastName,
			upper(t.Title) as TeacherTitle,
			t.Classroom,
			@OrderID OrderID,
			b.CampaignID,
			camp.ShiptoAccountID,
			upper(acc.Name) as AccountName,
			ad.Street1 as Address1,
			ad.Street2 as Address2,
			upper(ad.City) as City,
			upper(ad.StateProvince) as [State],
			ad.Postal_Code as Zip,
			ph.PhoneNumber AccountPhoneNumber,
			@OddEven as OddEven,
			s.FirstName as StudentFirstName,
			s.LastName as StudentLastName,
			t.FirstName as TeacherFirstName
FROM		QSPCanadaCommon..Campaign camp
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.Id = camp.ShiptoAccountID
LEFT JOIN	QSPCanadaCommon..Address ad
				ON	ad.AddressListID = acc.AddressListID
				AND	ad.Address_Type = 54001 --shipto address type
LEFT JOIN	QSPCanadaCommon..Phone ph
				ON	ph.PhoneListID = acc.PhoneListID
				AND	ph.[Type] = 30501
JOIN		QSPCanadaOrderManagement..Batch b
				ON	b.CampaignID = camp.ID
JOIN		QSPCanadaOrderManagement..CustomerOrderHeader coh
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.Date
JOIN		QSPCanadaOrderManagement..CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
JOIN		QSPCanadaOrderManagement..Student s
				ON	s.Instance = coh.StudentInstance
JOIN		QSPCanadaOrderManagement..Teacher t
				ON	t.Instance = s.TeacherInstance
JOIN		#HowManyLabels hml
				ON	hml.id = 1
JOIN		QSPCanadaCommon..QSPProductLine pl ON pl.ID = cod.ProductType
WHERE		(b.OrderID = @OrderID
			 OR (OrderID IN (SELECT DISTINCT OnlineOrderID  
			 FROM OnlineOrderMappingTable  
			 WHERE LandedOrderID = @OrderID)
			 AND IsShippedToAccount = 1))
AND			s.TeacherInstance = isnull(@TeacherID, s.TeacherInstance) 
AND			cod.ProductType IN (46002, 46007, 46008, 46013, 46014, 46018, 46019, 46020, 46022, 46024)
AND			pl.ShipmentGroupID = ISNULL(@ShipmentGroupID, pl.ShipmentGroupID)
ORDER BY	upper(dbo.[UDF_RemoveTitle](t.LastName)), t.FirstName, s.LastName, s.FirstName
 
DROP TABLE #HowManyLabels

--following lines are written by saqib on 13-Apr-2005 to update data driven subscription support tables
IF @ReportRequestID <> 0  -- if the value is not zero it means the report is called from a data driven subscription
BEGIN
     
   UPDATE Qspcanadaordermanagement.dbo.ReportRequestBatch_TeacherBoxLabelsReport
   set  RunDateStart = getdate()
   where [id]  = @ReportRequestID

END
GO
