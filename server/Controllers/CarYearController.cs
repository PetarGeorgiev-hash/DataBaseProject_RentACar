using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.DTOs;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarYearController : ControllerBase
    {
        DataContextEF _ef;
        IMapper _mapper;
        public CarYearController(IConfiguration configuration)
        {
            _ef = new(configuration);
            _mapper = new Mapper(new MapperConfiguration(config =>
            {
                config.CreateMap<CarYearDto, CarYear>();
            }));
        }

        [HttpPost("CreateCarYear")]
        public IActionResult CreateCarYear(CarYearDto carYearDto)
        {
            CarYear carYear = _mapper.Map<CarYear>(carYearDto);
            _ef.Add(carYear);
            if (_ef.SaveChanges() > 0)
            {
                return Ok();
            }
            throw new Exception("Failed to add CarYear");
        }

        [HttpGet("GetAllCarYears")]
        public List<CarYear> GetAllCarYears()
        {
            return _ef.CarYear.ToList<CarYear>();
        }
        [HttpGet("GetSingleCarYears/{carYearId}")]
        public CarYear GetSingleCarYears(Guid carYearId)
        {
            CarYear? carYear = _ef.CarYear.Find(carYearId);
            if (carYear != null)
            {
                return carYear;
            }
            throw new Exception("Failed to get CarYear");
        }

        [HttpPut("EditCarYear")]
        public CarYear EditCarYear(CarYear carYear)
        {
            CarYear? carYear1 = _ef.CarYear.Find(carYear.Id);
            if (carYear1 != null)
            {
                carYear1.Date = carYear.Date;
                if (_ef.SaveChanges() > 0)
                {
                    return carYear1;
                }
            }
            throw new Exception("Failed to edit CarYear");
        }

        [HttpDelete("DeleteCarYear/{carYearId}")]
        public IActionResult DeleteCarYear(Guid carYearId)
        {
            CarYear? carYear = _ef.CarYear.Find(carYearId);
            if (carYear != null)
            {
                _ef.CarYear.Remove(carYear);
                if (_ef.SaveChanges() > 0)
                {
                    return Ok();
                }
            }

            throw new Exception("Failed to delete CarYear");

        }

    }
}