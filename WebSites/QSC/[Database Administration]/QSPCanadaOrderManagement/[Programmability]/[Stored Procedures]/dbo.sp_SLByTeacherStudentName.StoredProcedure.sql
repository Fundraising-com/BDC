USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[sp_SLByTeacherStudentName]    Script Date: 06/07/2017 09:20:51 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[sp_SLByTeacherStudentName]
     @lAccountID int,
     @fromDate datetime,
     @toDate datetime
as
  set nocount on
  set ansi_warnings off
  SELECT Account.ID,   
              Teacher.Name, Teacher.Classroom, Teacher.Instance,Student.Instance,
              Student.LastName, Student.FirstName, 
              CustomerOrderDetail.CustomerORderheaderinstance,
              CustomerOrderDetail.ProductCode, 
              CustomerOrderDetail.Price, CustomerOrderDetail.PriceA, CustomerOrderDetail.CrossedBridgeDate, 
              CustomerOrderDetail.DelFlag,
              ISNUMERIC(SUBSTRING(Teacher.Name, 1, 1)) AS teacherfirstletter 
              FROM Account, Teacher,  Student, CustomerOrderHeader, CustomerOrderDetail 
  where (Account.ID = @lAccountID ) AND 
		Teacher.Instance = Student.TeacherInstance AND
                 CustomerOrderHeader.AccountID = account.id and
                 Student.Instance = CustomerOrderHeader.StudentInstance AND 
                 CustomerOrderHeader.Instance = CustomerOrderDetail.CustomerOrderHeaderInstance AND 
                 ( CustomerOrderHeader.CreationDate >= @fromDate AND CustomerOrderHeader.CreationDate <= @toDate )
  order by teacherfirstletter,Teacher.Name, Teacher.Classroom, Student.LastName, Student.FirstName, CustomerOrderDetail.CustomerOrderHeaderInstance
  return 0
GO
