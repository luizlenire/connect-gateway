namespace Api
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class Program
    {
        #region --> Public static methods. <--

        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(x =>
                {
                    x.UseStartup<Startup>().ConfigureKestrel(y =>
                    {
                        y.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(10);
                        y.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(10);
                    });

                });

        #endregion --> Public static methods. <--    
    }
}
