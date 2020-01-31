namespace HireMe.Models.Input
{
    using HireMe.Data.Repository;
    using System.ComponentModel.DataAnnotations;

    public class CreateMessageInputModel : BaseModel<int>
    {
        // [Required]
        //[MinLength(5)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public string ReceiverId { get; set; }


    }
}
