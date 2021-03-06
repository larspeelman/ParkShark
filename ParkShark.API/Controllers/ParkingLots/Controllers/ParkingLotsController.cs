﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkShark.API.Controllers.ParkingLots.DTO;
using ParkShark.API.Controllers.ParkingLots.Mappers.Interfaces;
using ParkShark.Services.ParkingLots.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkShark.API.Controllers.ParkingLots
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingLotsController : ControllerBase
    {

        private readonly IParkingLotMapper _parkingLotMapper;
        private readonly IParkingLotService _parkingLotService;

        public ParkingLotsController(IParkingLotMapper parkingLotMapper, IParkingLotService parkingLotService)
        {
            _parkingLotMapper = parkingLotMapper;
            _parkingLotService = parkingLotService;
        }

        // GET: api/ParkingLots
        [HttpGet]
        public ActionResult<List<ParkingLotDTO_Return>> GetAllParkingLots()
        {
            return Ok(_parkingLotMapper.CreateListOfParkingLotDTOReturnsFromParkingLotList(_parkingLotService.GetAllParkingLots()));
        }

        // GET: api/ParkingLots/5
        [HttpGet("{parkingLotID}")]
        //[HttpGet("{ParkingLotID}")]
        public ActionResult<ParkingLotDTO_Return> GetSingleParkingLot(string parkingLotID)
        {

            var result = _parkingLotService.GetSingleParkingLot(new Guid(parkingLotID));
            if (result == null)
            {
                return BadRequest("invalid input");
            }
            return Ok(_parkingLotMapper.FromParkingLotToParkingLotDTOReturn(result));
        }

        // POST: api/ParkingLots
        [HttpPost]
        public ActionResult<ParkingLotDTO_Return> CreateParkingLot([FromBody] ParkingLotDTO_Create parkingLotDTO)
        {
            var parkingLot = _parkingLotService.CreateParkingLot(_parkingLotMapper.FromParkingLotCreateToParkingLot(parkingLotDTO));

            if (parkingLot == null)
            {
                return BadRequest("Not valid");
            }

            return Ok(_parkingLotMapper.FromParkingLotToParkingLotDTOReturn(parkingLot));
        }
    }
}
