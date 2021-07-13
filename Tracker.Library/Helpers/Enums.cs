using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace Tracker.Library.Helpers {

    public enum Classes {

        Warrior = 1,
        Paladin = 2,
        Hunter = 3,
        Rogue = 4,
        Priest = 5,
        Shaman = 6,
        Mage = 7,
        Warlock = 8,
        Monk = 9,
        Druid = 10,
        [EnumMember(Value = "Demon Hunter")] DemonHunter = 11,
        [EnumMember(Value = "Death Knight")] DeathKnight = 12

    }

    public enum Professions {

        Alchemy = 1,
        Blacksmithing = 2,
        Enchanting = 3,
        Engineering = 4,
        Inscription = 5,
        Jewelcrafting = 6,
        Leatherworking = 7,
        Tailoring = 8

    }

    public static class Converter {

        public static List<string> AvailableClasses() {
            List<string> classes = new();

            foreach (var name in Enum.GetNames(typeof(Classes))) {
                var memberAttribute = typeof(Classes).GetField(name)?
                    .GetCustomAttribute(typeof(EnumMemberAttribute)) as EnumMemberAttribute;

                classes.Add(memberAttribute?.Value ?? name);
            }

            return classes;
        }

        public static List<string> AvailableProfessions() {
            List<string> professions = new();

            foreach (var profession in Enum.GetNames(typeof(Professions))) {
                professions.Add(profession);
            }

            return professions;
        }

    }

}