namespace ERP.API.Endpoints.Request;

internal record CreateAddressRequest(string Street, string City, string State, string Country, string ZipCode);