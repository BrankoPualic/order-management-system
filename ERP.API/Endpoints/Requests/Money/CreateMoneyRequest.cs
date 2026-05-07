namespace ERP.API.Endpoints.Request;

internal record CreateMoneyRequest(decimal Amount, CreateCurrencyRequest Currency);