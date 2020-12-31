--Update RemitCode for Mag, ProductCode for Non-Mag, in FFS. Then fix in Focus:

select top 99 *
from core.item i
join core.BrochureOffer bo on bo.ItemID = i.ItemID
join core.Brochure b on b.BrochureID = bo.BrochureID
where b.BrochureID in (
1625,
1626,
1627)

begin tran
update i
set SAPID = 'QSPCAE6523',
	UPCCode = 'E6523'
from core.Item i
where i.ItemID = 17370
--commit tran
