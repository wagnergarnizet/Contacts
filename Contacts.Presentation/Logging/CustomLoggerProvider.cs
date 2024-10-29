using System.Collections.Concurrent;

namespace Fiap.Team10.Contacts.Presentation.Logging
{
    public class CustomLoggerProvider : ILoggerProvider
    {

        private readonly CustomLoggerProviderConfiguration loggerConfig;
        private readonly ConcurrentDictionary<string, CustomLogger> loggers = new ConcurrentDictionary<string, CustomLogger>();

        public CustomLoggerProvider(CustomLoggerProviderConfiguration _loggerConfig)
        {
            this.loggerConfig = _loggerConfig;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return loggers.GetOrAdd(categoryName, name => new CustomLogger(name, loggerConfig));
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
