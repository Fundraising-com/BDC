USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[sp_remove_old_ca_data]    Script Date: 06/07/2017 09:20:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_remove_old_ca_data] AS

/*
-- SET Result to text

PRINT 'Record count for: NonBatchCreditCardPayment'  
SELECT COUNT(*)
  FROM QSPCanadaOrderManagement..NonBatchCreditCardPayment
 WHERE CustomerPaymentHeaderInstance IN (SELECT Instance
                                           FROM QSPCanadaOrderManagement..CustomerPaymentHeader
                                          WHERE CustomerOrderHeaderInstance in (SELECT instance
                                                                                  FROM QSPCanadaOrderManagement..CustomerOrderHeader
                                                                                 WHERE CreationDate >= '2010-01-01'));

PRINT 'Record count for: CreditCardPayment'  
SELECT COUNT(*) 
  FROM QSPCanadaOrderManagement..CreditCardPayment
 WHERE CustomerPaymentHeaderInstance IN (SELECT Instance
                                           FROM QSPCanadaOrderManagement..CustomerPaymentHeader
                                          WHERE CustomerOrderHeaderInstance IN (SELECT instance
                                                                                  FROM QSPCanadaOrderManagement..CustomerOrderHeader
                                                                                 WHERE CreationDate >= '2010-01-01'));

PRINT 'Record count for: CreditCardPaymentAudit'  
SELECT COUNT(*)
  FROM QSPCanadaOrderManagement..CreditCardPaymentAudit
 WHERE CustomerPaymentHeaderInstance IN (SELECT Instance
                                           FROM QSPCanadaOrderManagement..CustomerPaymentHeader
                                          WHERE CustomerOrderHeaderInstance IN (SELECT instance
                                                                                  FROM QSPCanadaOrderManagement..CustomerOrderHeader
                                                                                 WHERE CreationDate >= '2010-01-01'));

PRINT 'Record count for: CustomerPaymentDetail'  
SELECT COUNT(*) 
  FROM QSPCanadaOrderManagement..CustomerPaymentDetail
 WHERE CustomerPaymentHeaderInstance IN (SELECT Instance
                                           FROM QSPCanadaOrderManagement..CustomerPaymentHeader
                                          WHERE CustomerOrderHeaderInstance IN (SELECT instance
                                                                                  FROM QSPCanadaOrderManagement..CustomerOrderHeader
                                                                                 WHERE CreationDate >= '2010-01-01'));

PRINT 'Record count for: CustomerPaymentHeader'  
SELECT COUNT(*) 
  FROM QSPCanadaOrderManagement..CustomerPaymentHeader
 WHERE CustomerOrderHeaderInstance IN (SELECT instance
                                         FROM QSPCanadaOrderManagement..CustomerOrderHeader
                                        WHERE CreationDate >= '2010-01-01');

PRINT 'Record count for: IncidentAction'  
SELECT COUNT(*) 
  FROM QSPCanadaOrderManagement..IncidentAction
 WHERE IncidentInstance IN (SELECT IncidentInstance
                              FROM QSPCanadaOrderManagement..Incident
                             WHERE CustomerOrderHeaderInstance IN (SELECT instance
                                                                     FROM QSPCanadaOrderManagement..CustomerOrderHeader
                                                                    WHERE CreationDate >= '2010-01-01'));

PRINT 'Record count for: InternetOrderID'  
SELECT COUNT(*) 
  FROM QSPCanadaOrderManagement..InternetOrderID
 WHERE CustomerOrderHeaderInstance IN (SELECT instance
                                         FROM QSPCanadaOrderManagement..CustomerOrderHeader
                                        WHERE CreationDate >= '2010-01-01');

PRINT 'Record count for: Incident'  
SELECT COUNT(*) 
  FROM QSPCanadaOrderManagement..Incident
 WHERE CustomerOrderHeaderInstance IN (SELECT instance
                                         FROM QSPCanadaOrderManagement..CustomerOrderHeader
                                        WHERE CreationDate >= '2010-01-01');

PRINT 'Record count for: LetterBatchCustomerOrderDetail'  
SELECT COUNT(*) 
  FROM QSPCanadaOrderManagement..LetterBatchCustomerOrderDetail
 WHERE CustomerOrderHeaderInstance IN (SELECT instance
                                         FROM QSPCanadaOrderManagement..CustomerOrderHeader
                                        WHERE CreationDate >= '2010-01-01');

PRINT 'Record count for: CustomerOrderDetailRemitHistory'  
SELECT COUNT(*) 
  FROM QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory
 WHERE CustomerOrderHeaderInstance IN (SELECT instance
                                         FROM QSPCanadaOrderManagement..CustomerOrderHeader
                                        WHERE CreationDate >= '2010-01-01');

PRINT 'Record count for: CustomerOrderDetailRemitHistoryAudit'  
SELECT COUNT(*) 
  FROM QSPCanadaOrderManagement..CustomerOrderDetailRemitHistoryAudit
 WHERE CustomerOrderHeaderInstance IN (SELECT instance
                                         FROM QSPCanadaOrderManagement..CustomerOrderHeader
                                        WHERE CreationDate >= '2010-01-01');

PRINT 'Record count for: CustomerOrderDetail'  
SELECT COUNT(*) 
  FROM QSPCanadaOrderManagement..CustomerOrderDetail
 WHERE CustomerOrderHeaderInstance IN (SELECT instance
                                         FROM QSPCanadaOrderManagement..CustomerOrderHeader
                                        WHERE CreationDate >= '2010-01-01');

PRINT 'Record count for: CustomerOrderDetailAudit'  
SELECT COUNT(*) 
  FROM QSPCanadaOrderManagement..CustomerOrderDetailAudit
 WHERE CustomerOrderHeaderInstance IN (SELECT instance
                                         FROM QSPCanadaOrderManagement..CustomerOrderHeader
                                        WHERE CreationDate >= '2010-01-01');

PRINT 'Record count for: CustomerOrderHeader'  
SELECT COUNT(*) 
  FROM QSPCanadaOrderManagement..CustomerOrderHeader
 WHERE Instance IN (SELECT instance
                      FROM QSPCanadaOrderManagement..CustomerOrderHeader
                     WHERE CreationDate >= '2010-01-01');
*/

DISABLE TRIGGER auditCreditCardPayment ON CreditCardPayment;
DISABLE TRIGGER auditCustomerOrderDetailRemitHistory ON CustomerOrderDetailremitHistory;
DISABLE TRIGGER auditCustomerOrderDetail ON CustomerOrderDetail;
DISABLE TRIGGER delTrigger ON CustomerOrderDetail;

/*
CREATE INDEX IX_BatchAudit_1 ON QSPCanadaOrderManagement..BatchAudit(OrderID);
CREATE INDEX IX_CustomerOrderDetailAudit_1 ON QSPCanadaOrderManagement..CustomerOrderDetailAudit(CustomerOrderHeaderInstance);
CREATE INDEX IX_Incident_1 ON QSPCanadaOrderManagement..Incident(CustomerOrderHeaderInstance, IncidentInstance);
*/

SET NOCOUNT ON;

DECLARE @instance INT;
DECLARE @i INT;

SET @i = 0;

SELECT instance
  INTO #instance_to_delete
  FROM QSPCanadaOrderManagement..CustomerOrderHeader
 WHERE CreationDate < '2010-01-01';

SELECT COUNT(*) FROM #instance_to_delete


DECLARE c1 CURSOR FOR
SELECT instance
  FROM #instance_to_delete
 ORDER BY instance;
 
OPEN c1;
FETCH NEXT FROM c1 INTO @instance;
WHILE @@FETCH_STATUS = 0 BEGIN
  SET @i = @i + 1;
  PRINT @i;
  
  DELETE FROM QSPCanadaOrderManagement..NonBatchCreditCardPayment
   WHERE CustomerPaymentHeaderInstance IN (SELECT Instance
                                             FROM QSPCanadaOrderManagement..CustomerPaymentHeader
                                            WHERE CustomerOrderHeaderInstance = @instance);
  
  DELETE FROM QSPCanadaOrderManagement..CreditCardPayment
   WHERE CustomerPaymentHeaderInstance IN (SELECT Instance
                                             FROM QSPCanadaOrderManagement..CustomerPaymentHeader
                                            WHERE CustomerOrderHeaderInstance = @instance);
  
  DELETE FROM QSPCanadaOrderManagement..CreditCardPaymentAudit
   WHERE CustomerPaymentHeaderInstance IN (SELECT Instance
                                             FROM QSPCanadaOrderManagement..CustomerPaymentHeader
                                            WHERE CustomerOrderHeaderInstance = @instance);
  
  DELETE FROM QSPCanadaOrderManagement..CustomerPaymentDetail
   WHERE CustomerPaymentHeaderInstance IN (SELECT Instance
                                             FROM QSPCanadaOrderManagement..CustomerPaymentHeader
                                            WHERE CustomerOrderHeaderInstance = @instance);
                                            
  DELETE FROM QSPCanadaOrderManagement..CustomerPaymentHeader
   WHERE CustomerOrderHeaderInstance = @instance;
      
  DELETE FROM QSPCanadaOrderManagement..IncidentAction
   WHERE IncidentInstance IN (SELECT IncidentInstance
                                FROM QSPCanadaOrderManagement..Incident
                               WHERE CustomerOrderHeaderInstance = @instance);

  DELETE FROM QSPCanadaOrderManagement..InternetOrderID
   WHERE CustomerOrderHeaderInstance = @instance;

  DELETE FROM QSPCanadaOrderManagement..Incident
   WHERE CustomerOrderHeaderInstance = @instance;
   
  DELETE FROM QSPCanadaOrderManagement..LetterBatchCustomerOrderDetail
   WHERE CustomerOrderHeaderInstance = @instance;
  
  DELETE FROM QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory
   WHERE CustomerOrderHeaderInstance = @instance;

  DELETE FROM QSPCanadaOrderManagement..CustomerOrderDetailRemitHistoryAudit
   WHERE CustomerOrderHeaderInstance = @instance;

  DELETE FROM QSPCanadaOrderManagement..CustomerOrderDetail
   WHERE CustomerOrderHeaderInstance = @instance;
  
  DELETE FROM QSPCanadaOrderManagement..CustomerOrderDetailAudit
   WHERE CustomerOrderHeaderInstance = @instance;
  
  DELETE FROM QSPCanadaOrderManagement..CustomerOrderHeader
   WHERE Instance = @instance; 

  FETCH NEXT FROM c1 INTO @instance;
END;

CLOSE c1;
DEALLOCATE c1;

DROP TABLE #instance_to_delete;

DELETE FROM QSPCanadaOrderManagement..CreditCardBatch
 WHERE DateCreated < '2010-01-01';
 
DELETE FROM QSPCanadaOrderManagement..BatchDistributionCenter
 WHERE BatchDate < '2010-01-01';

/*
USE QSPCanadaOrderManagement;
EXEC sp_msForEachTable 'DBCC CLEANTABLE(0, "?")';
EXEC sp_msForEachTable 'DBCC DBREINDEX("?", " ", 95)';
DBCC SHRINKDATABASE (QSPCanadaOrderManagement);
*/ 

ENABLE TRIGGER auditCreditCardPayment ON CreditCardPayment;
ENABLE TRIGGER auditCustomerOrderDetailRemitHistory ON CustomerOrderDetailremitHistory;
ENABLE TRIGGER auditCustomerOrderDetail ON CustomerOrderDetail;
ENABLE TRIGGER delTrigger ON CustomerOrderDetail;
GO
