using System;
using System.Collections.Generic;
using TylerButler.GameToolkit;

namespace TylerButler.Kingsburg.Core
{
    [Serializable]
    public class Enemy : Character
    {
        private int level = 1, strength = 1, goodPenalty = 0, goldPenalty = 0, woodPenalty = 0, stonePenalty = 0, victoryPointPenalty = 0,
            buildingPenalty = 0, goodReward = 0, goldReward = 0, woodReward = 0, stoneReward = 0, victoryPointReward = 0;

        public Enemy(string nameIn, string descriptionIn)
            : base(nameIn, descriptionIn)
        {
        }

        public Enemy()
            : this("Enemy name", "Enemy description")
        {
        }

        public int Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
            }
        }

        public int Strength
        {
            get
            {
                return strength;
            }
            set
            {
                strength = value;
            }
        }

        public int GoodPenalty
        {
            get
            {
                return goodPenalty;
            }
            set
            {
                goodPenalty = value;
            }
        }

        public int GoldPenalty
        {
            get
            {
                return goldPenalty;
            }
            set
            {
                goldPenalty = value;
            }
        }

        public int WoodPenalty
        {
            get
            {
                return woodPenalty;
            }
            set
            {
                woodPenalty = value;
            }
        }

        public int StonePenalty
        {
            get
            {
                return stonePenalty;
            }
            set
            {
                stonePenalty = value;
            }
        }

        public int VictoryPointPenalty
        {
            get
            {
                return victoryPointPenalty;
            }
            set
            {
                victoryPointPenalty = value;
            }
        }

        public int BuildingPenalty
        {
            get
            {
                return buildingPenalty;
            }
            set
            {
                buildingPenalty = value;
            }
        }

        public int GoodReward
        {
            get
            {
                return goodReward;
            }
            set
            {
                goodReward = value;
            }
        }
        
        public int GoldReward
        {
            get
            {
                return goldReward;
            }
            set
            {
                goldReward = value;
            }
        }

        public int WoodReward
        {
            get
            {
                return woodReward;
            }
            set
            {
                woodReward = value;
            }
        }

        public int StoneReward
        {
            get
            {
                return stoneReward;
            }
            set
            {
                stoneReward = value;
            }
        }

        public int VictoryPointReward
        {
            get
            {
                return victoryPointReward;
            }
            set
            {
                victoryPointReward = value;
            }
        }
        private EnemyType type;

        public EnemyType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

    }

    // Public because that's necesarry to serialize
    [Serializable]
    public class EnemyCollection : List<Enemy>
    {
        internal EnemyCollection()
        {
        }
    }

    [Serializable]
    public enum EnemyType
    {
        Barbarians,
        Goblins,
        Orcs,
        Zombies,
        Demons,
        Dragons,
    }
}
