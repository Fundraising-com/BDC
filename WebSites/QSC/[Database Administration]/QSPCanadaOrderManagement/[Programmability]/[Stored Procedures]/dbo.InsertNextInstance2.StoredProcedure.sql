USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[InsertNextInstance2]    Script Date: 06/07/2017 09:19:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[InsertNextInstance2]
	@TableID int
AS



Declare @NextInstance int
Declare @TableName varchar(50)
Declare @execStr varchar(1024)

Select @TableName = Name from NextTableID where ID = @TableID

declare @guid varchar(100) 
set @guid = newid()
Select @execStr = 'Insert into ' + @TableName + ' (guid) VALUES('''  + @guid + ''')'
exec (@execStr)
Select @execStr = 'Select id  from ' +  @TableName  + ' where guid = ''' + @guid +''''
exec (@execStr)
 Select @execStr = 'delete from ' +  @TableName  + ' where guid = '''+@guid + ''''
exec (@execStr)
GO
