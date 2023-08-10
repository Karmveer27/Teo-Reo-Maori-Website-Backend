using Microsoft.AspNetCore.Mvc;

namespace A1.Data
{
    public interface IA1Repos
    {
        string GetVersion();
        List<string> Logo();
        List<Product> AllItems();
        List<Product> Items(string searchItem);
        List<string> ItemImage(int id);
        Comment GetComment(int commentId);
        Comment WriteComment(Comment comment);
        List<Comment> Comments(int? count);


    }

}