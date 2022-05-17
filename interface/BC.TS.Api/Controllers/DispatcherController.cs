using BC.TS.Api.Filters;
using BC.TS.Application.Dto;
using BC.TS.Application.Service.Facade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BC.TS.Api.Controllers
{
    /// <summary>
    /// Dispatcher api
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DispatcherController : ControllerBase
    {
        private readonly IDispatcherApplication _dispatcherApplication;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="dispatcherApplication"></param>
        public DispatcherController(IDispatcherApplication dispatcherApplication)
        {
            _dispatcherApplication = dispatcherApplication;
        }

        /// <summary>
        /// Generate examiner
        /// </summary>
        /// <returns></returns>
        [HttpPost("generate-candidate")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [TypeFilter(typeof(AppCodeAuthorizeFilter))]
        public async Task<string> GenerateCandidate(int count)
        {
            var result = await _dispatcherApplication.GenerateCandidateListAsync(count);
            return string.Join("/", result.Select(s => s.Name));
        }

        /// <summary>
        /// Generate candidate
        /// </summary>
        /// <returns></returns>
        [HttpPost("generate-examiner")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [TypeFilter(typeof(AppCodeAuthorizeFilter))]
        public async Task<IEnumerable<ExaminerDto>> GenerateExaminer(int count)
        {
            return await _dispatcherApplication.GenerateExaminerListAsync(count);
        }

        /// <summary>
        /// Appraise
        /// </summary>
        /// <returns></returns>
        [HttpPut("appraise")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [TypeFilter(typeof(AppCodeAuthorizeFilter))]
        public async Task<IEnumerable<ExaminerAppraiseDto>> Appraise()
        {
            return await _dispatcherApplication.AppraiseAsync();
        }

        /// <summary>
        /// Reset data
        /// </summary>
        /// <returns></returns>
        [HttpDelete("clear")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [TypeFilter(typeof(AppCodeAuthorizeFilter))]
        public async Task Reset()
        {
            await _dispatcherApplication.ResetAsync();
        }
    }
}
