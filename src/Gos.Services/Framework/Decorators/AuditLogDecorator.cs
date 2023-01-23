using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Gos.Core.Entities;
using Gos.Core.Interfaces;
using MediatR;

namespace Gos.Services.Framework.Decorators
{
    public class AuditLogDecorator<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly GosDbContext dbContext;
        private readonly IRequestHandler<TRequest, TResponse> decorated;
        private readonly ISessionIdResolver sessionIdResolver;
        private readonly ITraceIdentifierResolver traceIdentifierResolver;

        public AuditLogDecorator(
            GosDbContext dbContext,
            IRequestHandler<TRequest, TResponse> decorated,
            ISessionIdResolver sessionIdResolver,
            ITraceIdentifierResolver traceIdentifierResolver)
        {
            this.dbContext = dbContext;
            this.decorated = decorated;
            this.sessionIdResolver = sessionIdResolver;
            this.traceIdentifierResolver = traceIdentifierResolver;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var startDate = DateTime.UtcNow;
            var timer = Stopwatch.StartNew();
            var response = await decorated.Handle(request, cancellationToken);
            timer.Stop();
            var endDate = DateTime.UtcNow;

            var auditLog = new AuditLog()
            {
                StartDate = startDate,
                EndDate = endDate,
                DurationMs = timer.ElapsedMilliseconds,
                Message = SerializeRequest(request),
                ObjectType = request.GetType().ToString(),
                SessionId = sessionIdResolver.Resolve(),
                TraceIdentifier = traceIdentifierResolver.Resolve(),
            };
            await dbContext.AuditLogs.AddAsync(auditLog, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            return response;
        }

        private static string SerializeRequest(TRequest request)
        {
            var options = new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };
            return JsonSerializer.Serialize(request, options);
        }
    }
}