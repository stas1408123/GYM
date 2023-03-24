using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GYM.BLL.Abstractions;
using GYM.BLL.Models;
using Mapster;

namespace GYM.GrpcService.Services
{
    /// <summary>
    /// gRPC service for coaches
    /// </summary>
    public class CouchesApiService : CouchesService.CouchesServiceBase
    {
        private readonly IGenericService<CouchModel> _couchService;
        /// <summary>
        /// Constructor for CouchesApiService
        /// </summary>
        /// <param name="service"></param>
        public CouchesApiService(IGenericService<CouchModel> service)
        {
            _couchService = service;
        }

        /// <summary>
        /// Returns coach by Id
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<CouchReply> GetCoach(IdRequest request, ServerCallContext context)
        {
            var coach = await _couchService.Get(request.Id);
            if (coach == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
            }

            return await Task.FromResult(request.Adapt<CouchReply>());
        }


        /// <summary>
        /// returns all couches
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<ListCouchesReply> GetCoaches(Empty request, ServerCallContext context)
        {
            var listCoachesReply = new ListCouchesReply();
            var coachesModel = await _couchService.GetAll(); //testCoachList;
            listCoachesReply.Couches.AddRange(
                coachesModel.Select(
                    item => new CouchReply
                    {
                        Id = item.Id,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Description = item.Description
                    }
                )
                );

            return await Task.FromResult(listCoachesReply);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="RpcException"></exception>
        public override async Task<CouchReply> UpdateCoach(CouchUpdateRequest request, ServerCallContext context)
        {
            var coach = await _couchService.Get(request.Id);
            if (coach == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
            }

            await _couchService.Update(request.Adapt<CouchModel>());
            return await Task.FromResult(request.Adapt<CouchReply>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<Empty> CreateCoach(CouchCreateRequest request, ServerCallContext context)
        {
            await _couchService.Create(request.Adapt<CouchModel>());

            return await base.CreateCoach(request, context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="RpcException"></exception>
        public override async Task<CouchReply> DeleteUser(IdRequest request, ServerCallContext context)
        {
            var coach = await _couchService.Get(request.Id);
            if (coach == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
            }

            await _couchService.Delete(request.Id);

            return await Task.FromResult(request.Adapt<CouchReply>());
        }
    }
}
