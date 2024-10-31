using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using weekly_task_1.Models;
using Task = weekly_task_1.Models.Task;

namespace weekly_task_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskDbContext _context;

        public TaskController(TaskDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Task>> CreateTask(Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasks(string status = null)
        {
            var tasks = _context.Tasks.AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                if (Enum.TryParse<eStatus>(status, true, out var parsedStatus))
                {
                    tasks = tasks.Where(t => t.Status == parsedStatus);
                }
                else
                {
                    return BadRequest("Invalid status provided.");
                }
            }

            return await tasks.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Task>> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, Task task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
