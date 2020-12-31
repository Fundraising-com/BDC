select SUM(iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00)) NetAmount, SUM(iSec.Total_Tax_Excluded - ISNULL(iSec.US_Postage_Amount, 0.00)) AdjustedGross, SUM(iSec.ITEM_COUNT) NumUnits
from INVOICE_SECTION iSec
join invoice i on i.invoice_id = iSec.invoice_id
where iSec.section_type_id = 2
and i.invoice_date between '2014-07-01' and '2014-12-31'