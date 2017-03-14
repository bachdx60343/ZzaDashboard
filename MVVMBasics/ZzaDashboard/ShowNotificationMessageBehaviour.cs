using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace ZzaDashboard
{
    public class ShowNotificationMessageBehaviour : Behavior<ContentControl>
    {


        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(ShowNotificationMessageBehaviour), 
            new PropertyMetadata(null, OnMessageChanged));

        private static void OnMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = d as ShowNotificationMessageBehaviour;
            behavior.AssociatedObject.Content = e.NewValue;
            behavior.AssociatedObject.Visibility = Visibility.Visible;
        }

        protected override void OnAttached()
        {
            AssociatedObject.MouseLeftButtonDown += (s, e2) =>
            {
                AssociatedObject.Visibility = Visibility.Collapsed;
            };
        }
    }
}
