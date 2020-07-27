using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Core;
using Core.Services;

namespace Radio.Controllers
{
    public class ReleaseController : BaseController
    {
        private IReleaseService releaseService { get; }
        private IEmployerService employerService { get; }
        private IReclameBlockService reclameBlockService { get; }

        public ReleaseController(IReleaseService releaseService, IEmployerService employerService,IReclameBlockService reclameBlockService)
        {
            this.releaseService = releaseService;
            this.employerService = employerService;
            this.reclameBlockService = reclameBlockService;
        }

        public IActionResult Index(long rbId)
        {
            ViewBag.RbId = rbId;
            List<Release> list = releaseService.GetReclameBlocksReleases(rbId);
            list = list.Select(rel => { rel.State = releaseService.GetState(rel); return rel; }).ToList();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create(long rbId)
        {
            ViewBag.RbId = rbId;
            ViewBag.LeadingId = new SelectList(employerService.All().OrderBy(e => e.Name), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Release release) {
            //ViewBag.RbId = rbId;
            ViewBag.LeadingId = new SelectList(employerService.All().OrderBy(e => e.Name), "Id", "Name");
            if (ModelState.IsValid)
            {
                //release.ReclameBlockId = rbId;
                var id = releaseService.Create(release);
                if (id > 0)
                    return RedirectToActionOkMsg(nameof(Index), "Release",new { rbId = release.ReclameBlockId }, "Данные успешно сохранены");
                else
                    return RedirectToActionErrMsg(nameof(Index), "Release", new { rbId = release.ReclameBlockId }, "Ошибка сохранения данных");
            }
            return View(release);
        }
        [HttpGet]
        public IActionResult Edit(long id)
        {
            //ViewBag.RbId = rbId;
            ViewBag.LeadingId = new SelectList(employerService.All().OrderBy(e => e.Name), "Id", "Name");
            Release entity = releaseService.Get(id);
            if (entity == null) 
                return RedirectToActionErrMsg(nameof(Index), "Release", new { rbId = entity.ReclameBlockId }, "Объект не найден");
            return View(entity);
        }

        [HttpPost]
        public IActionResult Edit(Release entity)
        {
            ViewBag.RbId = entity.ReclameBlockId;
            ViewBag.LeadingId = new SelectList(employerService.All().OrderBy(e => e.Name), "Id", "Name");
            if (ModelState.IsValid)
            {
                var result = releaseService.Edit(entity);
                if (result)
                    return RedirectToActionOkMsg(nameof(Index), "Release", new { rbId = entity.ReclameBlockId }, "Данные успешно сохранены");
                else
                    return RedirectToActionErrMsg(nameof(Index), "Release", new { rbId = entity.ReclameBlockId }, "Ошибка сохранения данных");
            }
            return View(entity);
        }
        [HttpGet]
        [ActionName("Delete")]
        public IActionResult Remove(long id) {
            Release entity = releaseService.Get(id);
            if (entity == null)
                return RedirectToActionErrMsg(nameof(Index), "Release", new { rbId = entity.ReclameBlockId }, "Объект не найден");
            ViewBag.RbId = entity.ReclameBlockId;
            return View(entity);
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            Release entity = releaseService.Get(id);
            if (entity == null)
                return RedirectToActionErrMsg(nameof(Index), "Release", new { rbId = entity.ReclameBlockId }, "Объект не найден");
            ViewBag.RbId = entity.ReclameBlockId;
            bool result = releaseService.Remove(entity);
            if (result)
                return RedirectToActionOkMsg(nameof(Index), "Release", new { rbId = entity.ReclameBlockId }, "Данные успешно удалены");
            else
                return RedirectToActionErrMsg(nameof(Index), "Release", new { rbId = entity.ReclameBlockId }, "Ошибка удаления данных");
        }
        [HttpGet]
        public IActionResult GetAutoReleases(long rbId)
        {
            ReclameBlock block = reclameBlockService.Get(rbId);
            if (block == null) 
                return RedirectToActionErrMsg(nameof(Index), "ReclameBlock", "Объект не найден");
            if(!block.Status)
                return RedirectToActionErrMsg(nameof(Index), "ReclameBlock", "Блок не активен");
            ViewBag.Name = block.Name;
            List<Release> model = releaseService.GetAutoReleases(rbId);
            return View("Auto", model);
        }
    }
}