<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xs="http://www.w3.org/2001/XMLSchema" exclude-result-prefixes="xs">
	<xsl:output method="xml" encoding="UTF-8" indent="yes"/>
	<xsl:template match="/ImportOrder">
		<ImportOrder>
			<xsl:for-each select="@SchemaVersion">
				<xsl:variable name="VmarkerloopSchemaVersion" select="."/>
				<xsl:attribute name="SchemaVersion">
					<xsl:value-of select="$VmarkerloopSchemaVersion"/>
				</xsl:attribute>
			</xsl:for-each>
			<xsl:for-each select="Order">
				<Order>
					<xsl:for-each select="ExternalNumber">
						<xsl:variable name="VmarkerloopExternalNumber" select="."/>
						<ExternalNumber>
							<xsl:value-of select="$VmarkerloopExternalNumber"/>
						</ExternalNumber>
					</xsl:for-each>
					<xsl:for-each select="Customer">
						<Customer>
							<xsl:for-each select="CustExternalNumber">
								<xsl:variable name="VmarkerloopCustExternalNumber" select="."/>
								<ExternalNumber>
									<xsl:value-of select="$VmarkerloopCustExternalNumber"/>
								</ExternalNumber>
							</xsl:for-each>
							<xsl:for-each select="CustomerNumber">
								<xsl:variable name="VmarkerloopCustomerNumber" select="."/>
								<CustomerNumber>
									<xsl:value-of select="$VmarkerloopCustomerNumber"/>
								</CustomerNumber>
							</xsl:for-each>
							<xsl:for-each select="FirstName">
								<xsl:variable name="VmarkerloopFirstName" select="."/>
								<FirstName>
									<xsl:value-of select="$VmarkerloopFirstName"/>
								</FirstName>
							</xsl:for-each>
							<xsl:for-each select="LastName">
								<xsl:variable name="VmarkerloopLastName" select="."/>
								<LastName>
									<xsl:value-of select="$VmarkerloopLastName"/>
								</LastName>
							</xsl:for-each>
							<xsl:for-each select="Email">
								<xsl:variable name="VmarkerloopEmail" select="."/>
								<Email>
									<xsl:value-of select="$VmarkerloopEmail"/>
								</Email>
							</xsl:for-each>
							<xsl:for-each select="Password">
								<xsl:variable name="VmarkerloopPassword" select="."/>
								<Pass>
									<xsl:value-of select="$VmarkerloopPassword"/>
								</Pass>
							</xsl:for-each>
							<xsl:for-each select="SubscribeMailList">
								<xsl:variable name="VmarkerloopSubscribeMailList" select="."/>
								<Subscribed>
									<xsl:value-of select="$VmarkerloopSubscribeMailList"/>
								</Subscribed>
							</xsl:for-each>
							<xsl:for-each select="CustomerAddress">
								<CustomerAddress>
									<xsl:for-each select="NickName">
										<xsl:variable name="VmarkerloopNickName" select="."/>
										<NickName>
											<xsl:value-of select="$VmarkerloopNickName"/>
										</NickName>
									</xsl:for-each>
									<xsl:for-each select="FirstName">
										<xsl:variable name="VmarkerloopFirstName2" select="."/>
										<FirstName>
											<xsl:value-of select="$VmarkerloopFirstName2"/>
										</FirstName>
									</xsl:for-each>
									<xsl:for-each select="MI">
										<xsl:variable name="VmarkerloopMI" select="."/>
										<MI>
											<xsl:value-of select="$VmarkerloopMI"/>
										</MI>
									</xsl:for-each>
									<xsl:for-each select="LastName">
										<xsl:variable name="VmarkerloopLastName2" select="."/>
										<LastName>
											<xsl:value-of select="$VmarkerloopLastName2"/>
										</LastName>
									</xsl:for-each>
									<xsl:for-each select="Company">
										<xsl:variable name="VmarkerloopCompany" select="."/>
										<Company>
											<xsl:value-of select="$VmarkerloopCompany"/>
										</Company>
									</xsl:for-each>
									<xsl:for-each select="Address1">
										<xsl:variable name="VmarkerloopAddress1" select="."/>
										<Address1>
											<xsl:value-of select="$VmarkerloopAddress1"/>
										</Address1>
									</xsl:for-each>
									<xsl:for-each select="Address2">
										<xsl:variable name="VmarkerloopAddress2" select="."/>
										<Address2>
											<xsl:value-of select="$VmarkerloopAddress2"/>
										</Address2>
									</xsl:for-each>
									<xsl:for-each select="City">
										<xsl:variable name="VmarkerloopCity" select="."/>
										<City>
											<xsl:value-of select="$VmarkerloopCity"/>
										</City>
									</xsl:for-each>
									<xsl:for-each select="State">
										<xsl:variable name="VmarkerloopState" select="."/>
										<State>
											<xsl:value-of select="$VmarkerloopState"/>
										</State>
									</xsl:for-each>
									<xsl:for-each select="Zip">
										<xsl:variable name="VmarkerloopZip" select="."/>
										<Zip>
											<xsl:value-of select="$VmarkerloopZip"/>
										</Zip>
									</xsl:for-each>
									<xsl:for-each select="Country">
										<xsl:variable name="VmarkerloopCountry" select="."/>
										<Country>
											<xsl:value-of select="$VmarkerloopCountry"/>
										</Country>
									</xsl:for-each>
									<xsl:for-each select="Phone">
										<xsl:variable name="VmarkerloopPhone" select="."/>
										<Phone>
											<xsl:value-of select="$VmarkerloopPhone"/>
										</Phone>
									</xsl:for-each>
									<xsl:for-each select="Fax">
										<xsl:variable name="VmarkerloopFax" select="."/>
										<Fax>
											<xsl:value-of select="$VmarkerloopFax"/>
										</Fax>
									</xsl:for-each>
									<xsl:for-each select="Email">
										<xsl:variable name="VmarkerloopEmail2" select="."/>
										<Email>
											<xsl:value-of select="$VmarkerloopEmail2"/>
										</Email>
									</xsl:for-each>
								</CustomerAddress>
							</xsl:for-each>
						</Customer>
					</xsl:for-each>
					<xsl:for-each select="DateOrdered">
						<xsl:variable name="VmarkerloopDateOrdered" select="."/>
						<DateOrdered>
							<xsl:value-of select="$VmarkerloopDateOrdered"/>
						</DateOrdered>
					</xsl:for-each>
					<xsl:for-each select="SubTotal">
						<xsl:variable name="VmarkerloopSubTotal" select="."/>
						<SubTotal>
							<xsl:value-of select="$VmarkerloopSubTotal"/>
						</SubTotal>
					</xsl:for-each>
					<xsl:for-each select="ShippingTotal">
						<xsl:variable name="VmarkerloopShippingTotal" select="."/>
						<ShippingTotal>
							<xsl:value-of select="$VmarkerloopShippingTotal"/>
						</ShippingTotal>
					</xsl:for-each>
					<xsl:for-each select="HandlingTotal">
						<xsl:variable name="VmarkerloopHandlingTotal" select="."/>
						<HandlingTotal>
							<xsl:value-of select="$VmarkerloopHandlingTotal"/>
						</HandlingTotal>
					</xsl:for-each>
					<xsl:for-each select="CountryTax">
						<xsl:variable name="VmarkerloopCountryTax" select="."/>
						<CountryTax>
							<xsl:value-of select="$VmarkerloopCountryTax"/>
						</CountryTax>
					</xsl:for-each>
					<xsl:for-each select="StateTax">
						<xsl:variable name="VmarkerloopStateTax" select="."/>
						<StateTax>
							<xsl:value-of select="$VmarkerloopStateTax"/>
						</StateTax>
					</xsl:for-each>
					<xsl:for-each select="PaymentMethod">
						<xsl:variable name="VmarkerloopPaymentMethod" select="."/>
						<PaymentMethod>
							<xsl:value-of select="$VmarkerloopPaymentMethod"/>
						</PaymentMethod>
					</xsl:for-each>
					<xsl:for-each select="GiftCertificateTotal">
						<xsl:variable name="VmarkerloopGiftCertificateTotal" select="."/>
						<GiftCertificateTotal>
							<xsl:value-of select="$VmarkerloopGiftCertificateTotal"/>
						</GiftCertificateTotal>
					</xsl:for-each>
					<xsl:for-each select="DownloadDate">
						<xsl:variable name="VmarkerloopDownloadDate" select="."/>
						<DownloadDate>
							<xsl:value-of select="$VmarkerloopDownloadDate"/>
						</DownloadDate>
					</xsl:for-each>
					<xsl:for-each select="TotalAppliedDiscount">
						<xsl:variable name="VmarkerloopTotalAppliedDiscount" select="."/>
						<TotalAppliedDiscounts>
							<xsl:value-of select="$VmarkerloopTotalAppliedDiscount"/>
						</TotalAppliedDiscounts>
					</xsl:for-each>
					<xsl:for-each select="TotalBilled">
						<xsl:variable name="VmarkerloopTotalBilled" select="."/>
						<TotalBilled>
							<xsl:value-of select="$VmarkerloopTotalBilled"/>
						</TotalBilled>
					</xsl:for-each>
					<xsl:for-each select="AmountRemaining">
						<xsl:variable name="VmarkerloopAmountRemaining" select="."/>
						<AmountRemaining>
							<xsl:value-of select="$VmarkerloopAmountRemaining"/>
						</AmountRemaining>
					</xsl:for-each>
					<xsl:for-each select="LocalTaxTotal">
						<xsl:variable name="VmarkerloopLocalTaxTotal" select="."/>
						<LocalTaxTotal>
							<xsl:value-of select="$VmarkerloopLocalTaxTotal"/>
						</LocalTaxTotal>
					</xsl:for-each>
					<xsl:for-each select="Units">
						<xsl:variable name="VmarkerloopUnits" select="."/>
						<Units>
							<xsl:value-of select="$VmarkerloopUnits"/>
						</Units>
					</xsl:for-each>
					<xsl:for-each select="GrandTotal">
						<xsl:variable name="VmarkerloopGrandTotal" select="."/>
						<GrandTotal>
							<xsl:value-of select="$VmarkerloopGrandTotal"/>
						</GrandTotal>
					</xsl:for-each>
					<xsl:for-each select="PaymentStatus">
						<xsl:variable name="VmarkerloopPaymentStatus" select="."/>
						<PaymentStatus>
							<xsl:value-of select="$VmarkerloopPaymentStatus"/>
						</PaymentStatus>
					</xsl:for-each>
					<xsl:for-each select="Void">
						<xsl:variable name="VmarkerloopVoid" select="."/>
						<Void>
							<xsl:value-of select="$VmarkerloopVoid"/>
						</Void>
					</xsl:for-each>
					<xsl:for-each select="Referrer">
						<xsl:variable name="VmarkerloopReferrer" select="."/>
						<Referrer>
							<xsl:value-of select="$VmarkerloopReferrer"/>
						</Referrer>
					</xsl:for-each>
					<xsl:for-each select="BOPaymentPending">
						<xsl:variable name="VmarkerloopBOPaymentPending" select="."/>
						<BOPaymentsPending>
							<xsl:value-of select="$VmarkerloopBOPaymentPending"/>
						</BOPaymentsPending>
					</xsl:for-each>
					<xsl:for-each select="OrderAddress">
						<OrderAddress>
							<xsl:for-each select="Type">
								<xsl:variable name="VmarkerloopType" select="."/>
								<Type>
									<xsl:value-of select="$VmarkerloopType"/>
								</Type>
							</xsl:for-each>
							<xsl:for-each select="NickName">
								<xsl:variable name="VmarkerloopNickName2" select="."/>
								<NickName>
									<xsl:value-of select="$VmarkerloopNickName2"/>
								</NickName>
							</xsl:for-each>
							<xsl:for-each select="FirstName">
								<xsl:variable name="VmarkerloopFirstName3" select="."/>
								<FirstName>
									<xsl:value-of select="$VmarkerloopFirstName3"/>
								</FirstName>
							</xsl:for-each>
							<xsl:for-each select="MI">
								<xsl:variable name="VmarkerloopMI2" select="."/>
								<MI>
									<xsl:value-of select="$VmarkerloopMI2"/>
								</MI>
							</xsl:for-each>
							<xsl:for-each select="LastName">
								<xsl:variable name="VmarkerloopLastName3" select="."/>
								<LastName>
									<xsl:value-of select="$VmarkerloopLastName3"/>
								</LastName>
							</xsl:for-each>
							<xsl:for-each select="Company">
								<xsl:variable name="VmarkerloopCompany2" select="."/>
								<Company>
									<xsl:value-of select="$VmarkerloopCompany2"/>
								</Company>
							</xsl:for-each>
							<xsl:for-each select="Address1">
								<xsl:variable name="VmarkerloopAddress12" select="."/>
								<Address1>
									<xsl:value-of select="$VmarkerloopAddress12"/>
								</Address1>
							</xsl:for-each>
							<xsl:for-each select="Address2">
								<xsl:variable name="VmarkerloopAddress22" select="."/>
								<Address2>
									<xsl:value-of select="$VmarkerloopAddress22"/>
								</Address2>
							</xsl:for-each>
							<xsl:for-each select="City">
								<xsl:variable name="VmarkerloopCity2" select="."/>
								<City>
									<xsl:value-of select="$VmarkerloopCity2"/>
								</City>
							</xsl:for-each>
							<xsl:for-each select="State">
								<xsl:variable name="VmarkerloopState2" select="."/>
								<State>
									<xsl:value-of select="$VmarkerloopState2"/>
								</State>
							</xsl:for-each>
							<xsl:for-each select="Zip">
								<xsl:variable name="VmarkerloopZip2" select="."/>
								<Zip>
									<xsl:value-of select="$VmarkerloopZip2"/>
								</Zip>
							</xsl:for-each>
							<xsl:for-each select="Country">
								<xsl:variable name="VmarkerloopCountry2" select="."/>
								<Country>
									<xsl:value-of select="$VmarkerloopCountry2"/>
								</Country>
							</xsl:for-each>
							<xsl:for-each select="Phone">
								<xsl:variable name="VmarkerloopPhone2" select="."/>
								<Phone>
									<xsl:value-of select="$VmarkerloopPhone2"/>
								</Phone>
							</xsl:for-each>
							<xsl:for-each select="Fax">
								<xsl:variable name="VmarkerloopFax2" select="."/>
								<Fax>
									<xsl:value-of select="$VmarkerloopFax2"/>
								</Fax>
							</xsl:for-each>
							<xsl:for-each select="Email">
								<xsl:variable name="VmarkerloopEmail3" select="."/>
								<Email>
									<xsl:value-of select="$VmarkerloopEmail3"/>
								</Email>
							</xsl:for-each>
							<xsl:for-each select="ShippingCarrier">
								<xsl:variable name="VmarkerloopShippingCarrier" select="."/>
								<ShippingCarrier>
									<xsl:value-of select="$VmarkerloopShippingCarrier"/>
								</ShippingCarrier>
							</xsl:for-each>
							<xsl:for-each select="SpecialInstruction">
								<xsl:variable name="VmarkerloopSpecialInstruction" select="."/>
								<SpecialInstruction>
									<xsl:value-of select="$VmarkerloopSpecialInstruction"/>
								</SpecialInstruction>
							</xsl:for-each>
							<xsl:for-each select="CountryTaxRate">
								<xsl:variable name="VmarkerloopCountryTaxRate" select="."/>
								<CountryTaxRate>
									<xsl:value-of select="$VmarkerloopCountryTaxRate"/>
								</CountryTaxRate>
							</xsl:for-each>
							<xsl:for-each select="StateTaxRate">
								<xsl:variable name="VmarkerloopStateTaxRate" select="."/>
								<StateTaxRate>
									<xsl:value-of select="$VmarkerloopStateTaxRate"/>
								</StateTaxRate>
							</xsl:for-each>
							<xsl:for-each select="LocalTaxRate">
								<xsl:variable name="VmarkerloopLocalTaxRate" select="."/>
								<LocalTaxRate>
									<xsl:value-of select="$VmarkerloopLocalTaxRate"/>
								</LocalTaxRate>
							</xsl:for-each>
							<xsl:for-each select="DiscountTotal">
								<xsl:variable name="VmarkerloopDiscountTotal" select="."/>
								<DiscountTotal>
									<xsl:value-of select="$VmarkerloopDiscountTotal"/>
								</DiscountTotal>
							</xsl:for-each>
							<xsl:for-each select="IsShippingTaxed">
								<xsl:variable name="VmarkerloopIsShippingTaxed" select="."/>
								<IsShippingTaxed>
									<xsl:value-of select="$VmarkerloopIsShippingTaxed"/>
								</IsShippingTaxed>
							</xsl:for-each>
							<xsl:for-each select="ShippableShippingTotal">
								<xsl:variable name="VmarkerloopShippableShippingTotal" select="."/>
								<ShippableShippingTotal>
									<xsl:value-of select="$VmarkerloopShippableShippingTotal"/>
								</ShippableShippingTotal>
							</xsl:for-each>
							<xsl:for-each select="HandlingTotal">
								<xsl:variable name="VmarkerloopHandlingTotal2" select="."/>
								<HandlingTotal>
									<xsl:value-of select="$VmarkerloopHandlingTotal2"/>
								</HandlingTotal>
							</xsl:for-each>
							<xsl:for-each select="BackOrderShippingTotal">
								<xsl:variable name="VmarkerloopBackOrderShippingTotal" select="."/>
								<BackOrderShippingTotal>
									<xsl:value-of select="$VmarkerloopBackOrderShippingTotal"/>
								</BackOrderShippingTotal>
							</xsl:for-each>
							<xsl:for-each select="BOQuantity">
								<xsl:variable name="VmarkerloopBOQuantity" select="."/>
								<BOQuantity>
									<xsl:value-of select="$VmarkerloopBOQuantity"/>
								</BOQuantity>
							</xsl:for-each>
							<xsl:for-each select="BOPaymentPending">
								<xsl:variable name="VmarkerloopBOPaymentPending2" select="."/>
								<BOPaymentsPending>
									<xsl:value-of select="$VmarkerloopBOPaymentPending2"/>
								</BOPaymentsPending>
							</xsl:for-each>
							<xsl:for-each select="TotalBilled">
								<xsl:variable name="VmarkerloopTotalBilled2" select="."/>
								<TotalBilled>
									<xsl:value-of select="$VmarkerloopTotalBilled2"/>
								</TotalBilled>
							</xsl:for-each>
							<xsl:for-each select="AmountRemaining">
								<xsl:variable name="VmarkerloopAmountRemaining2" select="."/>
								<AmountRemaining>
									<xsl:value-of select="$VmarkerloopAmountRemaining2"/>
								</AmountRemaining>
							</xsl:for-each>
							<xsl:for-each select="CODAmount">
								<xsl:variable name="VmarkerloopCODAmount" select="."/>
								<CODAmount>
									<xsl:value-of select="$VmarkerloopCODAmount"/>
								</CODAmount>
							</xsl:for-each>
							<xsl:for-each select="Product">
								<Product>
									<xsl:for-each select="ProductName">
										<xsl:variable name="VmarkerloopProductName" select="."/>
										<ProductName>
											<xsl:value-of select="$VmarkerloopProductName"/>
										</ProductName>
									</xsl:for-each>
									<xsl:for-each select="ItemCode">
										<xsl:variable name="VmarkerloopItemCode" select="."/>
										<ProductCode>
											<xsl:value-of select="$VmarkerloopItemCode"/>
										</ProductCode>
									</xsl:for-each>
									<xsl:for-each select="Price">
										<xsl:variable name="VmarkerloopPrice" select="."/>
										<Price>
											<xsl:value-of select="$VmarkerloopPrice"/>
										</Price>
									</xsl:for-each>
									<xsl:for-each select="Cost">
										<xsl:variable name="VmarkerloopCost" select="."/>
										<Cost>
											<xsl:value-of select="$VmarkerloopCost"/>
										</Cost>
									</xsl:for-each>
									<xsl:for-each select="Quantity">
										<xsl:variable name="VmarkerloopQuantity" select="."/>
										<Quantity>
											<xsl:value-of select="$VmarkerloopQuantity"/>
										</Quantity>
									</xsl:for-each>
									<xsl:for-each select="Weight">
										<xsl:variable name="VmarkerloopWeight" select="."/>
										<Weight>
											<xsl:value-of select="$VmarkerloopWeight"/>
										</Weight>
									</xsl:for-each>
									<xsl:for-each select="IsOnSale">
										<xsl:variable name="VmarkerloopIsOnSale" select="."/>
										<IsOnSale>
											<xsl:value-of select="$VmarkerloopIsOnSale"/>
										</IsOnSale>
									</xsl:for-each>
									<xsl:for-each select="SalePrice">
										<xsl:variable name="VmarkerloopSalePrice" select="."/>
										<SalePrice>
											<xsl:value-of select="$VmarkerloopSalePrice"/>
										</SalePrice>
									</xsl:for-each>
									<xsl:for-each select="IsGiftWrap">
										<xsl:variable name="VmarkerloopIsGiftWrap" select="."/>
										<IsGiftWrap>
											<xsl:value-of select="$VmarkerloopIsGiftWrap"/>
										</IsGiftWrap>
									</xsl:for-each>
									<xsl:for-each select="GiftWrapPrice">
										<xsl:variable name="VmarkerloopGiftWrapPrice" select="."/>
										<GiftWrapPrice>
											<xsl:value-of select="$VmarkerloopGiftWrapPrice"/>
										</GiftWrapPrice>
									</xsl:for-each>
									<xsl:for-each select="GiftWrapQuantity">
										<xsl:variable name="VmarkerloopGiftWrapQuantity" select="."/>
										<GiftWrapQuantity>
											<xsl:value-of select="$VmarkerloopGiftWrapQuantity"/>
										</GiftWrapQuantity>
									</xsl:for-each>
									<xsl:for-each select="IsShipable">
										<xsl:variable name="VmarkerloopIsShipable" select="."/>
										<IsShipable>
											<xsl:value-of select="$VmarkerloopIsShipable"/>
										</IsShipable>
									</xsl:for-each>
									<xsl:for-each select="ShipPrice">
										<xsl:variable name="VmarkerloopShipPrice" select="."/>
										<ShipPrice>
											<xsl:value-of select="$VmarkerloopShipPrice"/>
										</ShipPrice>
									</xsl:for-each>
									<xsl:for-each select="HasCountryTax">
										<xsl:variable name="VmarkerloopHasCountryTax" select="."/>
										<HasCountryTax>
											<xsl:value-of select="$VmarkerloopHasCountryTax"/>
										</HasCountryTax>
									</xsl:for-each>
									<xsl:for-each select="HasStateTax">
										<xsl:variable name="VmarkerloopHasStateTax" select="."/>
										<HasStateTax>
											<xsl:value-of select="$VmarkerloopHasStateTax"/>
										</HasStateTax>
									</xsl:for-each>
									<xsl:for-each select="HasLocalTax">
										<xsl:variable name="VmarkerloopHasLocalTax" select="."/>
										<HasLocalTax>
											<xsl:value-of select="$VmarkerloopHasLocalTax"/>
										</HasLocalTax>
									</xsl:for-each>
									<xsl:for-each select="BackOrderedQty">
										<xsl:variable name="VmarkerloopBackOrderedQty" select="."/>
										<BackOrderedQty>
											<xsl:value-of select="$VmarkerloopBackOrderedQty"/>
										</BackOrderedQty>
									</xsl:for-each>
									<xsl:for-each select="VendorName">
										<xsl:variable name="VmarkerloopVendorName" select="."/>
										<VendorName>
											<xsl:value-of select="$VmarkerloopVendorName"/>
										</VendorName>
									</xsl:for-each>
									<xsl:for-each select="VendorEmail">
										<xsl:variable name="VmarkerloopVendorEmail" select="."/>
										<VendorEmail>
											<xsl:value-of select="$VmarkerloopVendorEmail"/>
										</VendorEmail>
									</xsl:for-each>
									<xsl:for-each select="Sku">
										<xsl:variable name="VmarkerloopSku" select="."/>
										<Sku>
											<xsl:value-of select="$VmarkerloopSku"/>
										</Sku>
									</xsl:for-each>
								</Product>
							</xsl:for-each>
						</OrderAddress>
					</xsl:for-each>
					<xsl:for-each select="Payment">
						<Payment>
							<xsl:for-each select="PaymentMethod">
								<xsl:variable name="VmarkerloopPaymentMethod2" select="."/>
								<PaymentMethod>
									<xsl:value-of select="$VmarkerloopPaymentMethod2"/>
								</PaymentMethod>
							</xsl:for-each>
							<xsl:for-each select="PONumber">
								<xsl:variable name="VmarkerloopPONumber" select="."/>
								<PONumber>
									<xsl:value-of select="$VmarkerloopPONumber"/>
								</PONumber>
							</xsl:for-each>
							<xsl:for-each select="CheckNumber">
								<xsl:variable name="VmarkerloopCheckNumber" select="."/>
								<CheckNumber>
									<xsl:value-of select="$VmarkerloopCheckNumber"/>
								</CheckNumber>
							</xsl:for-each>
							<xsl:for-each select="BankName">
								<xsl:variable name="VmarkerloopBankName" select="."/>
								<BankName>
									<xsl:value-of select="$VmarkerloopBankName"/>
								</BankName>
							</xsl:for-each>
							<xsl:for-each select="RoutingNumber">
								<xsl:variable name="VmarkerloopRoutingNumber" select="."/>
								<RoutingNumber>
									<xsl:value-of select="$VmarkerloopRoutingNumber"/>
								</RoutingNumber>
							</xsl:for-each>
							<xsl:for-each select="AccountNumber">
								<xsl:variable name="VmarkerloopAccountNumber" select="."/>
								<AccountNumber>
									<xsl:value-of select="$VmarkerloopAccountNumber"/>
								</AccountNumber>
							</xsl:for-each>
							<xsl:for-each select="CreditCardNumber">
								<xsl:variable name="VmarkerloopCreditCardNumber" select="."/>
								<CreditCardNumber>
									<xsl:value-of select="$VmarkerloopCreditCardNumber"/>
								</CreditCardNumber>
							</xsl:for-each>
							<xsl:for-each select="CardType">
								<xsl:variable name="VmarkerloopCardType" select="."/>
								<CardType>
									<xsl:value-of select="$VmarkerloopCardType"/>
								</CardType>
							</xsl:for-each>
							<xsl:for-each select="SecurityCode">
								<xsl:variable name="VmarkerloopSecurityCode" select="."/>
								<SecurityCode>
									<xsl:value-of select="$VmarkerloopSecurityCode"/>
								</SecurityCode>
							</xsl:for-each>
							<xsl:for-each select="ExpireMonth">
								<xsl:variable name="VmarkerloopExpireMonth" select="."/>
								<ExpireMonth>
									<xsl:value-of select="$VmarkerloopExpireMonth"/>
								</ExpireMonth>
							</xsl:for-each>
							<xsl:for-each select="ExpireYear">
								<xsl:variable name="VmarkerloopExpireYear" select="."/>
								<ExpireYear>
									<xsl:value-of select="$VmarkerloopExpireYear"/>
								</ExpireYear>
							</xsl:for-each>
							<xsl:for-each select="Last4Digits">
								<xsl:variable name="VmarkerloopLast4Digits" select="."/>
								<Last4Digits>
									<xsl:value-of select="$VmarkerloopLast4Digits"/>
								</Last4Digits>
							</xsl:for-each>
						</Payment>
					</xsl:for-each>
				</Order>
			</xsl:for-each>
		</ImportOrder>
	</xsl:template>
</xsl:stylesheet>
