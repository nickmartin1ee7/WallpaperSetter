namespace WallpaperSetter.Library.Repositories
{
    public static class UnitOfWorkFactory
    {
        private static IUnitOfWork _instance;

        public static IUnitOfWork Create()
        {
            return _instance ??= new UnitOfWork();
        }
    }
}