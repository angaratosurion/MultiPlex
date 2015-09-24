using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace MultiPlex.Core.Controllers
{
    public class CustomControllerFactory : IControllerFactory
    {
        private readonly DefaultControllerFactory _defaultControllerFactory;

        public CustomControllerFactory()
        {
            try
            {
                _defaultControllerFactory = new DefaultControllerFactory();
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
            }
        }

        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            try
            {
                var controller = Bootstrapper.GetInstance<IController>(controllerName);

                if (controller == null)
                    throw new Exception("Controller not found!");

                return controller;
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            try
            {
                return SessionStateBehavior.Default;
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return SessionStateBehavior.Default;
            }
        }

        public void ReleaseController(IController controller)
        {
            try
            {
                var disposableController = controller as IDisposable;

                if (disposableController != null)
                {
                    disposableController.Dispose();
                }
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
            }
        }
    }
}
