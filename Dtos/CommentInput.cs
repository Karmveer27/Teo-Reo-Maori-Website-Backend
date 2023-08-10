

using System.ComponentModel.DataAnnotations;

public class CommentInput
{
    [Required(ErrorMessage = "UserComment is required.")]
    public string UserComment { get; set; }
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }
}

