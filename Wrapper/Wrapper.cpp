/**
* \file      Wrapper.ccp
* \author    Poilane Pierre
* \version   1.0
* \date      08/01/2015
* \brief     Wrapper pemettant l'utilisation de code C++ dans le code C#
*
* \details   Suggère au maximum 3 cases, ainsi les cases suggérées ne sont qu'une partie 
				des cases où le joueur peut se déplacer.
*/

#include "Wrapper.h"

/**
* \brief       Génère la carte pour la nouvelle partie lancées
* \details	   Version basique où on a à peu près des cases de chaque type
* \param       taille         la taille de la carte
* \return      Une matrice d'entier correspondant aux cases
*/
array<array<int>^>^ Wrapper::Wrapper::genererCarte(int taille) {
	Case** result = GenerateurCarte::genererCarte(taille);

	//Converti le résultat de la librairie C++ dans une matrice lisible par C#
	array<array<int>^>^ carte = gcnew array<array<int>^>(taille);
	for (int i = 0; i<taille; i++) {
		carte[i] = gcnew array<int>(taille);
		for (int j = 0; j<taille; j++) {
			carte[i][j] = result[i][j];
		}
	}
	return carte;
}

/**
* \brief       Place les unités en début de partie
* \details	   Version basique où les unités son placées de par et dautres de la carte
*				soit en [0,0] et [nbCases-1,nbCases-1]
* \param       taille         la taille de la carte
* \return      Une matrice d'entier correspondant aux placement des unités
*/
array<array<int>^>^ Wrapper::Wrapper::placementJoueur(int taille) {
	Point* starts = GenerateurCarte::departUnites(taille);

	array<array<int>^>^ pos = gcnew array<array<int>^>(2);
	for (int i = 0; i<2; i++) {
		pos[i] = gcnew array<int>(2);
		pos[i][0] = starts[i].x;
		pos[i][1] = starts[i].y;
	}
	return pos;
}

/**
* \brief       Retourne des cases suggérées
* \details	   Les cases suggérées tiennent compte des points que rapportent la case visée, à l'unité se trouvant sur la case visée
*				ainsi que la petinence de déplacement et des points de déplacement de l'unité sélectionnée
* \param	   carte			La carte des cases de la partie en cours
* \param	   taille			La taille de la carte
* \param	   peupleJoueur1	Peuple du joueur 1
* \param	   peupleJoueur2	Peuple du joueur 2
* \param       x				L'abscisse de la case courante
* \param       y				L'ordonnée de la case courante
* \param       unites			La matrice des unités présentes sur la carte
* \param       ptsDeplacement   Les points de déplacement de l'unité sélectionnée
* \param       joueur			Le joueur courant
* \return      Les points suggérés
*/
array<array<int>^>^ Wrapper::Wrapper::getSuggestion(array<array<int>^>^ carte, int taille, int peupleJoueur1, int peupleJoueur2, int x, int y, array<array<int>^>^ unites, array<array<double>^>^ ptsDeplacement, int joueur) {
	Case** carteBis = new Case*[taille];
	for (int i = 0; i<taille; i++) {
		carteBis[i] = new Case[taille];
		for (int j = 0; j<taille; j++) {
			carteBis[i][j] = (Case)carte[i][j];
		}
	}
	SuggestionCase generator = SuggestionCase(carteBis, taille, (Peuple)peupleJoueur1, (Peuple)peupleJoueur2);

	int** unitesBis = new int*[taille];
	for (int i = 0; i<taille; i++) {
		unitesBis[i] = new int[taille];
		for (int j = 0; j<taille; j++) {
			unitesBis[i][j] = unites[i][j];
		}
	}

	double** ptsDeplacementBis = new double*[taille];
	for (int i = 0; i<taille; i++) {
		ptsDeplacementBis[i] = new double[taille];
		for (int j = 0; j<taille; j++) {
			ptsDeplacementBis[i][j] = ptsDeplacement[i][j];
		}
	}

	Point* advice = generator.getSuggestion(x, y, unitesBis, ptsDeplacementBis, joueur);

	array<array<int>^>^ result = gcnew array<array<int>^>(3);
	for (int i = 0; i < 3; i++) {
		result[i] = gcnew array<int>(2);
		result[i][0] = advice[i].x;
		result[i][1] = advice[i].y;
	}
	return result;
}