USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ReissueCreditCard]    Script Date: 06/07/2017 09:20:22 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_ReissueCreditCard]

@iCustomerOrderHeaderInstance int = 0

AS

SELECT 'OK'
GO
