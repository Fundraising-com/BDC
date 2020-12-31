USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectAllPublisherContacts]    Script Date: 06/07/2017 09:18:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectAllPublisherContacts]

	@iPub_Nbr	int = 0

AS

declare @sqlStatement nvarchar(4000)

set @sqlStatement = 'SELECT	DISTINCT
		pc.PContact_Instance,
		pc.Pub_Nbr,
		pub.Pub_Name,
		pc.PContact_FName,
		pc.PContact_LName,
		coalesce(pc.PContact_Title, '''') AS PContact_Title,
		coalesce(pc.PContact_Email, '''') AS PContact_Email,
		pc.PhoneListID,
		p.PhoneNumber,
		f.PhoneNumber AS FaxNumber,
		CASE coalesce(pcp.Product_Code, '''') WHEN '''' THEN ''Y'' ELSE ''N'' END AS IsMainContact

FROM		Publisher_Contacts pc
LEFT JOIN	Phone p
			ON	pc.PhoneListID = p.PhoneListID
			AND	p.Type = 1
LEFT JOIN	Phone f
			ON	pc.PhoneListID = f.PhoneListID
			AND	f.Type = 3
LEFT JOIN	PublisherContact_Product pcp
			ON	pcp.PublisherContactID = pc.PContact_Instance,
		Publishers pub

WHERE	pub.Pub_Nbr = pc.Pub_Nbr'

if(@iPub_Nbr <> 0)
BEGIN
	set @sqlStatement = @sqlStatement + ' AND pc.Pub_Nbr = ' + convert(varchar, @iPub_Nbr)
END


exec(@sqlStatement)
GO
