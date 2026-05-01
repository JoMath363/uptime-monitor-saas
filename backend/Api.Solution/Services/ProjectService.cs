using Api.Solution.Data;
using Api.Solution.Models;
using Api.Solution.Exceptions;
using Microsoft.EntityFrameworkCore;
using Api.Solution.Models.DTOs;

namespace Api.Solution.Services
{
    public class ProjectService
    {
        private readonly AppDbContext _context;

        public ProjectService(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<Project> CreateAsync(Guid userId, CreateProjectRequest dto)
        {
            Project project = new()
            {
                Title = dto.Title,
                Description = dto.Description,
                UserId = userId,
            };

            await _context.Projects.AddAsync(project);

            return project;
        }

        public async Task<List<Project>> GetUserProjectsAsync(Guid userId)
        {
            return await _context.Projects
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(Guid userId, Guid projectId)
        {
            return await _context.Projects
                .Where(p => p.UserId == userId && p.Id == projectId)
                .FirstOrDefaultAsync();
        }

        public async Task<Project> UpdateAsync(Guid userId, Guid projectId, UpdateProjectRequest dto)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.UserId == userId && p.Id == projectId);

            if (project == null)
                throw new NotFoundException("Project not found.");

            if (dto.Title != null) project.Title = dto.Title;
            if (dto.Description != null) project.Description = dto.Description;

            _context.Projects.Update(project);
            return project;
        }

        public async Task<bool> DeleteAsync(Guid userId, Guid projectId)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.UserId == userId && p.Id == projectId);

            if (project == null)
                return false;

            _context.Projects.Remove(project);
            return true;
        }
    }
}
