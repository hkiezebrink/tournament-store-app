using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Tournament.Common;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Tournament
{
	/// <summary>
	/// Matches. The logic should be in the corresponding ViewModel (MVVM pattern)
	/// </summary>
	public sealed partial class Matches : ViewBase
	{
		public Matches()
        {
            this.InitializeComponent();
        }
	}
}
