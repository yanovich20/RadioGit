using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

using Core.Services;
using Core;

namespace Radio.Controllers
{
    public class ReclameBlockController : BaseController
    {
        private IReclameBlockService reclameBlockService { get; }
        private IEmployerService employerService { get; }
        private IReleaseService releaseService { get; }
        private Setting setting { get; }

        public ReclameBlockController(IReclameBlockService reclameBlockService, IEmployerService employerService,IOptions<Setting> setting)
        {
            this.reclameBlockService = reclameBlockService;
            this.employerService = employerService;
            this.setting = setting.Value;
        }

        public IActionResult Index()
        {
            var list = reclameBlockService.All();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.ResponsibleId = new SelectList(employerService.All().OrderBy(e => e.Name), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(ReclameBlock block)
        {
            ViewBag.ResponsibleId = new SelectList(employerService.All().OrderBy(e => e.Name), "Id", "Name");
            if (ModelState.IsValid)
            {
                var id = reclameBlockService.Create(setting,block);
                if (id == -2)
                    return RedirectToActionErrMsg(nameof(Index), "ReclameBlock", String.Format("Нельзя создать  ежечасные блоки в количестве более {0}", setting.MaxBlocks));
                if (id > 0)
                    return RedirectToActionOkMsg(nameof(Index), "ReclameBlock", "Данные успешно сохранены");
                else
                    return RedirectToActionErrMsg(nameof(Index), "ReclameBlock", "Ошибка сохранения данных");
            }
            else
                return View();
        }
        [HttpGet]
        public IActionResult Edit(long id)
        {
            ReclameBlock oldBlock = reclameBlockService.Get(id);
            ViewBag.ResponsibleId = new SelectList(employerService.All().OrderBy(e => e.Name), "Id", "Name",oldBlock.ResponsibleId);
            if (oldBlock == null)
                return RedirectToActionErrMsg(nameof(Index), "ReclameBlock", "Объект не найден");
            return View(oldBlock);
        }
        [HttpPost]
        public IActionResult Edit(ReclameBlock block)
        {
            ViewBag.ResponsibleId = new SelectList(employerService.All().OrderBy(e => e.Name), "Id", "Name", block.ResponsibleId);
            if (ModelState.IsValid)
            {
                int result = reclameBlockService.Edit(setting, block);
                if (result == -2)
                    return RedirectToActionErrMsg(nameof(Index), "ReclameBlock", String.Format("Нельзя создать  ежечасные блоки в количестве более {0}", setting.MaxBlocks));
                if (result<=0)
                    return RedirectToActionOkMsg(nameof(Index), "ReclameBlock", "Данные успешно сохранены");
                else
                    return RedirectToActionErrMsg(nameof(Index), "ReclameBlock", "Ошибка сохранения данных");
            }
            return View(block);
        }
        [HttpGet]
        [ActionName("Delete")]
        public IActionResult Remove(long id)
        {
            var entity = reclameBlockService.GetBlockWithReleleases(id);
            if (entity == null)
                return RedirectToActionErrMsg(nameof(Index), "ReclameBlock", "Объект не найден");
            return View(entity);
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            var entity = reclameBlockService.GetBlockWithReleleases(id);
            if (entity == null) 
                return RedirectToActionErrMsg(nameof(Index), "ReclameBlock", "Объект не найден");
            //Следующие строки можно не писать.Все удалится каскадом.
            //while (entity.Releases.Count > 0)//тут считаю что коллекция небольшая и не пишу отдельный метод в реползитории
            //{
            //    var release = entity.Releases.FirstOrDefault();
            //    if(release!=null)
            //    {
            //        entity.Releases.Remove(release);
            //        releaseService.Remove(release);
            //    }
            //}
            bool result = reclameBlockService.Remove(entity);
            if (result)
                return RedirectToActionOkMsg(nameof(Index), "ReclameBlock", "Данные успешно удалены");
            else
                return RedirectToActionErrMsg(nameof(Index), "ReclameBlock", "Ошибка удаления данных");
            return View();
        }
    }
}