select	c.billtoaccountid AS AccountNum, a.Name AS AccountName, c.isstafforder IsFacultyProgram, count(*) AS NumNonStaffCampaignsWithSalesFiscal2012
from	campaign c,
		CAccount a
where	a.ID = c.BillToAccountID
and		c.startdate >= '2011-07-01'
and		c.enddate <= '2012-06-30'
and		c.status = 37002
--and		c.id in (select campaignid from qspcanadaordermanagement..customerorderheader)
and		c.id in (select campaignid from campaignProgram where ProgramID in (1,2))
group by c.billtoaccountid, a.Name, c.isstafforder
having count(*) > 1
order by c.billtoaccountid