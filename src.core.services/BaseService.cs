using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using src.app.shared.entities;
using src.infra.repositorys;

namespace src.core.services {
    public class BaseService<T> : IBaseService<T>
        where T : class, IEntity {

            private readonly IBaseRepository<T> _repository;
            public BaseService (IBaseRepository<T> repository) {
                _repository = repository;
            }

            public Task<T> Add (T entity) {
                return _repository.Add (entity);
            }

            public Task<T> Delete (T entity) {
                return _repository.Delete (entity);
            }

            public Task<T> Find (int id) {
                return _repository.Find (id);
            }

            public Task<List<T>> FindAll () {
                return _repository.FindAll ();
            }

            public Task<T> Update (T entity) {
                return _repository.Update (entity);
            }
        }
}