USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_AddressListNextID]    Script Date: 06/07/2017 09:33:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_get_AddressListNextID] AS

-------------------------------------------------
--  Get a new AddressListID   ---
-------------------------------------------------
insert into AddressList(CreateDate) values(GetDate())	
SELECT @@Identity AS AddressListID
GO
