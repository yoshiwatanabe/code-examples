function Test-SQLDatabaseReadAccess
{
    Param
    (
        $ServerInstance,
        $database,
        $table
    )

    $Credential = Get-Credential

    $conn = New-Object System.Data.SqlClient.SQLConnection
    $conn.ConnectionString = "Server={0};Database={1};User ID={2};Password={3};Trusted_Connection=True;Connect Timeout={4}" `
        -f $ServerInstance,$Database,$Credential.UserName,$Credential.GetNetworkCredential().Password,3000

    Try
    {
        $conn.Open()
        "Connection opened successfully"

        $command = $conn.CreateCommand()
        $command.CommandText = `
        "use $database
         SELECT TOP 10 * FROM $table"

        $dataTable = New-Object System.Data.DataTable
        $dataTable.Load($command.ExecuteReader())

        $json = $dataTable | ConvertTo-Json
        $json
    }
    Catch
    {
        Write-Error $_
        continue
    }
    finally
    {
        $conn.Close()
    }
}
