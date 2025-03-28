using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interface;

public interface IProductService
{
    Task AddProduct(Product product);
    Task<Product?> GetProduct(string id);
    Task<(List<Product> Items, long TotalCount)> GetPagedAsync(int page, int pageSize);
    Task<bool> UpdateProduct(string id, Product updated);
    Task<bool> DeleteProduct(string id);
}
