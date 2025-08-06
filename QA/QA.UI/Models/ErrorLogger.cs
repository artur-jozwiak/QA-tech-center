namespace QA.UI.Models
{
    using Microsoft.AspNetCore.Components.Authorization;
    using Serilog;
    using System;

    public class ErrorLogger : IErrorLogger
    {
        private readonly ILogger _logger;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public ErrorLogger(ILogger logger, AuthenticationStateProvider authenticationStateProvider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _authenticationStateProvider = authenticationStateProvider ?? throw new ArgumentNullException();
        }

        public async Task LogError(Exception exception, string location)
        {
            var authstate = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authstate.User;
            var name = user.Identity.Name;

            var contextLogger = _logger
                .ForContext("InnerExceptionMessage", exception.InnerException?.Message)
                .ForContext("AppUserName", name)
                .ForContext("Location", location);
            contextLogger.Error(exception, "");
        }

        public void LogInformation(string message, string location)
        {
            var contextLogger = _logger
                .ForContext("Message", message)
                .ForContext("Location", location);
            contextLogger.Information(message);
        }
    }
}
