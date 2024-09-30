using CrmManagement.APIs.Common;
using CrmManagement.APIs.Dtos;

namespace CrmManagement.APIs;

public interface IServicesService
{
    /// <summary>
    /// Create one Service
    /// </summary>
    public Task<Service> CreateService(ServiceCreateInput service);

    /// <summary>
    /// Delete one Service
    /// </summary>
    public Task DeleteService(ServiceWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Services
    /// </summary>
    public Task<List<Service>> Services(ServiceFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Service records
    /// </summary>
    public Task<MetadataDto> ServicesMeta(ServiceFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Service
    /// </summary>
    public Task<Service> Service(ServiceWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Service
    /// </summary>
    public Task UpdateService(ServiceWhereUniqueInput uniqueId, ServiceUpdateInput updateDto);

    /// <summary>
    /// Get a Reservation record for Service
    /// </summary>
    public Task<Reservation> GetReservation(ServiceWhereUniqueInput uniqueId);
}
