using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.app.shared.entities;
using src.infra.data;

namespace src.infra.repositorys {
    public class BaseRepository<T> : IBaseRepository<T>, IDisposable
    where T : class, IEntity {
        protected readonly AplicacaoContexto aplicacaoContexto;
        public BaseRepository (AplicacaoContexto aplicacaoContexto) {
            this.aplicacaoContexto = aplicacaoContexto;
        }
        public async Task<T> Add (T entity) {
            try {
                await this.aplicacaoContexto.Set<T> ().AddAsync (entity);
                await this.aplicacaoContexto.SaveChangesAsync();
            } catch (Exception e) {
                throw new Exception (e.Message);
            }
            return await Task.FromResult(entity);
        }

        public async Task<T> Find (int id) {
            try {
                T entity = this.aplicacaoContexto.Set<T> ().Find(id);
                return await Task.FromResult(entity);
            } catch (Exception e) {
                throw new Exception (e.Message);
            }
        }

        public async Task<List<T>> FindAll () {
            try {
                return await Task.FromResult(this.aplicacaoContexto.Set<T> ().ToList());
            } catch (Exception e) {
                throw new Exception (e.Message);
            }
        }

        public async Task<T> Update (T entity) {
            try {
                this.aplicacaoContexto.Set<T> ().Update (entity);
                await this.aplicacaoContexto.SaveChangesAsync();
            } catch (Exception e) {
                throw new Exception (e.Message);
            }
            return await Task.FromResult(entity);
        }

        public async Task<T> Delete (T entity) {
            try {
                this.aplicacaoContexto.Set<T> ().Remove (entity);
                await this.aplicacaoContexto.SaveChangesAsync();
            } catch (Exception e) {
                throw new Exception (e.Message);
            }
            return await Task.FromResult(entity);
        }

        public void Dispose () {
            GC.Collect ();
        }
    }
}