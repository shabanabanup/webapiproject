﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using student;
using webapiproject.Model;

namespace webapiproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class studentsController : ControllerBase
    {
        private readonly Context _context;

        public studentsController(Context context)
        {
            _context = context;
        }

        // GET: api/students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Students>>> Getstudent()
        {
            return await _context.student.ToListAsync();
        }

        // GET: api/students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Students>> Getstudent(int id)
        {
            var student = await _context.student.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putstudent(int id, Students student)
        {
            if (id != student.id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!studentExists(id))
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

        // POST: api/students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Students>> Poststudent(Students student)
        {
            _context.student.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getstudent", new { id = student.id }, student);
        }

        // DELETE: api/students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletestudent(int id)
        {
            var student = await _context.student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.student.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool studentExists(int id)
        {
            return _context.student.Any(e => e.id == id);
        }
    }
}
