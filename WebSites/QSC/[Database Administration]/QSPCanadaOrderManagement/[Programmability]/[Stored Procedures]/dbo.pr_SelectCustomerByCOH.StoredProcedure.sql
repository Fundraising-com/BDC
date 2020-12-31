USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectCustomerByCOH]    Script Date: 06/07/2017 09:20:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_SelectCustomerByCOH]

	@iCustomerOrderHeaderInstance int
AS

SELECT	c.Instance, c.StatusInstance, c.LastName, c.FirstName, c.Address1, c.Address2, c.City, c.County, c.State, c.Zip, c.ZipPlusFour, c.OverrideAddress, c.ChangeUserID,
		c.ChangeDate, c.Email, c.Phone, ISNULL(c.Type, 50601) [Type]

FROM		QSPCanadaOrderManagement..Customer c,
		QSPCanadaOrderManagement..CustomerOrderHeader coh

WHERE	c.Instance = coh.CustomerBillToInstance
AND		coh.Instance = @iCustomerOrderHeaderInstance
GO
