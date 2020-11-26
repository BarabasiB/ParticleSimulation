using Windows.UI.Xaml.Shapes;

namespace ParticleSimulation.Models
{
    public class Particle
    {
        public Ellipse Shape { get; set; }
        public double HorizontalPosition { get; set; }
        public double VerticalPosition { get; set; }
        public double HorizontalSpeed { get; set; }
        public double VerticalSpeed { get; set; }
    }
}
