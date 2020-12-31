<xsl:stylesheet version="1.0"
    xmlns="urn:schemas-microsoft-com:office:spreadsheet"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
 xmlns:msxsl="urn:schemas-microsoft-com:xslt"
 xmlns:user="urn:my-scripts"
 xmlns:o="urn:schemas-microsoft-com:office:office"
 xmlns:x="urn:schemas-microsoft-com:office:excel"
 xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet" > 
 
<xsl:template match="/">
  <Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet"
    xmlns:o="urn:schemas-microsoft-com:office:office"
    xmlns:x="urn:schemas-microsoft-com:office:excel"
    xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
    xmlns:html="http://www.w3.org/TR/REC-html40">
    <xsl:apply-templates/>
  </Workbook>
</xsl:template>

<xsl:template match="/*">
  <style>
	.xl24
	{mso-style-parent:style0;
	border:.5pt solid black;
	mso-number-format:"\@";
	white-space:normal;}
  </style>
  <style>
	.xl25
	{mso-style-parent:style0;
	background:yellow;
	border:.5pt solid black;
	mso-number-format:"\@";
	white-space:normal;}
  </style>
  <style>
	.xl26
	{mso-style-parent:style0;
	border:.5pt solid black;
	mso-number-format:"\0022$\0022\#\,\#\#0\.00";}
	white-space:normal;}
  </style>
  <style>
	.xl27
	{mso-style-parent:style0;
	border:.5pt solid black;
	background:yellow;
	mso-number-format:"\0022$\0022\#\,\#\#0\.00";}
	white-space:normal;}
  </style>
  <style>
	.xl28
	{mso-style-parent:style0;
	border:.5pt solid black;
	mso-number-format:0%;
	white-space:normal;}
  </style>
  <style>
	.xl29
	{mso-style-parent:style0;
	border:.5pt solid black;
	mso-number-format:0%;
	background:yellow;
	white-space:normal;}
  </style>
  <style>
	.xl30
	{mso-style-parent:style0;
	border:.5pt solid black;
	white-space:normal;}
  </style>
  <style>
	.xl31
	{mso-style-parent:style0;
	background:yellow;
	border:.5pt solid black;
	white-space:normal;}
  </style>
  <Worksheet>
  <xsl:attribute name="ss:Name">
  <xsl:value-of select="local-name(/*/*)"/>
  </xsl:attribute>
    <Table x:FullColumns="1" x:FullRows="1" >
      <tr style="FONT-WEIGHT: bold; COLOR: blue">
        <xsl:for-each select="*[position() = 1]/*">
          <td class="xl24">
            <xsl:value-of select="local-name()"/>
          </td>
        </xsl:for-each>
      </tr>
      <xsl:apply-templates/>
    </Table>
  </Worksheet>   
</xsl:template>


<xsl:template match="/*/*">
  <tr>
    <xsl:apply-templates/>
  </tr>
</xsl:template>


<xsl:template match="/*/*/*">
  <xsl:for-each select=".">
    <xsl:if test="contains(substring(local-name(), string-length(local-name())-6), 'Percent')">
      <xsl:if test="contains(.,'#highlight#')">
        <td class="xl29">
          <xsl:value-of select="substring(., 12)"/>
        </td>
      </xsl:if>
      <xsl:if test="not(contains(.,'#highlight#'))">
        <td class="xl28">
          <xsl:value-of select="."/>
        </td>
      </xsl:if>
    </xsl:if>
  <xsl:if test="contains(substring(local-name(), string-length(local-name())-4), 'Price')">
      <xsl:if test="contains(.,'#highlight#')">
        <td class="xl27">
          <xsl:value-of select="substring(., 12)"/>
        </td>
      </xsl:if>
      <xsl:if test="not(contains(.,'#highlight#'))">
        <td class="xl26">
          <xsl:value-of select="."/>
        </td>
      </xsl:if>
    </xsl:if>
    <xsl:if test="contains(local-name(),'Product_Code') or contains(local-name(),'Remit_Code')">
      <xsl:if test="contains(.,'#highlight#')">
        <td class="xl25">
          <xsl:value-of select="substring(., 12)"/>
        </td>
      </xsl:if>
      <xsl:if test="not(contains(.,'#highlight#'))">
        <td class="xl24">
          <xsl:value-of select="."/>
        </td>
      </xsl:if>
    </xsl:if>
    <xsl:if test="not(contains(substring(local-name(), string-length(local-name())-4), 'Price')) and not(contains(substring(local-name(), string-length(local-name())-6), 'Percent')) and not(contains(local-name(),'Product_Code') or contains(local-name(),'Remit_Code'))">
      <xsl:if test="contains(.,'#highlight#')">
        <td class="xl31">
          <xsl:value-of select="substring(., 12)"/>
        </td>
      </xsl:if>
      <xsl:if test="contains(local-name(), 'Changes') and contains(., 'Yes')">
        <td class="xl31">
          <xsl:value-of select="."/>
        </td>
      </xsl:if>
      <xsl:if test="not(contains(.,'#highlight#')) and not(contains(local-name(), 'Changes') and contains(., 'Yes'))">
        <td class="xl30">
          <xsl:value-of select="."/>
        </td>
      </xsl:if>
    </xsl:if>
  </xsl:for-each>
</xsl:template>


</xsl:stylesheet>
