using System;
using Microsoft.EntityFrameworkCore;

namespace hwapp.data
{
    /*
     * Not sure this is the best way to replace the connect
     * string while not modifing the generate class for
     * mapContext
     * works though...
     */
    public partial class mapContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var user = Environment.GetEnvironmentVariable("DB_USER");
            var pass = Environment.GetEnvironmentVariable("DB_PASSWORD");

            optionsBuilder.UseSqlServer
            (@"Server=tcp:mapprogram.database.windows.net,1433;Initial Catalog=map;uid=" 
            + user + ";Password=" + pass + ";");
        }
    }
}
