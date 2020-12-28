namespace WallpaperSetter.Library
{
    public static class UnitOfWorkFactory
    {
        public static IUnitOfWork Create()
        {
            return new UnitOfWork();
        }
    }
}