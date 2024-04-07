using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Crud.DTO
{
    public interface IDTO<TKey> 
    where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }

    /// <summary>
    /// Base interface for dtos with integer primary key named "Id".
    /// </summary>
    public interface IDTO : IDTO<int>
    {
    }
}
