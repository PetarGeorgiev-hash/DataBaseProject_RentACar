using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.DTOs;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ModelController : ControllerBase
    {
        DataContextEF _ef;
        IMapper _mapper;

        public ModelController(IConfiguration configuration)
        {
            _ef = new(configuration);
            _mapper = new Mapper(new MapperConfiguration(config =>
            {
                config.CreateMap<ModelDto, Model>();
                config.CreateMap<EditModelDto, Model>();


            }));
        }

        [HttpPost("AddModel")]
        public IActionResult AddPrice(ModelDto modelDto)
        {
            Category? category = _ef.Find<Category>(modelDto.CategoryId);
            if (category != null)
            {
                Model model = _mapper.Map<Model>(modelDto);
                model.Category = category;
                _ef.Add(model);
                if (_ef.SaveChanges() > 0)
                {
                    return Ok();
                }
                throw new Exception("Failed to add new Model");
            }
            throw new Exception("Category does not exist");

        }

        [HttpGet("GetAllModels")]
        public List<Model> GetAllModels()
        {
            return _ef.Models.ToList();
        }


        [HttpGet("GetSingleModel/{modelId}")]
        public Model GetAllModels(Guid modelId)
        {
            Model? model = _ef.Models.Find(modelId);
            if (model != null)
            {
                return model;
            }
            throw new Exception("Failed to find Model");

        }

        [HttpPut("EditModel")]
        public Model EditModel(EditModelDto editModelDto)
        {
            Model? model = _ef.Models.Find(editModelDto.Id);
            Category? category = _ef.Category.Find(editModelDto.CategoryId);
            if (model != null && category != null)
            {
                _mapper.Map(editModelDto, model);
                model.Category = category;
                if (_ef.SaveChanges() > 0)
                {
                    return model;
                }
            }
            throw new Exception("Failed to edit Model");
        }

        [HttpDelete("DeleteModel/{modelId}")]
        public IActionResult DeleteModel(Guid modelId)
        {
            Model? model = _ef.Models.Find(modelId);
            if (model != null)
            {
                _ef.Models.Remove(model);
                if (_ef.SaveChanges() > 0)
                {
                    return Ok();
                }
            }
            throw new Exception("Failed to delete Model");
        }
    }
}