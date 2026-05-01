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
    public class UpMonitorController : ControllerBase
    {
        private readonly UnityOfWork _unityOfWork;
        private readonly UpMonitorService _upMonitorService;
        private readonly Guid _currentUserId;

        public UpMonitorController(UnityOfWork unityOfWork, UpMonitorService upMonitorService, AuthService authService, CurrentUserService currentUserService)
        {
            _currentUserId = currentUserService.GetUserId();
            _unityOfWork = unityOfWork;
            _upMonitorService = upMonitorService;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateUpMonitorRequest dto)
        {
            var upMonitor = await _upMonitorService.CreateAsync(_currentUserId, dto);
            await _unityOfWork.SaveChangesAsync();

            var response = new
            {
                Message = "UpMonitor created.",
                Data = new UpMonitorResponse
                {
                    Title = upMonitor.Title,
                    Url = upMonitor.Url,
                    State = upMonitor.State,
                    Status = upMonitor.Status
                }
            };

            return Ok(response);
        }

        [HttpPut("/{upMonitorId}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] Guid upMonitorId, [FromBody] UpdateUpMonitorRequest dto)
        {
            var upMonitor = await _upMonitorService.UpdateAsync(_currentUserId, upMonitorId, dto);
            await _unityOfWork.SaveChangesAsync();

            var response = new
            {
                Message = "UpMonitor updated.",
                Data = new UpMonitorResponse
                {
                    Title = upMonitor.Title,
                    Url = upMonitor.Url,
                    State = upMonitor.State,
                    Status = upMonitor.Status
                }
            };

            return Ok(response);
        }

        [HttpDelete("/{upMonitorId}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] Guid upMonitorId)
        {
            var upMonitor = await _upMonitorService.DeleteAsync(_currentUserId, upMonitorId);
            await _unityOfWork.SaveChangesAsync();

            var response = new
            {
                Message = "UpMonitor deleted."
            };

            return Ok();
        }
    }
}
