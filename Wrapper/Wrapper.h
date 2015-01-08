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
		* \brief       G�n�re la carte pour la nouvelle partie lanc�es
		* \details	   Version basique o� on a � peu pr�s des cases de chaque type
		* \param       size         la taille de la carte
		* \return      Une matrice d'entier correspondant aux cases
		*/
		static array<array<int>^>^ genererCarte(int size);

		/**
		* \brief       Place les unit�s en d�but de partie
		* \details	   Version basique o� les unit�s son plac�es de par et dautres de la carte
		*				soit en [0,0] et [nbCases-1,nbCases-1]
		* \param       size         la taille de la carte
		* \return      Une matrice d'entier correspondant aux placement des unit�s
		*/
		static array<array<int>^>^ placementJoueur(int size);

		/**
		* \brief       Retourne des cases sugg�r�es
		* \details	   Les cases sugg�r�es tiennent compte des points que rapportent la case vis�e, � l'unit� se trouvant sur la case vis�e
		*				ainsi que la petinence de d�placement et des points de d�placement de l'unit� s�lectionn�e
		* \param       nbCases         le nombre de cases en hauteur/largeur
		* \return      Les points de placement pour les 2 unit�s
		*/
		static array<array<int>^>^ getSuggestion(array<array<int>^>^ carte, int taille, int peupleJoueur1, int peupleJoueur2, int x, int y, array<array<int>^>^ unites, array<array<double>^>^ ptsDeplacement, int joueur);
	};
}

#endif

