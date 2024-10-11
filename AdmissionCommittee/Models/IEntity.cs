using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Models
{
    /// <summary>
    /// Base interface for models
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// An unique identificator
        /// </summary>
        int Id { get; set; }
    }
}
