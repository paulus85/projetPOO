/**
* \file      GenerateuCarte.ccp
* \author    Poilane Pierre
* \version   1.0
* \date      08/01/2015
* \brief     Génère la carte et le placement des unités en début de partie 
*
* \details   genererCarte pour la génération de carte et departUnites pour le placement des unités
*/

#include "GenerateurCarte.h"
#include "Enum.h"
#include "Point.h"
#include <time.h>

/**
* \brief       Place les unités en début de partie
* \details	   Version basique où les unités son placées de par et dautres de la carte 
*				soit en [0,0] et [nbCases-1,nbCases-1]
* \param       nbCases         le nombre de cases en hauteur/largeur
* \return      Les points de placement pour les 2 unités
*/
Point* GenerateurCarte::departUnites(int nbCases) {
	srand((unsigned int)time(NULL));
	Point* result = new Point[2];
	if (rand() % 2 == 0){
		result[0] = Point(0, 0);
		result[1] = Point(nbCases - 1, nbCases - 1);
	}
	else {
		result[0] = Point(0, nbCases - 1);
		result[1] = Point(nbCases - 1, 0);
	}

	return result;
}

/**
* \brief       Génère la carte pour la nouvelle partie lancées
* \details	   Version basique où on a à peu près des cases de chaque type
* \param       nbCases         le nombre de cases en hauteur/largeur
* \return      Une matrice contenant les cases
*/
Case** GenerateurCarte::genererCarte(int nbCases) {
	// Initialisation:
	int val = 0;
	int controleTab[5] = { 0, 0, 0, 0 , 0};
	Case** carte = new Case*[nbCases];
	for (int i = 0; i<nbCases; i++) {
		carte[i] = new Case[nbCases];
	}

	srand((unsigned int)time(NULL));

	// Remplissage de la carte
	for (int i = 0; i<nbCases; i++) {
		for (int j = 0; j<nbCases; j++) {
			//Vérifie qu'on a à peu près le même nombre de case de chaque type
			do
			{
				val = rand() % 5;
				carte[i][j] = (Case)val;
			} while (controleTab[val] == (int)((nbCases*nbCases) / 5) + 1);
			controleTab[val]++;
		}
	}

	return carte;
}

