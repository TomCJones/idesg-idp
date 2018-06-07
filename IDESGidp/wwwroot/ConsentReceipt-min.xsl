<?xml version="1.0"?>
<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'>
  <xsl:output method='html' indent='yes' doctype-public='//W3C//DTD HTML 3.2  FINAL//EN' />
  <xsl:template match='/'>
    <HTML>
      <HEAD>
        <TITLE>Consent Receipt with minimal formating</TITLE>
      </HEAD>
      <BODY>
        <table border='1'>
          <tr>
            <td colspan='2' style='border:0'>
              <H3 style='text-align:center'>
                Consent Receipt with minimal formating
              </H3>
            </td>
          </tr>

          <tr>
            <td>
              User (piiPrincipalId)
            </td>
            <td>
              <xsl:value-of select='ConsentReceipt/piiPrincipalId' />
            </td>
          </tr>

          <tr>
            <td>
              Collection Method
            </td>
            <td>
              <xsl:value-of select='ConsentReceipt/collectionMethod' />
            </td>
          </tr>
          <tr style='height:1px'>
            <td colspan='2' style='height:1px;border:0;padding:0'>
              <p style='text-align:center; font-size:large; font-weight:bold; height:1px; padding:0; border:0'>
                Site(s) acquiring data (piiController)
              </p>
            </td>
          </tr>
          <xsl:apply-templates select ='ConsentReceipt/piiControllers' />

          <tr height='10'>
            <td colspan='2' style='height:1px;border:0;padding:0'>
              <p style='text-align:center; font-size:large; font-weight:bold; height:1px; padding:0; border:0'>
                Site acquiring data (services and their purposes)
              </p>
            </td>
          </tr>
          <xsl:apply-templates select ='ConsentReceipt/services' />

          <tr height='10'>
            <td colspan='2' style='height:1px;border:0;padding:0'>
              <p style='text-align:center; font-size:large; font-weight:bold; height:1px; padding:0; border:0'>
                General information about this receipt
              </p>
            </td>  
          </tr>
          
          <tr>
            <td>
              Sensitive + category
            </td>
            <td>
              <xsl:value-of select='ConsentReceipt/sensitive' />
              +
              <xsl:value-of select='ConsentReceipt/spiCat' />
            </td>
          </tr>
          

          <tr>
            <td>
              Jurisdiction
            </td>
            <td>
              <xsl:value-of select='ConsentReceipt/jurisdiction' />
            </td>
          </tr>
          
                    <tr>
            <td>
              policyUrl
            </td>
            <td>
              <xsl:value-of select='ConsentReceipt/policyUrl' />
            </td>
          </tr>

          <tr>
            <td>
              Consent Timestamp
            </td>
            <td>
              <xsl:value-of select='ConsentReceipt/consentTimestamp' />
            </td>
          </tr>

          <tr>
            <td>
              consentReceipt ID
            </td>
            <td>
              <xsl:value-of select='ConsentReceipt/consentReceiptID' />
            </td>
          </tr>

          <tr>
            <td>
              Language
            </td>
            <td>
              <xsl:value-of select='ConsentReceipt/language' />
            </td>
          </tr>
        </table>
        
        <div>
          Version:
          <xsl:value-of select='ConsentReceipt/version' />

        </div>
      </BODY>
    </HTML>
  </xsl:template>

  <xsl:template match='piiControllers'>
    <tr>
      <td>
        Site
      </td>
      <td>
        <xsl:value-of select='piiController' />
      </td>
    </tr>
    <tr>
      <td style='padding-left:10px'>
        |> Contact
      </td>
      <td>
        <xsl:value-of select='contact' />
      </td>
    </tr>
    <tr>
      <td style='padding-left:10px'>
        |> address
      </td>
      <td>
        <xsl:value-of select='address' />
      </td>
    </tr>
    <tr>
      <td style='padding-left:10px'>
        |> email
      </td>
      <td>
        <xsl:value-of select='email' />
      </td>
    </tr>
    <tr>
      <td style='padding-left:10px'>
        |> phone
      </td>
      <td>
        <xsl:value-of select='phone' />
      </td>
    </tr>
  </xsl:template>


  <xsl:template match='services'>
    <tr>
      <td style='padding-left:10px'>
        Service
      </td>
      <td>
        <xsl:value-of select='service' />
      </td>
    </tr>

    <xsl:apply-templates select ='purposes' />
  </xsl:template>


  <xsl:template match='purposes'>
    <tr>
      <td style='padding-left:10px'>
        |> purpose
        <xsl:choose>
          <xsl:when test="string(primaryPurpose) = 'true'">
          - primary
        </xsl:when>      
      </xsl:choose>
      </td>
      <td>
        <xsl:value-of select='purpose' />
      </td>
    </tr>

    <xsl:for-each select='purposeCategory'>
      <tr>
        <td style='padding-left:20px'>
          |>> purposeCategory
        </td>
        <td>
          <xsl:value-of select='.' />
        </td>
      </tr>
    </xsl:for-each>

    <tr>
      <td style='padding-left:20px'>
        |>> consentType
      </td>
      <td>
        <xsl:value-of select='consentType' />
      </td>
    </tr>

    <xsl:for-each select='piiCategory'>
      <tr>
        <td style='padding-left:20px'>
          |>> pii Data Category
        </td>
        <td>
          <xsl:value-of select='.' />
        </td>
      </tr>
    </xsl:for-each>

    <tr>
      <td style='padding-left:20px'>
        |>> termination event
      </td>
      <td>
        <xsl:value-of select='termination' />
      </td>
    </tr>

  </xsl:template>

</xsl:stylesheet>
