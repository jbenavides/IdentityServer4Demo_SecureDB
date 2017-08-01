using System.Collections.Generic;
using ConfArchApi.Repositories;
using ConfArchShared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConfArchApi.Controllers
{
    [Authorize]
    public class ConferenceController : Controller
    {
        private readonly ConferenceRepo repo;

        public ConferenceController(ConferenceRepo repo)
        {
            this.repo = repo;
        }

        public IEnumerable<ConferenceModel> GetAll()
        {
            return repo.GetAll();
        }

        [HttpPost]
        public void Add([FromBody]ConferenceModel conference)
        {
            repo.Add(conference);
        }
    }
}