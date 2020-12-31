USE [QSPCanadaCommon]
GO
/****** Object:  View [dbo].[viw_Schema_Procs]    Script Date: 06/07/2017 09:32:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE view [dbo].[viw_Schema_Procs] AS 
select 
	PARAMETER_NAME, 
	case lower(DATA_TYPE)
		when 'varchar' then 'SqlDbType.VarChar'
		when 'int' then 'SqlDbType.Int'
		when 'char' then 'SqlDbType.Char'
		when 'datetime' then 'SqlDbType.DateTime'
		else 'SqlDbType.UNKNOWN' + DATA_TYPE
	end AS SqlDbType,
	case 
		when CHARACTER_MAXIMUM_LENGTH is null then	
		CASE 
			WHEN DATA_TYPE = 'int' THEN 4
			else null
		end 
		else CHARACTER_MAXIMUM_LENGTH 
	end AS FieldSize,
	CASE PARAMETER_MODE
		WHEN 'IN'    THEN 'ParameterDirection.Input'
		WHEN 'OUT'   THEN 'ParameterDirection.Output'
		WHEN 'INOUT' THEN 'ParameterDirection.Output'
		ELSE PARAMETER_MODE
	END AS ParameterDirection,
	case lower(DATA_TYPE)
		when 'varchar' then 'string '
		when 'int' then 'int '
		when 'char' then 'string'
		when 'datetime' then 'DateTime'
		else 'System.UNKNOWN' + DATA_TYPE
	end AS SystemType,
	case SUBSTRING ( PARAMETER_NAME , 1 , 1 ) 
		when '@' then SUBSTRING ( PARAMETER_NAME , 2, len(PARAMETER_NAME)-1 )
		else PARAMETER_NAME
	end AS ParamName,
	case 
		when CHARACTER_MAXIMUM_LENGTH is null then	
		CASE 
			WHEN 	NUMERIC_SCALE is null 
				or NUMERIC_SCALE = 0 
				or DATETIME_PRECISION is not null 
			then	DATA_TYPE 
			else	DATA_TYPE + '(' + cast(NUMERIC_PRECISION as varchar(20)) 
				+ ',' + cast(NUMERIC_SCALE as varchar(20)) + ')' 
		end 
		else DATA_TYPE + '(' +cast(CHARACTER_MAXIMUM_LENGTH as varchar(4)) + ')' 
	end AS FullParam,
	SPECIFIC_CATALOG,
	SPECIFIC_SCHEMA,
	SPECIFIC_NAME
  FROM 
	QSPCanadaProduct.INFORMATION_SCHEMA.PARAMETERS
GO
