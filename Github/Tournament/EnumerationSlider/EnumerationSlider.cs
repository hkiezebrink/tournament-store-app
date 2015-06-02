namespace Tournament.EnumerationSlider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;

    /// <summary>
    /// Enumeration-bindable Slider Control.
    /// </summary>
    [TemplatePart(Name = SliderPartName, Type = typeof(Slider))]
    [TemplatePart(Name = TextBlockPartName, Type = typeof(TextBlock))]
    internal sealed class EnumerationSlider : Control
    {
        #region Constants

        private const string SliderPartName = "PART_Slider";

        private const string TextBlockPartName = "PART_TextBlock";

        #endregion Constants

        #region Dependency Property Registrations

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(EnumerationSlider), new PropertyMetadata(null, OnValueChanged));

        public static readonly DependencyProperty EnumerationProperty =
          DependencyProperty.Register("Enumeration", typeof(string), typeof(EnumerationSlider), new PropertyMetadata(null, OnEnumerationChanged));

        #endregion Dependency Property Registrations

        private List<string> names;

        #region Constructors

        public EnumerationSlider()
        {
            this.DefaultStyleKey = typeof(EnumerationSlider);
        }

        #endregion Constructors

        #region Properties

        public string Enumeration
        {
            get { return (string)GetValue(EnumerationProperty); }
            set { SetValue(EnumerationProperty, value); }
        }

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        #endregion Properties

        protected override void OnApplyTemplate()
        {
            var slider = this.GetTemplateChild(SliderPartName) as Slider;
            if (slider != null)
            {
                slider.ValueChanged += Slider_ValueChanged;
            }

            OnEnumerationChanged(this, null);
            OnValueChanged(this, null);
            base.OnApplyTemplate();
        }

        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            this.Value = this.names[(int)(sender as Slider).Value];
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EnumerationSlider es = d as EnumerationSlider;

            if (es != null)
            {
                var slider = es.GetTemplateChild(SliderPartName) as Slider;
                if (slider != null)
                {
                    if (es.Value == null)
                    {
                        slider.Value = 0;
                    }
                    else
                    {
                        slider.Value = es.names.IndexOf(es.Value.ToString());
                    }
                }
            }
        }

        private static void OnEnumerationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EnumerationSlider es = d as EnumerationSlider;

            if (es != null)
            {
                // TODO: wrap in a Try block.
                Type t = Type.GetType(es.Enumeration);
                es.names = Enum.GetNames(t).ToList();

                var slider = es.GetTemplateChild(SliderPartName) as Slider;
                if (slider != null)
                {
                    slider.Value = 0;
                    slider.Maximum = es.names.Count() - 1;
                }
            }
        }
    }
}