
StoreFront database for EZFund.com shopping cart (SF6.mdb)
----------------------------------------------------------



6/16/2006 -- (MLM)

I removed the IX_DESCRIPTION index in the Products table because of a MS Access bug inserting
text >3000 chars into a Memo field that has an index.  This issue is a known bug with the only
workaround being to delete the index (see MS KB article 302525).  Actually, we could trim the 
description text down below 3000 chars and recreate the index again if need be.  You be the judge!

The one known drawback to not having an IX_DESCRIPTION index is that the keyword search on the 
shopping cart will not find any search text in the description field (apparently because it relies
on that index).

If you need to recreate the index, here is the information:

	Index Name:	IX_DESCRIPTION
	Field Name:	Description
	Sort Order:	Ascending
	Primary:	No
	Unique:		No
	Ignore Nulls:	No



// end of document //

