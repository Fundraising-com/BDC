USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_PhoneListNextID]    Script Date: 06/07/2017 09:33:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_get_PhoneListNextID] AS

-----------------------------------------------
--  Get a new PhoneListID   ---
-----------------------------------------------
insert into PhoneList(CreateDate) values(GetDate())	
SELECT @@Identity AS PhoneListID
GO
