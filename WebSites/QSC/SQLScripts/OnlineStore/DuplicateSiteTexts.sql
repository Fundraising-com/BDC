USE GA
GO

/*
select *
from store.site

select *
from store.Text
where EntityID in (100001,306455,306456,306457) 
order by entityid, texttypecode
*/

DECLARE @FromSiteID		INT,
		@ToSiteID		INT,
		@FromEntityID	INT,
		@ToEntityID		INT

SET		@FromSiteID = 101
SET		@ToSiteID = 114

select	@FromEntityID = EntityID
from	store.Site
where	SiteID = @FromSiteID

select	@ToEntityID = EntityID
from	store.Site
where	SiteID = @ToSiteID

BEGIN TRAN

insert into Store.Text
select		@ToEntityID, fromText.TextTypeCode, fromText.LanguageCode, fromText.Content, fromText.IsSuppressed, Getdate(), NULL, NULL
from		Store.Text fromText
left join	Store.Text toText ON toText.TextTypeCode = fromText.TextTypeCode AND toText.EntityID = @ToEntityID AND toText.LanguageCode = fromText.LanguageCode
where		fromText.EntityID = @FromEntityID
and			toText.TextID IS NULL

COMMIT TRAN
