﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GoingTo_API.Domain.Models;
using GoingTo_API.Domain.Services;
using GoingTo_API.Domain.Services.Geographic;
using GoingTo_API.Extensions;
using GoingTo_API.Resources;
using Microsoft.AspNetCore.Mvc;

namespace GoingTo_API.Controllers
{
    [Route("/api/locatables/{locatableId}/tips")]
    public class LocatableTipsController : Controller
    {
        private readonly ILocatableService _locatableService;
        private readonly ITipService _tipService;
        private readonly IMapper _mapper;

        public LocatableTipsController(ILocatableService locatableService,ITipService tipService,IMapper mapper)
        {
            _locatableService = locatableService;
            _tipService = tipService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTipsByLocatableIdAsync(int locatableId)
        {
            var existingLocatable = await _locatableService.GetByIdAsync(locatableId);
            if (!existingLocatable.Success)
                return BadRequest(existingLocatable.Message);

            var tips = await _tipService.ListByLocatableIdAsync(locatableId);
            var resources = _mapper.Map<IEnumerable<Tip>, IEnumerable<TipResource>>(tips);

            return Ok(resources);
        }

        /*public async Task<IEnumerable<TipResource>> GetTipByLocatableIdAsync(int locatableId)
        {
            var tips = await _tipService.ListByLocatableIdAsync(locatableId);
            var resoruces = _mapper.Map<IEnumerable<Tip>, IEnumerable<TipResource>>(tips);

            return resoruces;
        }*/

        [HttpPost]
        public async Task<IActionResult> PostAsync(int locatableId,[FromBody] SaveTipResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var existingLocatable = await _locatableService.GetByIdAsync(locatableId);
            if (!existingLocatable.Success)
                return BadRequest(existingLocatable.Message);

            var tip = _mapper.Map<SaveTipResource, Tip>(resource);
            tip.Locatable=existingLocatable.Resource;

            var result = await _tipService.SaveAsync(tip);

            if (!result.Success)
                return BadRequest(result.Message);

            var tipResource = _mapper.Map<Tip, TipResource>(result.Resource);
            return Ok(tipResource);
        }


    }
}
