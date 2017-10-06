New-Alias Add-SvoxIVRVariable         Add-SvxIVRVariable
New-Alias Disconnect-SvoxCall         Disconnect-SvxCall
New-Alias Invoke-SvoxCall             Invoke-SvxCall
New-Alias Remove-SvoxIVRVariable      Remove-SvxIVRVariable
New-Alias Set-SvoxIVRVariable         Set-SvxIVRVariable

New-Alias Get-SvoxCall                Get-SvxCall
New-Alias Get-SvoxCallLog             Get-SvxCallLog
New-Alias Get-SvoxCallQueueLog        Get-SvxCallQueueLog
New-Alias Get-SvoxCallQueueStatus     Get-SvxCallQueueStatus
New-Alias Get-SvoxClient              Get-SvxClient
New-Alias Get-SvoxExtension           Get-SvxExtension
New-Alias Get-SvoxExtensionGroup      Get-SvxExtensionGroup
New-Alias Get-SvoxExtensionSettings   Get-SvxExtensionSettings
New-Alias Get-SvoxIVRVariable         Get-SvxIVRVariable
New-Alias Get-SvoxUserCallQueueStatus Get-SvxUserCallQueueStatus

New-Alias GoSvox Connect-GoSvoxServer

. $PSScriptRoot\SwitchvoxAPI.GoSvox.ps1

Export-ModuleMember -Alias * -Function *