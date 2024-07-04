using Wolny.P.Domain;
using Wolny.P.Infrastructure.Repo.Interfaces;

namespace Wolny.P.Infrastructure.Repo;

public class CiudadRepo(PContext context) : GenericRepo<Ciudad>(context), ICiudadRepo;
