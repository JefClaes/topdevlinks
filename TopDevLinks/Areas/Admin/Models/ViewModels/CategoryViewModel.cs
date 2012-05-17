namespace TopDevLinks.Areas.Admin.Models.ViewModels
{
    public class CategoryViewModel
    {
        public CategoryViewModel(string id, string name, int priority)
        {
            Id = id;
            Name = name;
            Priority = priority;
        }        

        public string Id { get; set; }

        public string Name { get; set; }

        public int Priority { get; set; }
    }
}