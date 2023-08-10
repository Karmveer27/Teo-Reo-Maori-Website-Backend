
using A1.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


    [Route("webapi")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly IA1Repos service;
        public CharacterController(IA1Repos service)
        {
            this.service = service;
        }

        [HttpGet("GetVersion")]
        
        public  ActionResult<string> GetVersion()
        {
            var version1 = service.GetVersion();
            return Ok(version1);
        }

        [HttpGet("Logo")]
        public  ActionResult GetLogo()
        {
        List<string> result = service.Logo(); // [0] = filename [1] = respHeader

        return PhysicalFile(result[0], result[1]);

    }

        [HttpGet("ItemImage/{id}")]
        public ActionResult ItemImage(int id)
        {
            List<string> result = service.ItemImage(id); // [0] = filename [1] = respHeader
            
            return PhysicalFile(result[0], result[1]);

        }



        [HttpGet("AllItems")]
        [Produces("application/json")]
        public  ActionResult<List<Product>> AllItems()
        {
            var products =  service.AllItems();
            return Ok(products);
    }

                 
        [HttpGet("Items/{term}")]
        [Produces("application/json")]
        public  ActionResult<List<Product>> Items(string term)
        {
            var item = service.Items(term);
            return Ok(item);
        }


        [HttpGet("GetComment/{id}")]
        public  ActionResult<Comment> GetComment(int id)
        {
            var comment =  service.GetComment(id);
            if (comment == null)
                return BadRequest("Comment " + id + " does not exist");
            return Ok(comment);
        }


        [HttpGet("Comments/{num}")]
        public  ActionResult<Comment> Comments(int num)
        {
            var comments = service.Comments(num);
            return Ok(comments);
        }



    [HttpPost("WriteComment")]
        public  ActionResult<Comment> WriteComment(CommentInput commentInput)
        {
        
            if (string.IsNullOrWhiteSpace(commentInput.UserComment) || string.IsNullOrWhiteSpace(commentInput.Name))
            {
                return BadRequest("Both UserComment and Name fields must have values provided.");
            }

            
            Comment newComment = new Comment
            {
                UserComment = commentInput.UserComment,
                Name = commentInput.Name,
                Time = DateTime.UtcNow.ToString("yyyyMMddTHHmmssZ"),
                IP = HttpContext.Connection.RemoteIpAddress?.ToString()
            };
           
            // Save the new comment to the database
               service.WriteComment(newComment);

            
            string locationUrl = $"{Request.Scheme}://{Request.Host}/api/Character/GetComment/{newComment.Id}";
            Response.Headers.Add("Location", locationUrl);

            
            return Ok(newComment);
        }



    }



    






