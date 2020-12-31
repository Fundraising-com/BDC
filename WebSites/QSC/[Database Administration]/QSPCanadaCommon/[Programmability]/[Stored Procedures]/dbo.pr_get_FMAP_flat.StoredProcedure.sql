USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_FMAP_flat]    Script Date: 06/07/2017 09:33:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_get_FMAP_flat]
 @fmid varchar(4)
AS

SET NOCOUNT ON
create table #FMaccts
(
  AccountID int NOT NULL,
  P1 bit null,
  P2 bit null,
  P3 bit null
)

    insert into #FMaccts
select distinct AccountID, 0, 0, 0
           from dbo.FieldManagerAccountProduct 
          where FMID = @fmid ;

update #FMaccts
   set [P1] = cast(1 as bit)
 where AccountID in (
		SELECT DISTINCT AccountID 
		  FROM dbo.FieldManagerAccountProduct 
		 WHERE FMID = @fmid AND MajorProductLineID = 1 
		);


update #FMaccts
   set [P2] = cast(1 as bit)
 where AccountID in (
		SELECT DISTINCT AccountID 
		  FROM dbo.FieldManagerAccountProduct 
		 WHERE FMID = @fmid AND MajorProductLineID = 2
		);


update #FMaccts
   set [P3] = cast(1 as bit)
 where AccountID in (
		SELECT DISTINCT AccountID 
		  FROM dbo.FieldManagerAccountProduct 
		 WHERE FMID = @fmid AND MajorProductLineID = 3
		);


/*
--check the results
--the count should be equal to
--select count(*) from dbo.FieldManagerAccountProduct WHERE FMID = @fmid

DECLARE @p1 bit, @p2 bit , @p3 bit, @c int
select @c = 0;

DECLARE Pcursor CURSOR FOR
 SELECT P1, P2, P3 
   FROM #FMaccts
 
open Pcursor
fetch next from Pcursor into @p1, @p2, @p3

while(@@fetch_status <> -1)
begin
	select @c = 
		@c 
		+ cast(@p1 as int)
		+ cast(@p2 as int)
		+ cast(@p3 as int);

	fetch next from Pcursor into @p1, @p2, @p3;
end
close Pcursor
deallocate Pcursor
print 'There are ' + cast(@c as varchar) + ' FM-Account-Product pairs for this FM';
*/

SET NOCOUNT OFF
SELECT * FROM #FMaccts;
drop table #FMaccts;
GO
