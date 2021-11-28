using CatShopSolution.Application.Catalog.Products;
using CatShopSolution.ViewModels.Catalog.ProductImage;
using CatShopSolution.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatShopSolution.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _ProductService;

        public ProductsController( IProductService ProductService)
        {
            _ProductService = ProductService;
          
        }     
        //http://localhost:port/products?pageIndex=1&pageSize=10&CategoryId=
        //[HttpGet("{languageId}")]
        //public async Task<IActionResult> GetAllPaging(string languageId,[FromQuery] GetPublicProductPagingRequest request)
        //{
        //    var products = await _ProductService.GetAllByCategoryId(languageId,request);
        //    return Ok(products);
        //}

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetProductPadingRequest request)
        {
            var products = await _ProductService.GetAllPaging(request);
            return Ok(products);
        }
        //http://localhost:port/product/{productId}
        [HttpGet("{productId}/{languageId}")]
        public async Task<IActionResult> GetById (int productId,string languageId)
        {
            var product = await _ProductService.GetById(productId,languageId);
            if (product == null)
                return BadRequest("Cannot find product");
            return Ok(product);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult>Create([FromForm]ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _ProductService.Create(request);
            if (productId == 0)
                return BadRequest();
            var product = await _ProductService.GetById(productId,request.LanguageId);
            return CreatedAtAction(nameof(GetById), new { id = productId } , product);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _ProductService.Update(request);
            if (affectedResult == 0)
                return BadRequest();;
            return Ok();
        }


        [HttpDelete("productId")]
        public async Task<IActionResult> Delete(int productId )
        {
            var affectedResult = await _ProductService.Delete(productId);
            if (affectedResult == 0)
                return BadRequest(); ;
            return Ok();
        }

        [HttpPatch("{productId}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice( int productId,decimal newPrice)
        {          
            var isSuccess = await _ProductService.UpdatePrice(productId, newPrice);
            if (!isSuccess)
                return BadRequest(); ;
            return Ok();
        }

        //Image
        [HttpPost("{productId}/images")]
        public async Task<IActionResult> CreateImage(int productId ,[FromForm]ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var imageId = await _ProductService.AddImage(productId, request);
            if(imageId == 0)
            {
                return BadRequest();
            }
            var image = await _ProductService.GetImageById(imageId);
            return CreatedAtAction(nameof(GetImageById),new { id = imageId }, image);
        }
        [HttpPut("{productId}/images/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _ProductService.UpdateImage(imageId, request);
            if (result == 0)
                return BadRequest();
            return Ok();
        }
        [HttpDelete("{productId}/images/{imageId}")]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _ProductService.RemoveImage(imageId);
            if (result == 0)
                return BadRequest();
            return Ok();
        }
        [HttpGet("{productId}/image/{imageId}")]
        public async Task<IActionResult> GetImageById(int productId,int imageId)
        {
            var image = await _ProductService.GetImageById(imageId);
            if (image == null)
                return BadRequest();
            return Ok(image);
        }

        [HttpPut("{id}/categories")]
        public async Task<IActionResult> CategoryAssign(int id, [FromBody] CategoryAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _ProductService.CategoryAssign(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}
