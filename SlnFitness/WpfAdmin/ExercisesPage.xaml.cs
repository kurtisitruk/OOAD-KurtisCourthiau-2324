using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace WpfAdmin
{
    /// <summary>
    /// Interaction logic for ExercisesPage.xaml
    /// </summary>
    public partial class ExercisesPage : Page
    {
        public ExercisesPage()
        {
            InitializeComponent();
            LoadExercises();
        }

        private void LoadExercises()
        {
            using (var context = new FitnessDbContext())
            {
                var exercises = context.Exercises.ToList();

                foreach (var exercise in exercises)
                {
                    var exerciseCard = CreateExerciseCard(exercise);
                    ExercisesWrapPanel.Children.Add(exerciseCard);
                }
            }
        }

        private Border CreateExerciseCard(Exercise exercise)
        {
            var card = new Border
            {
                BorderBrush = Brushes.Gray,
                BorderThickness = new System.Windows.Thickness(1),
                Margin = new System.Windows.Thickness(10),
                Padding = new System.Windows.Thickness(10),
                Width = 150,
                Height = 200,
                Background = Brushes.White,
                CornerRadius = new System.Windows.CornerRadius(5)
            };

            var stackPanel = new StackPanel();

            // Add an image (placeholder for this example)
            var image = new Image
            {
                Width = 100,
                Height = 100,
                Source = new BitmapImage(new System.Uri("https://via.placeholder.com/100")),
                Margin = new System.Windows.Thickness(0, 0, 0, 10)
            };

            // Add exercise name
            var nameTextBlock = new TextBlock
            {
                Text = exercise.Name,
                FontWeight = FontWeights.Bold,
                TextAlignment = TextAlignment.Center,
                Margin = new System.Windows.Thickness(0, 0, 0, 10)
            };

            // Add exercise type
            var typeTextBlock = new TextBlock
            {
                Text = $"Type: {GetExerciseType(exercise.Type)}",
                TextAlignment = TextAlignment.Center
            };

            stackPanel.Children.Add(image);
            stackPanel.Children.Add(nameTextBlock);
            stackPanel.Children.Add(typeTextBlock);

            card.Child = stackPanel;

            return card;
        }

        private string GetExerciseType(int type)
        {
            return type switch
            {
                1 => "Cardio",
                2 => "Dumbell",
                3 => "Yoga",
                _ => "Unknown"
            };
        }
    }
}
