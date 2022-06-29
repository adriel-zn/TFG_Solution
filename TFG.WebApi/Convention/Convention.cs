using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFG.WebApi.Convention
{
    public class Convention: IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var controllerNamespace = controller.ControllerType.Namespace;
            var apiVersion = controllerNamespace.Split(".").Last().ToLower();
            if(!apiVersion.StartsWith("v")) { apiVersion = "v1"; }
            controller.ApiExplorer.GroupName = apiVersion;

        }
    }
}
