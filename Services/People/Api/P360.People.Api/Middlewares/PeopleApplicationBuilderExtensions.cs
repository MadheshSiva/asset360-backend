namespace P360.People.Api.Middlewares;
using P360.People.Api.Endpoints;
public static class PeopleApplicationBuilderExtensions
{
    public static WebApplication UsePeopleApiMiddlewares(
        this WebApplication app)
    {
        if (app.Environment.IsDevelopment() ||
            app.Configuration.GetValue<bool>("Swagger:Enabled"))
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapGet("/health", () =>
            Results.Ok(new
            {
                status = "Healthy",
                service = "People"
            }))
            .WithName("PeopleHealth")
            .WithTags("Health");

        app.MapEmployeeEndpoints();
        app.MapContractorEndpoints();
        app.MapVisitorEndpoints();
        app.MapPersonalVisionGroupEndpoints();
        app.MapPersonalVisionAccessEndpoints();
        app.MapPersonalWorkScheduleEndpoints();
        app.MapPersonalVisionManualAttendanceEndpoints();
        app.MapPersonalVisionGreetingsIndividualEndpoints();
        app.MapPersonalVisionGreetingsGroupsEndpoints();
        app.MapGroupEndpoints();
        app.MapAccessEndpoints();

        return app;
    }
}