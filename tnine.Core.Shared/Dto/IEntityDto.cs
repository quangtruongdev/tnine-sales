namespace tnine.Core.Shared.Dto
{
    public interface IEntityDto<TKey> where TKey : struct
    {
        TKey? Id { get; set; }
    }
}
