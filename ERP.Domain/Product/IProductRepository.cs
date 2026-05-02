using ERP.Domain.Shared;

namespace ERP.Domain;

public interface IProductRepository : IRepository<Product, Product.ProductId> { }