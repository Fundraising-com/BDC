USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_Product_Unremittable_SelectAll]    Script Date: 06/07/2017 09:17:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Product_Unremittable_SelectAll]

AS

SELECT		p.Product_Code,
			p.Product_Sort_Name AS Product_Title,
			(SELECT COUNT(*) FROM QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory codrhSub
				WHERE	codrhSub.TitleCode = p.Product_Code
				AND		codrhSub.Status = 42000)  AS Num_Subs_Pending, --Needs to be sent
			(SELECT COUNT(*) FROM QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory codrhSub
				WHERE	codrhSub.TitleCode = p.Product_Code
				AND		codrhSub.Status = 42002) AS Num_Cancelations_Pending, --Needs to be cancelled
			(SELECT COUNT(*) FROM QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory codrhSub
				WHERE	codrhSub.TitleCode = p.Product_Code
				AND		codrhSub.Status = 42006) AS Num_Chadds_Pending, --CHADD needs to be sent
			cup.FirstName + ' ' + cup.LastName AS LastUpdatedBy,
			p.CommentDate AS LastUpdatedDate
FROM		Product p
JOIN		QSPCanadaCommon.dbo.Season s
				ON	s.Season = p.Product_Season
				AND	s.FiscalYear = p.Product_Year
				AND	GetDate() BETWEEN s.StartDate AND s.EndDate
LEFT JOIN	QSPCanadaOrderManagement.dbo.CustomerOrderDetailRemitHistory codrhSub
				ON	codrhSub.TitleCode = p.Product_Code
				AND	codrhSub.Status = 42000 --Needs to be sent
LEFT JOIN	QSPCanadaOrderManagement.dbo.CustomerOrderDetailRemitHistory codrhCanc
				ON	codrhCanc.TitleCode = p.Product_Code
				AND	codrhCanc.Status = 42002 --Needs to be cancelled
LEFT JOIN	QSPCanadaOrderManagement.dbo.CustomerOrderDetailRemitHistory codrhChadd
				ON	codrhChadd.TitleCode = p.Product_Code
				AND	codrhChadd.Status = 42006 --CHADD needs to be sent
LEFT JOIN	QSPCanadaCommon..CUserProfile cup
				ON	cup.Instance = p.Logged_By
WHERE		p.Status = 30603 --Unremittable
GROUP BY	p.Product_Code,
			p.Product_Sort_Name,
			cup.FirstName + ' ' + cup.LastName,
			p.CommentDate
GO
