using System;
using System.Collections.Generic;
using System.Security.Claims;
using AutoMapper;
using InvestingOak.Data;
using InvestingOak.Data.Entities.PaperTrading;
using InvestingOak.Models.PaperTrading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InvestingOak.Controllers
{
    [Route("api/projects")]
    [ApiController]
    [Produces("application/json")]
    public class ProjectsController : ControllerBase
    {
        private readonly ILogger<ProjectsController> logger;
        private readonly IMapper mapper;
        private readonly IRepository repository;
        private readonly UserManager<ApplicationUser> userManager;

        public ProjectsController(IRepository repository, IMapper mapper, ILogger<ProjectsController> logger,
            UserManager<ApplicationUser> userManager)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetProjects()
        {
            try
            {
                IEnumerable<Project> projects = repository.GetProjects(GetUser());
                var result = mapper.Map<IEnumerable<ProjectDescModel>>(projects);
                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError($"Failed to get project list: {e}");
                return BadRequest("Failed to get project list.");
            }
        }

        [HttpGet("{name}", Name = "GetProjectByName")]
        [Authorize]
        public IActionResult GetProjectByName(string name)
        {
            try
            {
                Project project = repository.GetProjectByName(name, GetUser());

                if (project is null)
                {
                    return NotFound();
                }

                var projectModel = mapper.Map<ProjectModel>(project);
                return Ok(projectModel);
            }
            catch (Exception e)
            {
                logger.LogError($"Failed to get project: {e}");
                return BadRequest("Failed to get project.");
            }
        }

        [HttpPost("create")]
        [Authorize]
        public IActionResult CreateProject(ProjectCreateModel projectCreateModel)
        {
            try
            {
                var newProject = mapper.Map<Project>(projectCreateModel);
                newProject.CreationDate = DateTime.UtcNow;
                newProject.Balance = newProject.InitialBalance;
                newProject.User = GetUser();

                repository.AddEntity(newProject);
                repository.SaveAll();

                var projectDescModel = mapper.Map<ProjectDescModel>(projectCreateModel);
                return CreatedAtRoute(nameof(GetProjectByName), new {projectDescModel.Name}, projectDescModel);
            }
            catch (Exception e)
            {
                logger.LogError($"Failed to create project: {e}");
                return BadRequest("Failed to create project.");
            }
        }

        [HttpDelete("{name}")]
        [Authorize]
        public IActionResult DeleteProject(string name)
        {
            try
            {
                Project project = repository.GetProjectByName(name, GetUser());
                if (project is null || repository.RemoveEntity(project).State != EntityState.Deleted)
                {
                    return NotFound();
                }

                if (!repository.SaveAll())
                {
                    return BadRequest("Failed to delete project.");
                }

                return Ok();
            }
            catch (Exception e)
            {
                logger.LogError($"Failed to delete project: {e}");
                return BadRequest("Failed to delete project.");
            }
        }

        [HttpPatch("{name}")]
        [Authorize]
        public IActionResult UpdateProject(string name, JsonPatchDocument<ProjectUpdateModel> patchDocument)
        {
            try
            {
                Project project = repository.GetProjectByName(name, GetUser());
                if (project is null)
                {
                    return NotFound();
                }

                var projectToPatch = mapper.Map<ProjectUpdateModel>(project);
                patchDocument.ApplyTo(projectToPatch, ModelState);
                if (!TryValidateModel(projectToPatch))
                {
                    return ValidationProblem(ModelState);
                }

                mapper.Map(projectToPatch, project);
                repository.SaveAll();

                return NoContent();
            }
            catch (Exception e)
            {
                logger.LogError($"Failed to update project: {e}");
                return BadRequest("Failed to update project.");
            }
        }

        private ApplicationUser GetUser()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userManager.FindByIdAsync(userId).Result;
        }
    }
}
