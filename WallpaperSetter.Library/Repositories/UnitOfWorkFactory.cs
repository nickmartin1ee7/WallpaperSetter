namespace WallpaperSetter.Library.Repositories
{
    public static class UnitOfWorkFactory
    {
        public static IUnitOfWork Create()
        {
            return new UnitOfWork();
        }
    }
}