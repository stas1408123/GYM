using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GYM.BLL.Abstractions;
using GYM.BLL.Models;
using Mapster;

namespace GYM.GrpcService.Services
{
    /// <summary>
    /// CRUD service for visitor.
    /// </summary>
    public class VisitorsApiService : VisitorsService.VisitorsServiceBase
    {
        private readonly IGenericService<VisitorModel> _visitorService;

        /// <summary>
        /// Constructor for VisitorApiService.
        /// </summary>
        /// <param name="service"></param>
        public VisitorsApiService(IGenericService<VisitorModel> service)
        {
            _visitorService = service;
        }

        /// <summary>
        /// Get visitor by id.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="RpcException"></exception>
        public override async Task<VisitorReply> GetVisitor(IdVisitorsRequest request, ServerCallContext context)
        {
            var visitor = await _visitorService.Get(request.Id);

            if (visitor == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Visitor not found"));

            }

            return await Task.FromResult(visitor.Adapt<VisitorReply>());
        }

        /// <summary>
        /// Get all visitors.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<ListVisitorsReply> GetVisitors(Empty request, ServerCallContext context)
        {
            var listVisitorsReply = new ListVisitorsReply();
            var visitorsModel = await _visitorService.GetAll();
            listVisitorsReply.Visitors.AddRange(visitorsModel.Select(item =>
                    new VisitorReply
                    {
                        Id = item.Id,
                        FirstName = item.FirstName,
                        LastName = item.LastName
                    }

                ));

            return await Task.FromResult(listVisitorsReply);
        }

        /// <summary>
        /// Create visitor.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<Empty> CreateVisitor(CreateVisitorRequest request, ServerCallContext context)
        {
            await _visitorService.Create(request.Adapt<VisitorModel>());

            return await base.CreateVisitor(request, context);
        }

        /// <summary>
        /// Update visitor.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="RpcException"></exception>
        public override async Task<VisitorReply> UpdateVisitor(UpdateVisitorRequest request, ServerCallContext context)
        {
            var visitor = await _visitorService.Get(request.Id);
            if (visitor == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Visitor not found"));
            }

            await _visitorService.Update(request.Adapt<VisitorModel>());

            return await Task.FromResult(request.Adapt<VisitorReply>());
        }

        /// <summary>
        /// Delete visitor by id.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<VisitorReply> DeleteVisitor(IdVisitorsRequest request, ServerCallContext context)
        {
            await _visitorService.Delete(request.Id);

            return await base.DeleteVisitor(request, context);
        }
    }
}
