$path = "c:\inetpub\wwwroot\3202.aspx.vb"
$text = [System.IO.File]::ReadAllText($path, [System.Text.Encoding]::UTF8)
$utf8bom = New-Object System.Text.UTF8Encoding($true)
[System.IO.File]::WriteAllText($path, $text, $utf8bom)
$bytes = [System.IO.File]::ReadAllBytes($path)
Write-Host "BOM bytes: $($bytes[0]) $($bytes[1]) $($bytes[2])"
Write-Host "File size: $($bytes.Length) bytes"
Write-Host "Done."
