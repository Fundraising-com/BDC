USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateAuditing]    Script Date: 02/14/2014 13:08:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_CreateAuditing] @TableOwner VARCHAR(100), @TableName VARCHAR(100)
AS
BEGIN
    DECLARE @ColumnName               VARCHAR(50),
            @ColumnType               VARCHAR(50),
            @ColumnSize               VARCHAR(50),
            @ColumnList               VARCHAR(8000),
            @SQLText                  VARCHAR(8000);
    
    
    -- CREATE TABLE
    
    PRINT 'CREATE TABLE ' + @TableName + '_Audit'
    PRINT '(AUDIT_ID INT IDENTITY(1,1) NOT NULL,'
    PRINT 'AUDIT_OPERATION CHAR(2) NOT NULL,'
    PRINT 'AUDIT_USERID VARCHAR(50) NOT NULL,'
    PRINT 'AUDIT_DATETIME DATETIME NOT NULL,'

    DECLARE c1 CURSOR FOR
    SELECT c.name,
           UPPER(ty.name),
           CASE WHEN ty.name IN ('varbinary', 'char', 'varchar') THEN CASE WHEN c.max_length = -1 THEN '(MAX)' ELSE '(' + CAST(c.max_length AS VARCHAR(20)) + ')' END
                WHEN ty.name IN ('nchar', 'nvarchar') THEN CASE WHEN c.max_length = -1 THEN '(MAX)' ELSE '(' + CAST(c.max_length / 2 AS VARCHAR(20)) + ')' END
                WHEN ty.name IN ('decimal', 'numeric') THEN '(' + CAST(c.precision AS VARCHAR(20)) + ',' + CAST(c.scale AS VARCHAR(20)) + ')'
                ELSE '' END AS size            
      FROM sys.schemas s
           JOIN sys.tables ta ON ta.[schema_id] = s.[schema_id]
           JOIN sys.columns c ON c.[object_id] = ta.[object_id]
           JOIN sys.types ty ON c.user_type_id = ty.user_type_id
     WHERE LOWER(s.name) = LOWER(@TableOwner)
       AND LOWER(ta.name) = LOWER(@TableName)
     ORDER BY c.column_id;


    SET @ColumnList = 'AUDIT_OPERATION, AUDIT_USERID, AUDIT_DATETIME, ';
    OPEN c1;
    FETCH NEXT FROM c1 INTO @ColumnName, @ColumnType, @ColumnSize
    WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @ColumnList = @ColumnList + @ColumnName + ', '
        PRINT @ColumnName + ' ' + @ColumnType + @ColumnSize + ' NULL,'
        FETCH NEXT FROM c1 INTO @ColumnName, @ColumnType, @ColumnSize
    END;
    CLOSE c1;
    DEALLOCATE c1;

    SET @ColumnList = LEFT(@ColumnList, LEN(@ColumnList) - 1); -- To remove trailing comma

    PRINT 'CONSTRAINT PK_' + @TableName + '_AUDIT PRIMARY KEY CLUSTERED (AUDIT_ID))'
    PRINT '';
    PRINT 'GO';
    PRINT '';

    
    -- CREATE TRIGGER
    
    PRINT 'CREATE TRIGGER tr_' + @TableName + '_Audit ON ' + @TableOwner + '.' + @TableName;
    PRINT 'AFTER INSERT, UPDATE, DELETE';
    PRINT 'AS';
    PRINT 'BEGIN';
    
    PRINT '    IF (SELECT TOP 1 1 FROM deleted) IS NOT NULL AND (SELECT TOP 1 1 FROM inserted) IS NULL';
    PRINT '    BEGIN';
    PRINT '        INSERT INTO ' + @TableName +  '_Audit (' + @ColumnList + ') ';
    PRINT '        SELECT ''D'', USER_NAME(), GETDATE(), *';
    PRINT '          FROM deleted;';
    PRINT '    END;';
    PRINT '';

    PRINT '    IF (SELECT TOP 1 1 FROM deleted) IS NULL AND (SELECT TOP 1 1 FROM inserted) IS NOT NULL';
    PRINT '    BEGIN';
    PRINT '        INSERT INTO ' + @TableName +  '_Audit (' + @ColumnList + ') ';
    PRINT '        SELECT ''I'', USER_NAME(), GETDATE(), *';
    PRINT '          FROM inserted;';
    PRINT '    END;';
    PRINT '';

    PRINT '    IF (SELECT TOP 1 1 FROM deleted) IS NOT NULL AND (SELECT TOP 1 1 FROM inserted) IS NOT NULL';
    PRINT '    BEGIN';
    PRINT '        INSERT INTO ' + @TableName +  '_Audit (' + @ColumnList + ') ';
    PRINT '        SELECT ''UD'', USER_NAME(), GETDATE(), *';
    PRINT '          FROM deleted;';
    PRINT '';
    PRINT '        INSERT INTO ' + @TableName +  '_Audit (' + @ColumnList + ') ';
    PRINT '        SELECT ''UI'', USER_NAME(), GETDATE(), *';
    PRINT '          FROM inserted;';
    PRINT '    END;';
    PRINT 'END;';
    PRINT '';
    PRINT 'GO';
    PRINT '';

END;
GO
