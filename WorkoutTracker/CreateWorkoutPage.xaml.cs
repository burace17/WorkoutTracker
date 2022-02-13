namespace WorkoutTracker;
public partial class CreateWorkoutPage : ContentPage
{
  public CreateWorkoutPage()
  {
    InitializeComponent();
    BindingContext = new CreateWorkoutViewModel();
  }
}
