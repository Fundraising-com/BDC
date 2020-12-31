USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_UpdateCustomerForCoupon]    Script Date: 06/07/2017 09:20:41 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_UpdateCustomerForCoupon] 

@iCustomerInstance int,
@sFirstName	nvarchar (50)	='',
@sLastName	nvarchar (50)	='',
@sAddress1	nvarchar (128)	='',
@sAddress2	nvarchar (128)	='',
@sCity		nvarchar (50)	='',
@sState	nvarchar (5)	='',
@sZip		nvarchar (15)	='',
@sEmail	nvarchar (100)	='',
@sPhone	nvarchar (25)	='',
@iType 	int 		= 0,
@iUserID	int 		= 0

AS



update Customer 

set
			StatusInstance =300,
			LastName =@sFirstName,
			FirstName = @sLastName,
			Address1 =@sAddress1,
			Address2 =@sAddress2,
			City =@sCity,
			County = 'CA',
			State = @sState,
			Zip =@sZip,
			ZipPlusFour ='',
			OverrideAddress = 0,
			ChangeUserID =@iUserID,
			ChangeDate =GetDate(),
			Email =@sEmail,
			Phone =@sPhone,
			Type =@iType
		

where Instance = @iCustomerInstance
GO
