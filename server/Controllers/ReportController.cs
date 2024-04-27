using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Server.Data;
using Server.DTOs;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        DataContextEF _ef;
        IMapper _mapper;

        public ReportController(IConfiguration configuration)
        {
            _ef = new(configuration);
            _mapper = new Mapper(new MapperConfiguration(config =>
            {
                config.CreateMap<ReportDto, Report>();
                config.CreateMap<EditReportDto, Report>();

            }));
        }
        [HttpPost("AddReport")]
        public IActionResult AddReport(ReportDto reportDto)
        {
            CarsForRent? car = _ef.CarsForRent.Find(reportDto.CarsForRentId);
            Client? client = _ef.Client.Find(reportDto.ClientId);
            if (car != null && client != null && reportDto.DaysRented > 0)
            {
                Report report = new()
                {
                    CarsForRent = car,
                    Client = client,
                    DaysRented = reportDto.DaysRented
                };
                _ef.Add(report);

                if (_ef.SaveChanges() > 0)
                {
                    return Ok();
                }
            }

            throw new Exception("Failed to add new Report");
        }

        [HttpGet("GetAllReports")]

        public List<Report> GetAllPrices()
        {
            return _ef.Report.ToList();
        }

        [HttpGet("GetSingleReport/{reportId}")]
        public Report GetSinglePrice(Guid reportId)
        {
            Report? report = _ef.Report.Find(reportId);
            if (report != null)
            {
                return report;
            }
            throw new Exception("Failed to get Report");
        }

        [HttpPut("EditReport")]
        public Report EditPrice(EditReportDto editReportDto)
        {
            Report? report1 = _ef.Report.Find(editReportDto.Id);
            CarsForRent? car = _ef.CarsForRent.Find(editReportDto.CarsForRentId);
            Client? client = _ef.Client.Find(editReportDto.ClientId);


            if (report1 != null && car != null && client != null && editReportDto.DaysRented > 0)
            {
                report1.CarsForRent = car;
                report1.Client = client;
                report1.DaysRented = editReportDto.DaysRented;
                if (_ef.SaveChanges() > 0)
                {
                    return report1;
                }
            }
            throw new Exception("Failed to edit Report");
        }

        [HttpDelete("DeleteReport/{reportId}")]
        public IActionResult DeleteReport(Guid reportId)
        {
            Report? report = _ef.Report.Find(reportId);
            if (report != null)
            {
                _ef.Report.Remove(report);
                if (_ef.SaveChanges() > 0)
                {
                    return Ok();
                }
            }
            throw new Exception("Failed to delete price");

        }


        [HttpGet("MostActiveClient")]
        public Client MostActiveClient()
        {
            var clientWithMostOrders = _ef.Report
             .GroupBy(r => r.Client)
             .ToList()
             .OrderByDescending(g => g.Count())
             .FirstOrDefault()
             .Key;

            return clientWithMostOrders;
        }


        [HttpGet("LeastActiveClient")]
        public Client LeastActiveClient()
        {
            var clientWithLeastOrders = _ef.Report
             .GroupBy(r => r.Client)
             .ToList()
             .OrderByDescending(g => g.Count())
             .LastOrDefault()
             .Key;

            return clientWithLeastOrders;
        }

        [HttpGet("MostRentedCar")]
        public CarsForRent MostRentedCar()
        {
            var mostRentedCar = _ef.Report
            .GroupBy(r => r.CarsForRent)
            .ToList() // Client-side evaluation
            .OrderByDescending(g => g.Count())
            .FirstOrDefault()
            .Key;

            return mostRentedCar;
        }

        [HttpGet("LeastRentedCar")]
        public CarsForRent LeastRentedCar()
        {
            var leastRentedCar = _ef.Report
            .GroupBy(r => r.CarsForRent)
            .ToList() // Client-side evaluation
            .OrderByDescending(g => g.Count())
            .LastOrDefault()
            .Key;

            return leastRentedCar;
        }


    }
}