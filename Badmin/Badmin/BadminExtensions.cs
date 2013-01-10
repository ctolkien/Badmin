namespace Badmin.Badmin
{
    public static class BadminExtensions
    {
        public static BadminConfiguration Name(this BadminConfiguration config, string name)
        {
            config.DataConfiguration.Name = name;
            return config;
        }
    }
}