﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoPrograIV.Data;
using ProyectoPrograIV.Models;

namespace ProyectoPrograIV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonsterApiController : ControllerBase
    {
        private readonly AppDBContext _context;

        public MonsterApiController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/MonsterApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Monster>>> GetMonsters()
        {
            return await _context.Monsters.ToListAsync();
        }

        // GET: api/MonsterApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Monster>> GetMonster(int id)
        {
            var monster = await _context.Monsters.FindAsync(id);

            if (monster == null)
            {
                return NotFound();
            }

            return monster;
        }

        // PUT: api/MonsterApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMonster(int id, Monster monster)
        {
            if (id != monster.Id)
            {
                return BadRequest();
            }

            _context.Entry(monster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonsterExists(id))
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

        // POST: api/MonsterApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Monster>> PostMonster(Monster monster)
        {
            _context.Monsters.Add(monster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMonster", new { id = monster.Id }, monster);
        }

        // DELETE: api/MonsterApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonster(int id)
        {
            var monster = await _context.Monsters.FindAsync(id);
            if (monster == null)
            {
                return NotFound();
            }

            _context.Monsters.Remove(monster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MonsterExists(int id)
        {
            return _context.Monsters.Any(e => e.Id == id);
        }
    }
}
