﻿using System;
using NLog.Web;
using System.IO;
using System.Linq;


namespace BlogsConsole
{
    class Program
    {
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        static void Main(string[] args)
        {
            logger.Info("Program started");

            String Choose;
            do{
                Console.WriteLine("Enter 1 to Display all blogs");
                Console.WriteLine("Enter 2 to add blog");
                Console.WriteLine("Enter 3 to Create Post");
                Console.WriteLine("Enter 4 to Display Posts");
                Console.WriteLine("Enter any other key to exit");

                Choose = Console.ReadLine();

                if (Choose == "1"){
                try
            {
                
                var db = new BloggingContext();
                // Display all Blogs from the database
                var query = db.Blogs.OrderBy(b => b.Name);

                Console.WriteLine("All blogs in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
                }
                else if(Choose == "2"){
                try{                
                // Create and save a new Blog
                Console.Write("Enter a name for a new Blog: ");
                var name = Console.ReadLine();

                var blog = new Blog { Name = name };

                var db = new BloggingContext();
                db.AddBlog(blog);
                logger.Info("Blog added - {name}", name);
                }
                catch ( Exception ex)
                {
                    logger.Error(ex.Message);
                }}
                else if(Choose == "3"){}
                else if(Choose == "4"){}

            } while(Choose == "1" || Choose == "2" || Choose == "3" || Choose == "4" );

            logger.Info("Program ended");

        }
    }
}
