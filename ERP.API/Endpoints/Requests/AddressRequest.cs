namespace ERP.API.Endpoints.Request;

internal record AddressRequest(string Street, string City, string State, string Country, string ZipCode);