<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xs="http://www.w3.org/2001/XMLSchema" exclude-result-prefixes="xs">
	<xsl:output method="xml" encoding="UTF-8" indent="yes"/>
	<xsl:template match="/ImportCustomer">
		<ImportCustomer>
			<xsl:for-each select="@SchemaVersion">
				<xsl:variable name="VmarkerloopSchemaVersion" select="."/>
				<xsl:attribute name="SchemaVersion">
					<xsl:value-of select="$VmarkerloopSchemaVersion"/>
				</xsl:attribute>
			</xsl:for-each>
			<xsl:for-each select="Customer">
				<Customer>
					<xsl:for-each select="ExternalNumber">
						<xsl:variable name="VmarkerloopExternalNumber" select="."/>
						<ExternalNumber>
							<xsl:value-of select="$VmarkerloopExternalNumber"/>
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
						<xsl:variable name="VmarkerloopEMail" select="."/>
						<Email>
							<xsl:value-of select="$VmarkerloopEMail"/>
						</Email>
					</xsl:for-each>
					<xsl:for-each select="Password">
						<xsl:variable name="VmarkerloopPassword" select="."/>
						<Pass>
							<xsl:value-of select="$VmarkerloopPassword"/>
						</Pass>
					</xsl:for-each>
					<xsl:for-each select="Subscribed">
						<xsl:variable name="VmarkerloopSubscribed" select="."/>
						<Subscribed>
							<xsl:value-of select="$VmarkerloopSubscribed"/>
						</Subscribed>
					</xsl:for-each>
					<xsl:for-each select="CustAddress">
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
		</ImportCustomer>
	</xsl:template>
</xsl:stylesheet>
