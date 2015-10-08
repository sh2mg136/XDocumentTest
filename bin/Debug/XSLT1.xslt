<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="xml" indent="yes"/>

  <xsl:template match="message">
    <p>

      <span>
        <small>
          <xsl:value-of select="datestr"/>
          <br/>
        </small>
      </span>

      <strong>
        <xsl:value-of select="text" />
        <br/>
      </strong>

      <i>
        <small>
          <xsl:value-of select="@date" />
        </small>
      </i>

    </p>

    <hr />

  </xsl:template>

</xsl:stylesheet>
