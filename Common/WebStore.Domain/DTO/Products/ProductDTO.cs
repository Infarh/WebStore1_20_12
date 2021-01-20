namespace WebStore.Domain.DTO.Products
{
    //public class BrandDTO
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public int Order { get; set; }
    //}

    public record BrandDTO(int Id, string Name, int Order);

    public record SectionDTO(int Id, string Name, int Order, int? ParentId);

    public record ProductDTO(
        int Id,
        string Name, 
        int Order, 
        decimal Price, 
        string ImageUrl, 
        BrandDTO Brand, 
        SectionDTO Section);
}
