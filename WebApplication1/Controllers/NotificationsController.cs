using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly SignalRDemoContext _context;
        private readonly IHubContext<BroadcastHub> _hubContext;

        public NotificationsController(SignalRDemoContext context, IHubContext<BroadcastHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }
        // GET: api/Notifications/notificationcount  
        [Route("notificationcount")]  
        [HttpGet]  
        public async Task<ActionResult<NotificationCountResult>> GetNotificationCount()  
        {  
            var count = (from not in _context.Notification  
                         select not).CountAsync();  
            NotificationCountResult result = new NotificationCountResult  
            {  
                Count = await count  
            };  
            return result;  
        }  
  
        // GET: api/Notifications/notificationresult  
        [Route("notificationresult")]  
        [HttpGet]  
        public async Task<ActionResult<List<NotificationResult>>> GetNotificationMessage()  
        {  
            var results = from message in _context.Notification  
                        orderby message.Id descending  
                        select new NotificationResult  
                        {  
                            EmployeeName = message.EmployeeName,  
                            TranType = message.TranType  
                        };  
            return await results.ToListAsync();  
        }  
  
        // DELETE: api/Notifications/deletenotifications  
        [HttpDelete]  
        [Route("deletenotifications")]  
        public async Task<IActionResult> DeleteNotifications()  
        {  
            await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Notification");  
            await _context.SaveChangesAsync();  
            await _hubContext.Clients.All.SendAsync("hihihi");  
  
            return NoContent();  
        }  
        // GET: api/Notifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotification()
        {
            return await _context.Notification.ToListAsync();
        }

        // GET: api/Notifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetNotification(int id)
        {
            var notification = await _context.Notification.FindAsync(id);

            if (notification == null)
            {
                return NotFound();
            }

            return notification;
        }

        // PUT: api/Notifications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotification(int id, Notification notification)
        {
            if (id != notification.Id)
            {
                return BadRequest();
            }

            _context.Entry(notification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationExists(id))
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

        // POST: api/Notifications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Notification>> PostNotification(Notification notification)
        {
            _context.Notification.Add(notification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotification", new { id = notification.Id }, notification);
        }

        // DELETE: api/Notifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var notification = await _context.Notification.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            _context.Notification.Remove(notification);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotificationExists(int id)
        {
            return _context.Notification.Any(e => e.Id == id);
        }
    }
}
