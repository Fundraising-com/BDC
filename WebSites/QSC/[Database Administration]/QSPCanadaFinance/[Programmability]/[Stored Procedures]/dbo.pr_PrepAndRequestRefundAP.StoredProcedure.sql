USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[pr_PrepAndRequestRefundAP]    Script Date: 06/07/2017 09:17:27 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
/*

	Run the AP request for customer service checks 
	Copies from staging table
*/

CREATE PROCEDURE [dbo].[pr_PrepAndRequestRefundAP] AS

INSERT INTO QSPOracleInterface..OM_TBL_AP_INVOICES_INTF_BKUP select * from QSPOracleInterface..OM_TBL_AP_INVOICES_INTERFACE 
where Invoice_num Not IN ( select invoice_num from QSPOracleInterface..OM_TBL_AP_INVOICES_INTF_BKUP)

INSERT INTO QSPOracleInterface..OM_TBL_AP_INV_LINES_INTF_BKUP select * from QSPOracleInterface..OM_TBL_AP_INV_LINES_INTERFACE
where Invoice_num Not IN ( select invoice_num from QSPOracleInterface..OM_TBL_AP_INV_LINES_INTF_BKUP)

INSERT INTO QSPOracleInterface..OM_TBL_PO_VENDORS_INTF_BKUP select * from QSPOracleInterface..OM_TBL_PO_VENDORS_INTERFACE
where Invoice_num Not IN ( select invoice_num from QSPOracleInterface..OM_TBL_PO_VENDORS_INTF_BKUP)


-- Clear out the table
/*
TRUNCATE TABLE QSPOracleInterface..OM_TBL_AP_INVOICES_INTERFACE
TRUNCATE TABLE QSPOracleInterface..OM_TBL_AP_INV_LINES_INTERFACE
TRUNCATE TABLE QSPOracleInterface..OM_TBL_PO_VENDORS_INTERFACE
*/

Delete  From  QSPOracleInterface..OM_TBL_AP_INVOICES_INTERFACE
Delete  From QSPOracleInterface..OM_TBL_AP_INV_LINES_INTERFACE
Delete  From QSPOracleInterface..OM_TBL_PO_VENDORS_INTERFACE

-- Copy from the refund staging tables
Insert into  QSPOracleInterface..OM_TBL_AP_INV_LINES_INTERFACE
select * from QSPOracleInterface..OM_TBL_AP_INV_LINES_INTERFACE_REFUND_STAGING

Insert into QSPOracleInterface..OM_TBL_AP_INVOICES_INTERFACE
select * from QSPOracleInterface..OM_TBL_AP_INVOICES_INTERFACE_REFUND_STAGING

Insert into  QSPOracleInterface..OM_TBL_PO_VENDORS_INTERFACE
select * from QSPOracleInterface..OM_TBL_PO_VENDORS_INTERFACE_REFUND_STAGING

--Clear out refund staging
/*
TRUNCATE TABLE QSPOracleInterface..OM_TBL_AP_INVOICES_INTERFACE_REFUND_STAGING
TRUNCATE TABLE QSPOracleInterface..OM_TBL_AP_INV_LINES_INTERFACE_REFUND_STAGING
TRUNCATE TABLE QSPOracleInterface..OM_TBL_PO_VENDORS_INTERFACE_REFUND_STAGING
*/
Delete  From QSPOracleInterface..OM_TBL_AP_INVOICES_INTERFACE_REFUND_STAGING
Delete  From QSPOracleInterface..OM_TBL_AP_INV_LINES_INTERFACE_REFUND_STAGING
Delete  From QSPOracleInterface..OM_TBL_PO_VENDORS_INTERFACE_REFUND_STAGING

-- Log the run

insert into QSPOracleInterface..APRequestLog
select distinct invoice_num,Convert(varchar(10),GetDate(),101)  from QSPOracleInterface..OM_TBL_AP_INVOICES_INTERFACE

/*
-- Request the AP
exec MASTER..XP_SMTP_SENDMAIL 
	    @FROM 	= 'karen_tracy@rd.com',
	    @FROM_NAME 	= 'Karen Tracy',
	    @TO 	= 'rdaops@infocrossing.com, rdaprodserv@infocrossing.com',
	    @CC 	= 'karen.tracy@rd.com,muhammad.siddiqui@rd.com,brett.greenberg@rd.com, hazel.wylie@rd.com',
	    @priority 	= 'NORMAL',
	    @subject 	= 'Please run this job',
	    @type 	= 'text/plain',
	    @message  	= 'RCANAINV',
	    @server 	= 'nasmtp.us.rdigest.com'
*/
GO
