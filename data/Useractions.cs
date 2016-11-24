using System;
using System.Collections.Generic;

namespace hwapp.data
{
    public partial class Useractions
    {
        public int Id { get; set; }
        public int Userid { get; set; }
        public int Actionsid { get; set; }
        public DateTime Actiondate { get; set; }

        public virtual Actions Actions { get; set; }
        public virtual Mapuser User { get; set; }
    }
}
