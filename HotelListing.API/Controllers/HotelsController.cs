﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.IRepository;
using AutoMapper;
using HotelListing.API.DTOs.Hotels;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelRepository hotelRepo;
        private readonly IMapper mapper;


        public HotelsController(IHotelRepository context, IMapper mapper)
        {
            hotelRepo = context;
            this.mapper = mapper;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDTO>>> GetHotels()
        {
            var hotels = await hotelRepo.GetAllAsync();
          
            return mapper.Map<List<HotelDTO>>(hotels);
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDTO>> GetHotel(int id)
        {

            var hotel = await hotelRepo.GetAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return mapper.Map<HotelDTO>(hotel);
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, HotelDTO hotelDTO)
        {
            if (id != hotelDTO.Id)
            {
                return BadRequest();
            }

            var hotel = await hotelRepo.GetAsync(id);

            mapper.Map(hotelDTO, hotel);

            try
            {
                await hotelRepo.UpdateAsync(hotel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await hotelRepo.Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(CreateHotelDTO hotelDTO)
        {
            var hotel = mapper.Map<Hotel>(hotelDTO);
            await hotelRepo.AddAsync(hotel);

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            
            if (! await hotelRepo.Exists(id))
            {
                return NotFound();
            }

            await hotelRepo.DeleteAsync(id);

            return NoContent();
        }


    }
}
