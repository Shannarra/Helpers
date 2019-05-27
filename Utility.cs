namespace WorkerApplication
{
    using System;

    public static class Utility
    {
        /// <summary>
        /// Redirects to another XAML view.
        /// </summary>
        /// <param name="viewType">The type of the view. To get it use the <see cref="typeof()"/> function.</param>
        public static void RedirectTo(System.Type viewType)
        {
            Windows.UI.Xaml.Controls.Frame frame = new Windows.UI.Xaml.Controls.Frame();
            frame.Navigate(viewType, null);
            Windows.UI.Xaml.Window.Current.Content = frame;
        }

        /// <summary>
        /// Displays a <see cref="Windows.UI.Xaml.Controls.ContentDialog"/>.
        /// </summary>
        /// <param name="title">The dialog's title.</param>
        /// <param name="textContent">The dialog's text contents.</param>
        /// <param name="primaryButton">The primary button text.</param>
        /// <param name="secondaryButton">The secondary button text.</param>
        /// <param name="defaultBtn">Which <see cref="Windows.UI.Xaml.Controls.ContentDialogButton"/> will be primary?</param>
        /// <returns>bool? result of displaying.</returns>
        public static bool? ShowDialog
            (string title, string primaryButton, string secondaryButton, string textContent = "",
            Windows.UI.Xaml.Controls.ContentDialogButton? defaultBtn = Windows.UI.Xaml.Controls.ContentDialogButton.Primary)
        {
            Windows.UI.Xaml.Controls.ContentDialog dialog = new Windows.UI.Xaml.Controls.ContentDialog
            {
                Title = title,
                Content = textContent,
                PrimaryButtonText = primaryButton,
                SecondaryButtonText = secondaryButton,
                CloseButtonText = "Cancel",
                DefaultButton = (Windows.UI.Xaml.Controls.ContentDialogButton)defaultBtn
            };

            Windows.Foundation.IAsyncOperation<Windows.UI.Xaml.Controls.ContentDialogResult> operation = dialog.ShowAsync(); //run the dialog asynchronously
            if (operation.Status == Windows.Foundation.AsyncStatus.Started) //check the status
                return true;
            return null;
        }

        /// <summary>
        /// Sets the given <paramref name="pic"/> with another picture.
        /// </summary>
        /// <param name="pic"><see cref="Windows.UI.Xaml.Controls.PersonPicture"/> to be changed</param>
        /// <returns>bool? result of the job done.</returns>
        public static async System.Threading.Tasks.Task<bool?> SetPictureAsync(Windows.UI.Xaml.Controls.PersonPicture pic)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker
            {
                ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail,
                SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary
            };
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            var file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                var image = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
                image.SetSource(stream);
                pic.ProfilePicture = image;
                return true; //picture changed successfully
            }
            else
                return null; //the file is null

        }


        /// <summary>
        /// Returns a Brush for the given ARGB values.
        /// </summary>
        /// <param name="r">Red</param>
        /// <param name="g">Blue</param>
        /// <param name="b">Green</param>
        /// <returns></returns>
        public static Windows.UI.Xaml.Media.Brush ConvertColor(int r, int g, int b)
        {
            return new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(Convert.ToByte(255), Convert.ToByte(r), Convert.ToByte(g), Convert.ToByte(b)));
        }
    }
}
