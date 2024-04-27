using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.DTOs;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        DataContextEF _ef;
        IMapper _mapper;

        public CategoryController(IConfiguration configuration)
        {
            _ef = new(configuration);
            _mapper = new Mapper(new MapperConfiguration(config =>
            {
                config.CreateMap<CategoryDto, Category>();
            }));
        }

        [HttpPost("AddCategory")]
        public IActionResult AddCategory(CategoryDto categoryDto)
        {
            Category category = _mapper.Map<Category>(categoryDto);
            _ef.Add(category);
            if (_ef.SaveChanges() > 0)
            {
                return Ok();
            }
            throw new Exception("Failed to add Client");
        }

        [HttpGet("GetAllCategory")]
        public List<Category> GetAllBrands()
        {
            return _ef.Category.ToList();
        }

        [HttpGet("GetSingleCategory/{categoryId}")]
        public Category GetSingleBrands(Guid categoryId)
        {
            Category? category = _ef.Category.Where<Category>(cat => cat.Id == categoryId).FirstOrDefault();
            if (category != null)
            {
                return category;
            }
            throw new Exception("Category with this id does not exist");

        }

        [HttpPut("EditCategory")]
        public Category EditBrand(Category category)
        {
            Category? category1 = _ef.Category.Where(c => c.Id == category.Id).FirstOrDefault<Category>();
            if (category1 != null)
            {
                category1.Name = category.Name;
                if (_ef.SaveChanges() > 0)
                {
                    return category1;
                }
            }

            throw new Exception("Failed to edit Category");
        }

        [HttpDelete("DeleteCategory/{categoryId}")]
        public IActionResult DeleteBrand(Guid categoryId)
        {
            Category? category = _ef.Category.Find(categoryId);
            if (category != null)
            {
                _ef.Category.Remove(category);
                if (_ef.SaveChanges() > 0)
                {
                    return Ok();
                }
            }

            throw new Exception("Failed to edit Category");
        }

    }
}