/*
select *
from qspcanadaproduct..product
where product_sort_name like 'The%Magazine'
and product_year = 2014
*/

SELECT DISTINCT 
                         CustomerOrderHeaderInstance, TransID, RecipientFirstName, RecipientLastName, Address1, Address2, City, Province, PostalCode, MagazineTitle, Price, 
                         NbrOfIssues, Language, CreationDate, RunID, ProductCode, RemitCode
FROM            (SELECT        cod.CustomerOrderHeaderInstance, cod.TransID, LEFT(COALESCE (cod.Recipient, ''), CHARINDEX(' ', COALESCE (cod.Recipient, ''), CHARINDEX(' ', 
                                                    COALESCE (cod.Recipient, ''), 1))) AS RecipientFirstName, LTRIM(RIGHT(COALESCE (cod.Recipient, ''), LEN(REPLACE(COALESCE (cod.Recipient, ''), ' ', 
                                                    '_')) - COALESCE (CHARINDEX(' ', COALESCE (cod.Recipient, ''), 1), 0))) AS RecipientLastName, c.Address1, COALESCE (c.Address2, '') AS Address2, 
                                                    c.City, c.State AS Province, c.Zip AS PostalCode, p.Product_Sort_Name AS MagazineTitle, CONVERT(numeric(10, 2), cod.Price) AS Price, 
                                                    pd.Nbr_of_Issues AS NbrOfIssues, COALESCE (p.Lang, 'EN') AS Language, cod.CreationDate, COALESCE (rb.RunID, 0) AS RunID, 
                                                    p.Product_Code AS ProductCode, p.RemitCode
                          FROM            CustomerOrderDetail AS cod INNER JOIN
                                                    CustomerOrderHeader AS coh ON coh.Instance = cod.CustomerOrderHeaderInstance INNER JOIN
                                                    Customer AS c ON 
                                                    c.Instance = CASE cod.CustomerShipToInstance WHEN 0 THEN coh.CustomerBillToInstance ELSE cod.CustomerShipToInstance END INNER JOIN
                                                    QSPCanadaProduct.dbo.PRICING_DETAILS AS pd ON pd.MagPrice_Instance = cod.PricingDetailsID INNER JOIN
                                                    QSPCanadaProduct.dbo.Product AS p ON p.Product_Instance = pd.Product_Instance INNER JOIN
                                                    CustomerOrderDetailRemitHistory AS codrh ON codrh.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance AND 
                                                    codrh.TransID = cod.TransID INNER JOIN
                                                    RemitBatch AS rb ON rb.ID = codrh.RemitBatchID
                          WHERE        (cod.DelFlag <> 1) AND (codrh.Status = 42001)) AS vw
WHERE	RemitCode IN ('8381')
AND		RunID BETWEEN 1430 AND 1460
ORDER BY RunID, ProductCode, CustomerOrderHeaderInstance, TransID