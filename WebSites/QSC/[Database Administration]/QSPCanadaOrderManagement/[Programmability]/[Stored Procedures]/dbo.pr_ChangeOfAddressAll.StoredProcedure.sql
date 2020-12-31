USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ChangeOfAddressAll]    Script Date: 06/07/2017 09:19:46 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_ChangeOfAddressAll] 

@iCustomerInstance			int 		= 0,
@iCustomerOrderHeaderInstance		int		= 0,
@iTransID 				int 		= 0,
@sFirstName 				nvarchar(50)	= '',
@sLastName 				nvarchar(50)	= '',
@sAddress1				nvarchar(50)	= '',
@sAddress2				nvarchar(50)	= '',
@sCity					nvarchar(50)	= '',
@sStateCode				nvarchar(5)	= '',
@sZip					nvarchar(20)	= '',
@sZipPlusFour				nvarchar(5)	= '',
@sUserID 				varchar(15) ,
--@dDate				datetime = '',
@iCustomerRemitHistoryInstance		int out

AS

select 1
GO
