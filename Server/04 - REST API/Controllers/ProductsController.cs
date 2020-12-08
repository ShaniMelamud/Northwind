using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Cool
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("EntireWorld")]
    public class ProductsController : ControllerBase, IDisposable
    {
        private readonly ProductsLogic logic;
        public ProductsController(ProductsLogic logic)
        {
            this.logic = logic;
        }
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                List<ProductModel> products = logic.GetAllProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOneProduct(int id)
        {
            try
            {
                ProductModel product = logic.GetOneProduct(id);
                if (product == null)
                    return NotFound($"id {id} not found");

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public IActionResult AddProduct(ProductModel productModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ErrorHelper.ExtractErrors(ModelState));
                ProductModel addedProduct = logic.AddProduct(productModel);
                return Created("api/products/" + addedProduct.ID, addedProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateFullProduct(int id, ProductModel productModel)
        {
            try
            {
                productModel.ID = id;
                ProductModel updatedProduct = logic.UpdateFullProduct(productModel);
                if (updatedProduct == null)
                    return NotFound($"id {id} not found");

                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPatch]
        [Route("{id}")]
        public IActionResult UpdatePartialProduct(int id, ProductModel productModel)
        {
            try
            {
                productModel.ID = id;
                ProductModel updatedProduct = logic.UpdatePartialProduct(productModel);
                if (updatedProduct == null)
                    return NotFound($"id {id} not found");

                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            logic.DeleteProduct(id);
            return NoContent();
        }
        [HttpGet]
        [Route("cheaper-than/{price}")]
        public IActionResult GetProductsCheaperThan(decimal price)
        {
            try
            {
                List<ProductModel> products = logic.GetProductsCheaperThan(price);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("expensive-than")]
        public IActionResult GetProductsExpensiveThan(decimal price)
        {
            try
            {
                List<ProductModel> products = logic.GetProductsExpensiveThan(price);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        public void Dispose()
        {
            logic.Dispose();
        }
    }
}