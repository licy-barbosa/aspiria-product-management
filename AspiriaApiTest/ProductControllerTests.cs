using Microsoft.AspNetCore.Mvc;
using Aspiria.Repositories;
using Aspiria.Controllers;
using Aspiria.Models;
using Aspiria.DTOs;

namespace AspiriaApiTest
{
    [TestClass]
    public sealed class ProductControllerTests : DatabaseTest
    {
        // Obtener todos
        [TestMethod]
        public async Task GetAll_ShouldReturnProducts()
        {
            var context = BuildContext(Guid.NewGuid().ToString());
            var mapper = ConfigMapper();
            var controller = new ProductsController(new ProductRepository(context), mapper);

            context.Products.Add(new Product
            {
                Name = "Toy",
                Company = "A",
                Price = 50
            });

            context.SaveChanges();
      
            var result = await controller.Get();

            Assert.IsNotNull(result);

            var okResult = result.Result as OkObjectResult;

            Assert.IsNotNull(okResult);

            var data = okResult.Value as IEnumerable<ProductDto>;

            Assert.IsNotNull(data);

            Assert.AreEqual(expected: 1, actual: data.Count());
        }

        // Crear producto
        [TestMethod]
        public async Task CreateProduct_ShouldAddProduct()
        {
            var context = BuildContext(Guid.NewGuid().ToString());
            var mapper = ConfigMapper();
            var controller = new ProductsController(new ProductRepository(context), mapper);

            var dto = new CreateProductDto
            {
                Name = "Car",
                Company = "Mattel",
                Price = 100
            };

            var result = await controller.Create(dto);

            Assert.IsNotNull(result);

            var createdResult = result.Result as CreatedAtActionResult;

            Assert.IsNotNull(createdResult);

            var data = createdResult.Value as ProductDto;

            Assert.IsNotNull(data);
            Assert.AreEqual(dto.Name, data.Name);
        }

        // Obtener por Id
        [TestMethod]
        public async Task GetById_ShouldReturnProduct()
        {
            var context = BuildContext(Guid.NewGuid().ToString());
            var mapper = ConfigMapper();

            var product = new Product
            {
                Name = "Toy",
                Company = "A",
                Price = 50
            };

            context.Products.Add(product);
            context.SaveChanges();

            var controller = new ProductsController(new ProductRepository(context), mapper);

            var result = await controller.GetById(product.Id);

            Assert.IsNotNull(result);

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var data = okResult.Value as ProductDto;

            Assert.IsNotNull(data);
            Assert.AreEqual(product.Id, data.Id);
        }

        //Eliminar producto
        [TestMethod]
        public async Task Delete_ShouldRemoveProduct()
        {
            var context = BuildContext(Guid.NewGuid().ToString());
            var mapper = ConfigMapper();

            var product = new Product
            {
                Name = "Toy",
                Company = "A",
                Price = 50
            };

            context.Products.Add(product);
            context.SaveChanges();

            var controller = new ProductsController(new ProductRepository(context), mapper);

            var result = await controller.Delete(product.Id);

            Assert.IsNotNull(result);

            Assert.IsInstanceOfType(result, typeof(NoContentResult));

            Assert.AreEqual(0, context.Products.Count());
        }
    }
}