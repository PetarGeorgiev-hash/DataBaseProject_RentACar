using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Server.Data;
using Server.DTOs;
using Server.Models;
using System.Linq;


namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        DataContextEF _ef;
        IMapper _mapper;

        public CarController(IConfiguration configuration)
        {
            _ef = new(configuration);
            _mapper = new Mapper(new MapperConfiguration(config =>
            {
                config.CreateMap<CarDto, CarsForRent>();
                config.CreateMap<CarEditDto, CarsForRent>();

            }));
        }

        [HttpPost("AddCar")]
        public IActionResult AddCar(CarDto carDto)
        {
            Model? model = _ef.Find<Model>(carDto.ModelId);
            Brand? brand = _ef.Find<Brand>(carDto.BrandId);
            CarYear? carYear = _ef.Find<CarYear>(carDto.CarYearId);
            Price? price = _ef.Find<Price>(carDto.PriceId);

            if (model != null && brand != null && carYear != null && price != null)
            {
                CarsForRent car = _mapper.Map<CarsForRent>(carDto);
                car.Brand = brand;
                car.Model = model;
                car.CarYear = carYear;
                car.Price = price;
                _ef.CarsForRent.Add(car);
                if (_ef.SaveChanges() > 0)
                {
                    return Ok();
                }
            }

            throw new Exception("Failed to add new Brand");
        }

        [HttpGet("GetAllCars")]
        public List<CarsForRent> GetAllCars()
        {
            return _ef.CarsForRent.ToList<CarsForRent>();
        }

        [HttpGet("GetSingleCar/{carId}")]
        public CarsForRent GetSingleCar(Guid carId)
        {
            CarsForRent? car = _ef.CarsForRent.Find(carId);
            if (car != null)
            {
                return car;
            }
            throw new Exception("Failed to get Car");
        }

        [HttpPut("EditCar")]
        public CarsForRent EditCar(CarEditDto carEditDto)
        {
            Model? model = _ef.Find<Model>(carEditDto.ModelId);
            Brand? brand = _ef.Find<Brand>(carEditDto.BrandId);
            CarYear? carYear = _ef.Find<CarYear>(carEditDto.CarYearId);
            Price? price = _ef.Find<Price>(carEditDto.PriceId);
            CarsForRent? car = _ef.Find<CarsForRent>(carEditDto.Id);

            if (model != null && brand != null && carYear != null && price != null && car != null)
            {
                car.Brand = brand;
                car.Model = model;
                car.CarYear = carYear;
                car.Price = price;
                if (_ef.SaveChanges() > 0)
                {
                    return car;
                }
            }
            throw new Exception("Failed to edint Car");
        }

        [HttpDelete("DeleteCar/{carId}")]
        public IActionResult DeleteCar(Guid carId)
        {
            CarsForRent? car = _ef.Find<CarsForRent>(carId);
            if (car != null)
            {
                _ef.CarsForRent.Remove(car);
                if (_ef.SaveChanges() > 0)
                {
                    return Ok();
                }
            }
            throw new Exception("Failed to delete Car");

        }

        [HttpGet("HighestPriceCar")]
        public decimal MostExpensive()
        {

            return _ef.CarsForRent
                .Select(car => car.Price.CarPrice)
                .Max();


        }

        [HttpGet("LowestPriceCar")]
        public decimal LeastExpensive()
        {

            return _ef.CarsForRent
                .Select(car => car.Price.CarPrice)
                .Min();


        }

    }
}