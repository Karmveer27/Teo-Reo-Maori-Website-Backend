using System;
using System.Runtime.Serialization;
using System.Text.Json;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace A1.Data
{



    public class A1Repo : IA1Repos
    {
        private readonly A1DbContext context;
        public A1Repo(A1DbContext context) 
        {
            this.context = context; 
        }


        public  string GetVersion()
        {
            string version = "1.0.0 (Ngongotahā) by ksin518"; 
            return (version);
        }

        public  List<string> Logo()
        {
            string path = Directory.GetCurrentDirectory();
            string imgDir = Path.Combine(path, "Logos");
            string respHeader = "image/png";
            string fileName = Path.Combine(imgDir, "Logo.png");
            List<string> result = new List<string> { fileName, respHeader };
            return result;
        }

        public  List<Product> AllItems()
        {
            var products = context.Products.ToList();
            return products;
        }

        public  List<Product> Items(string searchItem)
        {
            var products =  context.Products.ToList();
            var filteredProducts = products.Where(p => p.Name.Contains(searchItem, StringComparison.OrdinalIgnoreCase))
                .ToList(); 
            return filteredProducts;

        }

        public  List<string> ItemImage(int id)
        {
            //General
            string name = id.ToString();
            string path = Directory.GetCurrentDirectory();
            string imgDir = Path.Combine(path, "ItemsImages");
            //Default Values
            string fileName = Path.Combine(imgDir, "default.png");
            string respHeader = "image/png";
            //Checkers
            string PNGfileName = Path.Combine(imgDir, name + ".png");
            string JPGfileName = Path.Combine(imgDir, name + ".jpg");
            string GIFfileName = Path.Combine(imgDir, name + ".gif");
            string SVGfileName = Path.Combine(imgDir, name + ".svg");
            string JPEGfileName = Path.Combine(imgDir, name + ".jpeg");
            //Now we need to check
            if (System.IO.File.Exists(PNGfileName))
            {
                fileName = PNGfileName;


            }
            else if (System.IO.File.Exists(JPGfileName))
            {
                fileName = JPGfileName;
                respHeader = "image/jpg";
            }
            else if (System.IO.File.Exists(GIFfileName))
            {
                fileName = GIFfileName;
                respHeader = "image/gif";
            }
            else if (System.IO.File.Exists(SVGfileName))
            {
                fileName = SVGfileName;
                respHeader = "image/svg";
            }
            else if (System.IO.File.Exists(JPEGfileName))
            {
                fileName = JPEGfileName;
                respHeader = "image/jpeg";
            }

            List<string> result = new List<string>{ fileName, respHeader };
            return result;
            

        }

        public  Comment GetComment(int commentId)
        {
            var comment =  context.Comments.FirstOrDefault(c => c.Id == commentId);
            if (comment == null)
            {
                return null;
            }

            return comment;
        }

        public  Comment WriteComment(Comment comment)
        {

            EntityEntry<Comment>e =  context.Comments.Add(comment);
            Comment c = e.Entity;
            context.SaveChanges();
            return c;
            



        }

        public List<Comment> Comments(int? count = 5)
        {
            var comments =  context.Comments.ToList();
            var commentsToDisplay = count.HasValue ? comments.TakeLast(count.Value).ToList() : comments;
            return (commentsToDisplay);



        }
    }


}