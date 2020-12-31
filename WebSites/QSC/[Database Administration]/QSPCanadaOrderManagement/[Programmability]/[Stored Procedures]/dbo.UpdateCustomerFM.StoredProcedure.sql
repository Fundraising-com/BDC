USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[UpdateCustomerFM]    Script Date: 06/07/2017 09:20:57 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[UpdateCustomerFM]  
	@lnstance int,
	@fmid varchar(4)
as

	declare @count int
	select @count from CustomerFM where customerinstance=@lnstance and FMID=@fmid
	
	if(@count = 0)
	begin
		insert CustomerFM(CustomerInstance, FMID) values(@lnstance, @fmid)

	end
	select * from Customer where instance= @lnstance
GO
