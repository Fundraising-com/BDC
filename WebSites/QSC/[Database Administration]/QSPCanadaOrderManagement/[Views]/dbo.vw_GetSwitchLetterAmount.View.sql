USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_GetSwitchLetterAmount]    Script Date: 06/07/2017 09:18:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[vw_GetSwitchLetterAmount] AS


SELECT	--CASE ca.IsStaffOrder WHEN 0 THEN cod.Price ELSE cod.Price * ca.StaffOrderDiscount / 100.00 END AS Price, MS April 30, 2007
		convert(numeric(10,2), CASE ca.IsStaffOrder WHEN 0 THEN cod.Price ELSE cod.Price * ((100 -  Isnull(ca.StaffOrderDiscount,0)) / 100.00) END) AS Price, 
		cod.CustomerOrderHeaderInstance,
		cod.TransID

FROM		CustomerOrderHeader coh,
		CustomerOrderDetail cod,
		QSPCanadaCommon..Campaign ca

WHERE	coh.Instance = cod.CustomerOrderHeaderInstance
AND		ca.ID = coh.CampaignID
GO
