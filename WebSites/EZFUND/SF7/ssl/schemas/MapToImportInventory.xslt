<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xs="http://www.w3.org/2001/XMLSchema" exclude-result-prefixes="xs">
	<xsl:output method="xml" encoding="UTF-8" indent="yes"/>
	<xsl:template match="/Request">
		<ImportInventory>
			<xsl:for-each select="Document">
				<xsl:variable name="Vvar3_CONST" select="'1.0'"/>
				<xsl:attribute name="SchemaVersion">
					<xsl:value-of select="$Vvar3_CONST"/>
				</xsl:attribute>
				<xsl:for-each select="ImportInventory">
					<xsl:for-each select="Product">
						<Product>
							<xsl:for-each select="ProductCode">
								<xsl:variable name="VmarkerloopProductCode" select="."/>
								<Code>
									<xsl:value-of select="$VmarkerloopProductCode"/>
								</Code>
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
						</Product>
					</xsl:for-each>
				</xsl:for-each>
				<xsl:for-each select="ImportInventory">
					<xsl:for-each select="Product">
						<xsl:variable name="VmarkerloopProductCode" select="ProductCode"/>
						<xsl:for-each select="Inventory">
							<Product>
								<xsl:if test="count(Sku) > 0">
									<xsl:for-each select="Sku">
										<xsl:variable name="VmarkerloopSku" select="."/>
										<Sku>
											<xsl:value-of select="$VmarkerloopSku"/>
										</Sku>
									</xsl:for-each>
								</xsl:if>
								<xsl:if test="count(Sku) = 0">
									<Code>
										<xsl:value-of select="$VmarkerloopProductCode"/>
									</Code>
								</xsl:if>
								<xsl:for-each select="Quantity">
									<xsl:variable name="VmarkerloopQuantity" select="."/>
									<QtyInStock>
										<xsl:value-of select="$VmarkerloopQuantity"/>
									</QtyInStock>
								</xsl:for-each>
								<xsl:for-each select="LowQtyFlag">
									<xsl:variable name="VmarkerloopLowQtyFlag" select="."/>
									<QtyLowFlag>
										<xsl:value-of select="$VmarkerloopLowQtyFlag"/>
									</QtyLowFlag>
								</xsl:for-each>
							</Product>
						</xsl:for-each>
					</xsl:for-each>
				</xsl:for-each>
			</xsl:for-each>
		</ImportInventory>
	</xsl:template>
</xsl:stylesheet>
