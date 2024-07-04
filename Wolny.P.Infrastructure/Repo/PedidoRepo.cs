using Wolny.P.Domain;
using Wolny.P.Infrastructure.Repo.Interfaces;

namespace Wolny.P.Infrastructure.Repo;

public class PedidoRepo(PContext context) : GenericRepo<Pedido>(context), IPedidoRepo;
