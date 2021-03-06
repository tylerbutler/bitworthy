using System.Collections.Generic;
using TylerButler.GameToolkit;
using TylerButler.Kingsburg.Utilities;
using System.Xml.Serialization;
using System.IO;
using TylerButler.Kingsburg.Core.UI;

namespace TylerButler.Kingsburg.Core
{
    class ProgramRunner
    {
        static void Main(string[] args)
        {
            //List<Building> bs = DataLoader.LoadBuildings(@"Data\Buildings.xml");
            //List<Advisor> advisors = DataLoader.LoadAdvisors( @"Data\Advisors.xml" );
            //Dictionary<Enemy, int> e = DataLoader.LoadEnemies(@"Data\Enemies.xml");

            //Advisor a = advisors[(int)Advisors.MasterHunter];
            //a.AdvisorNameEnum = Advisors.MasterHunter;
            //a.Name = "foobar"; // should fail
            //Enemy e = new Enemy( "Name", "Description" );
            //e.Type = EnemyType.Dragons;
            //EnemyCollection ec = new EnemyCollection();
            //ec.Add( e );
            //XmlSerializer serializer = new XmlSerializer( typeof( EnemyCollection ) );
            //TextWriter writer = new StreamWriter( @"c:\my.xml" );
            //serializer.Serialize( writer, ec );
            //writer.Close();

            GameManager.Instance.UI = new UIManager();
            GameManager.Instance.MainExecutionMethod();

            //KingsburgDie d = new KingsburgDie( KingsburgDie.DieTypes.Regular );
            //d.Value = 1;
            //DiceBag<KingsburgDie> bag = new DiceBag<KingsburgDie>( 3, 6 );
            //bag.Dice[0].Value = 1;
            //bag.Dice[1].Value = 4;
            //bag.Dice[2].Value = 3;
            //bag.AddDie( new KingsburgDie( KingsburgDie.DieTypes.White ) );
            //bag.Dice[3].Value = 2;

            //List<List<KingsburgDie>> combos;
            //Dictionary<int, List<List<KingsburgDie>>> d = new Dictionary<int, List<List<KingsburgDie>>>();
            //Helpers.SumComboFinder comboFinder = new Helpers.SumComboFinder();
            //HashSet<int> foo = Helpers.Sums( bag.Values );
            //foreach( int sum in foo )
            //{
            //    combos = comboFinder.Find( sum, bag.Dice );
            //    d[sum] = combos;
            //}
            //List<List<KingsburgDie>> temp = d[2];
            //temp = d[3];
        }
    }
}
