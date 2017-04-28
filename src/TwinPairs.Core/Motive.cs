
using System.Diagnostics.Contracts;

namespace TwinPairs.Core
{
    public  class Motive
    {
        public int Id { get; }
        public string Name { get;}

        public Motive(int id, string name)
        {
            Contract.Requires(id != 0);
            Contract.Requires(!string.IsNullOrEmpty(name));

            this.Id = id;
            this.Name = name;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Motive);
        }

        public bool Equals(Motive obj)
        {
            if (obj == null)
                return false;

            return int.Equals( this.Id, obj.Id) && string.Equals(this.Name, obj.Name);
        }

        public override int GetHashCode()
        {
            return HashCombiner.CombineHashCodes(this.Id, this.Name);
        }
    }
}
