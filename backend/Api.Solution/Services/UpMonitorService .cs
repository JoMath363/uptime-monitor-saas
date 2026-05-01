using Api.Solution.Data;
using Api.Solution.Models;
using Api.Solution.Exceptions;
using Microsoft.EntityFrameworkCore;
using Api.Solution.Models.DTOs;

namespace Api.Solution.Services
{
    public class UpMonitorService
    {
        private readonly AppDbContext _context;

        public UpMonitorService(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<UpMonitor> CreateAsync(Guid userId, CreateUpMonitorRequest dto)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == dto.ProjectId && p.UserId == userId);

            if (project == null)
                throw new NotFoundException("Project not found.");

            var upMonitorStatus = UpMonitorStatus.Up;

            UpMonitor upMonitor = new()
            {
                Title = dto.Title,
                Url = dto.Url,
                Status = upMonitorStatus,
                State = dto.State,
                ProjectId = dto.ProjectId
            };

            await _context.UpMonitors.AddAsync(upMonitor);

            return upMonitor;
        }

        public async Task<List<UpMonitor>> GetUserUpMonitorsAsync(Guid userId)
        {
            return await _context.UpMonitors
                .Where(p => p.Project.UserId == userId)
                .ToListAsync();
        }

        public async Task<UpMonitor?> GetByIdAsync(Guid userId, Guid upMonitorId)
        {
            return await _context.UpMonitors
                .Where(p => p.Project.UserId == userId && p.Id == upMonitorId)
                .FirstOrDefaultAsync();
        }

        public async Task<UpMonitor> UpdateAsync(Guid userId, Guid upMonitorId, UpdateUpMonitorRequest dto)
        {
            var upMonitor = await _context.UpMonitors
                .FirstOrDefaultAsync(p => p.Project.UserId == userId && p.Id == upMonitorId);

            if (upMonitor == null)
                throw new NotFoundException("UpMonitor not found.");

            if (dto.Title != null) upMonitor.Title = dto.Title;
            if (dto.Url != null) upMonitor.Url = dto.Url;

            _context.UpMonitors.Update(upMonitor);
            return upMonitor;
        }

        public async Task<bool> DeleteAsync(Guid userId, Guid upMonitorId)
        {
            var upMonitor = await _context.UpMonitors
                .FirstOrDefaultAsync(p => p.Project.UserId == userId && p.Id == upMonitorId);

            if (upMonitor == null)
                return false;

            _context.UpMonitors.Remove(upMonitor);
            return true;
        }
    }
}
