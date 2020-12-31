<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"> 
 
<xsl:template match="/">
  <html><body>
    <xsl:apply-templates/>
  </body></html>
</xsl:template>

<xsl:template match="/*">
    <table style="font-family:arial;">
      <tr style="FONT-WEIGHT: bold">
        <xsl:for-each select="*[position() = 1]/*">
          <td>
          <xsl:value-of select="local-name()"/>
          </td>
        </xsl:for-each>
      </tr>
      <xsl:apply-templates/>
    </table> 
</xsl:template>


<xsl:template match="/*/*">
  <tr>
    <xsl:apply-templates/>
  </tr>
</xsl:template>


<xsl:template match="/*/*/*">
  <td>
    <xsl:value-of select="."/>
  </td>
</xsl:template>


</xsl:stylesheet>
