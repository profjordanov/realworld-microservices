Write-Host "Creating Certificates for Self-Signed Testing"

Write-Host "Creating Root Certificate"
$cert = New-SelfSignedCertificate -Type Custom -KeySpec Signature `
-Subject "CN=localhost" `
-FriendlyName "ManagerRootCert" `
-KeyExportPolicy Exportable `
-HashAlgorithm sha256 -KeyLength 4096 `
-CertStoreLocation "cert://currentuser/My" `
-KeyUsageProperty Sign `
-KeyUsage CertSign `
-NotAfter (Get-Date).AddYears(5)


# Client Auth
Write-Host "Creating Client Auth Certificate"
$clientCert = New-SelfSignedCertificate -Type Custom -KeySpec Signature `
-Subject "CN=localhost" -KeyExportPolicy Exportable `
-FriendlyName "ManagerClientCert" `
-HashAlgorithm sha256 -KeyLength 2048 `
-NotAfter (Get-Date).AddMonths(24) `
-CertStoreLocation "cert://currentuser/My" `
-Signer $cert -TextExtension @("2.5.29.37={text}1.3.6.1.5.5.7.3.2")

# TLS Cert
Write-Host "Creating Web Server Certificate"
$webCert = New-SelfSignedCertificate -Type Custom `
-Subject "CN=localhost" -KeyExportPolicy Exportable `
-DnsName "localhost" `
-FriendlyName "ManagerTlsCert" `
-HashAlgorithm sha256 -KeyLength 2048 `
-KeyUsage "KeyEncipherment", "DigitalSignature" `
-NotAfter (Get-Date).AddMonths(24) `
-CertStoreLocation "cert://currentuser/My" `
-Signer $cert


$PFXPass = ConvertTo-SecureString -String "P@ssw0rd!" -Force -AsPlainText

Write-Host "Exporting Certificates to File"

Export-PfxCertificate -Cert $clientCert `
-Password $PFXPass `
-FilePath managerSelfCert.pfx

Export-PfxCertificate -Cert $webCert `
-Password $PFXPass `
-FilePath managerSslCert.pfx

