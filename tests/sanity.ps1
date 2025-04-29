$baseUrl = "http://localhost:5119"

# 1. Create a new user
$userBody = @{
    username = "string"
    password = "String@123"
    phone    = "911"
    email    = "string@string123.CO"
    status   = 1
    role     = 1
} | ConvertTo-Json -Depth 5

$userResponse = Invoke-RestMethod -Method POST -Uri "$baseUrl/api/Users" -Body $userBody -ContentType "application/json"
Write-Host "User created"

# 2. Authenticate
$authBody = @{
    email    = "string@string123.CO"
    password = "String@123"
} | ConvertTo-Json

$authResponse = Invoke-RestMethod -Method POST -Uri "$baseUrl/api/Auth" -Body $authBody -ContentType "application/json"
$token = $authResponse.data.data.token
Write-Host "Token acquired"

$headers = @{
    Authorization = "Bearer $token"
}

# 3. Create Sale
$saleBody = @{
    saleNumber = 100
    branch     = "string 1"
    items      = @(
        @{ productId = "3fa85f64-5717-4562-b3fc-2c963f66afa6"; quantity = 5; unitPrice = 1000 },
        @{ productId = "3fa85f64-5717-4562-b3fc-2c963f66afa3"; quantity = 30; unitPrice = 1000 }
    )
} | ConvertTo-Json -Depth 5

$saleResponse = Invoke-RestMethod -Method POST -Uri "$baseUrl/api/v1.0/Sales" -Body $saleBody -Headers $headers -ContentType "application/json"
$saleId = $saleResponse.id
Write-Host "Sale created with ID $saleId"

# 4. Get Sale By Id
$saleGetResponse = Invoke-WebRequest -Method GET -Uri "$baseUrl/api/v1.0/Sales/$saleId" -Headers $headers
if ($saleGetResponse.StatusCode -eq 200) {
    Write-Host "Sale retrieved successfully"
}

# 5. Update Sale
$updateBody = @{
    id              = $saleId
    saleNumber      = 1001
    totalSaleAmount = 135.00
    branch          = "string teste"
    items           = @(
        @{ productId = "3fa85f64-5717-4562-b3fc-2c963f66afa6"; quantity = 10; unitPrice = 10; discount = 0.10; totalAmount = 45.00 },
        @{ productId = "3fa85f64-5717-4562-b3fc-2c963f66afa3"; quantity = 1; unitPrice = 20; discount = 0.10; totalAmount = 90.00 }
    )
} | ConvertTo-Json -Depth 5

Invoke-RestMethod -Method PUT -Uri "$baseUrl/api/v1.0/Sales/$saleId" -Body $updateBody -Headers $headers -ContentType "application/json"
Write-Host "Sale updated"

# 6. Get Sale Again
$saleUpdatedResponse = Invoke-WebRequest -Method GET -Uri "$baseUrl/api/v1.0/Sales/$saleId" -Headers $headers
if ($saleUpdatedResponse.StatusCode -eq 200) {
    Write-Host "Updated sale retrieved successfully"
}

# 7. Delete Sale
Invoke-RestMethod -Method DELETE -Uri "$baseUrl/api/v1.0/Sales/$saleId" -Headers $headers
Write-Host "Sale deleted"

# 8. Try to Get Deleted Sale (expect 404)
try {
    Invoke-WebRequest -Method GET -Uri "$baseUrl/api/v1.0/Sales/$saleId" -Headers $headers -ErrorAction Stop
} catch {
    if ($_.Exception.Response.StatusCode.Value__ -eq 404) {
        Write-Host "Confirmed deletion: Sale not found (404)"
    } else {
        throw
    }
}
