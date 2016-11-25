using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using hwapp.data;

namespace hwapp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting....");

            // Check for envvars
            var dbVars = CheckEnvVars();
            if(dbVars.Item1 == null || dbVars.Item2 == null) {
                Console.WriteLine("Environment vars not set, stopping....");
                Environment.Exit(1);
            } else {
                Console.WriteLine("User: " + dbVars.Item1 + " Pass:" + dbVars.Item2);
            }

            var controller = new MapController();
            controller.AllUserActions();
            controller.ViaMapUsers();
            controller.JustMe();
            
            Console.WriteLine("Done...");
            Console.ReadLine();
        }

        private static Tuple<string, string> CheckEnvVars() {
            var user = Environment.GetEnvironmentVariable("DB_USER");
            var pass = Environment.GetEnvironmentVariable("DB_PASSWORD");
            
            return new Tuple<string, string>(user, pass);

        }

// Replace these once i test them out

        private static void AddUser(mapContext db, string emailAddress) {

            Mapuser mu = new Mapuser{
                Email = emailAddress};
                db.Mapuser.Add(mu);
                db.SaveChanges();

                Console.WriteLine(mu.Id + " " + mu.Email);
        }

        private static void AddItem(mapContext db, string who) {
            // get the user
            var me = db.Mapuser
            .Single(u => u.Email.ToString() == who);

            // get the first action
            var action = db.Actions
            .Single(i => i.Id == 1);

            // New up an object
            var ua = new Useractions();
            ua.Actions = action;
            ua.User = me;
            ua.Actiondate = DateTime.Now;

            // Save it
            db.Useractions.Add(ua);
            db.SaveChanges();

            // if it returns an ID, we're good
            Console.WriteLine("Done: " + ua.Id);

        }
    }
}
