USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_GenerateResolveFMList]    Script Date: 06/07/2017 09:33:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_GenerateResolveFMList]
as
SELECT FMID,lastname,firstname,
	C.Street1,
	isnull(C.Street2,''),
	C.City,
	C.StateProvince,
	C.Postal_Code,
	email
	FROM
		QSPCanadaCommon..FieldManager A
		INNER JOIN QSPCanadaCommon..AddressList B ON A.AddressListId = B.Id
		INNER JOIN QSPCanadaCommon..Address C ON C.AddressListId = B.Id
GO
