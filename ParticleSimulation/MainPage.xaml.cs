using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using ParticleSimulation.Models;

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
                    Left = rand.Next(0, wComponent),
                    Top = rand.Next(0, hComponent),
                    Color = scb[rand.Next(0, 9)],
                    HorizontalDirection = rand.Next(1, 2) * -1,
                    VerticalDirection = rand.Next(1, 2) * -1,
                    Shape = new Ellipse()
                    {
                        Width = radius,
                        Height = radius
                    }
                };
                particle.Shape.Fill = particle.Color;
                particles.Add(particle);
                //particles[i].Left = rand.Next(0, wComponent);
                //particles[i].Top = rand.Next(0, hComponent);
                //particles[i].Color = scb[rand.Next(0, 9)];

                //particles[i].HorizontalDirection = rand.Next(1, 2) * -1;
                //particles[i].VerticalDirection = rand.Next(1, 2) * -1;

                //particles[i].Shape = new Ellipse();
                //particles[i].Shape.Width = radius;
                //particles[i].Shape.Height = radius;
                //particles[i].Shape.Fill = particles[i].Color;

                Canvas.Children.Add(particles[i].Shape);
                Canvas.SetLeft(particles[i].Shape, particles[i].Left);
                Canvas.SetTop(particles[i].Shape, particles[i].Top);
            }
        }

        private void CompositionTarget_Rendering(object sender, object e)
        {
            // Loop over all the particles.
            for (int i = 0; i < maxParticles; i++)
            {
                // Update the horizontal position.
                particles[i].Left += particles[i].HorizontalDirection;

                // If outside the bounds, then invert the direction of motion.
                if (particles[i].Left <= 0)
                {
                    particles[i].HorizontalDirection = 1;
                }
                else if (particles[i].Left >= wComponent)
                {
                    particles[i].HorizontalDirection = -1;
                }

                // Update the vertical position.
                particles[i].Top += particles[i].VerticalDirection;

                // If outside the bounds, then invert the direction of motion.
                if (particles[i].Top <= 0)
                {
                    particles[i].VerticalDirection = 1;
                }
                else if (particles[i].Top >= hComponent)
                {
                    particles[i].VerticalDirection = -1;
                }

                // Update the position of the ellipse on the Canvas.
                particles[i].Shape.SetValue(Canvas.LeftProperty, particles[i].Left);
                particles[i].Shape.SetValue(Canvas.TopProperty, particles[i].Top);
            }
        }
    }
}
