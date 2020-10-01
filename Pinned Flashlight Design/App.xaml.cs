using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Pinned_Flashlight_Design
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void PinnedFlashlightButton_Loaded(object sender, RoutedEventArgs e)
        {
            Border border = sender as Border;
            LoadPinnedFlashlightButton(border);
        }

        private void PinnedFlashlightButton_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Border border = sender as Border;
            double x = e.GetPosition(border).X;
            double y = e.GetPosition(border).Y;
            MovePinnedFlashlightButton(border, x, y);
        }

        private void LoadPinnedFlashlightButton(Border border)
        {
            Color backgroundColor = (border.Background as SolidColorBrush).Color;
            Color flashlightColor = Colors.Green;
            double borderSquare = (border.Width + border.Height) / 2;
            double cornerRadius = borderSquare / 200 * 38;
            border.CornerRadius = new CornerRadius(cornerRadius);
            ContentPresenter contentPresenter = border.Child as ContentPresenter;
            contentPresenter.Width = border.Width / 100 * 62;
            contentPresenter.Height = border.Height / 100 * 62;
            double contentPresenterSquare = (contentPresenter.Width + contentPresenter.Height) / 2;
            GradientStopCollection intGradientStopCollection = new GradientStopCollection();
            intGradientStopCollection.Add(new GradientStop(flashlightColor, 0));
            intGradientStopCollection.Add(new GradientStop(backgroundColor, 3.8));
            RadialGradientBrush intRadialGradientBrush = new RadialGradientBrush(intGradientStopCollection);
            intRadialGradientBrush.GradientOrigin = new Point(0.5, 0.5);
            intRadialGradientBrush.Center = new Point(0.5, 0.5);
            border.Background = intRadialGradientBrush;
            GradientStopCollection extGradientStopCollection = new GradientStopCollection();
            extGradientStopCollection.Add(new GradientStop(Colors.White, 0.38));
            extGradientStopCollection.Add(new GradientStop(backgroundColor, 0.62));
            RadialGradientBrush extRadialGradientBrush = new RadialGradientBrush(extGradientStopCollection);
            extRadialGradientBrush.GradientOrigin = new Point(0.5, 0.5);
            extRadialGradientBrush.Center = new Point(0.5, 0.5);
            border.BorderBrush = extRadialGradientBrush;
            DropShadowEffect extDropShadowEffect = new DropShadowEffect();
            extDropShadowEffect.RenderingBias = RenderingBias.Quality;
            extDropShadowEffect.Color = Colors.DarkGray;
            extDropShadowEffect.Direction = 0;
            extDropShadowEffect.ShadowDepth = 0;
            extDropShadowEffect.Opacity = 0.62;
            extDropShadowEffect.BlurRadius = borderSquare / 200 * 38;
            border.Effect = extDropShadowEffect;
            DropShadowEffect intDropShadowEffect = new DropShadowEffect();
            intDropShadowEffect.RenderingBias = RenderingBias.Quality;
            intDropShadowEffect.Color = Colors.DarkGray;
            intDropShadowEffect.Direction = 0;
            intDropShadowEffect.ShadowDepth = 0;
            intDropShadowEffect.Opacity = 0.62;
            intDropShadowEffect.BlurRadius = contentPresenterSquare / 200 * 38;
            contentPresenter.Effect = intDropShadowEffect;
        }

        private void MovePinnedFlashlightButton(Border border, double x, double y)
        {
            Color backgroundColor = (border.Background as RadialGradientBrush).GradientStops[1].Color;
            Color flashlightColor = Colors.Green;
            double borderSquare = (border.Width + border.Height) / 2;
            ContentPresenter contentPresenter = border.Child as ContentPresenter;
            double contentPresenterSquare = (contentPresenter.Width + contentPresenter.Height) / 2;
            double xPercent = x / border.Width;
            double yPercent = y / border.Height;
            int period = 0;
            if (xPercent > 0.5 && yPercent < 0.5)
                period = 1;
            else if (xPercent < 0.5 && yPercent < 0.5)
                period = 2;
            else if (xPercent < 0.5 && yPercent > 0.5)
                period = 3;
            else if (xPercent > 0.5 && yPercent > 0.5)
                period = 4;
            GradientStopCollection intGradientStopCollection = new GradientStopCollection();
            intGradientStopCollection.Add(new GradientStop(flashlightColor, 0));
            intGradientStopCollection.Add(new GradientStop(backgroundColor, 3.8));
            RadialGradientBrush intRadialGradientBrush = new RadialGradientBrush(intGradientStopCollection);
            intRadialGradientBrush.GradientOrigin = new Point(xPercent, yPercent);
            intRadialGradientBrush.Center = new Point(xPercent, yPercent);
            border.Background = intRadialGradientBrush;
            GradientStopCollection extGradientStopCollection = new GradientStopCollection();
            extGradientStopCollection.Add(new GradientStop(Colors.White, 0.38));
            extGradientStopCollection.Add(new GradientStop(backgroundColor, 0.62));
            RadialGradientBrush extRadialGradientBrush = new RadialGradientBrush(extGradientStopCollection);
            extRadialGradientBrush.GradientOrigin = new Point(xPercent, yPercent);
            extRadialGradientBrush.Center = new Point(xPercent, yPercent);
            border.BorderBrush = extRadialGradientBrush;
            //Point vector1Point = new Point(0.5, 0);
            Point vector2Point = new Point(xPercent - 0.5, yPercent - 0.5);
            double scalar = 0.5 * vector2Point.X; //+ vector1Point.Y * vector2Point.Y;
            //double vector1Absolute = Math.Sqrt(Math.Pow(vector1Point.X, 2) + Math.Pow(vector1Point.Y, 2));
            double vector2Absolute = Math.Sqrt(Math.Pow(vector2Point.X, 2) + Math.Pow(vector2Point.Y, 2));
            double angle = Math.Acos(scalar / (0.5 * vector2Absolute)) / Math.PI * 180;
            if (period > 2)
                angle = 360 - angle;
            double distanceFromCenter = Math.Sqrt(Math.Pow((xPercent - 0.5), 2) + Math.Pow((yPercent - 0.5), 2)) * borderSquare;
            DropShadowEffect extDropShadowEffect = new DropShadowEffect();
            extDropShadowEffect.RenderingBias = RenderingBias.Quality;
            extDropShadowEffect.Color = Colors.Black;
            extDropShadowEffect.Direction = angle;
            extDropShadowEffect.ShadowDepth = distanceFromCenter / 200 * 38;
            extDropShadowEffect.Opacity = 0.62;
            extDropShadowEffect.BlurRadius = borderSquare / 200 * 38;
            border.Effect = extDropShadowEffect;
            DropShadowEffect intDropShadowEffect = new DropShadowEffect();
            intDropShadowEffect.RenderingBias = RenderingBias.Quality;
            intDropShadowEffect.Color = Colors.Black;
            intDropShadowEffect.Direction = angle;
            intDropShadowEffect.ShadowDepth = distanceFromCenter / 100 * 62 / 200 * 38;
            intDropShadowEffect.Opacity = 0.62;
            intDropShadowEffect.BlurRadius = contentPresenterSquare / 200 * 38;
            contentPresenter.Effect = intDropShadowEffect;
        }
    }
}
