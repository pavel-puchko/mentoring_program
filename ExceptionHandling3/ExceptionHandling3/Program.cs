using System;

namespace ExceptionHandling3
{
	class Program
	{
		static void Main(string[] args)
		{
		}
	}

	[Serializable]
	public class StackNetworkException : Exception
	{
		private readonly int state;
		public StackNetworkException()
		{

		}

		public StackNetworkException(string message) : base(message)
		{

		}

		public StackNetworkException(string message, int state) : base(message)
		{
			this.state = state;
		}

		public StackNetworkException(string message, Exception inner) : base(message, inner)
		{

		}

		public StackNetworkException(string message, int state, Exception inner) : base(message, inner)
		{
			this.state = state;
		}
	}
}
