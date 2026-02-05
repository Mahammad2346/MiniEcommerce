using AutoFixture;
using MiniEcommerce.BusinessLogicLayer.Dtos;
using MiniEcommerce.BusinessLogicLayer.Exceptions.Category;
using MiniEcommerce.BusinessLogicLayer.Exceptions.Product;
using MiniEcommerce.BusinessLogicLayer.Interfaces;
using MiniEcommerce.BusinessLogicLayer.Services;
using MiniEcommerce.Contracts.Entities;
using MiniEcommerce.Contracts.Interfaces;
using MiniEcommerce.DataAccessLayer;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MiniEcommerce.BusinessLogicLayer.Tests
{
    public class ProductServiceTest: ProductServiceTestBase
    {
        [Fact]

        public async Task CreateProduct_Valid_ShouldCreateProduct()
        {
            CategoryRepository.AnyAsync(Arg.Any<Expression<Func<Category, bool>>>(), Arg.Any<CancellationToken>()).Returns(true);
            ProductRepository.AnyAsync(Arg.Any<Expression<Func<Product, bool>>>(), Arg.Any<CancellationToken>()).Returns(false);

			UnitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

            var service = CreateService();
            var dto = Fixture.Create<CreateProductDto>();

            var result = await service.CreateProductAsync(dto, CancellationToken.None);

            Assert.NotNull(result);

            ProductRepository.Received(1).Add(Arg.Any<Product>());

            await UnitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
		}

        [Fact]

        public async Task CreateProduct_DuplicateName_ShouldThrowException()
        {
            CategoryRepository.AnyAsync(Arg.Any<Expression<Func<Category, bool>>>(), Arg.Any<CancellationToken>()).Returns(true);
            ProductRepository.AnyAsync(Arg.Any<Expression<Func<Product, bool>>>(), Arg.Any<CancellationToken>()).Returns(true);

            var service = CreateService();
			var dto = Fixture.Build<CreateProductDto>().With(x => x.Name, "Electronics").Create();

			await Assert.ThrowsAsync<ProductAlreadyExistsException>(() => service.CreateProductAsync(dto, CancellationToken.None));
		}

        [Fact]

        public async Task CreateProduct_CategoryNotFound_ShouldThrowException()
        {
            CategoryRepository.AnyAsync(Arg.Any<Expression<Func<Category, bool>>>(), Arg.Any<CancellationToken>()).Returns(false);

            var service = CreateService();
            var dto = Fixture.Create<CreateProductDto>();
            await Assert.ThrowsAsync<CategoryNotFoundException>(() => service.CreateProductAsync(dto, CancellationToken.None));
        }

        [Fact]
        public async Task UpdateProduct_Valid_ShouldUpdateProduct()
        {
			var product = new Product
			{
				Id = 1,
				Name = "Electronics",
				Price = 100,
				CategoryId = 1
			};
			var category = new Category
			{
				Id = 1,
				Name = "Test Category"
			};
			CategoryRepository.FirstOrDefaultAsync(Arg.Any<Expression<Func<Category, bool>>>(), Arg.Any<CancellationToken>()).Returns(category);
			CategoryRepository.AnyAsync(Arg.Any<Expression<Func<Category, bool>>>(), Arg.Any<CancellationToken>()).Returns(false);
			ProductRepository.FirstOrDefaultAsync(Arg.Any<Expression<Func<Product, bool>>>(), Arg.Any<CancellationToken>()).Returns(product);

			ProductRepository.AnyAsync(Arg.Any<Expression<Func<Product, bool>>>(), Arg.Any<CancellationToken>()).Returns(false);
			UnitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

			var service = CreateService();
			var dto = Fixture.Build<UpdateProductDto>().With(c=>c.Name, "Books").With(c=>c.CategoryId, 1).Create();

            var result = await service.UpdateProductAsync(1, dto, CancellationToken.None);  

            Assert.NotNull(result);
            Assert.Equal("Books", result.Name);
			await UnitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
		}

        [Fact]
        public async Task UpdateProduct_ProductNotFound_ShouldThrowException()
        {
			CategoryRepository.AnyAsync(Arg.Any<Expression<Func<Category, bool>>>(), Arg.Any<CancellationToken>()).Returns(true);
            ProductRepository.FirstOrDefaultAsync(Arg.Any<Expression<Func<Product, bool>>>(), Arg.Any<CancellationToken>()).Returns((Product?) null);

            var service = CreateService();
            var dto = Fixture.Create<UpdateProductDto>();

            await Assert.ThrowsAsync<ProductNotFoundException>(() => service.UpdateProductAsync(1, dto, CancellationToken.None));
		}

        [Fact]

        public async Task DeleteProduct_Valid_ShouldDeleteProduct()
        {
			var product = new Product
			{
				Id = 1,
				Name = "Electronics",
				Price = 100,
				CategoryId = 1
			};

			CategoryRepository.AnyAsync(Arg.Any<Expression<Func<Category, bool>>>(), Arg.Any<CancellationToken>()).Returns(true);
			ProductRepository.FirstOrDefaultAsync(Arg.Any<Expression<Func<Product, bool>>>(), Arg.Any<CancellationToken>()).Returns(product);
			UnitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);
			var service = CreateService();
            var result = await service.DeleteProductAsync(1, CancellationToken.None);
			Assert.NotNull(result);
			Assert.Equal("Electronics", result.Name);

			ProductRepository.Received(1).Delete(product);
			await UnitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
		}

        [Fact]

        public async Task DeleteProduct_ProductNotFound_ShouldThrowException()
        {
			ProductRepository.FirstOrDefaultAsync(Arg.Any<Expression<Func<Product, bool>>>(), Arg.Any<CancellationToken>()).Returns((Product?)null);

            var service = CreateService();

            await Assert.ThrowsAsync<ProductNotFoundException>(() => service.DeleteProductAsync(1, CancellationToken.None));
		}
	}
}
