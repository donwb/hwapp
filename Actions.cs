using System;
using System.Collections.Generic;

namespace hwapp
{
    public partial class Actions
    {
        public Actions()
        {
            Useractions = new HashSet<Useractions>();
        }

        public int Id { get; set; }
        public string Action { get; set; }
        public int Points { get; set; }

        public virtual ICollection<Useractions> Useractions { get; set; }
    }
}
