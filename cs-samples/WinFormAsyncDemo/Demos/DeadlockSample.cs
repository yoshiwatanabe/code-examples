using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormAsyncDemo.Demos
{
    class DeadlockSample
    {
        private static async Task DelayAsync()
        {
            // This participate in a deadlock. When delay is over after 1000 miliseconds, it wants to
            // resume on the thread of SynchronizationContext, but that thread is waiting for DelayAsync
            // to complete.
            await Task.Delay(1000);            
        }

        // This method causes a deadlock when called in a GUI or ASP.NET context.
        public static void Demo()
        {
            Debug.WriteLine("Shows that SynchronizationContext.Current is not null. IsWaitNotificationRequired retuns: {0}", WindowsFormsSynchronizationContext.Current.IsWaitNotificationRequired());
            /*
             *
             * WindowsFormsSynchronizationContext (System.Windows.Forms.dll: System.Windows.Forms) 
             * Windows Forms apps will create and install a WindowsFormsSynchronizationContext as the current 
             * context for any thread that creates UI controls. This SynchronizationContext uses the 
             * ISynchronizeInvoke methods on a UI control, which passes the delegates to the underlying 
             * Win32 message loop. The context for WindowsFormsSynchronizationContext is a single UI thread.
             * All delegates queued to the WindowsFormsSynchronizationContext are executed one at a time; 
             * they’re executed by a specific UI thread in the order they were queued. The current 
             * implementation creates one WindowsFormsSynchronizationContext for each UI thread.
             */

            Debug.WriteLine("ID: {0}, IsBackgroundThread: {1}", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsBackground);
            
            // Start the delay.
            var delayTask = DelayAsync();
            
            // Wait for the delay to complete.
            // This will participate in a deadlock.
            // The thread of the SynchronizationContext is waiting for DelayAsync to complete, but
            // DelayAsync's Task.Delay() is also waiting for the same thread, in the SynchronizationContext, to
            // become available so that it can "resume".
            delayTask.Wait();
        }
    }
}
