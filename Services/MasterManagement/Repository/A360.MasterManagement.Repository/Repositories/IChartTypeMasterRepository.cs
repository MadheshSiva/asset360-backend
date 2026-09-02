using A360.Repository.Repositories;
using ChartTypeMasterEntity = A360.MasterManagement.Domain.Entities.ChartTypeMaster;

namespace A360.MasterManagement.Repository.Repositories;

public interface IChartTypeMasterRepository : IMongoRepository<ChartTypeMasterEntity>
{
}
