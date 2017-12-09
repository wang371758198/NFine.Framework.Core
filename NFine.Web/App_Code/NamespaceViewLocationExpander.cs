using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace NFine.Web
{
    public class NamespaceViewLocationExpander: IViewLocationExpander
    {
        private const string VIEWS_FOLDER_NAME = "Views";

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            ControllerActionDescriptor cad = context.ActionContext.ActionDescriptor as ControllerActionDescriptor;
            string controllerNamespace = cad.ControllerTypeInfo.Namespace;
            int firstDotIndex = controllerNamespace.IndexOf('.')+4;
            int lastDotIndex = controllerNamespace.LastIndexOf('.');
            if (firstDotIndex < 0)
                return viewLocations;

            string viewLocation;
            if (firstDotIndex == lastDotIndex)
            {
                // controller folder is the first level sub folder of root folder
                viewLocation = "/{1}/Views/{0}.cshtml";
            }
            else
            {
                string viewPath = controllerNamespace.Substring(firstDotIndex +1, lastDotIndex - firstDotIndex - 1).Replace(".", "/");
                viewLocation = $"/{viewPath}/Views/{{1}}/{{0}}.cshtml";
            }

            if (viewLocations.Any(l => l.Equals(viewLocation, StringComparison.InvariantCultureIgnoreCase)))
                return viewLocations;

            if (viewLocations is List<string> locations)
            {
                locations.Add(viewLocation);
                return locations;
            }

            // it turns out the viewLocations from ASP.NET Core is List<string>, so the code path should not go here.
            List<string> newViewLocations = viewLocations.ToList();
            newViewLocations.Add(viewLocation);
            return newViewLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {

        }
    }
}
