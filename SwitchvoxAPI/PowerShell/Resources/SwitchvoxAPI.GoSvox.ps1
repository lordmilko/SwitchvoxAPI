function Get-GoSvoxServer
{
	[CmdletBinding()]
	param (
		[Parameter(Mandatory = $false, Position = 0)]
		[string]
		$Server
	)

    if(Get-Command -Name __goSvoxGetServers -ErrorAction SilentlyContinue)
    {
		$serversText = @(__goSvoxGetServers)|foreach {
			"`"[ ]`",$_"
		}
		
		$servers = $serversText | ConvertFrom-Csv -Header "[!]","Server","Alias","UserName","Password"

		$client = Get-SvxClient

		foreach($s in $servers)
		{
			if($s.Alias -eq "")
			{
				$s.Alias = $null
			}

			if($s.Server -eq $client.Server -and $s.UserName -eq $client.UserName)
			{
				$s."[!]" = "[*]"
			}
		}

		if($Server)
		{
			$servers = @($servers | ForEach-Object {
				if($_.Server -like $Server)
				{
					$_
				}
				else
				{
					if($_.Alias -eq $Server)
					{
						$_
					}
				}
			})
		}

		$resp = $servers | Select-Object @{name="[!]";expression={$_."[!]"}},Server,Alias,UserName

		return $resp

        $response = @()

        foreach($server in $servers)
        {
            $username = $server.UserName
            $alias = $servers.Alias

            $obj = New-Object PSObject
            $obj | Add-Member NoteProperty Server $server.Server
            $obj | Add-Member NoteProperty Alias $alias
            $obj | Add-Member NoteProperty UserName $username

            $response += $obj
        }

        return $response
    }
    else
    {
        Write-ColorOutput "`nGoSvox is not installed. Run Install-GoSvoxServer first to install a GoSvox server.`n" -ForegroundColor Red
    }
}

function Connect-GoSvoxServer
{
	[CmdletBinding()]
	param (
		[Parameter(Mandatory = $false, Position = 0)]
		[string]
		$Server
	)

	if(Get-Command __goSvoxGetServers -ErrorAction SilentlyContinue)
	{
		$servers = @(GetServers)

		if(!$Server)
		{
			$connectTo = $servers | Select-Object -First 1

			ConnectToGoSvoxServer $connectTo
		}
		else
		{
			$matches = @($servers | Where-Object {$_.Server -like $Server -or $_.Alias -eq $Server})

			if($matches.Count -eq 1)
			{
				ConnectToGoSvoxServer $matches
			}
			elseif($matches.Count -gt 1)
			{
				Write-ColorOutput "`nAmbiguous server specified. The following servers matched the specified server name or alias" -ForegroundColor Red

				Get-GoSvoxServer | Where-Object {$_.Server -like $Server -or $_.Alias -eq $Server}
			}
			else
			{
				Write-ColorOutput "`nCould not find server that matches name or alias '$Server'`n" -ForegroundColor Red
			}
		}	
	}
	else
	{
		Write-ColorOutput "`nNo GoSvox servers are installed. Please install a server first using Install-GoSvoxServer`n" -ForegroundColor Red
	}
}

function ConnectToGoSvoxServer($server)
{
	$client = Get-SvxClient

	if($server.Server -eq $client.Server -and $server.UserName -eq $client.UserName)
	{
		Write-ColorOutput "`nAlready connected to $($server.Server) as $($server.UserName)`n" -ForegroundColor Yellow
	}
	else
	{
		$credential = New-Object System.Management.Automation.PSCredential -ArgumentList $server.UserName, (ConvertTo-SecureString $server.Password)
		Connect-SvxServer $server.Server $credential -Force

		Write-ColorOutput "`nConnected to $($server.Server) as $($server.UserName)`n" -ForegroundColor Green
	}
}

function Set-GoSvoxAlias
{
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute("PSUseShouldProcessForStateChangingFunctions", "", Scope="Function")]
	[CmdletBinding()]
	param (
		[Parameter(Mandatory = $false, Position = 0)]
		[string]
		$Alias
	)

	if(!(Test-Path $Profile))
	{
		throw "No GoSvox servers are installed. To install a GoSvox server, run Install-GoSvoxServer."
	}

	$contents = Get-Content $Profile

	$functionStart = GetGoSvoxStart $contents
	$functionEnd = GetGoSvoxEnd $contents $functionStart

	$functionExists = Get-Command __goSvoxGetServers -ErrorAction SilentlyContinue

	ValidateGoSvoxBlock $functionStart $functionEnd $functionExists

	if($functionExists)
	{
		if(!(Get-SvoxClient))
		{
			throw "You are not connected to a Switchvox Server. Please connect first using GoSvox [<server>]."
		}

		$client = Get-SvxClient

		$servers = @(GetServers)

		if($servers | Where-Object Alias -EQ $Alias)
		{
			throw "Cannot set alias for server '$($client.Server)': a record with alias '$Alias' already exists. For more information see Get-GoSvoxServer."
		}

		#todo: when we update the aliases in the file, we need to also update them in memory!! is that even happening?

		$targetServer = @($servers | Where-Object {$_.Server -eq $client.Server -and $_.UserName -eq $client.UserName })

		if($targetServer.Count -eq 0)
		{
            $serverIgnoringUserName = @($servers | Where-Object {$_.Server -eq $client.Server})

            if($serverIgnoringUserName.Count -eq 0)
            {
                throw "Server '$($client.Server)' is not a valid GoSvox server. To install this server, run Install-GoSvoxServer [<alias>]"
            }
            else
            {
                throw "Server '$($client.Server)' is a valid GoSvox server, however you are not authenticated as a valid user for this server. To modify this server, first run GoSvox [<alias>], then re-run the original command"
            }
		}
		elseif($targetServer.Count -eq 1)
		{
			if(!$Alias -and $targetServer.Alias -ne $null)
			{
				if(($servers | Where-Object {$_.Server -eq $client.Server}).Count -gt 1)
				{
					throw "Cannot remove alias of server: multiple entries for server '$($client.Server)' are stored within GoSvox. To remove this alias uninstall all other entries for this server. For more information see Get-GoSvoxServer."
				}
			}

			SetSvoxGoAliasInternal $contents $targetServer $functionStart $functionEnd $Alias
		}
		elseif($targetServer.Count -gt 1)
		{
			throw "A critical error has occurred: more than one server exists with the same server and username. Please remove the duplicate from your $Profile manually."
		}
	}
	else
	{
		throw "GoSvox is not installed. Run Install-GoSvoxServer <alias> to install a server with the specified alias."
	}
}

function SetSvoxGoAliasInternal($contents, $server, $functionStart, $functionEnd, $Alias)
{
	$new = @()

	for($i = $functionStart + 3; $i -le $functionEnd -3; $i++)
	{
		$line = $contents[$i]

		$compare = $null

		if($null -ne $server.Alias -and $server.Alias -ne "")
		{
			$compare = "    `"```````"$($server.Server)```````",```````"$($server.Alias)```````",```````"$($server.UserName)```````",```````"$($server.Password)```````"`"*"
		}
		else
		{
			$compare = "    `"```````"$($server.Server)```````",,```````"$($server.UserName)```````",```````"$($server.Password)```````"`"*"
		}

		if($line -like $compare)
		{
			$newLine = $null

			if($null -ne $Alias -and $Alias -ne "")
			{
				$newLine = "    `"```"$($server.Server)```",```"$Alias```",```"$($server.UserName)```",```"$($server.Password)```"`""
			}
			else
			{
				$newLine = "    `"```"$($server.Server)```",,```"$($server.UserName)```",```"$($server.Password)```"`""
			}

			if($line.EndsWith(","))
			{
				$newLine += ","
			}

			$new += $newLine
		}
		else
		{
			$new += $line
		}		
	}

	$final = AddGoSvoxFunctionHeaderAndFooter $contents $new $functionStart $functionEnd

	UpdateGoSvoxFunctionBody $final $contents $functionStart $functionEnd

	.([ScriptBlock]::Create(($final -replace "function ","function global:")))
}

#region Install-GoSvoxServer

function Install-GoSvoxServer
{
	[CmdletBinding()]
	param (
		[Parameter(Mandatory = $false, Position = 0)]
		[string]
		$Alias
	)

	if(!(Get-SvxClient))
	{
		throw "You are not connected to a Switchvox Server. Please connect first using Connect-SvxServer."
	}

	$new = $false

	if(!(Test-Path $Profile))
	{
		New-Item $Profile -Type File -Force
		$new = $true
	}

	#todo: what is the force parameter meant to do? overwrite? if so we need to update some of the existing exception messages

    $newContents = GetNewContents $Alias
	$profileContent = GetProfileContents $newContents

    if($new)
    {
        Add-Content $Profile $profileContent
    }
    else
    {
        Set-Content $Profile $profileContent
    }

    .([ScriptBlock]::Create(($newContents.Func -replace "function ","function global:")))
}

function GetNewContents($alias)
{
    $contents = Get-Content $Profile

	$functionStart = GetGoSvoxStart $contents
	$functionEnd = GetGoSvoxEnd $contents $functionStart

	$functionExists = Get-Command __goSvoxGetServers -ErrorAction SilentlyContinue

	ValidateGoSvoxBlock $functionStart $functionEnd $functionExists

	# If the function didn't exist but the header and footer did, an exception would have been thrown above.
	# We can infer then that if we've reached this stage and the function doesn't exist, the header and footer don't exist either

	if($functionExists)
	{
		$servers = @(GetServers)

		CheckServerAgainstExistingRecords $servers $alias

        $pre = GetPre $contents $functionStart

        $post = GetPost $contents $functionEnd

        $new = GetNew $servers $alias
		
        $obj = ContentsObj
        $obj.Pre = $pre
        $obj.Func = $new
        $obj.Post = $post

        return $obj
	}
	else
	{
		$new = GetNew $null $alias

        $obj = ContentsObj
        $obj.Pre = $contents
        $obj.Func = $new
        $obj.Post = $null

        return $obj
	}
}

function ValidateGoSvoxBlock
{
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute("PSPossibleIncorrectComparisonWithNull", "", Scope="Function")]
	param($functionStart, $functionEnd, $functionExists)

	if($functionStart -eq $null -and $functionEnd -ne $null)
	{
		throw "GoSvox Servers start line '########################### Start GoSvox Servers ###########################' has been removed from PowerShell profile. Please reinstate line or remove all lines pertaining to GoSvox from your profile."
	}

	if($functionStart -ne $null -and $functionEnd -eq $null)
	{
		throw "GoSvox Servers end line '############################ End GoSvox Servers ############################' has been removed from PowerShell profile. Please reinstate line or remove all lines pertaining to GoSvox from your profile."
	}

	if($functionStart -ne $null -and $functionEnd -ne $null)
	{
		if(!$functionExists)
		{
			throw "GoSvox header and footer are present in PowerShell profile, however __goSvoxGetServers function was not loaded into the current session. Please verify the function has not been corrupted or remove the GoSvox header and footer and re-run Install-GoSvoxServer."
		}
	}
}

function GetServers
{
	$servers = __goSvoxGetServers | ConvertFrom-Csv -Header "Server","Alias","UserName","Password"

	foreach($server in $servers)
	{
		if($server.Alias -eq "")
		{
			$server.Alias = $null
		}
	}

	return $servers
}

function GetProfileContents($newContents)
{
	$funcBody = ""

    if($null -ne $newContents.Pre)
    {
        $funcBody += [string]::Join("`r`n", $newContents.Pre) + "`r`n"
    }

	# Due to the way Powershell's Command Discovery works, PowerShell will detect match the Get-GoSvox alias to the command GoSvox
	# before detecting GoSvox is in fact a valid alias all by itself. By forcing the module to import, PowerShell will correctly
	# identify that GoSvox is the intended command.

    $funcBody += "########################### Start GoSvox Servers ###########################`r`n`r`n" + 
				 [string]::Join("`r`n", $newContents.Func) +
				 "`r`n`r`n############################ End GoSvox Servers ############################"				

    if($null -ne $newContents.Post)
    {
        $funcBody += "`r`n" + [string]::Join("`r`n", $newContents.Post)
    }

	return $funcBody
}

function CheckServerAgainstExistingRecords($servers, $alias)
{
	$client = Get-SvxClient

	if($servers | Where-Object {$_.Server -eq $client.Server })
	{
		$record = $servers | Where-Object {$_.Server -eq $client.Server } | Select-Object -first 1

		if($null -ne $record.Alias)
		{
			if($record.UserName -eq $client.UserName)
			{
				throw "Cannot add server '$($client.Server)': a record for user '$($client.UserName)' already exists. To update the alias of this record use Set-GoSvoxAlias. To reinstall this record, first uninstall with Uninstall-GoSvoxServer and then re-run Install-GoSvoxServer."
			}

			if(!$alias)
			{
				throw "Cannot add server '$($client.Server)': an alias must be specified to differentiate this connection from an existing connection with the same server address."
			}
		}
		else
		{
			if($record.UserName -eq $client.UserName)
			{
				throw "Cannot add server '$($client.Server)': a record for user '$($client.UserName)' already exists."
			}

			throw "Cannot add server '$($client.Server)': a record for server already exists without an alias. Please update the alias of this record with Set-GoSvoxAlias and try again."
		}
	}
	elseif($servers | Where-Object {$_.Alias -eq $alias})
	{
		throw "Cannot add server '$($client.Server)' with alias '$alias': a record for this alias already exists. For more information see 'Get-GoSvoxServer $alias'"
	}
}

function GetGoSvoxStart($contents)
{
    for($i = 0; $i -lt $contents.Length; $i++)
	{
		if($contents[$i] -eq "########################### Start GoSvox Servers ###########################")
		{
			return $i
		}
	}
    
    return $null
}

function GetGoSvoxEnd($contents)
{
    if($functionStart -ne -1)
	{
		for($i = $functionStart + 1; $i -lt $contents.Length; $i++)
		{
			if($contents[$i] -eq "############################ End GoSvox Servers ############################")
			{
				return $i
			}
		}
	}

    return $null
}

function GetPre($contents, $functionStart)
{
    $index = 0

	$pre = $contents | ForEach-Object {
		if($index -lt $functionStart)
		{
			$_
		}

		$index++
    }

    return $pre
}

function GetPost($contents, $functionEnd)
{
    $index = 0

	$post = $contents | ForEach-Object {
		if($index -gt $functionEnd)
		{
			return $_
		}

		$index++
    }

    return $post
}

function GetNew
{
	[Diagnostics.CodeAnalysis.SuppressMessageAttribute("PSAvoidUsingConvertToSecureStringWithPlainText", "", Scope="Function")]
	param($servers, $alias)

    $client = Get-SvxClient

    $secureString = ConvertTo-SecureString $client.Password -AsPlainText -Force
	$encryptedString = ConvertFrom-SecureString $secureString
    #$encryptedString = $client.PassHash

    if(!$servers)
    {
        $servers = @()
    }

	$newRecord = SerializeRecord $client.Server $alias $client.UserName $encryptedString

	$newRecord = $newRecord -replace "`"```"","`"" -replace "```"`"","`"" -replace "```"","`""

	$r = $newRecord | ConvertFrom-Csv -Header "Server","Alias","UserName","Password"
	
	if($r.Alias -eq "")
	{
		$r.Alias = $null
	}

	<#$obj = New-Object PSObject
	$obj | Add-Member NoteProperty Server $client.Server
	$obj | Add-Member NoteProperty Alias $alias
	$obj | Add-Member NoteProperty UserName $client.UserName
	$obj | Add-Member NoteProperty PassHash $encryptedString#>

	$servers += $r

    $newContents = @()

    $newContents += "function __goSvoxGetServers {@("

    $index = 0
    $count = $servers.Count

    foreach($s in $servers)
    {
        $toAdd = SerializeRecord $s.Server $s.Alias $s.UserName $s.Password

        if($count -gt 1 -and ($index -lt $count - 1))
        {
            $toAdd += ","
        }

        $newContents += $toAdd

        $index++
    }

	$newContents += ")}"

    return $newContents
}

function SerializeRecord($server, $alias, $username, $password)
{
	$record = $null

	if($alias)
	{
		$record = "    `"```"$server```",```"$alias```",```"$username```",```"$password```"`""
	}
	else
	{
		$record = "    `"```"$server```",,```"$username```",```"$password```"`""
	}

	return $record
}

function ContentsObj
{
    $obj = @{
        "Pre" = @();
        "Func" = @();
        "Post" = @()
    }

    return $obj
}

#endregion Install-GoSvoxServer
#region Uninstall-GoSvoxServer

function Uninstall-GoSvoxServer([string]$Server, [switch]$Force)
{
	if(!(Test-Path $Profile))
	{
		throw "No GoSvox servers are installed. To install a GoSvox server, run Install-GoSvoxServer."
	}

	$contents = Get-Content $Profile

	$functionStart = GetGoSvoxStart $contents
	$functionEnd = GetGoSvoxEnd $contents $functionStart

	$functionExists = Get-Command __goSvoxGetServers -ErrorAction SilentlyContinue

	ValidateGoSvoxBlock $functionStart $functionEnd $functionExists

    if($functionExists)
    {
        $servers = @(GetServers)

        $Server = SetServerWildcardIfMissing $Server $servers $Force
        $matches = @(GetMatches $Server $servers)

        $filtered = GetFiltered $contents $matches $functionStart $functionEnd

		if($filtered)
		{
			$filtered = AddGoSvoxFunctionHeaderAndFooter $contents $filtered $functionStart $functionEnd
		}

        UpdateGoSvoxFunctionBody $filtered $contents $functionStart $functionEnd

		if($null -ne $filtered)
		{
			.([ScriptBlock]::Create(($filtered -replace "function ","function global:")))
		}
		else
		{
			Remove-Item Function:\__goSvoxGetServers
		}
    }
    else
    {
        throw "No GoSvox servers are installed. To install a GoSvox server, run Install-GoSvoxServer."
    }
}

function SetServerWildcardIfMissing($server, $servers, $force)
{
	if($force)
	{
		$server = "*"
	}
	else
	{
		if(!$server)
		{
			if($servers.Count -gt 1)
			{
				throw "Cannot remove servers; server name or alias must be specified when multiple entries exist. To remove all servers, specify -Force"
			}
			else
			{
				$server = "*"
			}
		}
	}

    return $server
}

function GetMatches($server, $servers)
{
	$matches = @()

	foreach($s in $servers)
	{
		if($s.Server -like $server)
        {
            $matches += $s
        }
        else
        {
            if($null -ne $s.Alias)
            {
                if($s.Alias -like $server) # the alias
                {
                    $matches += $s
                }
            }
        }
	}

    if(!$matches)
    {
        throw "'$server' is not a valid server name or alias. To view all saved servers, run Get-GoSvoxServer"
    }

    return $matches
}

function GetFiltered($contents, $matches, $functionStart, $functionEnd)
{
    $filtered = @()

    for($i = $functionStart + 3; $i -le $functionEnd - 3; $i++)
    {
        $line = $contents[$i]

        $include = $true

        foreach($s in $matches)
        {
            $toRemove = $null

            if($null -ne $s.Alias)
            {
                $toRemove = "    `"```````"$($s.Server)```````",```````"$($s.Alias)```````",```````"$($s.UserName)```````",```````"$($s.Password)```````"`"*" #do we need twice as many backticks?
            }
            else
            {
                $toRemove = "    `"```````"$($s.Server)```````",,```````"$($s.UserName)```````",```````"$($s.Password)```````"`"*"
            }

            if($line -like $toRemove)
            {
                $include = $false
                break
            }
        }

        if($include)
        {
            $filtered += $line
        }
    }

    if($filtered)
    {
        $filtered = AdjustFilterRecordDelimiters $contents $filtered $functionStart $functionEnd
    }

    return $filtered
}

function AdjustFilterRecordDelimiters($contents, $filtered, $functionStart, $functionEnd)
{
    for($i = 0; $i -lt $filtered.Count; $i++)
    {
        if($i -lt $filtered.Count - 1)
        {
            if(!$filtered[$i].EndsWith(","))
            {
                $filtered[$i] = $filtered[$i] + ","
            }
        }
        else
        {
            if($filtered[$i].EndsWith(","))
            {
                $filtered[$i] = $filtered[$i].Substring(0, $filtered[$i].Length - 1)
            }
        }
    }

    return $filtered
}

function AddGoSvoxHeaderAndFooter($contents, $funcBody, $functionStart, $functionEnd)
{
	$formatted = @()
    $formatted += $contents[$functionStart]
	$formatted += $contents[$functionStart + 1]
    $formatted += $funcBody
	$formatted += $contents[$functionEnd - 1]
    $formatted += $contents[$functionEnd]

	return $formatted
}

function AddGoSvoxFunctionHeaderAndFooter($contents, $funcBody, $functionStart, $functionEnd)
{
	$formatted = @()
	$formatted += $contents[$functionStart + 2]
    $formatted += $funcBody
	$formatted += $contents[$functionEnd - 2]

	return $formatted
}

function UpdateGoSvoxFunctionBody($funcBody, $contents, $functionStart, $functionEnd)
{
	$pre = GetPre $contents $functionStart
    $post = GetPost $contents $functionEnd

    $newContent = @()
    $newContent += $pre

    if($null -ne $funcBody)
    {
		$funcBody = AddGoSvoxHeaderAndFooter $contents $funcBody $functionStart $functionEnd
        $newContent += $funcBody
    }

    $newContent += $post

    $str = [string]::Join("`r`n", $newContent)

    if($str -eq "")
	{
		Set-Content $Profile $str -NoNewline
	}
	else
	{
		Set-Content $Profile $str
	}
}

#endregion

function Write-ColorOutput($Object, $ForegroundColor)
{
	$initialColor = $Host.UI.RawUI.ForegroundColor

	if($initialColor -ne -1)
	{
		$Host.UI.RawUI.ForegroundColor = $ForegroundColor
	}

	$Object | Write-Output

	if($initialColor -ne -1)
	{
		$Host.UI.RawUI.ForegroundColor = $initialColor
	}

	#need to test piping something with a header
}


#todo: all of these tests should test extracting the passhash and confirming it decrypts to the original value


#todo: run invoke-scriptanalyzer when finished

#todo: make the passhash encrypted again, change the should be's to should belike's

#todo: if you remove the last server, the second last server should remove its semicolon

#make nuget.org and powershellgallery nuspecs able to show whats new by querying appveyor
#push notes

#maybe we should consider using the password vault?
#https://www.powershellgallery.com/packages/BetterCredentials/4.3/Content/BetterCredentials.psm1

#note: if we use the windows credential store, our module wont be cross platform compatible

Export-ModuleMember -Function Install-GoSvoxServer,Uninstall-GoSvoxServer,Get-GoSvoxServer,Connect-GoSvoxServer,Set-GoSvoxAlias