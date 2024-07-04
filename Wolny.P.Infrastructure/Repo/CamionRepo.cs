using Wolny.P.Domain;
using Wolny.P.Infrastructure.Repo.Interfaces;

namespace Wolny.P.Infrastructure.Repo;

public class CamionRepo(PContext context) : GenericRepo<Camion>(context), ICamionRepo;
