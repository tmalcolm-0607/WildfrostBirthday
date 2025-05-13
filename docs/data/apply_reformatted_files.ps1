# Apply reformatted files
# This script will create a backup of the original files and replace them with the reformatted versions

# Create backup directory if it doesn't exist
$backupDir = "original_files_backup"
if (-not (Test-Path $backupDir)) {
    New-Item -ItemType Directory -Path $backupDir | Out-Null
    Write-Host "Created backup directory: $backupDir"
}

# Get all reformatted files
$reformattedFiles = Get-ChildItem -Filter "*_reformatted.md"
$count = 0

foreach ($file in $reformattedFiles) {
    # Get the original file name
    $originalName = $file.Name -replace "_reformatted", ""
    
    # Check if original file exists
    if (Test-Path $originalName) {
        # Backup original file
        Copy-Item -Path $originalName -Destination "$backupDir\$originalName" -Force
        Write-Host "Backed up: $originalName"
        
        # Copy reformatted content to original file
        Copy-Item -Path $file.FullName -Destination $originalName -Force
        Write-Host "Applied: $originalName"
        $count++
    } else {
        Write-Host "Warning: Original file $originalName not found, skipping."
    }
}

Write-Host "Complete! Applied $count reformatted files. Original files are backed up in $backupDir."
