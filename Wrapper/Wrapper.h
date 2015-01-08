/**
* \file      Wrapper.h
* \author    Poilane Pierre
* \version   1.0
* \date      08/01/2015
* \brief     Header pour le wrapper
*/

#ifndef __WRAPPER__
#define __WRAPPER__

#pragma once

#include "../DLL/GenerateurCarte.h"
#include "../DLL/SuggestionCase.h"
#include "../DLL/Point.h"
#include "../DLL/Enum.h"

using namespace System;

namespace Wrapper {

	public ref class Wrapper {

	public:
		/**
		* \brief       Génère la carte pour la nouvelle partie lancées
		* \details	   Version basique où on a à peu près des cases de chaque type
		* \param       size         la taille de la carte
		* \return      Une matrice d'entier correspondant aux cases
		*/
		static array<array<int>^>^ genererCarte(int size);

		/**
		* \brief       Place les unités en début de partie
		* \details	   Version basique où les unités son placées de par et dautres de la carte
		*				soit en [0,0] et [nbCases-1,nbCases-1]
		* \param       size         la taille de la carte
		* \return      Une matrice d'entier correspondant aux placement des unités
		*/
		static array<array<int>^>^ placementJoueur(int size);

		/**
		* \brief       Retourne des cases suggérées
		* \details	   Les cases suggérées tiennent compte des points que rapportent la case visée, à l'unité se trouvant sur la case visée
		*				ainsi que la petinence de déplacement et des points de déplacement de l'unité sélectionnée
		* \param       nbCases         le nombre de cases en hauteur/largeur
		* \return      Les points de placement pour les 2 unités
		*/
		static array<array<int>^>^ getSuggestion(array<array<int>^>^ carte, int taille, int peupleJoueur1, int peupleJoueur2, int x, int y, array<array<int>^>^ unites, array<array<double>^>^ ptsDeplacement, int joueur);
	};
}

#endif

