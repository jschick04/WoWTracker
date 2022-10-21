using System.Collections.Generic;
using Tracker.UI.Core.Helpers;
using Tracker.UI.Core.ViewModels;

namespace Tracker.UI.DesignModels;

public class CharacterListDesignModel : CharacterListViewModel
{
    public CharacterListDesignModel()
    {
        Characters = new List<CharacterListItemViewModel>
        {
            new CharacterListItemViewModel
            {
                CanCraft = false,
                ClassInitial = "M",
                CharacterName = "Moutagg",
                ClassIconColorValue = ClassColor.Monk,
                Profession = "Enchanting - Alchemy - Cooking",
                IsSelected = false
            },
            new CharacterListItemViewModel
            {
                CanCraft = true,
                ClassInitial = "W",
                CharacterName = "Mout",
                ClassIconColorValue = ClassColor.Warrior,
                Profession = "Jewelcrafting",
                IsSelected = false
            },
            new CharacterListItemViewModel
            {
                CanCraft = false,
                ClassInitial = "R",
                CharacterName = "Makogg",
                ClassIconColorValue = ClassColor.Rogue,
                Profession = "Leatherworking",
                IsSelected = true
            },
            new CharacterListItemViewModel
            {
                CanCraft = false,
                ClassInitial = "DK",
                CharacterName = "Erisi",
                ClassIconColorValue = ClassColor.DeathKnight,
                Profession = "Blacksmithing",
                IsSelected = false
            }
        };
    }

    public static CharacterListDesignModel Instance => new CharacterListDesignModel();
}
