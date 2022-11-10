using System;
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
                else if(Choose == "3"){
                try{    
                        //Display All Blogs in the database
                        var db = new BloggingContext();
                        var query = db.Blogs.OrderBy(b => b.Name);
                        foreach(var item in query)
                        {
                            Console.WriteLine(item.Name);
                        }


                        Console.Write("Enter the blog you want to post to: ");
                        string blogName = Console.ReadLine();
                        int IDEntry = 0;

                        try{
                            //finding blog 
                        var blogChoice = db.Blogs.First(b => b.Name == blogName);
                        Console.WriteLine($"blog found with name: \"{blogChoice.Name}\"");
                        //prompting post creation
                        Console.Write("Would you like to create a post to this blog (Y/N): ");
                        string wouldContinue = Console.ReadLine();
                        if(wouldContinue.ToUpper() == "Y"){
                            IDEntry = blogChoice.BlogId;
                        }
                        }
                        catch{
                            Console.WriteLine($"There was no blog with the name: \"{blogName}\"");
                        }
                        try{
                            var finalBlog = db.Blogs.First(b => b.BlogId == IDEntry);
                            int blogID = finalBlog.BlogId;

                            Console.Write("Enter the Name of you Post: ");
                            string postTitle = Console.ReadLine();

                            Console.Write("Enter the description of the post: ");
                            string postContent = Console.ReadLine();

                            var post = new Post{Title = postTitle, Content = postContent, BlogId = blogID, Blog = finalBlog};

                            db.AddPost(post);
                            logger.Info("Post added - {postTitle} to {blogName}",postTitle,blogName);
                    }
                    catch(Exception ex){
                        logger.Error(ex.Message);
                    }
                    }
                    catch(Exception ex){
                        logger.Error(ex.Message);
                    }
                }
                else if(Choose == "4")
                {
                }

            } while(Choose == "1" || Choose == "2" || Choose == "3" || Choose == "4" );

            logger.Info("Program ended");

        }
    }
}
