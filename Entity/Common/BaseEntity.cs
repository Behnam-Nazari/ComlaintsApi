using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Common
{

    // IEntity is used for Reflection
    public interface IEntity
    {

    }


    public class BaseEntity<Tkey> : IEntity
    {
        public Tkey Id { get; set; }
    }
}
