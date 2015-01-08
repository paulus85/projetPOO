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
		* \param	   carte			La carte des cases de la partie en cours
		* \param	   taille			La taille de la carte
		* \param	   peupleJoueur1	Peuple du joueur 1
		* \param	   peupleJoueur2	Peuple du joueur 2
		* \param       x				L'abscisse de la case courante
		* \param       y				L'ordonn�e de la case courante
		* \param       unites			La matrice des unit�s pr�sentes sur la carte
		* \param       ptsDeplacement   Les points de d�placement de l'unit� s�lectionn�e
		* \param       joueur			Le joueur courant
		* \return      Les points sugg�r�s
		*/
		static array<array<int>^>^ getSuggestion(array<array<int>^>^ carte, int taille, int peupleJoueur1, int peupleJoueur2, int x, int y, array<array<int>^>^ unites, array<array<double>^>^ ptsDeplacement, int joueur);
	};
}

#endif

