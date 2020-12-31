USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_DateFormat]    Script Date: 06/07/2017 09:33:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_DateFormat]
  @fmid varchar(4)
AS

DECLARE	@DateFormat int
SELECT	@DateFormat =	CASE DefaultLang
					WHEN 'en-US' THEN 101
					WHEN 'en'       THEN 101
					WHEN 'en-CA' THEN 103
					WHEN 'fr-CA'   THEN 103
					WHEN 'fr'         THEN 103
					ELSE 1
				END
FROM		CUserProfile
WHERE	FMNumber = @fmid
IF @@rowcount <> 1
begin
	SELECT @DateFormat = 101
end

return @DateFormat
GO
