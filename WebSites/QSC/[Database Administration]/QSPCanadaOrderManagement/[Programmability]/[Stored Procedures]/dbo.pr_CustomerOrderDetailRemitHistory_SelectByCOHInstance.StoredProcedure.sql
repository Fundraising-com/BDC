USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_CustomerOrderDetailRemitHistory_SelectByCOHInstance]    Script Date: 06/07/2017 09:19:51 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_CustomerOrderDetailRemitHistory_SelectByCOHInstance]
	@iCustomerOrderHeaderInstance int,
	@iTransID int
	
	
AS
SET NOCOUNT ON
-- SELECT an existing row from the table.

select distinct * from vw_GetAllStatusByCODRH
where CustomerOrderHeaderInstance=@iCustomerOrderHeaderInstance and
TransID = @iTransID

order by DateChanged desc
GO
