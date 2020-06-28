using System;
using src.core.entities;
using src.infra.repositorys.veiculo;

namespace src.core.services.veiculo
{
    public class VeiculoService: BaseService<Veiculo>, IVeiculoService
    {
        public VeiculoService(IVeiculoRepository repository): base(repository)
        {
            
        }
    }
}
