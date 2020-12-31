USE GA
GO

/*
select *
from store.SiteGoalType sgt
LEFT JOIN Store.Entity e on sgt.EntityID = e.EntityID
LEFT JOIN Store.Text t on t.EntityID = e.EntityID
order by sgt.siteid
*/

DECLARE @FromEntityID	INT,
		@ToEntityID		INT,
		@GoalTypeCode	INT

set		@FromEntityID = 103431
set		@ToEntityID = 306681

BEGIN TRAN

insert into Store.Text
select		@ToEntityID, fromText.TextTypeCode, fromText.LanguageCode, fromText.Content, fromText.IsSuppressed, Getdate(), NULL, NULL
from		Store.Text fromText
left join	Store.Text toText ON toText.TextTypeCode = fromText.TextTypeCode AND toText.EntityID = @ToEntityID AND toText.LanguageCode = fromText.LanguageCode
where		fromText.EntityID = @FromEntityID
and			toText.TextID IS NULL

update store.Text
set Content = '<p>  You have reached your goal of ${Goal}!</p> <p>  &nbsp;</p> <p>  You have sold ${Sales}.</p> <p>  &nbsp;</p> <p>  <strong>Great Job!</strong></p> <p>  &nbsp;</p> '
where TextID = 704133

update store.Text
set Content = '<p>  You have reached your goal of {Goal} items!</p> <p>  &nbsp;</p> <p>  You have sold {Quantity} items.</p> <p>  &nbsp;</p> <p>  <strong>Great Job!</strong></p> <p>  &nbsp;</p> '
where TextID = 704148

COMMIT TRAN

--French duplication

begin tran
select *
into #tt
from Store.Text t
where t.EntityID = 104709

insert into Store.Text
select EntityID, TextTypeCode, 2, 'FR - ' + Content, IsSuppressed, GETDATE(), NULL, NULL
from #tt
order by texttypecode
