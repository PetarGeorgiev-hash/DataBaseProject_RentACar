using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.DTOs;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PriceController : ControllerBase
    {
        DataContextEF _ef;
        IMapper _mapper;

        public PriceController(IConfiguration configuration)
        {
            _ef = new(configuration);
            _mapper = new Mapper(new MapperConfiguration(config =>
            {
                config.CreateMap<PriceDto, Price>();
            }));
        }

        [HttpPost("AddPrice")]
        public IActionResult AddPrice(PriceDto priceDto)
        {
            Price price = _mapper.Map<Price>(priceDto);
            _ef.Add(price);
            if (_ef.SaveChanges() > 0)
            {
                return Ok();
            }
            throw new Exception("Failed to add new Price");
        }

        [HttpGet("GetAllPrices")]

        public List<Price> GetAllPrices()
        {
            return _ef.Price.ToList();
        }

        [HttpGet("GetSinglePrice/{priceId}")]
        public Price GetSinglePrice(Guid priceId)
        {
            Price? price = _ef.Price.Find(priceId);
            if (price != null)
            {
                return price;
            }
            throw new Exception("Failed to get Price");
        }

        [HttpPut("EditPrice")]
        public Price EditPrice(Price price)
        {
            Price? price1 = _ef.Price.Find(price.Id);
            if (price1 != null)
            {
                price1.CarPrice = price.CarPrice;
                if (_ef.SaveChanges() > 0)
                {
                    return price1;
                }
            }
            throw new Exception("Failed to edit Price");
        }

        [HttpDelete("DeletePrice/{priceId}")]
        public IActionResult DeletePrice(Guid priceId)
        {
            Price? price = _ef.Price.Find(priceId);
            if (price != null)
            {
                _ef.Price.Remove(price);
                if (_ef.SaveChanges() > 0)
                {
                    return Ok();
                }
            }
            throw new Exception("Failed to delete price");

        }
    }
}