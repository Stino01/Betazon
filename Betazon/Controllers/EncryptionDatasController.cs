using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Betazon.Models;
using Betazon.BLogic.Encryption;

namespace Betazon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncryptionDatasController : ControllerBase
    {
        private readonly DbEngineContext _context;

        public EncryptionDatasController(DbEngineContext context)
        {
            _context = context;
        }

        // GET: api/EncryptionDatas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EncryptionData>>> GetEncryptionData()
        {
            return await _context.EncryptionData.ToListAsync();
        }

        // GET: api/EncryptionDatas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EncryptionData>> GetEncryptionData(int id)
        {
            var encryptionData = await _context.EncryptionData.FindAsync(id);

            if (encryptionData == null)
            {
                return NotFound();
            }

            return encryptionData;
        }

        // PUT: api/EncryptionDatas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEncryptionData(int id, EncryptionData encryptionData)
        {
            if (id != encryptionData.Id)
            {
                return BadRequest();
            }

            _context.Entry(encryptionData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EncryptionDataExists(id))
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

        // POST: api/EncryptionDatas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EncryptionData>> PostEncryptionData(EncryptionData encryptionData)
        {
            Encryption encryption = new Encryption();

            encryptionData = encryption.EncryptString(encryptionData.EncryptedValue, "AES");

            _context.EncryptionData.Add(encryptionData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEncryptionData", new { id = encryptionData.Id }, encryptionData);
        }

        // DELETE: api/EncryptionDatas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEncryptionData(int id)
        {
            var encryptionData = await _context.EncryptionData.FindAsync(id);
            if (encryptionData == null)
            {
                return NotFound();
            }

            _context.EncryptionData.Remove(encryptionData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EncryptionDataExists(int id)
        {
            return _context.EncryptionData.Any(e => e.Id == id);
        }
    }
}
