USE [fastfundraising]
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertRequest]    Script Date: 02/14/2014 13:06:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  Stored Procedure dbo.sp_InsertRequest    Script Date: 10/19/00 3:17:52 PM ******/
CREATE PROCEDURE [dbo].[sp_InsertRequest] 
@orgname varchar(100),
@contactname varchar(100),
@address varchar(100),
@city varchar(40),
@state varchar(2),
@zip varchar(20),
@phone varchar(30),
@emailaddr varchar(255),
@groupType varchar(30),
@numberofsellers varchar(10),
@desiredprofit varchar(10),
@desiredprofit2 varchar(10),
@notes varchar(8000),
@requestString varchar(8000),
@bestTimeToCall varchar(100),
@cookieID int
AS
if exists (select 1 from tbl_sampleRequest where cookieid = @cookieid and processedDateTime is NULL)
	update  tbl_SampleRequest set
	orgname = @orgname,
	contactname = @contactname,
	address = @address,
	city = @city,
	state = @state,
	zip = @zip,
	phone = @phone,
	emailaddr = @emailaddr,
	grouptype = @grouptype,
	numberofsellers = @numberofsellers,
	desiredprofit=@desiredprofit,
	desiredprofit2 = @desiredprofit2,
	notes = @notes,
	requeststring = @requeststring,
	besttimetocall = @besttimetocall
		where cookieID = @cookieID and processedDateTime is NULL
else
	insert into tbl_SampleRequest (
		orgname,
		contactname,
		address,
		city,
		state,
		zip,
		phone,
		emailaddr,
		grouptype,
		numberofsellers,
		desiredprofit,
		desiredprofit2,
		notes,
		requeststring,
		besttimetocall,
		cookieID,
		requestdatetime
	) values (
		@orgname,
		@contactname,
		@address,
		@city,
		@state,
		@zip,
		@phone,
		@emailaddr,
		@grouptype,
		@numberofsellers,
		@desiredprofit,
		@desiredprofit2,
		@notes,
		@requeststring,
		@besttimetocall,
		@cookieID,
		getdate())
GO
