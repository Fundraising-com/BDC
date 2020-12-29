USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[crm_get_sale_consultants]    Script Date: 02/14/2014 13:03:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[crm_get_sale_consultants]
as

SELECT Consultant_ID, Name 
FROM Consultant 
WHERE Is_Active=1 AND
      CSR_Consultant=0 AND
      Consultant.Is_Fm = 0 AND
      Consultant.Department_ID = 7
ORDER BY Consultant.Name
GO
