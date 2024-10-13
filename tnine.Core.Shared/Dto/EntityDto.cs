namespace tnine.Core.Shared.Dto
{
    public class EntityDto<TKey> : IEntityDto<TKey> where TKey : struct
    {
        public TKey Id { get; set; }
    }
}
