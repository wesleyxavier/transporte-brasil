using System;
using src.core.entities;
using src.infra.data;

namespace src.infra.repositorys.veiculo
{
    public class VeiculoRepository: BaseRepository<Veiculo>, IVeiculoRepository
    {
        public VeiculoRepository(AplicacaoContexto aplicacaoContexto): base(aplicacaoContexto)
        {
            
        }
    }
}
