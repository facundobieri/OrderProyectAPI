using Application.Models.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ProductService
    {
        private readonly DollarApiService _dollarApiService;

        public ProductService(DollarApiService dollarApiService)
        {
            _dollarApiService = dollarApiService;
        }

        public async Task<IEnumerable<ProductResponseDto>> GetProductsAsync()
        {
            // Simulación de productos en memoria
            var productos = new List<Product>
            {
                new Product { Id = 1, Nombre = "Hamburguesa Clásica", PrecioArs = 6500 },
                new Product { Id = 2, Nombre = "Hamburguesa Doble", PrecioArs = 8500 }
            };

            var dollarRate = await _dollarApiService.GetOfficialDollarRateAsync();

            if (dollarRate == null)
                dollarRate = 1; // fallback, evita división por null

            return productos.Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                PrecioArs = p.PrecioArs,
                PrecioUsd = Math.Round(p.PrecioArs / dollarRate.Value, 2)
            });
        }
    }
}
