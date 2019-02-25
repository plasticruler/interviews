namespace Realmdigital_Interview.Configuration
{
    public class ConfigurationData : IConfigurationData
    {
        //get the execution context and load custom configuration data from some source (file, db, etc)
        //i'll just hard-code it
        public string ServiceUrl => "http://192.168.0.241/eanlist?type=Web";

        public string FileLocation => "../../mock-data/many-products.json";
    }
}