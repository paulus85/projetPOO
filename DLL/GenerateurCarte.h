/**
* \file      GenerateuCarte.h
* \author    Poilane Pierre
* \version   1.0
* \date      08/01/2015
* \brief     Header de g�n�ration de carte et de placement des unit�s
*/

#ifdef WANTDLLEXP
#define DLL _declspec(dllexport)
#define EXTERNC extern "C"
#else
#define DLL
#define EXTERNC
#endif

#include "Enum.h"
#include "Point.h"
#include <stdlib.h>
#include <time.h>

/** 
* \class GenerateurCarte
* \brief classe representant g�n�rateur de carte
*/
class DLL GenerateurCarte {

public:
	/**
	* \brief       Place les unit�s en d�but de partie
	* \details	   Version basique o� les unit�s son plac�es de par et dautres de la carte
	*				soit en [0,0] et [nbCases-1,nbCases-1]
	* \param       nbCases         le nombre de cases en hauteur/largeur
	* \return      Les points de placement pour les 2 unit�s
	*/
	EXTERNC DLL static Point* departUnites(int nbCases);

	/**
	* \brief       G�n�re la carte pour la nouvelle partie lanc�es
	* \details	   Version basique o� on a � peu pr�s des cases de chaque type
	* \param       nbCases         le nombre de cases en hauteur/largeur
	* \return      Une matrice contenant les cases
	*/
	EXTERNC DLL static Case** genererCarte(int nbCases);
	
};


