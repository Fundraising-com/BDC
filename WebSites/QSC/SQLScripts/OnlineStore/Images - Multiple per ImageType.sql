select *
from imagetype
where description like 'Motivate%'

declare @entityid int
set @entityid = 306455

declare @imagetypecode int
set @imagetypecode = 55 --56 --62 --63

select *
from image
where imagetypecode = @imagetypecode
and entityid = @entityid

begin tran
insert [image]
select entityid, imagetypecode, 2, actualwidth, actualheight, filename, issuppressed, isdefault, internalpath, GETDATE(), null, null
from image
where imagetypecode = @imagetypecode
and languagecode = 1
and entityid = @entityid