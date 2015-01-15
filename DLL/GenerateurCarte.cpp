/**
* \file      GenerateuCarte.ccp
* \author    Poilane Pierre
* \version   1.0
* \date      08/01/2015
* \brief     G�n�re la carte et le placement des unit�s en d�but de partie 
*
* \details   genererCarte pour la g�n�ration de carte et departUnites pour le placement des unit�s
*/

#include "GenerateurCarte.h"
#include "Enum.h"
#include "Point.h"
#include <time.h>

/**
* \brief       Place les unit�s en d�but de partie
* \details	   Version basique o� les unit�s son plac�es de par et dautres de la carte 
*				soit en [0,0] et [nbCases-1,nbCases-1]
* \param       nbCases         le nombre de cases en hauteur/largeur
* \return      Les points de placement pour les 2 unit�s
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
* \brief       G�n�re la carte pour la nouvelle partie lanc�es
* \details	   Version basique o� on a � peu pr�s des cases de chaque type
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
			//V�rifie qu'on a � peu pr�s le m�me nombre de case de chaque type
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

