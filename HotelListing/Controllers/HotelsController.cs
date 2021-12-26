using AutoMapper;
using HotelListing.Model;
using HotelListing.Services.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HotelsController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger<HotelsController> _logger;
		private readonly IMapper _mapper;
		public HotelsController(ILogger<HotelsController> logger, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetHotels()
		{
			try
			{
				var countries = await _unitOfWork.HotelsRepo.GetAll();
				var res = _mapper.Map<List<HotelDto>>(countries);
				return Ok(res);
			}
			catch (Exception e)
			{
				_logger.LogError(e, $"something went wrong {nameof(GetHotels)}");
				return StatusCode(500, "Internal server error");
			}

		}

		[Authorize]
		[HttpGet("{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetHotel(int id)
		{
			try
			{
				var hotel = await _unitOfWork.HotelsRepo.Get(h => h.Id == id, new List<string> { "Country" });
				var res = _mapper.Map<HotelDto>(hotel);
				return Ok(res);
			}
			catch (Exception e)
			{
				_logger.LogError(e, $"something went wrong {nameof(GetHotel)}");
				return StatusCode(500, "Internal server error");
			}

		}
	}
}