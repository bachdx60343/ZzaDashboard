using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZzaDashboard
{
    public static class ViewModelLocator
    {
        public static bool GetAutoWireViewModel(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoWireViewModelProperty, value);
        }

        // Using a DependencyProperty as the backing store for AutoWireViewModelProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoWireViewModelProperty =
            DependencyProperty.RegisterAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), 
                new PropertyMetadata(false, AutoWireViewModelChanged));

        private static void AutoWireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(d)) return;

            var viewType = d.GetType(); //we assume the property will be set only on elements which root is View
            var viewTypeName = viewType.FullName;
            var viewModelTypeName = viewTypeName + "Model";  //based on convention
            var viewModelType = Type.GetType(viewModelTypeName);
            var viewModel = Activator.CreateInstance(viewModelType);
            ((FrameworkElement)d).DataContext = viewModel;
        }
    }
}
