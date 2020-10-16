using NuclearWinter.Models;
using System.Collections.Generic;

namespace NuclearWinter.Data
{
    public class DataContext
    {
        public AbilitieLt[] abilitiesLt { get; set; }
        public PersonLt[] personsLt { get; set; }

        public readonly List<PersonLt> personList = new List<PersonLt>();
        public readonly List<AbilitieLt> abilitiesList = new List<AbilitieLt>();
        public readonly List<GamePerson> gamePerson = new List<GamePerson>();
    }
}