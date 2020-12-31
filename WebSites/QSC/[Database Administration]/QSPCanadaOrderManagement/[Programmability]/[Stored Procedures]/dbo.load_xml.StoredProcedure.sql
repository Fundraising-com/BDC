USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[load_xml]    Script Date: 06/07/2017 09:19:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[load_xml] (
							@serverName 		VARCHAR(255),
							@packageName 	VARCHAR(255),
							@serverSecurity 	BIT 		= 0,	 -- 0 = SQL Server Security, 1 = Windows Authentication Security
							@serverPassword 	VARCHAR(255) 	= NULL, -- Server Password if using SQL Security to load Package (UID is SUSER_NAME())
							@packagePassword 	VARCHAR(255) 	= '',	 -- Package Password
							@loadMethod		CHAR(1)	= 'S',	-- S = load xml as string, D - load xml from file, U - load xml file through URL
							@msgText 		VARCHAR(8000),
							@fileName		VARCHAR(255) 	= '' -- if loadMethod = 'D' or loadMethod = 'U'
						     ) AS

BEGIN
/*************************************************************************************************************************************************************************
** dbo.usp_validate_xml
**
** Description: This Procedure sets the parameter to the DTS package and gets the return value from the package
**
** Author : Vaiyapuri Subramanian
**
** Date   : 01/17/2003
**
** Usage: DECLARE @status	INT
**              EXEC @status = usp_validate_xml 
**              			@serverName = <Server Name>,
**              			@packageName = 'XMLValidator',
**              			@serverSecurity = 1, -- 1 for Windows NT Authentication, 0 for SQL Server Authentication
**              			@serverPassword='',  -- Required if @serverSecurity is 0
**              			@packagePassword='dummy', -- May or may not be there
**              			@loadMethod = 'U', -- 'S' for String, 'D' for flat file, 'U' for URL
**              			@msgText='hi', -- required only if @loadMethod = 'S'
**              			@fileName = 'http://www.w3schools.com/dom/note.xml' -- required only if @loadMethod = 'D' or @loadMethod = 'U'
**	  PRINT 'XML Status = ' + STR(@status)
**
** Dependencies: None
**
** Logic:
**
** change log:
**
**
**  date                                          who                                                                                           change description
**
**                      
*************************************************************************************************************************************************************************/
	SET NOCOUNT ON

	DECLARE
			@oleStatus 		INT,
			@oPackage		INT,
			@Cmd 			VARCHAR(1000),
			@oReturnValue		INT,
			@globalFileName	VARCHAR(200),
			@globalXmlStringName	VARCHAR(50),
			@globalXmlStatusString	VARCHAR(50),
			@file varchar(200)

	SET @globalFileName = 'GlobalVariables("o_fileName").Value'
	SET @globalXmlStringName = 'GlobalVariables("o_XmlString").Value'
	SET @globalXmlStatusString = 'GlobalVariables("o_XmlStatus").Value'
print @fileName
	-- Create a Package Object
	EXEC @oleStatus = sp_OACreate 'DTS.Package', @oPackage OUTPUT
	IF @oleStatus <> 0
	BEGIN
		PRINT 'Creation Of Package Object Failed'
		RETURN -1
	END

	-- Evaluate Security and Build LoadFromSQLServer Statement
	IF @serverSecurity = 0
		BEGIN
			IF(LEN(ISNULL(@serverPassword, '')) = 0)
				BEGIN
					PRINT 'Please specify Server Password'
					RETURN -1
				END
			ELSE
				SET @Cmd = 'LoadFromSQLServer("' + @serverName +'", "' + SUSER_SNAME() + '", "' + @serverPassword + '", 0, "' + @packagePassword + '", , , "' + @packageName + '")'
		END
	ELSE
		BEGIN
			SET @Cmd = 'LoadFromSQLServer("' + @serverName +'", "", "", 256, "' + @packagePassword + '", , , "' + @packageName + '")'

			EXEC @oleStatus = sp_OAMethod @oPackage, @Cmd, NULL

			IF @oleStatus <> 0
				BEGIN
					    PRINT 'Loading of DTS Package ' + @packageName +' From ' + @serverName + ' Failed'
					    RETURN -1
				END
		END
print @Cmd
	-- If loadMethod is 1 the write the XML to file, else assign it to the DTS package global variable
	IF(@loadMethod = 'D' OR @loadMethod = 'U')
		BEGIN
			IF(LEN(ISNULL(@fileName,'')) = 0)
				BEGIN
					PRINT 'Please specify filename'
					RETURN -1
				END
			ELSE
				BEGIN
					EXEC @oleStatus = sp_OASetProperty @oPackage, @globalFileName, @fileName

					IF @oleStatus <> 0
					BEGIN
					    PRINT 'Assigning value ' + @fileName + ' To Variable o_fileName Failed'
					    RETURN -1
					END

					EXEC @oleStatus = sp_OASetProperty @oPackage, @globalXmlStringName, 'N/A'

					IF @oleStatus <> 0
					BEGIN
					    PRINT 'Initializing value '''' To Variable o_XmlString Failed'
					    RETURN -1
					END
print 'variable assigned'
				END
		END
	ELSE
		BEGIN
			IF (LEN(ISNULL(@msgText, '')) = 0)
				BEGIN
					PRINT 'Please specify the XML String'
					RETURN -1
				END
			ELSE
				BEGIN

					EXEC @oleStatus = sp_OASetProperty @oPackage, @globalXmlStringName, @msgText

					IF @oleStatus <> 0
					BEGIN
					    PRINT 'Assigning value ' + @msgText + ' To Variable o_XmlString Failed'
					    RETURN -1
					END

					EXEC @oleStatus = sp_OASetProperty @oPackage, @globalFileName, 'N/A'

					IF @oleStatus <> 0
					BEGIN
					    PRINT 'Assigning value ' + @fileName + ' To Variable o_fileName Failed'
					    RETURN -1
					END

				END
		END

	-- Execute Package
	EXEC @oleStatus = sp_OAMethod @oPackage, 'Execute'
	IF @oleStatus <> 0
	BEGIN
	    PRINT 'Package Execution Failed'
	    RETURN -1
	END

	-- Get the return value from the DTS package's global variable
	EXEC @oleStatus = sp_OAGetProperty @oPackage, @globalXmlStatusString, @oReturnValue OUT
	IF @oleStatus <> 0
	BEGIN
	    PRINT 'Global Variable Read Failed'
	    RETURN -1
	END
print @oReturnValue
EXEC @oleStatus = sp_OAGetProperty @oPackage, @globalFileName, @filename OUT
	IF @oleStatus <> 0
	BEGIN
	    PRINT 'Global Variable Read Failed'
	    RETURN -1
	END
print @filename
	-- Unitialize the Package
	EXEC @oleStatus = sp_OAMethod @oPackage, 'UnInitialize'
	IF @oleStatus <> 0
	BEGIN
	    PRINT 'UnInitializing Package Failed'
	    RETURN -1
	END

	-- Clean Up
	EXEC @oleStatus = sp_OADestroy @oPackage
	IF @oleStatus <> 0
	BEGIN
		RETURN -1
	END

	RETURN @oReturnValue

END
GO
