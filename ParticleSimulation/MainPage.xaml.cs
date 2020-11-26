using ParticleSimulation.Models;
using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ParticleSimulation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const int MAXWIDTH = 1280;
        private const int MAXHEIGHT = 720;

        private const int maxParticles = 64;    // Number of particles to draw
        private float radius = 32.0f;           // Particle size

        // Store the width and height limits
        private int wComponent;
        private int hComponent;

        private List<Particle> particles;

        public static Random rand = new Random();

        public MainPage()
        {
            this.InitializeComponent();
            InitParticles();
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        public void InitParticles()
        {
            particles = new List<Particle>(maxParticles);

            // Create an array of colours.
            SolidColorBrush[] scb = new SolidColorBrush[10];

            scb[0] = new SolidColorBrush(Color.FromArgb(255, 124, 212, 85));
            scb[1] = new SolidColorBrush(Color.FromArgb(255, 124, 85, 212));
            scb[2] = new SolidColorBrush(Color.FromArgb(255, 85, 124, 212));
            scb[3] = new SolidColorBrush(Color.FromArgb(255, 212, 124, 85));
            scb[4] = new SolidColorBrush(Color.FromArgb(255, 212, 85, 124));
            scb[5] = new SolidColorBrush(Color.FromArgb(255, 85, 212, 124));
            scb[6] = new SolidColorBrush(Color.FromArgb(255, 24, 21, 85));
            scb[7] = new SolidColorBrush(Color.FromArgb(255, 24, 85, 21));
            scb[8] = new SolidColorBrush(Color.FromArgb(255, 85, 21, 24));
            scb[9] = new SolidColorBrush(Color.FromArgb(255, 85, 85, 124));

            // Set the x and y bounds.
            wComponent = MAXWIDTH - (int)radius;
            hComponent = MAXHEIGHT - (int)radius;

            // Loop over all the particles and draw them on the canvas.
            for (int i = 0; i < maxParticles; i++)
            {
                var particle = new Particle()
                {
                    HorizontalPosition = rand.Next(0, wComponent),
                    VerticalPosition = rand.Next(0, hComponent),
                    HorizontalSpeed = rand.Next(1, 5) * -1,
                    VerticalSpeed = rand.Next(1, 5) * -1,
                    Shape = new Ellipse()
                    {
                        Width = radius,
                        Height = radius
                    }
                };
                particle.Shape.Fill = scb[rand.Next(0, 9)];
                particles.Add(particle);

                Canvas.Children.Add(particles[i].Shape);
                Canvas.SetLeft(particles[i].Shape, particles[i].HorizontalPosition);
                Canvas.SetTop(particles[i].Shape, particles[i].VerticalPosition);
            }
        }

        private void CompositionTarget_Rendering(object sender, object e)
        {
            // Loop over all the particles.
            for (int i = 0; i < maxParticles; i++)
            {

                // If outside the bounds, then invert the direction of motion.
                if (particles[i].HorizontalPosition <= 0 || particles[i].HorizontalPosition >= wComponent)
                {
                    particles[i].HorizontalSpeed *= -1;
                }

                // Update the horizontal position.
                particles[i].HorizontalPosition += particles[i].HorizontalSpeed;

                // If outside the bounds, then invert the direction of motion.
                if (particles[i].VerticalPosition <= 0 || particles[i].VerticalPosition >= hComponent)
                {
                    particles[i].VerticalSpeed *= -1;
                }

                // Update the vertical position.
                particles[i].VerticalPosition += particles[i].VerticalSpeed;

                // Update the position of the ellipse on the Canvas.
                particles[i].Shape.SetValue(Canvas.LeftProperty, particles[i].HorizontalPosition);
                particles[i].Shape.SetValue(Canvas.TopProperty, particles[i].VerticalPosition);
            }
        }
    }
}
