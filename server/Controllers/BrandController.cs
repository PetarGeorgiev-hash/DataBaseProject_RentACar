using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.DTOs;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandController : ControllerBase
    {
        DataContextEF _ef;
        IMapper _mapper;

        public BrandController(IConfiguration configuration)
        {
            _ef = new(configuration);
            _mapper = new Mapper(new MapperConfiguration(config =>
            {
                config.CreateMap<BrandDto, Brand>();
            }));
        }

        [HttpPost("AddBrand")]
        public IActionResult AddBrand(BrandDto brandDto)
        {
            Brand brand = _mapper.Map<Brand>(brandDto);
            _ef.Add(brand);
            if (_ef.SaveChanges() > 0)
            {
                return Created();
            }
            throw new Exception("Failed to add new Brand");
        }

        [HttpGet("GetAllBrands")]
        public List<Brand> GetAllBrands()
        {
            return _ef.Brand.ToList();
        }

        [HttpGet("GetSingleBrand/{brandId}")]
        public Brand GetSingleBrands(string brandId)
        {
            Brand? brand = _ef.Brand.Where<Brand>(brand => brand.Id.ToString() == brandId).FirstOrDefault();
            if (brand != null)
            {
                return brand;
            }
            throw new Exception("Brand with this id does not exist");

        }

        [HttpPut("EditBrand")]
        public Brand EditBrand(Brand brand)
        {
            Brand? brand2 = _ef.Brand.Where(b => b.Id == brand.Id).FirstOrDefault<Brand>();
            if (brand2 != null)
            {
                brand2.Name = brand.Name;
                if (_ef.SaveChanges() > 0)
                {
                    return brand;
                }
            }

            throw new Exception("Failed to edit Brand");
        }

        [HttpDelete("DeleteBrand/{brandId}")]
        public IActionResult DeleteBrand(Guid brandId)
        {
            Brand? brand = _ef.Brand.Find(brandId);
            if (brand != null)
            {
                _ef.Brand.Remove(brand);
                if (_ef.SaveChanges() > 0)
                {
                    return Ok();
                }
            }

            throw new Exception("Failed to edit Brand");
        }
    }
}