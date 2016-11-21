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

                // Collection of UserActions
                var users = db.Useractions
                .Include(mu => mu.User)
                .Include(act => act.Actions)
                .ToList();
                
                foreach (var u in users) {
                    Console.WriteLine(u.User.Email + " ----- " + u.Actions.Action);
                }

                // Go  through the MapUsers to then get the Actions
                var fromUsers = db.Mapuser
                .Include(ua => ua.Useractions)
                .ToList();
                
                foreach (var u in fromUsers) {
                    Console.WriteLine(u.Email);
                    foreach(var a in u.Useractions){
                        Console.WriteLine("\t" + a.Actions.Action);
                    }
                }
            }
            
            Console.WriteLine("Done...");
            Console.ReadLine();
        }
    }
}
