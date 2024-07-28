
namespace StardropTools.CharacterControllers
{
    public struct CharacterControllerModuleStateID
    {
        public int ID { get; }
        public string Name { get; }

        public static CharacterControllerModuleStateID None => new CharacterControllerModuleStateID(-1, "None");

        public CharacterControllerModuleStateID(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public override bool Equals(object obj)
        {
            return obj is CharacterControllerModuleStateID stateID &&
                   ID == stateID.ID &&
                   Name == stateID.Name;
        }
    }
}
