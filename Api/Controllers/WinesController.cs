using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Models;
using Api.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WinesController : ControllerBase
    {
        private readonly IWineRepository _repository;

        public WinesController(IWineRepository repository)
        {            
            _repository = repository;
        }

        // GET: api/Wines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wine>>> GetWines()
        {
            var items = await _repository.GetAll();
            return Ok(items);
        }

        // GET: api/Wines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Wine>> GetWine(long id)
        {
            var wine = await _repository.GetById(id);

            if (wine == null)
            {
                return NotFound();
            }

            return Ok(wine); ;
        }

        // PUT: api/Wines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWine(long id, Wine wine)
        {
            if (id != wine.Id)
            {
                return BadRequest();
            }

            try
            {
                await _repository.Update(id, wine);
            }
            catch (WineNotFoundException)
            {
                NotFound();
            }

            return NoContent();
        }

        // POST: api/Wines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Wine>> PostWine(Wine wine)
        {
            try
            {
                await _repository.Insert(wine);
            }
            catch (ArgumentNullException)
            {
                return Problem("Entity set 'AppDbContext.Wines'  is null.");
            }
            catch (Exception)
            {
                return Problem("There was a problem");
            }

            return CreatedAtAction("GetWine", new { id = wine.Id }, wine);
        }

        // DELETE: api/Wines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWine(long id)
        {
            try
            {
                await _repository.Delete(id);
            }
            catch (WineNotFoundException)
            {
                return NotFound(id);
            }

            return NoContent();
        }
    }
}
