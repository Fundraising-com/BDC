USE [fastfundraising]
GO
/****** Object:  StoredProcedure [dbo].[efr_store_insert_cookie_info]    Script Date: 02/14/2014 13:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[efr_store_insert_cookie_info]
	-- Add the parameters for the stored procedure here
@cookieid int,
@ipaddr varchar(50),
@itemid int,
@itemname varchar(50),
@quantity int,
@price float,
@totalcost float,
@extfmid int,
@promotion int


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
	insert into ffcart(cookieid,ipaddr,itemid,itemname,qty,itempriceapplied,totalcost,addtocartdatetime,extfmid,promotionid) values(@cookieid,@ipaddr,@itemid,@itemname,@quantity, @price, @totalcost, getdate(),@extfmid,@promotion )

END
GO
