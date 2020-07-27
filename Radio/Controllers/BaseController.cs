using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Radio.Controllers
{
    public class BaseController : Controller
    {
        protected void SetTempOkMsg(string message)
        {
            TempData["status"] = "success";
            TempData["text"] = message;
        }

        protected void SetTempWarnMsg(string message)
        {
            TempData["status"] = "warning";
            TempData["text"] = message;
        }

        protected void SetTempErrMsg(string message)
        {
            TempData["status"] = "danger";
            TempData["text"] = message;
        }


        protected RedirectToActionResult RedirectToActionOkMsg(string actionName, string controllerName, string message)
        {
            SetTempOkMsg(message);
            return RedirectToAction(actionName, controllerName);
        }

        protected RedirectToActionResult RedirectToActionOkMsg(string actionName, string controllerName, object routeValues, string message)
        {
            SetTempOkMsg(message);
            return RedirectToAction(actionName, controllerName, routeValues);
        }


        protected RedirectToActionResult RedirectToActionWarnMsg(string actionName, string controllerName, string message)
        {
            SetTempWarnMsg(message);
            return RedirectToAction(actionName, controllerName);
        }

        protected RedirectToActionResult RedirectToActionWarnMsg(string actionName, string controllerName, object routeValues, string message)
        {
            SetTempWarnMsg(message);
            return RedirectToAction(actionName, controllerName, routeValues);
        }


        protected RedirectToActionResult RedirectToActionErrMsg(string actionName, string controllerName, string message)
        {
            SetTempErrMsg(message);
            return RedirectToAction(actionName, controllerName);
        }

        protected RedirectToActionResult RedirectToActionErrMsg(string actionName, string controllerName, object routeValues, string message)
        {
            SetTempErrMsg(message);
            return RedirectToAction(actionName, controllerName, routeValues);
        }

        protected RedirectResult RedirectToUrlOkMsg(string url, string message)
        {
            SetTempOkMsg(message);
            return Redirect(url);
        }
        protected RedirectResult RedirectToUrlErrMsg(string url, string message)
        {
            SetTempErrMsg(message);
            return Redirect(url);
        }
    }
}