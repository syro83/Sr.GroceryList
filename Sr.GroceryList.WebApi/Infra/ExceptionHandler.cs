using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Sr.GroceryList.Infra.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Sr.GroceryList.WebApi.Infra
{
	public class ExceptionHandler
	{
		private readonly RequestDelegate _next;

		public ExceptionHandler(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next.Invoke(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			var response = context.Response;
			var customException = exception as SrException;
			var statusCode = (int)HttpStatusCode.InternalServerError;
			var message = "Unexpected error";
			var description = "Unexpected error";

			if (null != customException)
			{
				message = customException.Message;
				description = customException.InternalDescription;
			}

			response.ContentType = "application/json";
			response.StatusCode = statusCode;
			await response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponse
			{
				Message = message,
				Description = description
			}));
		}
	}
}
