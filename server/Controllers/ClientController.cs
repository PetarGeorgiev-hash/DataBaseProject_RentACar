using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.DTOs;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {

        DataContextEF _ef;
        IMapper _mapper;
        public ClientController(IConfiguration configuration)
        {
            _ef = new(configuration);
            _mapper = new Mapper(new MapperConfiguration(mapper =>
            {
                mapper.CreateMap<ClientDto, Client>();
                mapper.CreateMap<EditClientDto, Client>();

            }));

        }
        [HttpPost("AddClient")]
        public IActionResult AddClient(ClientDto clientDto)
        {
            Client client = _mapper.Map<Client>(clientDto);
            _ef.Add(client);
            if (_ef.SaveChanges() > 0)
            {
                return Ok();
            }
            throw new Exception("Failed to add Client");
        }


        [HttpGet("GetAllClients")]

        public List<Client> GetAllClients()
        {
            return _ef.Client.ToList();
        }

        [HttpGet("GetSingleClinet/{clientId}")]
        public Client GetSinglePrice(Guid clientId)
        {
            Client? client = _ef.Client.Find(clientId);
            if (client != null)
            {
                return client;
            }
            throw new Exception("Failed to get Client");
        }

        [HttpPut("EditClient")]
        public Client EditPrice(EditClientDto client)
        {
            Client? client1 = _ef.Client.Find(client.Id);
            if (client1 != null)
            {
                _mapper.Map(client, client1);
                if (_ef.SaveChanges() > 0)
                {
                    return client1;
                }
            }
            throw new Exception("Failed to edit Client");
        }

        [HttpDelete("DeleteClient/{clientId}")]
        public IActionResult DeletePrice(Guid clientId)
        {
            Client? client = _ef.Client.Find(clientId);
            if (client != null)
            {
                _ef.Client.Remove(client);
                if (_ef.SaveChanges() > 0)
                {
                    return Ok();
                }
            }
            throw new Exception("Failed to delete Client");

        }
    }
}