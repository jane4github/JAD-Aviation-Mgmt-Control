namespace janine_aviation.Models
{
    public class MenuItem
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public List<MenuItem> Children { get; set; } = new();


    }
}
