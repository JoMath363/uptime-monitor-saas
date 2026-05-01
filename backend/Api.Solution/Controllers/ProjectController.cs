using Api.Solution.Data;
using Api.Solution.Models;
using Api.Solution.Models.DTOs;
using Api.Solution.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Solution.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly UnityOfWork _unityOfWork;
        private readonly ProjectService _projectService;
        private readonly Guid _currentUserId;

        public ProjectController(UnityOfWork unityOfWork, ProjectService projectService, AuthService authService, CurrentUserService currentUserService)
        {
            _currentUserId = currentUserService.GetUserId();
            _unityOfWork = unityOfWork;
            _projectService = projectService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateProjectRequest dto)
        {
            var project = await _projectService.CreateAsync(_currentUserId, dto);
            await _unityOfWork.SaveChangesAsync();

            var response = new
            {
                Message = "Project created.",
                Data = new ProjectResponse
                {
                    Title = project.Title,
                    Description = project.Description,
                    UpMonitors = project.UpMonitors,
                }
            };

            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _projectService.GetUserProjectsAsync(_currentUserId);

            var response = new
            {
                Message = $"{projects.Count} project(s) found.",
                Data = projects.Select(p => new ProjectResponse
                {
                    Title = p.Title,
                    Description = p.Description,
                    UpMonitors = p.UpMonitors,
                })
                .ToList()
            };

            return Ok(response);
        }

        [HttpPut("/{projectId}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] Guid projectId, [FromBody] UpdateProjectRequest dto)
        {
            var project = await _projectService.UpdateAsync(_currentUserId, projectId, dto);
            await _unityOfWork.SaveChangesAsync();

            var response = new
            {
                Message = "Project updated.",
                Data = new ProjectResponse
                {
                    Title = project.Title,
                    Description = project.Description,
                    UpMonitors = project.UpMonitors,
                }
            };

            return Ok(response);
        }

        [HttpDelete("/{projectId}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] Guid projectId)
        {
            var project = await _projectService.DeleteAsync(_currentUserId, projectId);
            await _unityOfWork.SaveChangesAsync();

            var response = new
            {
                Message = "Project deleted."
            };

            return Ok();
        }
    }
}
