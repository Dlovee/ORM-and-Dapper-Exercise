using System.Data;
using Dapper;

namespace ORM_Dapper;

public class DapperProductRepository : IProductRepository
{
    private readonly IDbConnection _conn;

    public DapperProductRepository(IDbConnection conn)
    {
        _conn = conn;
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _conn.Query<Product>("SELECT * FROM products;");
    }

    public Product GetProduct(int id)
    {
        return _conn.QueryFirstOrDefault<Product>("SELECT * FROM products WHERE ProductID = @id;", 
            new { id = id});
    }

    public void UpdateProduct(Product product)
    {
        _conn.Execute(@"UPDATE products 
                    SET Name = @Name, 
                        Price = @Price, 
                        CategoryID = @CategoryID, 
                        OnSale = @OnSale, 
                        StockLevel = @StockLevel 
                    WHERE ProductID = @ProductID",
            product);
    }
    

    public void CreateProduct(string name, double price, int categoryID)
    {
        _conn.Execute("INSERT INTO products (Name, Price, CategoryID) VALUES (@name, @price, @categoryID);",
            new { Name = name, Price = price, CategoryID = categoryID });
    }

    public void DeleteProduct(int id)
    {
        _conn.Execute("DELETE FROM sales WHERE ProductID = @id;", new {id = id});
        _conn.Execute("DELETE FROM reviews WHERE ProductID = @id;",new {id = id});
        _conn.Execute("DELETE FROM products WHERE ProductID = @id;", new {id = id});
    }
    
    public void DeleteProductByName(string name)
    {
        _conn.Execute("DELETE FROM sales WHERE ProductID IN (SELECT ProductID FROM products WHERE Name = @name);", new { name });
        _conn.Execute("DELETE FROM reviews WHERE ProductID IN (SELECT ProductID FROM products WHERE Name = @name);", new { name });
        _conn.Execute("DELETE FROM products WHERE Name = @name;", new { name });
    }

}