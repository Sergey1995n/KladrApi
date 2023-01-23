namespace KladrApi.Dtos
{
    public class KladrDto
    {
        public string Id { get; set; }
        public string? ParentId { get; set; }
        public string Name { get; set; }
        
        public string DisplayName { get; set; }

        public KladrType KladrType { get; set; }

        public KladrDto(string id, string? parentId, string name, KladrType kladrType)
        {
            Id = id;
            ParentId = parentId;
            Name = name;
            KladrType = kladrType;
        }
        
        public KladrDto(string id, string? parentId, string name, KladrType kladrType, string displayName)
        {
            Id = id;
            ParentId = parentId;
            Name = name;
            KladrType = kladrType;
            DisplayName = displayName;
        }
    }
}