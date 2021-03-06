using ElmSharp.Wearable;
using System.Windows.Input;
using TextReader.Utils;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TextReader.Tizen.Wearable.Views
{
    /// <summary>
    /// Page with text to be read.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextReaderPage : CirclePage
    {
        #region properties

        /// <summary>
        /// Bindable property for BackButtonPressed command.
        /// </summary>
        public static BindableProperty BackButtonPressedProperty =
            BindableProperty.Create(nameof(BackButtonPressed), typeof(ICommand), typeof(TextReaderPage));

        /// <summary>
        /// Bindable property for UpdateActiveParagraph command.</see>
        /// </summary>
        public static BindableProperty UpdateActiveParagraphCommandProperty =
            BindableProperty.Create(nameof(UpdateActiveParagraphCommand), typeof(ICommand), typeof(TextReaderPage));

        /// <summary>
        /// Command to execute by pressing the back button.
        /// </summary>
        public ICommand BackButtonPressed
        {
            get => (ICommand)GetValue(BackButtonPressedProperty);
            set => SetValue(BackButtonPressedProperty, value);
        }

        /// <summary>
        /// Command updating active paragraph.
        /// </summary>
        private ICommand UpdateActiveParagraphCommand
        {
            get => (ICommand)GetValue(UpdateActiveParagraphCommandProperty);
            set => SetValue(UpdateActiveParagraphCommandProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes the class.
        /// </summary>
        public TextReaderPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called when the page appears.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            RotaryEventManager.Rotated += OnRotationChanged;
        }

        /// <summary>
        /// Called when the page disappears.
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            RotaryEventManager.Rotated -= OnRotationChanged;
        }

        /// <summary>
        /// Method called when the back button is pressed.
        /// Executes BackButtonPressed command.
        /// Calls "base.OnBackButtonPressed" method.
        /// </summary>
        /// <returns>Result of "base.OnBackButtonPressed" method.</returns>
        protected override bool OnBackButtonPressed()
        {
            BackButtonPressed?.Execute(null);
            return base.OnBackButtonPressed();
        }

        /// <summary>
        /// Handles rotation change.
        /// Executes UpdateActiveParagraph command.
        /// </summary>
        /// <param name="args">Event arguments.</param>
        private void OnRotationChanged(ElmSharp.Wearable.RotaryEventArgs args)
        {
            UpdateActiveParagraphCommand.Execute(
                args.IsClockwise ? UtteranceChangeType.Next : UtteranceChangeType.Previous);
        }

        #endregion
    }
}