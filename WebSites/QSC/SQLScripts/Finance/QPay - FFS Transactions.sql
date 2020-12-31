--Cust Svc
select top 100 *
from BatchPaymentSystem..bpps_tx b
left join BatchPaymentSystem..bpps_tx_response br on br.bpps_tx_id = b.bpps_tx_id
join qspcanadaordermanagement..incident i on i.incidentinstance = b.client_transaction_id
left join qspcanadaordermanagement..incidentaction ia on ia.incidentinstance = i.incidentinstance and ia.actioninstance = 18
where b.bpps_application_id = 1209
--and b.request_type = 'DEPOSIT'
and i.customerorderheaderinstance = 12174108    
order by b.bpps_tx_id desc

--Landed
select *
from BatchPaymentSystem..bpps_tx b
left join BatchPaymentSystem..bpps_tx_response br on br.bpps_tx_id = b.bpps_tx_id
join qspcanadaordermanagement..customerpaymentheader cph on cph.Instance = b.client_transaction_id
join qspcanadaordermanagement..customerorderdetail cod on cod.customerorderheaderinstance = cph.customerorderheaderinstance
join qspcanadaordermanagement..customerorderheader coh on coh.instance = cod.customerorderheaderinstance
join qspcanadaordermanagement..batch ba on ba.id = coh.orderbatchid and ba.date = coh.orderbatchdate
where b.bpps_application_id = 1209
and b.client_transaction_id > 3000000 --exlude cust svc
--and b.request_type = 'DEPOSIT'
and ba.orderid = 916816
order by b.customer_reference_id, b.bpps_tx_id desc

--Landed reverse
select coh.Instance, b.bpps_tx_id, b.transaction_amount, br.response_message, coh.PaymentMethodInstance, cph.StatusInstance, ccb.IsGLDone, *
from qspcanadaordermanagement..customerpaymentheader cph
left join qspcanadaordermanagement..CustomerPaymentHeaderQPAYTxResponse cphq on cphq.customerpaymentheaderinstance = cph.instance
left join qspcanadaordermanagement..creditcardpayment ccp on ccp.customerpaymentheaderinstance = cph.instance
left join QSPCanadaOrderManagement..CreditCardBatch ccb on ccb.ID = ccp.BatchID
left join BatchPaymentSystem..bpps_tx b on cph.Instance = b.client_transaction_id and b.bpps_application_id = 1209-- and b.request_type = 'DEPOSIT'
left join BatchPaymentSystem..bpps_tx_response br on br.bpps_tx_id = b.bpps_tx_id
left join qspcanadaordermanagement..customerorderheader coh on coh.instance = cph.customerorderheaderinstance
where coh.instance in (
	
)

order by bppstxid, coh.PaymentMethodInstance--b.bpps_tx_id

--qsp.ca
select coh.Instance, b.bpps_tx_id, b.transaction_amount, br.response_message, coh.PaymentMethodInstance, cph.StatusInstance, ccb.IsGLDone, *
from qspcanadaordermanagement..customerpaymentheader cph
left join qspcanadaordermanagement..CustomerPaymentHeaderQPAYTxResponse cphq on cphq.customerpaymentheaderinstance = cph.instance
left join qspcanadaordermanagement..creditcardpayment ccp on ccp.customerpaymentheaderinstance = cph.instance
left join QSPCanadaOrderManagement..CreditCardBatch ccb on ccb.ID = ccp.BatchID
left join QSPCanadaOrderManagement..CustomerOrderHeader coh on coh.Instance = cph.CustomerOrderHeaderInstance
left join QSPCanadaOrderManagement..InternetOrderID ioi on ioi.CustomerOrderHeaderInstance = coh.Instance
left join QSPEcommerce..Cart cart on cart.EDS_Order_ID = ioi.InternetOrderID
left join BatchPaymentSystem..bpps_tx b on b.customer_reference_id = Cast(cart.Cart_Id as varchar(max))--and b.bpps_application_id = 1209-- and b.request_type = 'DEPOSIT'
left join BatchPaymentSystem..bpps_tx_response br on br.bpps_tx_id = b.bpps_tx_id
where coh.instance in (
12247704,
12247713
)

order by bppstxid, coh.PaymentMethodInstance--b.bpps_tx_id