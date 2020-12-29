USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[mod_sproc_columns]    Script Date: 02/14/2014 13:08:45 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
Created By: 	Paolo De Rosa
Created  On: 	October 31, 2003
Description: 	This function is a modification of Master.dbo.sp_sproc_columns.  It is used to generalize the reporting center.  
		It returns the parameters of a stored procedure.
*/
CREATE PROCEDURE [dbo].[mod_sproc_columns]
	@procedure_name nvarchar(390) = '%'
AS
DECLARE @procedure_qualifier sysname
DECLARE @procedure_owner nvarchar(384)
DECLARE @full_procedure_name nvarchar(774)
DECLARE @procedure_id int
DECLARE @column_name nvarchar(384)
DECLARE @ODBCVer int 
DECLARE @group_num_lower smallint
DECLARE @group_num_upper smallint
DECLARE @semi_position int

SET @full_procedure_name = quotename(@procedure_name)
SET @group_num_lower = 1
SET @group_num_upper = 32767			
SET @procedure_id = object_id(@full_procedure_name)
SET @procedure_qualifier = NULL
SET @procedure_owner = '%'
SET @column_name = '%'
SET @ODBCVer = 2

SELECT
	PROCEDURE_QUALIFIER = convert(sysname, DB_NAME()),
	PROCEDURE_OWNER = convert(sysname, USER_NAME(o.uid)),
	PROCEDURE_NAME = convert(nvarchar(134), o.name),
	COLUMN_NAME = convert(sysname, c.name),
	TYPE_NAME = t.name
FROM
	syscolumns c,
	sysobjects o,
	master.dbo.spt_datatype_info d,
	systypes t
WHERE
	o.name like @procedure_name
	AND user_name(o.uid) like @procedure_owner
	AND o.id = c.id
	AND c.xtype = d.ss_dtype
	AND c.length = isnull(d.fixlen, c.length)
	AND (d.ODBCVer is null or d.ODBCVer = @ODBCVer)
	AND isnull(d.AUTO_INCREMENT,0) = 0
	AND c.xusertype = t.xusertype
	AND c.name like @column_name
	AND (o.type in ('P', 'TF', 'IF') OR (len(c.name) > 0 and o.type = 'FN'))
	AND ((c.number between @group_num_lower and @group_num_upper) OR (c.number = 0 and o.type = 'FN'))
UNION ALL
SELECT		   /* return value row*/
	PROCEDURE_QUALIFIER = convert(sysname, DB_NAME()),
	PROCEDURE_OWNER = convert(sysname, USER_NAME(o.uid)),
	PROCEDURE_NAME = convert(nvarchar(134), o.name),
	COLUMN_NAME = convert(sysname, '@RETURN_VALUE'),
	TYPE_NAME = convert(sysname, 'int')
FROM
	syscomments c, sysobjects o
WHERE
	o.name like @procedure_name
	AND c.id = o.id
	AND user_name(o.uid) like @procedure_owner
	AND c.colid = 1
	AND o.type = 'P'					/* Procedures */
	AND '@RETURN_VALUE' like @column_name
	AND c.number between @group_num_lower and @group_num_upper
UNION ALL
SELECT		/* UDF return value */
	PROCEDURE_QUALIFIER = convert(sysname, DB_NAME()),
	PROCEDURE_OWNER = convert(sysname, USER_NAME(o.uid)),
	PROCEDURE_NAME = convert(nvarchar(134), o.name),
	COLUMN_NAME = convert(sysname, '@RETURN_VALUE'),
	TYPE_NAME = t.name
FROM
	syscolumns c,
	sysobjects o,
	master.dbo.spt_datatype_info d,
	systypes t
WHERE
	o.name like @procedure_name
	AND user_name(o.uid) like @procedure_owner
	AND o.id = c.id
	AND c.xtype = d.ss_dtype
	AND c.length = isnull(d.fixlen, c.length)
	AND (d.ODBCVer is null or d.ODBCVer = @ODBCVer)
	AND isnull(d.AUTO_INCREMENT,0) = 0
	AND c.xusertype = t.xusertype
	AND o.type = 'FN'			/* Scalar UDF */
	AND c.name like @column_name
	AND c.colid = 0
	AND c.number = 0
UNION ALL
SELECT		/* Table valued functions */
	PROCEDURE_QUALIFIER = convert(sysname, DB_NAME()),
	PROCEDURE_OWNER = convert(sysname, USER_NAME(o.uid)),
	PROCEDURE_NAME = convert(nvarchar(134), o.name),
	COLUMN_NAME = convert(sysname, '@TABLE_RETURN_VALUE'),
	TYPE_NAME = convert(sysname, 'table')
FROM
	syscomments c, sysobjects o
WHERE
	o.name like @procedure_name
	AND user_name(o.uid) like @procedure_owner
	AND c.id = o.id
	AND c.colid = 1
	AND o.type IN ('TF', 'IF')
	AND '@TABLE_RETURN_VALUE' like @column_name
	AND c.number = 0

/*
exec sp_sproc_columns 'rpt_overview_campaign'
*/
GO
