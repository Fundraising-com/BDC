This is by no means perfect, but it seems to work in dev for the two sites I tried. It takes some editing but I will try to list out the steps.

1.	Set the New Entity ID (parameter value in declarations) for the site you are copying too (from the site table).
2.	Set the existing entity ID you are copying from (also in declarations)
3.	Change the server if you are not running this in DEV (from GAFISD01)
4.	If you are copying to french, make sure you update this in the DOS command being printed: + '\EN\' + => ‘\FR\’
5.	Run the script but make sure you copy the output before committing or you will lose it. After copying, do a find and replace and remove the (1 row(s) affected) from the script. Save this output as a .BAT file
6.	Commit the DB changes
7.	Run the .BAT file

Drew

begin tran

DECLARE @EntityID INT = 100001
DECLARE @ImageID INT
DECLARE @NewEntityID INT = 103707
DECLARE @NewImageID INT
DECLARE @ImageTypeCode INT
DECLARE @LanguageCode INT
DECLARE @ActualWidth INT
DECLARE @ActualHeight INT
DECLARE @FileName VARCHAR(50)
DECLARE @IsSuppressed INT
DECLARE @IsDefault INT
DECLARE @InternalPath VARCHAR(50)
DECLARE @Created DATETIME
DECLARE @Modified DATETIME
DECLARE @Version TIMESTAMP

DECLARE image_types CURSOR 
     FOR 
select *
from Store.Image t
where t.EntityID = @EntityID

OPEN image_types
FETCH NEXT FROM image_types 
INTO @ImageID, @EntityID, @ImageTypeCode, @LanguageCode, @ActualWidth, @ActualHeight, @FileName, @IsSuppressed, @IsDefault, @InternalPath, @Created, @Modified, @Version

WHILE @@FETCH_STATUS = 0
BEGIN

insert into Store.Image
        ( EntityID ,
          ImageTypeCode ,
          LanguageCode ,
          ActualWidth ,
          ActualHeight ,
          Filename ,
          IsSuppressed ,
          IsDefault ,
          InternalPath ,
          Created ,
          Modified ,
          Version
        )
VALUES  ( @NewEntityID , -- EntityID - int
          @ImageTypeCode , -- ImageTypeCode - smallint
          @LanguageCode , -- LanguageCode - smallint
          @ActualWidth , -- ActualWidth - smallint
          @ActualHeight , -- ActualHeight - smallint
          @FileName , -- Filename - varchar(255)
          @IsSuppressed , -- IsSuppressed - bit
          @IsDefault, -- IsDefault - bit
          @InternalPath , -- InternalPath - varchar(200)
          '2014-02-26 14:13:52' , -- Created - datetime2
          '2014-02-26 14:13:52' , -- Modified - datetime2
          NULL  -- Version - timestamp
        )
SELECT @NewImageID = SCOPE_IDENTITY()

PRINT 'COPY \\GAFISD01\EntityImages\' + CAST(@EntityID as VARCHAR) + '\EN\' + CAST(@ImageID as VARCHAR(5))  + '  \\GAFISD01\EntityImages\' + CAST(@NewEntityID as VARCHAR) + '\EN\' + CAST(@NewImageID as VARCHAR(5))

FETCH NEXT FROM image_types 
INTO @ImageID, @EntityID, @ImageTypeCode, @LanguageCode, @ActualWidth, @ActualHeight, @FileName, @IsSuppressed, @IsDefault, @InternalPath, @Created, @Modified, @Version

END
CLOSE image_types
DEALLOCATE image_types

--ROLLBACK

--COMMIT
