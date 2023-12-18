using AutoMapper;
using BusinessLayer.Services;
using DatabaseLayer.Model;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Filters;
using PresentationLayer.ViewModels;

namespace PresentationLayer.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;


    public ProductController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }


    [HttpGet]
    public async Task<IActionResult> EditProduct(int productId)
    {
        var product = await _productService.GetProductByIdAsync(productId);
        if (product == null)
            return View(null);
        var productVM = _mapper.Map<MyProductVM>(product);

        return View(productVM);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [PostPutActionFilter]
    public async Task<IActionResult> EditProduct(MyProductVM myProductVM)
    {
        var myProduct = _mapper.Map<MyProduct>(myProductVM);
        var updatedProduct = await _productService.UpdateProductAsync(myProduct);
        if (updatedProduct == null)
            return View(myProductVM);

        return RedirectToAction(nameof(ProductPage),new { productId =myProduct.ProductId});
    }
    [HttpGet]
    public async Task<IActionResult> DeleteProduct(int productId)
    {
        var deletedProduct = await _productService.DeleteProductAsync(productId);
        if (deletedProduct == null)
            return NotFound("Specified product is not found");

        return RedirectToAction(nameof(HomePage));
    }

    [HttpGet]
    public async Task<IActionResult> ProductPage(int productId)
    {
        var product = await _productService.GetProductByIdAsync(productId);
        if (product == null)
            return View(null);
        var productVM = _mapper.Map<MyProductVM>(product);
        return View(productVM);
    }

    [HttpGet]
    public async Task<IActionResult> HomePage()
    {
        var products = await _productService.GetAllProductsAsync();
        var productVMs = _mapper.Map<List<MyProductVM>>(products);
        var homePageVM = new HomePageVM
        {
            Products = productVMs
        };

        return View(homePageVM);
    }

    [HttpGet]
    public IActionResult CreateProduct() => View();
    [HttpPost]
    [ValidateAntiForgeryToken]
    [PostPutActionFilter]
    public async Task<IActionResult> CreateProduct(MyProductVM myProductVM)
    {
        var myProduct = _mapper.Map<MyProduct>(myProductVM);
        var createdProduct = await _productService.AddNewProductAsync(myProduct);
        if (createdProduct == null)
            return View(myProductVM);

        return RedirectToAction(nameof(HomePage));
    }

}
