namespace WallpaperSetter.Library.Repositories
{
    public interface IUnitOfWork
    {
        IImageUriRepository ImageUriRepository { get; }
    }

    /// <summary>
    ///     Should be used with a singleton helper class!
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork()
        {
            ImageUriRepository = new InMemoryImageUriRepository();
        }

        public IImageUriRepository ImageUriRepository { get; }
    }
}