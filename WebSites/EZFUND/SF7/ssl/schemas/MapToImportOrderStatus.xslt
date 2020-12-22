<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xs="http://www.w3.org/2001/XMLSchema" exclude-result-prefixes="xs">
	<xsl:output method="xml" encoding="UTF-8" indent="yes"/>
	<xsl:template match="/Request">
		<ImportOrderStatus>
			<xsl:for-each select="Document">
				<xsl:variable name="Vvar3_CONST" select="'1.0'"/>
				<xsl:attribute name="SchemaVersion">
					<xsl:value-of select="$Vvar3_CONST"/>
				</xsl:attribute>
				<xsl:for-each select="SalesOrder">
					<Order>
						<xsl:for-each select="SalesOrderNumber">
							<xsl:variable name="VmarkerloopSalesOrderNumber" select="."/>
							<OrderNumber>
								<xsl:value-of select="$VmarkerloopSalesOrderNumber"/>
							</OrderNumber>
						</xsl:for-each>
						<xsl:for-each select="PaymentStatus">
							<xsl:variable name="VmarkerloopPaymentStatus" select="."/>
							<PaymentStatus>
								<xsl:value-of select="$VmarkerloopPaymentStatus"/>
							</PaymentStatus>
						</xsl:for-each>
						<xsl:for-each select="ShippingStatus">
							<xsl:variable name="VmarkerloopShippingStatus" select="."/>
							<ShippingStatus>
								<xsl:value-of select="$VmarkerloopShippingStatus"/>
							</ShippingStatus>
						</xsl:for-each>
						<xsl:for-each select="ShippingCarrier">
							<xsl:variable name="VmarkerloopShippingCarrier" select="."/>
							<ShippingCarrier>
								<xsl:value-of select="$VmarkerloopShippingCarrier"/>
							</ShippingCarrier>
						</xsl:for-each>
						<xsl:for-each select="TrackingNumbers/TrackingNumber">
							<xsl:variable name="VmarkerloopTrackingNumber" select="." />
							<TrackingNumber>
								<xsl:value-of select="$VmarkerloopTrackingNumber" />
							</TrackingNumber>
						</xsl:for-each>
					</Order>
				</xsl:for-each>
			</xsl:for-each>
		</ImportOrderStatus>
	</xsl:template>
</xsl:stylesheet>
