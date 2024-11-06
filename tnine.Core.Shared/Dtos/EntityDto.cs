namespace tnine.Core.Shared.Dtos
{
    public class EntityDto<TKey> : IEntityDto<TKey> where TKey : struct
    {
        public TKey? Id { get; set; }
    }
}
