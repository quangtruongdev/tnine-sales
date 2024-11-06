namespace tnine.Core.Shared.Dtos
{
    public interface IEntityDto<TKey> where TKey : struct
    {
        TKey? Id { get; set; }
    }
}
