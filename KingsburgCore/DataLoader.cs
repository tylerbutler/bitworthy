using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using TylerButler.Kingsburg.Core;

namespace TylerButler.Kingsburg.Utilities
{
    class DataLoader
    {
        private static XmlSerializer serializer;

        public static BuildingCollection LoadBuildings(string pathToFile)
        {
            serializer = new XmlSerializer(typeof(List<Building>));
            TextReader reader = new StreamReader(pathToFile);
            List<Building> toReturn = (List<Building>)serializer.Deserialize(reader);
            reader.Close();
            return new BuildingCollection(toReturn);
        }

        public static AdvisorCollection LoadAdvisors(string pathToFile)
        {
            serializer = new XmlSerializer(typeof(List<Advisor>));
            TextReader reader = new StreamReader(pathToFile);
            List<Advisor> toReturn = (List<Advisor>)serializer.Deserialize(reader);
            reader.Close();
            toReturn.Sort(new AdvisorOrderComparer());
            return new AdvisorCollection(toReturn);
        }

        public static EnemyCollection LoadEnemies(string pathToFile)
        {
            serializer = new XmlSerializer(typeof(EnemyCollection));
            TextReader reader = new StreamReader(pathToFile);
            EnemyCollection toReturn = (EnemyCollection)serializer.Deserialize( reader );
            reader.Close();
            return toReturn;
        }
    }
}
