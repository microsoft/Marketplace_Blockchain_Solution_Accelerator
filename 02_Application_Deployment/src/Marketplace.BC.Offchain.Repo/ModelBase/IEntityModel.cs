using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.BC.Offchain.Repo
{
    public interface IEntityModel<TIdentifier>
    {
        TIdentifier Id { get; set; }
    }
}
