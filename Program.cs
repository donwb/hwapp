using System;


namespace hwapp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (var db = new mapContext()){
                
                foreach (var a in db.Useractions) {
                
                    Console.WriteLine(a.Userid);
                }
            }
            
            Console.ReadLine();
        }
    }
}
