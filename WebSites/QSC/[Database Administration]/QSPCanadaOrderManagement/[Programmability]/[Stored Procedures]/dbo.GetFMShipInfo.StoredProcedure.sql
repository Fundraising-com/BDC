USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[GetFMShipInfo]    Script Date: 06/07/2017 09:19:34 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetFMShipInfo] 
	@fmid varchar(4)
as

select fmid,'' as District,
	'' as FieldManagerSS,
	firstname+ ' ' + lastname as Name,
	street1,
	street2 as FourthLine,
	city,
	stateprovince,
	postal_code
 from qspcanadacommon..fieldmanager f,qspcanadacommon..addresslist al, qspcanadacommon..address a
where f.addresslistid=al.id
and a.addresslistid = al.id
and a.address_type=54004
and fmid=@fmid
GO
