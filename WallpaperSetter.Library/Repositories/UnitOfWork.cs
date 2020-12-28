namespace WallpaperSetter.Library.Repositories
{
    public interface IUnitOfWork
    {
        IImageUriRepository ImageUriRepository { get; }
    }

    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork()
        {
            ImageUriRepository = new ImageUriRepository();
        }

        public IImageUriRepository ImageUriRepository { get; }
    }
}