﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Blyatmir_Putin_Bot.Model
{
	public class PersistantStorage<T> where T : PersistantStorage<T>
	{
		/// <summary>
		/// Gets the file locations based on object type
		/// </summary>
		public static readonly Dictionary<Type, string> Path = new Dictionary<Type, string>()
		{
			{ typeof(Guild), $"{System.IO.Path.Combine(AppEnvironment.ConfigLocation)}Guilds.xml"},
			{ typeof(Container), $"{System.IO.Path.Combine(AppEnvironment.ConfigLocation)}Containers.xml"},
			{ typeof(User), $"{System.IO.Path.Combine(AppEnvironment.ConfigLocation)}Users.xml"},
		};


		/// <summary>
		/// Makes sure that the persistant storage has been properly initialized
		/// </summary>
		public static void InitializeStorage()
		{
			if (!File.Exists(Path[typeof(T)]))
			{
				List<T> initialisationList = new List<T>();
				//initializatinList.Add(new ServerData());

				using (StreamWriter sr = new StreamWriter(Path[typeof(T)]))
				using (XmlWriter writer = XmlWriter.Create(sr, XmlSettings()))
				{
					XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
					serializer.Serialize(writer, initialisationList);
				}

				Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} Log \t     A new GuildData file has been created");
			}
		}

		public static List<T> Read()
		{
			InitializeStorage();
			using (XmlReader reader = XmlReader.Create(Path[typeof(T)]))
			{
				XmlSerializer serialize = new XmlSerializer(typeof(List<T>));
				return serialize.Deserialize(reader) as List<T>;
			}
		}

		/// <summary>
		/// Write a list of type <typeparamref name="T"/> to an Xml file
		/// </summary>
		/// <param name="objects"></param>
		public static void Write(List<T> objects)
		{
			using (StreamWriter sr = new StreamWriter(Path[typeof(T)]))
			using (XmlWriter writer = XmlWriter.Create(sr, XmlSettings()))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
				serializer.Serialize(writer, objects);
			}
		}

		/// <summary>
		/// The settings that should be used by all XmlWriters
		/// </summary>
		/// <returns></returns>
		private static XmlWriterSettings XmlSettings()
		{
			XmlWriterSettings settings = new XmlWriterSettings();

			settings.Indent = true;
			settings.IndentChars = "    ";
			settings.CloseOutput = true;

			return settings;
		}
	}
}
