using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface Tour
    {
        ResultatCombat ResDernierCombat { get; }

        List<String> ResumeCombat { get; }

        void SelectUnites(List<Unite> unites);

        void SelectUnites(List<Unite> unites, Point position);

        void UnselectUnites();

        List<Unite> GetUnites(Point position);

        bool JoueurSurPosition(Point position);

        bool SetDestination(Point destination);

        void ExecuterDeplacement();

        ResultatCombat Combat(Unite unite);

        Unite MeilleureUniteDefensive(Point destination);

        Double CalculAttaque(Unite unite);

        Double CalculDefense(Unite unite);

        List<Point> SuggestionsCase(Unite unite, Point pos);
    }
}
