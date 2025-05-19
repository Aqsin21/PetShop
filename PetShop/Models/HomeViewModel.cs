using PetShop.DataContext.Entities;

namespace PetShop.Models
{
    public class HomeViewModel
    {
        public List<Slider> Sliders { get; set; } = new List<Slider>();
        public List<Product> Products { get; set; }= new List<Product>();
    }
}
