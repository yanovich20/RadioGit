using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Core;
using Core.Services;

namespace Radio.Controllers
{
    public class EmployerController : BaseController
    {
        private IEmployerService employerService { get; }

        public EmployerController(IEmployerService employerService)
        {
            this.employerService = employerService;
        }

        public IActionResult Index()
        {
            var list = employerService.All();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employer entity)
        {
            if(ModelState.IsValid)
            {
                long id = employerService.Create(entity);
                if (id > 0)
                    return RedirectToActionOkMsg(nameof(Index), "Employer", "Данные успешно сохранены");
                else
                    return RedirectToActionErrMsg(nameof(Index), "Employer", "Ошибка сохранения данных");
            }
            return View(entity);
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            var oldEmployer = employerService.Get(id);
            if(oldEmployer==null)
                return RedirectToActionErrMsg(nameof(Index), "Employer", "Объект не найден");
            return View(oldEmployer);
        }
        [HttpPost]
        public IActionResult Edit(Employer entity)
        {
            if(ModelState.IsValid)
            {
                var result = employerService.Edit(entity);
                if(result)
                    return RedirectToActionOkMsg(nameof(Index), "Employer", "Данные успешно сохранены");
                else
                    return RedirectToActionErrMsg(nameof(Index), "Employer", "Ошибка сохранения данных");
            }
            return View(entity);
        }
        [HttpGet]
        [ActionName("Delete")]
        public IActionResult DeleteEmployer(long id)
        {
            var entity = employerService.Get(id);
            if (entity == null)
                return RedirectToActionErrMsg(nameof(Index), "Employer", "Объект не найден");
            return View(entity);
        }
        [HttpPost]
        public IActionResult Delete(long id)
        {
            var entity = employerService.Get(id);
            if (entity == null)
                return RedirectToActionErrMsg(nameof(Index), "Employer", "Объект не найден");
            var result = employerService.SoftRemove(entity);
            if (result)
                return RedirectToActionOkMsg(nameof(Index), "Employer", "Данные успешно удалены");
            else
                return RedirectToActionErrMsg(nameof(Index), "Employer", "Ошибка удаления данных");
        }
    }
}