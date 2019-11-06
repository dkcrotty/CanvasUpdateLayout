using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

//NOTE ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
//NOTE ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
//NOTE ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
//           LIne 80   this is where i was trying to do the UpDateLayout and Task delay
//           sub MyDelay...was a test ..did nothing
//     
//           This is simple..  random generate a move to a different place on the screen
//           Do it in 5000 - to 10000 times
//
//
//           I think that somehow,i am not properly doing the  
//                canvas.UpdateLayout ()  maybe application.UpdateLayout or something similar
//
//NOTE ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
//NOTE ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
//NOTE ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::


namespace DemoScreenRefreshNeed
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        void myInit()
        {
            Message.Text = "Count: ";
            Random rng = new Random();

            int intMax  = 0;
            int randomY = 0;
            int randomX = 0;

            intMax = rng.Next(5000, 10000);
            for (int intRand = 1; intRand < intMax; intRand++)
            {
                randomY = rng.Next(1, 10);
                randomX = rng.Next(1, 15);

                setImagePosition(randomY, randomX, intRand);
            }
            //int intTest = myDelay();  //this was to test what  FEC-4RP said  ..didnt work
        }

        async void setImagePosition(int intRow, int intCol, int intCount )
        {
            MyImage.SetValue(Canvas.TopProperty, (intRow * MyImage.Height) + 1 );
            MyImage.SetValue(Canvas.LeftProperty, (intRow * MyImage.Width) + 1);
            Message.Text = "Max: " + intCount.ToString() + "   :::  " + 
                           "Row: " + intRow.ToString()   + "   :::  " + 
                           "Col: " + intCol.ToString();

            int it = intCount % 5;
            if (it ==  0)
                canPlayBoard.UpdateLayout();
                //await Task.Delay(5000);
                await Task.Delay(TimeSpan.FromSeconds(5));

        }
        int   myDelay()
        {
            CancellationTokenSource source = new CancellationTokenSource();

            var t = Task.Run(async delegate
            {
                await Task.Delay(TimeSpan.FromSeconds(1.5), source.Token);
                return 42;
            });
            source.Cancel();
            try
            {
                t.Wait();
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                    Console.WriteLine("{0}: {1}", e.GetType().Name, e.Message);
            }
            return 0;
        }


        private void Start_Click(object sender, RoutedEventArgs e)
        {
            myInit();
        }
    }
}
