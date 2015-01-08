/**
* \file      GenerateuCarte.h
* \author    Poilane Pierre
* \version   1.0
* \date      08/01/2015
* \brief     Header de génération de carte et de placement des unités
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
* \brief classe representant générateur de carte
*/
class DLL GenerateurCarte {

public:
	/**
	* \brief       Place les unités en début de partie
	* \details	   Version basique où les unités son placées de par et dautres de la carte
	*				soit en [0,0] et [nbCases-1,nbCases-1]
	* \param       nbCases         le nombre de cases en hauteur/largeur
	* \return      Les points de placement pour les 2 unités
	*/
	EXTERNC DLL static Point* departUnites(int nbCases);

	/**
	* \brief       Génère la carte pour la nouvelle partie lancées
	* \details	   Version basique où on a à peu près des cases de chaque type
	* \param       nbCases         le nombre de cases en hauteur/largeur
	* \return      Une matrice contenant les cases
	*/
	EXTERNC DLL static Case** genererCarte(int nbCases);
	
};


