using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace ParticleSimulation.Models
{
    public class Particle
    {
        public Ellipse Shape { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }
        public double HorizontalDirection { get; set; }
        public double VerticalDirection { get; set; }
        public SolidColorBrush Color { get; set; }
    }
}
