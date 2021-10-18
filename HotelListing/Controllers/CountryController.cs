using AutoMapper;
using HotelListing.Model;
using HotelListing.Services.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CountryController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger<CountryController> _logger;
		private readonly IMapper _mapper;
		public CountryController(ILogger<CountryController> logger, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetCountries()
		{
			try
			{
				var countries = await _unitOfWork.CountriesRepo.GetAll();
				var res = _mapper.Map<List<CountryDto>>(countries);
				return Ok(res);
			}
			catch (Exception e)
			{
				_logger.LogError(e, $"something went wrong {nameof(GetCountries)}");
				return StatusCode(500,"Internal server error");
			}

		}

		[HttpGet("{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetCountry(int id)
		{
			try
			{
				var country = await _unitOfWork.CountriesRepo.Get(c=>c.Id==id, new List<string> { "Hotels"});
				var res = _mapper.Map<CountryDto>(country);
				return Ok(res);
			}
			catch (Exception e)
			{
				_logger.LogError(e, $"something went wrong {nameof(GetCountry)}");
				return StatusCode(500, "Internal server error");
			}

		}
	}

}
