USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Lead_SelectAll]    Script Date: 06/07/2017 09:20:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select all rows from the table 'Lead'
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Lead_SelectAll]

@FMID varchar(4)

AS

if @FMID = '9999' or coalesce(@FMID, '') = '' OR @FMID = '0503' OR @FMID = '0899'
-- SELECT all rows from the table.
begin
	SELECT
		l.[UserID],
		c.[UserName],
		l.[Date],
		l.[DateSent],
		l.[ContactName],
		l.[ContactHomePhoneNumber],
		l.[ContactWorkPhoneNumber],
		l.[ContactFaxNumber],
		l.[ContactEMail],
		l.[SchoolGroup],
		l.[CityTown],
		l.[Province],
		l.[InterestedInWhat],
		l.[WhereHearAboutQSP],
		l.[FMID],
		l.[Comments],
		l.Instance,
		fm.LastName+ ' ' + fm.FirstName as FieldManagerName
	FROM
		[dbo].[Lead] l
	LEFT OUTER JOIN
		qspcanadacommon..fieldmanager fm
			ON fm.fmid = l.fmid
	LEFT OUTER JOIN
		QSPCanadaCommon..CUserProfile c
			ON c.Instance = l.UserID
	ORDER BY
		l.Instance DESC

end
/*else if @FMID = '0097' -- DIVISION 1
begin
	SELECT
		l.[UserID],
		c.[UserName],
		l.[Date],
		l.[DateSent],
		l.[ContactName],
		l.[ContactHomePhoneNumber],
		l.[ContactWorkPhoneNumber],
		l.[ContactFaxNumber],
		l.[ContactEMail],
		l.[SchoolGroup],
		l.[CityTown],
		l.[Province],
		l.[InterestedInWhat],
		l.[WhereHearAboutQSP],
		l.[FMID],
		l.[Comments],
		l.Instance,
		fm.LastName+ ' ' + fm.FirstName as FieldManagerName
	FROM 
		[dbo].[Lead] l
	LEFT OUTER JOIN
		qspcanadacommon..fieldmanager fm
			ON fm.fmid = l.fmid
	LEFT OUTER JOIN
		QSPCanadaCommon..CUserProfile c
			ON c.Instance = l.UserID
	WHERE
		l.province in('ON', 'BC', 'NB', 'PE', 'NS', 'NL', 'MB', 'AB', 'SK')
	ORDER BY
		l.Instance DESC
end*/
/*else if @FMID = '0503' -- DIVISION 2
begin
	SELECT
		[UserID],
		[Date],
		[DateSent],
		[ContactName],
		[ContactHomePhoneNumber],
		[ContactWorkPhoneNumber],
		[ContactFaxNumber],
		[ContactEMail],
		[SchoolGroup],
		[CityTown],
		[Province],
		[InterestedInWhat],
		[WhereHearAboutQSP],
		lead.[FMID],
		[Comments],
		lead.Instance,
		fm.LastName+ ' ' + fm.FirstName as FieldManagerName
	FROM 
	
		[dbo].[Lead]  left outer join qspcanadacommon..fieldmanager fm on fm.fmid = lead.fmid
	WHERE
		lead.province in('MB', 'AB', 'SK')
end*/
else if @FMID = '0510' -- DIVISION 3
begin
	SELECT
		l.[UserID],
		c.[UserName],
		l.[Date],
		l.[DateSent],
		l.[ContactName],
		l.[ContactHomePhoneNumber],
		l.[ContactWorkPhoneNumber],
		l.[ContactFaxNumber],
		l.[ContactEMail],
		l.[SchoolGroup],
		l.[CityTown],
		l.[Province],
		l.[InterestedInWhat],
		l.[WhereHearAboutQSP],
		l.[FMID],
		l.[Comments],
		l.Instance,
		fm.LastName+ ' ' + fm.FirstName as FieldManagerName
	FROM
		[dbo].[Lead] l
	LEFT OUTER JOIN
		qspcanadacommon..fieldmanager fm
			ON fm.fmid = l.fmid
	LEFT OUTER JOIN
		QSPCanadaCommon..CUserProfile c
			ON c.Instance = l.UserID
	WHERE
		l.province in('QC')
	ORDER BY
		l.Instance DESC
end
else  -- SELECT All FOR SPECIFIC FM
begin
	SELECT
		l.[UserID],
		c.[UserName],
		l.[Date],
		l.[DateSent],
		l.[ContactName],
		l.[ContactHomePhoneNumber],
		l.[ContactWorkPhoneNumber],
		l.[ContactFaxNumber],
		l.[ContactEMail],
		l.[SchoolGroup],
		l.[CityTown],
		l.[Province],
		l.[InterestedInWhat],
		l.[WhereHearAboutQSP],
		l.[FMID],
		l.[Comments],
		l.Instance,
		fm.LastName+ ' ' + fm.FirstName as FieldManagerName
	FROM 
		[dbo].[Lead] l
	LEFT OUTER JOIN
		qspcanadacommon..fieldmanager fm
			ON fm.fmid = l.fmid
	LEFT OUTER JOIN
		QSPCanadaCommon..CUserProfile c
			ON c.Instance = l.UserID
	WHERE
		fm.fmid = @FMID
	ORDER BY
		l.Instance DESC
end
GO
