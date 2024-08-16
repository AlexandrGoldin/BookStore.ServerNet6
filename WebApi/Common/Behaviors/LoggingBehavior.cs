﻿using MediatR;
using Serilog;

namespace WebApi.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
       : IPipelineBehavior<TRequest, TResponse> where TRequest
       : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
           CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            Log.Information("---WebApi.CqrsMediatrFeatures Request: {Name} {@Request}",
                requestName, request);

            var response = await next();

            return response;
        }
    }
}
