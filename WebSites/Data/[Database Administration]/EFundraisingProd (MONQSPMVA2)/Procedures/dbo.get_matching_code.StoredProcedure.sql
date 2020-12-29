USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[get_matching_code]    Script Date: 02/14/2014 13:08:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
--This proc will return the matching code for the address provided

CREATE    procedure [dbo].[get_matching_code] --'68 pine valley', 'h2k3a9'
           @street_address as varchar(75),
           @zip_code as varchar(10) 
          
as


select qspcommon.dbo.fct_get_zzzzz(@zip_code) +  qspcommon.dbo.fct_get_aa99(@street_address)
GO
