using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Collections;
using TylerButler.Kingsburg.Utilities;

namespace TylerButler.Kingsburg.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<Building> bs = DataLoader.LoadBuildings(@"Data\Buildings.xml");
            List<Advisor> advisors = DataLoader.LoadAdvisors(@"Data\Advisors.xml");
            //Dictionary<Enemy, int> e = DataLoader.LoadEnemies(@"Data\Enemies.xml");

            Advisor a = advisors[(int)Advisors.MasterHunter];
            a.AdvisorNameEnum = Advisors.MasterHunter;
            a.Name = "foobar"; // should fail
            //XmlSerializer serializer = new XmlSerializer(typeof(Advisor));
            //TextWriter writer = new StreamWriter(@"c:\my.xml");
            //serializer.Serialize(writer, a);
            //writer.Close();
        }
    }
}
