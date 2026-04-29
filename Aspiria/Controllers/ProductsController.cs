using AutoMapper;
using Aspiria.DTOs;
using Aspiria.Models;
using Aspiria.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Aspiria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>Obtiene todos los productos</summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
        {
            var products = await _repository.GetAllAsync();

            if (products == null)
                return Ok(new List<ProductDto>());

            var result = _mapper.Map<IEnumerable<ProductDto>>(products);

            return Ok(result);
        }

        /// <summary>Obtiene un producto por Id</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(_mapper.Map<ProductDto>(product));
        }

        /// <summary>Crea un producto</summary>
        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var product = _mapper.Map<Product>(dto);
                var created = await _repository.AddAsync(product);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = created.Id },
                    _mapper.Map<ProductDto>(created)
                );
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>Actualiza un producto</summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = _mapper.Map<Product>(dto);
            product.Id = id;

            try
            {
                var updated = await _repository.UpdateAsync(product);

                if (!updated)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>Elimina un producto</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Id inválido");

            var ok = await _repository.DeleteAsync(id);

            if (!ok)
                return NotFound($"Producto con id {id} no encontrado");

            return NoContent();
        }
    }
}