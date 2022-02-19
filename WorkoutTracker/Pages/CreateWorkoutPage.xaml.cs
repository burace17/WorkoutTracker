namespace WorkoutTracker;
public partial class CreateWorkoutPage : ContentPage
{
  public CreateWorkoutPage(CreateWorkoutViewModel vm)
  {
    InitializeComponent();
    BindingContext = vm;
  }
}
