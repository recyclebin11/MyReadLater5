using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Base;

namespace Entity
{
    public class Category : BaseEntity
    {
        [StringLength(maximumLength: 50)]
        public string Name { get; set; }
    }
}
