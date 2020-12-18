--Run this to add 0's before
update zip_prefix
set zip_prefix = '00' + zip_prefix
where len(zip_prefix) = 1

update zip_prefix
set zip_prefix = '0' + zip_prefix
where len(zip_prefix) = 2
