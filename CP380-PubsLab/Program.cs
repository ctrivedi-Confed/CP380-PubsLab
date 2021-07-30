using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CP380_PubsLab
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbcontext = new Models.PubsDbContext())
            {
                if (dbcontext.Database.CanConnect())
                {
                    Console.WriteLine("Yes, I can connect");
                } else
                {
                    Console.WriteLine("Not possible");
                }

                // 1:Many practice
                //
                // TODO: - Loop through each employee
                //       - For each employee, list their job description (job_desc, in the jobs table)

                // TODO: - Loop through all of the jobs
                //       - For each job, list the employees (first name, last name) that have that job

                Console.WriteLine("\n----------------------------------------------");
                Console.WriteLine("Result of Employee with Job Description");
                Console.WriteLine("----------------------------------------------");
                foreach (var e in dbcontext.Employee.Include(a => a.Jobs))
                {
                    Console.WriteLine($"{ e.First_name} -> { e.Jobs.Job_desc}");
                }

                Console.WriteLine("\n----------------------------------------------");
                Console.WriteLine("Result of Job Description with Employee First and Last Name List");
                Console.WriteLine("----------------------------------------------");

                foreach (var job in dbcontext.Jobs.Include(a => a.Employee))
                {
                    var employee_list = job.Employee.Select(a => a.First_name + ' '+a.Last_name).ToList();
                    var employee_name = String.Join(',', employee_list);

                    Console.WriteLine($"{job.Job_desc} -> {employee_name} ");
                }

                // Many:many practice
                //
                // TODO: - Loop through each Store
                //       - For each store, list all the titles sold at that store
                //
                // e.g.
                //  Bookbeat -> The Gourmet Microwave, The Busy Executive's Database Guide, Cooking with Computers: Surreptitious Balance Sheets, But Is It User Friendly?

                // TODO: - Loop through each Title
                //       - For each title, list all the stores it was sold at
                //
                // e.g.
                //  The Gourmet Microwave -> Doc-U-Mat: Quality Laundry and Books, Bookbeat
                Console.WriteLine("\n----------------------------------------------");
                Console.WriteLine("Result of Each Store with Multiple Titles");
                Console.WriteLine("----------------------------------------------");
                var q = dbcontext.Stores.Include(t => t.Titles);

                foreach (var t in q)
                {
                    var titles = t.Titles.Select(s => s.Title).ToList();
                    var titlesString = String.Join(',', titles);

                    Console.WriteLine($"{t.Store_name} -> {titlesString}");
                }

                Console.WriteLine("\n----------------------------------------------");
                Console.WriteLine("Result of Each Title with Multiple Stores");
                Console.WriteLine("----------------------------------------------");

                var q1 = dbcontext.Titles.Include(s => s.Stores);
                
                foreach(var s in q1)
                {
                    var stores = s.Stores.Select(s => s.Store_name).ToList();
                    var storesString = String.Join(',', stores);

                    Console.WriteLine($"{s.Title} -> {storesString}");
                }

            }
        }
    }
}
