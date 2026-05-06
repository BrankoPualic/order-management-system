namespace ERP.API.Endpoints.Request;

internal record UpdateMoneyRequest(decimal? Amount, string? Currency);