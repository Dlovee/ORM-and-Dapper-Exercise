﻿using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace ORM_Dapper
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string connString = config.GetConnectionString("DefaultConnection");

            IDbConnection conn = new MySqlConnection(connString);
            
            #region Department Section
            var repo = new DapperDepartmentRepository(conn);
            Console.WriteLine("Here are the current departments:");
            Console.WriteLine("Please press enter...");
            
            var newDepartment = Console.ReadLine();
            
            repo.InsertDepartment(newDepartment);
            
            var departments = repo.GetAllDepartments();
            
            foreach (var dept in departments)
            {
                Console.WriteLine(dept.Name);
            }
            #endregion

            var productRepository = new DapperProductRepository(conn);
            
             //var productToUpdate = productRepository.GetProduct(940);
            //
            // productToUpdate.Name = "UPDATED!!!";
            // productToUpdate.Price = 12.99;
            // productToUpdate.CategoryID = 1;
            // productToUpdate.OnSale = false;
            // productToUpdate.StockLevel = 1000;
            //
            // productRepository.UpdateProduct(productToUpdate);

            productRepository.CreateProduct("newStuff", 20, 1);
            productRepository.DeleteProductByName("newStuff");
            productRepository.CreateProduct("newStuff", 20, 1);

            
            var products = productRepository.GetAllProducts();
            foreach (var product in products)
            {
                Console.WriteLine(product.ProductID);
                Console.WriteLine(product.Name);
                Console.WriteLine(product.Price);
                Console.WriteLine(product.CategoryID);
                Console.WriteLine(product.OnSale);
                Console.WriteLine(product.StockLevel);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}