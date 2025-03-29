namespace MinAPISeparateFile;
    public static class TodoEndpoints
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/",async context =>
            {
                await context.Response.WriteAsJsonAsync(new { Message = "Hello World" });
            });

            app.MapGet("/{id}", async context =>
            {
                await context.Response.WriteAsJsonAsync(new { Message = "One todo item" });

            });
        }
    }

