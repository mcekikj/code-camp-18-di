using System;
using System.Collections.Generic;

namespace EncryptionLoggerDemo
{
	class Program
	{
		static void Main(string[] args)
		{
			var messenger = new Messenger(new HeadEncryptor());

			messenger.IntegrateNewLoggerInstance(new ConsoleLogger());
			messenger.IntegrateNewLoggerInstance(new DbLogger());

			messenger.DispatchMessage("Raw message");

			Console.ReadLine();
		}
	}

	public class Messenger
	{
		private readonly IEncryptor _encryptor;
		private readonly IList<ILogger> _loggers;

		public Messenger(IEncryptor encryptor)
		{
			_encryptor = encryptor;
			_loggers = new List<ILogger>();
		}

		public void DispatchMessage(string message)
		{
			_encryptor.Encrypt(message);

			//Logic for sending encrypted message

			foreach (var logger in _loggers)
				logger.Log("Message has been successfully sent");
		}

		public void IntegrateNewLoggerInstance(ILogger logger)
				=> _loggers.Add(logger);
	}

	public interface ILogger
	{
		void Log(string message);
	}

	public class DbLogger : ILogger
	{
		public void Log(string message)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine($"Database Logger: {message}, Date: {DateTime.UtcNow.Date}");
		}
	}

	public class ConsoleLogger : ILogger
	{
		public void Log(string message)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"Console Logger: {message}, Date: {DateTime.UtcNow.Date}");
		}
	}

	public interface IEncryptor
	{
		void Encrypt(string message);
	}

	public class HeadEncryptor : IEncryptor
	{
		public void Encrypt(string message)
		{
			Console.WriteLine("Encrypting message...");

			//Encryption logic

			Console.WriteLine($"{message}_{DateTime.UtcNow.Date}");
		}
	}

	public class TailEncryptor : IEncryptor
	{
		public void Encrypt(string message)
		{
			Console.WriteLine("Encrypting message...");

			//Encryption logic

			Console.WriteLine($"{DateTime.UtcNow.Date}_{message}");
		}
	}
}