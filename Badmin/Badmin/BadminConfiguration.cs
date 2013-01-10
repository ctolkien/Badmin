namespace Badmin.Badmin
{
    public class BadminConfiguration
    {
        public BadminConfiguration(DataConfiguration dataConfiguration)
        {
            this.DataConfiguration = dataConfiguration;
        }

        public DataConfiguration DataConfiguration { get; set; }
    }
}