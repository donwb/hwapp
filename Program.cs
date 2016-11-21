using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace hwapp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting....");

            using (var db = new mapContext()){

                var users = db.Useractions.Include(mu => mu.User)
                .Include(act => act.Actions)
                .ToList();
                
                foreach (var u in users) {
                    Console.WriteLine(u.User.Email + " ----- " + u.Actions.Action);
                }
            }
            
            Console.WriteLine("Done...");
            Console.ReadLine();
        }
    }
}
