USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[Rpt_StudentAndCustomerListing_postcard]    Script Date: 06/07/2017 09:33:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
 *
 * Christopher Quinn Thursday, May 25, 2009
 *
 * Paul Tsai 	July 19, 2006
 * 
 * Based off of Rpt_StudentAndCustomerListing
 * Called by Procedure - uspvl3k16.OrionRD.dbo.pr_postcard_data_collector
 * 
 * Procedure Populates QSPCanadaCommon.dbo.Postcard_Data One Account at a time
 */

CREATE  PROCEDURE [dbo].[Rpt_StudentAndCustomerListing_postcard]
    (
      @AccountID INT,
      @FM_ID CHAR(4),
      @fromDate DATETIME,
      @toDate DATETIME
    )
AS

    SET NOCOUNT ON

-- -- Create Temp Tables used in stored procedure Rpt_StudentAndCustomerListing_Postcard
    CREATE TABLE #InvoiceSale
        (
          [AccountId] [int] NOT NULL,
          [AccountName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS
                                      NULL,
          [InvoiceDate] [datetime] NULL,
          [InvoiceNumber] [int] NULL,
          [StudentInstance] [int] NOT NULL,
          [SF] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS
                             NULL,
          [SL] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS
                             NULL,
          [CustomerBillToInstance] [int] NOT NULL,
          [CF] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS
                             NULL,
          [CL] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS
                             NULL,
          [ProductCode] [varchar](4) COLLATE SQL_Latin1_General_CP1_CI_AS
                                     NULL,
          [AlphaProductCode] [varchar](4) COLLATE SQL_Latin1_General_CP1_CI_AS
                                          NULL,
          [ProductName] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS
                                      NULL,
          [FY] [int] NOT NULL,
          rLast varchar(50),
          rFirst varchar(50),
        )
    ON  [PRIMARY]
 --Get all the Invoices for the account
    CREATE TABLE #AccountInvoices
        (
          AccountId int NOT NULL,
          InvoiceDate Datetime NOT NULL,
          InvoiceNumber int NOT NULL,
          FY int NOT NULL
        ) 

--clean up the account info
    --execute dbo.spRemoveBadAccountCharacters @AccountID

--current items - get the invoices
--Fiscal 2009
    insert  into #AccountInvoices
            Select  Account_Id,
                    Invoice_Date,
                    Invoice_ID,
                    2011
            from    QSPCanadaFinance.dbo.invoice
            where   Account_Id = @AccountId
                    and Invoice_Date between @fromDate and @toDate
            Group by Account_Id,
                    Invoice_Date,
                    Invoice_ID


-- current items - Get all the sales, products, student and Customers
    INSERT  #InvoiceSale
            (
              AccountId,
              InvoiceDate,
              InvoiceNumber,
              StudentInstance,
              CustomerBillToInstance,
              ProductCode,
              AlphaProductCode,
              ProductName,
              FY, rLast,rFirst
            )
            SELECT  AI.AccountId,
                    AI.InvoiceDate,
                    AI.InvoiceNumber,
                    COH.StudentInstance,
                    COH.CustomerBillToInstance,
                    COD.ProductCode,
                    COD.AlphaProductCode,
                    COD.ProductName,
                    AI.FY,
                    parsename(REPLACE(COD.Recipient,' ','.'),1)
                    ,LTRIM(RTRIM(ISNULL(parsename(REPLACE(COD.Recipient,' ','.'),4),'')+' '+ISNULL(parsename(REPLACE(COD.Recipient,' ','.'),3),'')+' '+ISNULL(parsename(REPLACE(COD.Recipient,' ','.'),2),'')))
                    

            FROM    qspcanadaordermanagement.dbo.CustomerOrderHeader COH with ( nolock )
                    INNER JOIN qspcanadaordermanagement.dbo.CustomerOrderDetail COD
                    with ( nolock ) ON COH.Instance = COD.CustomerOrderHeaderInstance
                    INNER JOIN #AccountInvoices AI with ( nolock ) ON COH.AccountID = AI.AccountId
                                                                      AND COD.InvoiceNumber = AI.InvoiceNumber
            WHERE   ( COD.DelFlag = 0 )
                    AND ( COH.DelFlag = 0 )
                    AND ( COH.Type = 902 )
                    AND producttype = 46001
                    and ( AI.[FY] IN ( 2011,2010,2009, 2008, 2007 ) )
            GROUP BY AI.AccountId,
                    AI.InvoiceDate,
                    AI.InvoiceNumber,
                    COH.StudentInstance,
                    COH.CustomerBillToInstance,
                    COD.ProductCode,
                    COD.AlphaProductCode,
                    COD.ProductName,
                    COD.Price,
                    COD.Quantity,
                    COD.CouponPage,
                    AI.FY,
                    parsename(REPLACE(COD.Recipient,' ','.'),1)
                    ,LTRIM(RTRIM(ISNULL(parsename(REPLACE(COD.Recipient,' ','.'),4),'')+' '+ISNULL(parsename(REPLACE(COD.Recipient,' ','.'),3),'')+' '+ISNULL(parsename(REPLACE(COD.Recipient,' ','.'),2),'')))

--Update Account Info
    UPDATE  i
    SET     i.AccountName = c.[Name]
    FROM    QSPCanadacommon.dbo.CAccount c
            INNER JOIN #InvoiceSale i ON c.ID = i.AccountId

--Update Student Info
    UPDATE  i
    SET     SL = RTrim(LTrim(s.LastName)),
            SF = RTrim(LTrim(s.FirstName))
    FROM    #InvoiceSale i
            INNER JOIN qspcanadaordermanagement.dbo.Student s ON i.StudentInstance = s.Instance
    WHERE   i.FY IN ( 2011,2010,2009, 2008, 2007 )


--Update Customer Info
    UPDATE  i
    set     CL = RTrim(LTrim(c.LastName)),
            CF = RTrim(LTrim(c.FirstName))
    FROM    #InvoiceSale i
            INNER JOIN qspcanadaordermanagement.dbo.Customer c 
            ON i.CustomerBillToInstance = c.Instance
    WHERE   i.FY IN ( 2011,2010,2009, 2008, 2007 )

--Update Customer Info
    UPDATE  i
    set     CL = rLast,
            CF = rFirst
    FROM    #InvoiceSale i
    WHERE   i.FY IN ( 2011,2010,2009, 2008, 2007 )
    and CF IS NULL
    SET NOCOUNT OFF



    SET NOCOUNT OFF





    INSERT  tmp
            (
              AccountId,
              InvoiceDate,
              InvoiceNumber,
              StudentInstance,
              CustomerBillToInstance,
              ProductCode,
              AlphaProductCode,
              ProductName,
              FY
            )
            select 
                          AccountId,
              InvoiceDate,
              InvoiceNumber,
              StudentInstance,
              CustomerBillToInstance,
              ProductCode,
              AlphaProductCode,
              ProductName,
              FY
            FROM #InvoiceSale




    INSERT  dbo.postcard_data_2011
            (
              HEADER_ACCT_#,
              HEADER_ACCT_NAME,
              HEADER_FMID,
              STUDENT_DET_STUDENT_NAME_LAST,
              STUDENT_DET_STUDENT_NAME_FIRST,
              STUDENT_DET_CUSTOMER_NAME_LAST,
              STUDENT_DET_CUSTOMER_NAME_FIRST,
              STUDENT_DET_TITLE1
            )
            SELECT  [AccountId],
                    [AccountName],
                    @FM_ID as FMID,
                    [SL],
                    [SF],
                    [CL],
                    [CF],
                    [ProductName]
            FROM    #InvoiceSale with ( nolock )
            ORDER BY AccountId ASC,
                    [SL] ASC,
                    [SF] ASC,
                    [CL] ASC,
                    [CF] ASC,
                    [ProductName] ASC ;

    DELETE  #InvoiceSale
    DELETE  #AccountInvoices

-- -- Drop Temp Tables
    DROP TABLE #InvoiceSale
    DROP TABLE #AccountInvoices
GO
