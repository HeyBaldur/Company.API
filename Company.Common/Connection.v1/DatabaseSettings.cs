namespace Company.Common.Connection.v1
{
    public class DatabaseSettings
    {
        /// <summary>
        /// Connection string to Mongo DB
        /// </summary>
        public string ConnectionString { get; set; } = null!;

        /// <summary>
        /// Database name
        /// </summary>
        public string DatabaseName { get; set; } = null!;

        /// <summary>
        /// User collection name
        /// </summary>
        public string UserCollectionName { get; set; } = null!;
    }
}
