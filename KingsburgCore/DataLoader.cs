using System;
using System.Collections.Generic;
using System.Text;
using TylerButler.Kingsburg.Core;
using System.IO;
using System.Xml.Serialization;

namespace TylerButler.Kingsburg.Utilities
{
    class DataLoader
    {
        private static XmlSerializer serializer;

        public static List<Building> LoadBuildings(string pathToFile)
        {
            serializer = new XmlSerializer(typeof(List<Building>));
            TextReader reader = new StreamReader(pathToFile);
            List<Building> toReturn = (List<Building>)serializer.Deserialize(reader);
            reader.Close();
            return toReturn;
        }

        public static List<Advisor> LoadAdvisors(string pathToFile)
        {
            serializer = new XmlSerializer(typeof(List<Advisor>));
            TextReader reader = new StreamReader(pathToFile);
            List<Advisor> toReturn = (List<Advisor>)serializer.Deserialize(reader);
            reader.Close();
            toReturn.Sort(new AdvisorOrderComparer());
            return toReturn;
        }

        public static Dictionary<Enemy, int> LoadEnemies(string pathToFile)
        {
            serializer = new XmlSerializer(typeof(SerializableDictionary<Enemy, int>));
            TextReader reader = new StreamReader(pathToFile);
            Dictionary<Enemy, int> toReturn = (SerializableDictionary<Enemy, int>)serializer.Deserialize(reader);
            reader.Close();
            return toReturn;
        }
    }
}
