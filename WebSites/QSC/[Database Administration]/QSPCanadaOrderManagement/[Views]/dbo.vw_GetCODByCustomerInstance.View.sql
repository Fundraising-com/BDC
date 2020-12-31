USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_GetCODByCustomerInstance]    Script Date: 06/07/2017 09:18:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_GetCODByCustomerInstance] AS

select c.instance as customerinstance, cod.CustomerOrderHeaderInstance, cod.TransID 
  from customer c,
          customerorderheader coh,
          customerorderdetail cod
where coh.customerbilltoinstance = c.instance and
          coh.instance = cod.customerorderheaderinstance 
group by
          c.instance,
          cod.customerorderheaderinstance,
          cod.transid
GO
