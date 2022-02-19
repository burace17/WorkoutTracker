namespace WorkoutTracker;

public static class MauiProgram
{
  public static MauiApp CreateMauiApp()
  {
    var builder = MauiApp.CreateBuilder();
    builder
      .UseMauiApp<App>()
      .ConfigureServices()
      .ConfigureViewModels()
      .ConfigureFonts(fonts =>
      {
        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
      });



    Routing.RegisterRoute("createWorkout", typeof(CreateWorkoutPage));

    return builder.Build();
  }
}
