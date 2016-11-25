using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace hwapp.data
{
    public class MapController 
    {
        public void AllUserActions() 
        {
            using(var db = new mapContext())
            {
                // Collection of UserActions
                var users = db.Useractions
                .Include(mu => mu.User)
                .Include(act => act.Actions)
                .ToList();
                
                foreach (var u in users) {
                    Console.WriteLine(u.User.Email + " ----- " + u.Actions.Action);
                }
            }
        }

        public void ViaMapUsers() 
        {
            using(var ctx = new mapContext())
            {
                var mapUsers = ctx.Mapuser
                .Include(ua => ua.Useractions)
                .ToList();

                

                foreach(var u in mapUsers)
                {
                    Console.WriteLine("User: "+ u.Email);
                    
                    foreach(var act in u.Useractions)
                    {
                        // This killed me!
                        ctx.Entry(act)
                        .Reference(a => a.Actions)
                        .Load();

                        Console.WriteLine("Action: " + act.Actions.Action);
                    }
                }
                
            }
        }

        public void JustMe() 
        {
            using(var db = new mapContext())
            {
                // Just me
                var me = db.Mapuser
                .Include(ua => ua.Useractions)
                .Single(u => u.Email.ToString() == "don.browning@turner.com");
                
                Console.WriteLine(me.Email);
                foreach (var a in me.Useractions) {
                    db.Entry(a)
                    .Reference(act => act.Actions)
                    .Load();
                    
                    Console.WriteLine("\t" + a.Actions.Action);
                }
            }
        }

        public void AddUser(mapContext db, string emailAddress) 
        {

            Mapuser mu = new Mapuser{
                Email = emailAddress};
                db.Mapuser.Add(mu);
                db.SaveChanges();

                Console.WriteLine(mu.Id + " " + mu.Email);
        }

        public void AddItem(mapContext db, string who) 
        {
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