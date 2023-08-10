
using System.ComponentModel.DataAnnotations;

public class Comment
{
    public int Id { get; set; }
    [Required(ErrorMessage = "UserComment is required.")]
    public string UserComment { get; set; }


    public string Name { get; set; }
    public string Time { get; set; }
    public string IP { get; set; }

}