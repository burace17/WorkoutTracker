namespace WorkoutTracker;

public partial class MainPage : ContentPage
{
  private MainPageViewModel? VM => BindingContext as MainPageViewModel;
  public MainPage(MainPageViewModel vm)
  {
    InitializeComponent();
    BindingContext = vm;
  }

  protected override void OnAppearing()
  {
    base.OnAppearing();
    Utilities.NonAwaitCall(VM?.InitializeAsync() ?? Task.CompletedTask);
  }
}

