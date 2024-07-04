using Wolny.P.Domain;
using Wolny.P.Infrastructure.Repo.Interfaces;

namespace Wolny.P.Infrastructure.Repo;

public class RecorridoRepo(PContext context) : GenericRepo<Recorrido>(context), IRecorridoRepo;
