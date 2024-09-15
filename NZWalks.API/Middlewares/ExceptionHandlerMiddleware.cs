namespace NZWalks.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger,
                                          RequestDelegate next)
        {
            this._logger = logger;
            this._next = next;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString();

                //Log this exception
                _logger.LogError(ex, $"{errorId} : {ex.Message}");

                //Return a custom exception message
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                //Creamos un objeto con el error
                var error = new
                {
                    ErrorId = errorId,
                    ErrorMessage = "A problem happened while handling your request"
                };

                //Devolvemos un error en forma de JSON
                await context.Response.WriteAsJsonAsync(error);
            }
        }

    }
}
