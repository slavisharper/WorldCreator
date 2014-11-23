using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using WorldCreator.ViewModels;

namespace WorldCreator.Common
{
    public class Animator
    {
        private const int DefaultSteps = 10;
        private const int DefaultTime = 80;

        public Animator()
            :this(DefaultTime, DefaultSteps)
        { }

        public Animator(int time, int steps)
        {
            this.Time = time;
            this.Steps = steps;
        }

        private int Time { get; set; }

        private int Steps { get; set; }

        public void MoveItem(ItemViewModel movedItem, double left, double top)
        {
            var timer = new DispatcherTimer();
            double leftStep = (left - movedItem.Left) / this.Steps;
            double topStep = (top - movedItem.Top) / this.Steps;
            int currentStep = 1;

            timer.Interval = new TimeSpan(this.Time);
            timer.Tick += (ev, args) =>
            {
                if (currentStep >= this.Steps || movedItem.Left <= 0 || movedItem.Top <= 0)
	            {
	            	timer.Stop();
	            }
                else{
                    movedItem.Left += leftStep;
                    movedItem.Top += topStep;
                    currentStep++;
                }
            };

            timer.Start();
        }
    }
}
