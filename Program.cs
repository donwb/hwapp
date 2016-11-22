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

            // Check for envvars
            var dbVars = CheckEnvVars();
            if(dbVars.Item1 == null || dbVars.Item2 == null) {
                Console.WriteLine("Environment vars not set, stopping....");
                Environment.Exit(1);
            } else {
                Console.WriteLine("User: " + dbVars.Item1 + " Pass:" + dbVars.Item2);
            }
            
            using (var db = new mapContext()){

                AllUserActions(db);

                ViaMapUsers(db);

                JustMe(db);

                // AddUser(db, "traci@mariettahometeam.com");

                // AddItem(db, "traci@mariettahometeam.com");
            }
            
            Console.WriteLine("Done...");
            Console.ReadLine();
        }

        private static Tuple<string, string> CheckEnvVars() {
            var user = Environment.GetEnvironmentVariable("DB_USER");
            var pass = Environment.GetEnvironmentVariable("DB_PASSWORD");
            
            return new Tuple<string, string>(user, pass);

        }

        private static void AllUserActions(mapContext db) {
            // Collection of UserActions
            var users = db.Useractions
            .Include(mu => mu.User)
            .Include(act => act.Actions)
            .ToList();
            
            foreach (var u in users) {
                Console.WriteLine(u.User.Email + " ----- " + u.Actions.Action);
            }
        }

        private static void ViaMapUsers(mapContext db) {
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

        private static void JustMe(mapContext db) {
            // Just me
            var me = db.Mapuser
            .Include(ua => ua.Useractions)
            .Single(u => u.Email.ToString() == "don.browning@turner.com");
            
            Console.WriteLine(me.Email);
            foreach (var a in me.Useractions) {
                Console.WriteLine("\t" + a.Actions.Action);
            }
        }

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
