using System;
using System.Reflection;

namespace Reflection2
{
	class Program
	{
		static void Main(string[] args)
		{
			var eventPublisher = new EventPublisher();
			var method = typeof(Program).GetMethod("MyEventHandle", BindingFlags.NonPublic | BindingFlags.Instance);

			EventInfo eventInfo = typeof(EventPublisher).GetEvent("Event");
			Type eventHandlerType = eventInfo.EventHandlerType;

			var handler = Delegate.CreateDelegate(eventHandlerType, new Program(), method);

			eventInfo.AddEventHandler(eventPublisher, handler);
			eventPublisher.RaiseEvent();

			Console.Read();
		}

		void MyEventHandler(object sender, EventArgs e)
		{
			Console.WriteLine("MyEventHandler");
		}
	}

	class EventPublisher
	{
		public event EventHandler Event;

		public void RaiseEvent()
		{
			Event(this, EventArgs.Empty);
		}
	}
}