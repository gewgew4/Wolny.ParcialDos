using Wolny.P.Domain;
using Wolny.P.Infrastructure.Repo.Interfaces;

namespace Wolny.P.Infrastructure.Repo;

public class PlanRecorridoRepo(PContext context) : GenericRepo<PlanRecorrido>(context), IPlanRecorridoRepo;
