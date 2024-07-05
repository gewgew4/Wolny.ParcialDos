using System.Linq.Expressions;
using Wolny.P.Application.Result;
using Wolny.P.Domain;

namespace Wolny.P.Application.Services.Interfaces;

public interface IGenericService<T> where T : BaseEntity<T>
{
    Task<Result<List<T>>> GetAllAsync();
    Task<Result<List<T>>> GetWheresync(Expression<Func<T, bool>> predicate,
                                  Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                  int? top = null,
                                  int? skip = null,
                                  params string[] includeProperties);
    Task<Result<T>> GetByIdAsync(int id);
    Task<Result<T>> Update(T entity);
}