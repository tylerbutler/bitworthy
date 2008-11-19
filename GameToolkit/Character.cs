using System;

namespace TylerButler.GameToolkit
{
    public abstract class Character
    {
        private string description;
        private string name;

        public Character(string nameIn, string descriptionIn)
        {
            this.Name = nameIn;
            this.Description = descriptionIn;
        }


        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (value.Length <= 0 || value == null)
                {
                    throw new ArgumentNullException("Character.Name",
                        "Character.Name must not be null or less than 1 character.");
                }
                else
                {
                    this.name = value;
                }
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }
    }
}
