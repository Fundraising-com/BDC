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
	mso-number-format:"\@";
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
  <td class="xl24">
    <xsl:value-of select="."/>
  </td>
</xsl:template>


</xsl:stylesheet>
