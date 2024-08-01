﻿using System;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Color = System.Windows.Media.Color;
using Label = System.Windows.Controls.Label;
using System.Media;
using System.IO;

namespace Trixi.V2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Label labelR;
        public Label labelA;
        public Label TurnLabel;
        private DispatcherTimer timer;
        public int act = 0;
        private SoundPlayer Musica;
        private SoundPlayer buttonSound = new SoundPlayer();
        
        public MainWindow()
        {
            InitializeComponent();
            PlayBackGroundMusic();
            labelR = new Label();
            labelR.Content = "Puntaje Rojo: " + PuntajeR;
            labelR.Width = 100;
            labelR.Height = 100;
            Grid.SetRow(labelR, 0);
            Grid.SetColumn(labelR, 3);
            grid.Children.Add(labelR);
            
            labelA = new Label();
            labelA.Content = "Puntaje Azul: " + PuntajeA;
            labelA.Width = 100;
            labelA.Height = 100;
            Grid.SetRow(labelA, 2);
            Grid.SetColumn(labelA, 3);
            grid.Children.Add(labelA);
            
            TurnLabel = new Label();
            TurnLabel.Width = 100;
            TurnLabel.Height = 100;
            Grid.SetRow(TurnLabel, 1);
            Grid.SetColumn(TurnLabel, 3);
            grid.Children.Add(TurnLabel);

            UpdateTurnLabel();

        }

        int PuntajeR = 0;
        int PuntajeA = 0;
        bool TurnoRojo = true;
        bool[] Activaciones = new bool[9];
        bool[] ActivacionesR = new bool[9];
        bool[] ActivacionesA = new bool[9];
        int gano = 0;
        int activacion = 0;
        Ellipse circulo;
        private void ActualizarPuntaje(int puntaje, int puntaje2)
        {
            labelR.Content = "Puntaje Rojo: " + puntaje.ToString();
            labelA.Content = "Puntaje Azul: " + puntaje2.ToString();
        }
        public void UpdateTurnLabel()
        {
            if (TurnoRojo == false)
            {
                TurnLabel.Content = "Turno = Azul";
            }
            else
            {
                TurnLabel.Content = "Turno = Rojo";
            }
        }
        public void PlayBackGroundMusic()
        {
            Musica = new SoundPlayer();
            Musica.Stream = Trixi.V2.Properties.Resources.background_music;
            buttonSound.Stream = Trixi.V2.Properties.Resources.button_124476;
            Musica.PlayLooping();
        }

        private bool ShowMathProblem()
        {
            Random rand = new Random();
            int Ope = rand.Next(1, 4);
            string input = "";
            int correctAnswer = 0;
            int a = rand.Next(1, 100);
            int b = rand.Next(1, 100);
            switch (Ope)
            {
                case 1:
                     input = Microsoft.VisualBasic.Interaction.InputBox($"Resuelve: {a} + {b} =", "Problema Matemático", "");
                    correctAnswer = a + b;
                    break;
                case 2:
                    input = Microsoft.VisualBasic.Interaction.InputBox($"Resuelve: {a} - {b} =", "Problema Matemático", "");
                    correctAnswer = a - b;
                    break;
                case 3:
                    input = Microsoft.VisualBasic.Interaction.InputBox($"Resuelve: {a} * {b} =", "Problema Matemático", "");
                    correctAnswer = a * b;
                    break;
                case 4:
                    input = Microsoft.VisualBasic.Interaction.InputBox($"Resuelve: {a} / {b} =", "Problema Matemático", "");
                    correctAnswer = a / b;
                    break;
            }
            
            if (int.TryParse(input, out int userAnswer))
            {
                return userAnswer == correctAnswer;
            }
            return false;
        }
        public void quienGano()
        {
            
            if (ActivacionesA[0] == true && ActivacionesA[1] == true && ActivacionesA[2] == true || ActivacionesA[3] == true && ActivacionesA[4] == true
                && ActivacionesA[5] == true || ActivacionesA[6] == true && ActivacionesA[7] == true && ActivacionesA[8] == true || ActivacionesA[0] == true
                && ActivacionesA[4] == true && ActivacionesA[8] == true || ActivacionesA[2] == true && ActivacionesA[4] == true && ActivacionesA[6] == true
                || ActivacionesA[0] == true && ActivacionesA[3] == true && ActivacionesA[6] == true || ActivacionesA[1] == true && ActivacionesA[4] == true
                && ActivacionesA[7] == true || ActivacionesA[2] == true && ActivacionesA[5] && ActivacionesA[8] == true)
            {
                MessageBox.Show("Gano el Azul!!");

                PuntajeA++;
                ActualizarPuntaje(PuntajeR, PuntajeA);
                ResetGame();
            }
            else if (ActivacionesR[0] == true && ActivacionesR[1] == true && ActivacionesR[2] == true || ActivacionesR[3] == true && ActivacionesR[4] == true
                && ActivacionesR[5] == true || ActivacionesR[6] == true && ActivacionesR[7] == true && ActivacionesR[8] == true || ActivacionesR[0] == true
                && ActivacionesR[4] == true && ActivacionesR[8] == true || ActivacionesR[2] == true && ActivacionesR[4] == true && ActivacionesR[6] == true
                || ActivacionesR[0] == true && ActivacionesR[3] == true && ActivacionesR[6] == true || ActivacionesR[1] == true && ActivacionesR[4] == true
                && ActivacionesR[7] == true || ActivacionesR[2] == true && ActivacionesR[5] && ActivacionesR[8] == true)
            {
                MessageBox.Show("Gano el Rojo!!");
                PuntajeR++;
                ActualizarPuntaje(PuntajeR, PuntajeA);
                ResetGame();
            }
            else if (activacion == 9)
            {
                MessageBox.Show("Empate!!");
                ResetGame();
            }
        }
        public void Circulos(int colum, int row, Color color)
        {
            var brush = new SolidColorBrush(color);
            circulo = new Ellipse();
            circulo.Width = 100;
            circulo.Height = 100;
            circulo.Fill = brush;
            circulo.VerticalAlignment = VerticalAlignment.Center;
            circulo.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetColumn(circulo, colum);
            Grid.SetRow(circulo, row);
            grid.Children.Add(circulo);
        }

        public void Origin_button_Click(int columna, int Row, int numero, int numero2)
        {
            buttonSound.Play();
            if (Activaciones[numero2] == true)
            {
                return;
            }
            if (!ShowMathProblem())
            {
                MessageBox.Show("Respuesta incorrecta.");
                if(TurnoRojo == true)
                {
                    TurnoRojo = false;
                }
                else
                {
                TurnoRojo = true; 
                }
                UpdateTurnLabel();
                return;
            }
            quienGano();
            if (TurnoRojo == true)
            {
                Circulos(columna, Row, Colors.Red);
                ActivacionesR[numero] = true;
                TurnoRojo = false;
            }
            else
            {
                Circulos(columna, Row, Colors.Blue);
                ActivacionesA[numero] = true;
                TurnoRojo = true;
            }
            Activaciones[numero2] = true;
            activacion++;
            UpdateTurnLabel();
            act++;
            quienGano();
            
        }
        public void Button_Click0(object sender, RoutedEventArgs e)
        {
            Button boton = (Button)sender; // Convertir el sender al tipo Button
            int identificador = Convert.ToInt32(boton.Tag);

            switch (identificador)
            {
                case 1:
                    Origin_button_Click(0, 0, 0, 0);
                    break;
                case 2:
                    Origin_button_Click(0, 1, 1, 1);
                    break;
                case 3:
                    Origin_button_Click(0, 2, 2, 2);
                    break;
                case 4:
                    Origin_button_Click(1, 0, 3, 3);
                    break;
                case 5:
                    Origin_button_Click(1, 1, 4, 4);
                    break;
                case 6:
                    Origin_button_Click(1, 2, 5, 5);
                    break;
                case 7:
                    Origin_button_Click(2, 0, 6, 6);
                    break;
                case 8:
                    Origin_button_Click(2, 1, 7, 7);
                    break;
                case 9:
                    Origin_button_Click(2, 2, 8, 8);
                    break;
                default:

                    break;

            }
        }
        public void ResetGame()
        {
            var ellipses = grid.Children.OfType<Ellipse>().ToList();
            foreach (var ellipse in ellipses)
            {
                grid.Children.Remove(ellipse);
            }
            for (int i = 0; i < ActivacionesR.Length; i++)
            {
                ActivacionesR[i] = false;
            }
            for (int i = 0; i < ActivacionesA.Length; i++)
            {
                ActivacionesA[i] = false;
            }
            for (int i = 0; i < Activaciones.Length; i++)
            {
                Activaciones[i] = false;
            }
            gano = 0;
            activacion = 0;
        }

    }
}