using System;
using System.Collections.Generic;
using System.Web.Http;

namespace EP {
	public static class WebApiConfig {
		public static void Register(HttpConfiguration config) {
			// Web API configuration and services

			// Web API routes
			config.MapHttpAttributeRoutes();

			//
			//NOSSA ROTA!!!!!!!!!
			//
			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{action}"
			);
		}
	}
}
