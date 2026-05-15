# Create assets directory if it doesn't exist
if (!(Test-Path -Path "assets")) {
    New-Item -ItemType Directory -Path "assets"
}

$ua = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0.0.0 Safari/537.36"

$weapons = @(
    @{ name="ak47.png"; url="https://upload.wikimedia.org/wikipedia/commons/thumb/6/65/AK-47_type_II_noBG.png/640px-AK-47_type_II_noBG.png" },
    @{ name="m4a1.png"; url="https://upload.wikimedia.org/wikipedia/commons/thumb/4/41/M4A1_carbine_noBG.png/640px-M4A1_carbine_noBG.png" },
    @{ name="svd.png"; url="https://upload.wikimedia.org/wikipedia/commons/thumb/1/15/SVD_noBG.png/640px-SVD_noBG.png" },
    @{ name="shotgun.png"; url="https://upload.wikimedia.org/wikipedia/commons/thumb/a/a2/Remington_870_noBG.png/640px-Remington_870_noBG.png" },
    @{ name="glock.png"; url="https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Glock_17_noBG.png/640px-Glock_17_noBG.png" },
    @{ name="revolver.png"; url="https://upload.wikimedia.org/wikipedia/commons/thumb/b/be/Colt_Python_noBG.png/640px-Colt_Python_noBG.png" },
    @{ name="machete.png"; url="https://upload.wikimedia.org/wikipedia/commons/thumb/9/9b/Machete_noBG.png/640px-Machete_noBG.png" },
    @{ name="axe.png"; url="https://upload.wikimedia.org/wikipedia/commons/thumb/a/a2/Fire_axe_noBG.png/640px-Fire_axe_noBG.png" },
    @{ name="crossbow.png"; url="https://upload.wikimedia.org/wikipedia/commons/thumb/b/b3/Crossbow_noBG.png/640px-Crossbow_noBG.png" },
    @{ name="knife.png"; url="https://upload.wikimedia.org/wikipedia/commons/thumb/c/c5/Combat_knife_noBG.png/640px-Combat_knife_noBG.png" }
)

Write-Host "Downloading weapon assets with User-Agent..." -ForegroundColor Cyan
foreach ($w in $weapons) {
    $dest = "assets/" + $w.name
    Write-Host "Downloading $($w.name)..."
    try {
        Invoke-WebRequest -Uri $w.url -OutFile $dest -UserAgent $ua
        Start-Sleep -Seconds 1 # Add a small delay to avoid rate limiting
    } catch {
        Write-Host "Failed to download $($w.name): $($_.Exception.Message)" -ForegroundColor Red
    }
}

Write-Host "Finished!" -ForegroundColor Green
