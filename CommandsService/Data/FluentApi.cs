namespace CommandsService.Data;
public static class FluentApi
{
    public static void ApplyFluentApi(this ModelBuilder builder) =>
     builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}