using WarLeague.Interfaces;

namespace WarLeagueUI.Models
{
    public class ArmyBuilderModel
    {
        public ICharacterCard SelectedCard { get; private set; }
        public int MaxSelectedCharacters { get; private set; }

        public ArmyBuilderModel(int maxSelectedCharacters)
        {
            MaxSelectedCharacters = maxSelectedCharacters;
            SelectedCard = null;
        }

        public void SelectCard(ICharacterCard card)
        {
            SelectedCard = card;
        }

        public void DeselectCard(ICharacterCard card)
        {
            if (SelectedCard == card)
                SelectedCard = null;
        }
    }
} 