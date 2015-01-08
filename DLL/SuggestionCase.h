/**
* \file      SuggestionCase.h
* \author    Poilane Pierre
* \version   1.0
* \date      08/01/2015
* \brief     Header pour la suggestion de cases au cours d'une partie
*/

#pragma once

#ifdef WANTDLLEXP
#define DLL _declspec(dllexport)
#define EXTERNC extern "C"
#else
#define DLL
#define EXTERNC
#endif

#include <map>
#include <vector>
#include "Enum.h"
#include "Point.h"

using namespace std;

/**
* \class SuggestionCase
* \brief classe permettant la suggestion de cases
*/
class DLL SuggestionCase {
private:

	//Carte des cases du jeu en cours
	Case** carte;

	//Taille de la carte du jeu en cours
	int taille;

	//Peuple des 2 joueurs du jeu en cours
	Peuple* peuples;

	/**
	* \brief       Donne les coodonnées des cases correspondant aux cases voisines de la case courante
	* \param       x           la coordonées x permettant de savoir quelle coordonnées il faut renvoyées
	* \param       *xOffset    le pointeur pou les coodonnées x voisines
	* \param       *yOffset    le pointeur pou les coodonnées y voisines
	*/
	void getVoisin(int x, int *xOffset[6], int *yOffset[6]) const;

	/**
	* \brief       Retourne le score par rapport au point que rapporte une case en fonction du peuple du joueur courant
	* \param       pos       Le point destination visé
	* \param       peuple    Le peuple du joueur courant
	*/
	int getScoreMouvement(Point pos, Peuple peuple) const;

	/**
	* \brief       Retourne le score par rapport à l'unité se trouvant sur la case destination
	* \param       occupant     Le joueur se trouvant sur la case destination
	* \param       joueur       Le joueur courant
	* \param       peuple       Le peuple du joueur courant
	*/
	int getScoreCapture(Joueur occupant, Joueur joueur, Peuple peuple) const;

	/**
	* \brief       Retourne le score par rapport à la pertinence du déplacement et aux points de déplacement de l'unité
	* \param       dest				Le point de destination
	* \param       ptsDeplacement   Les points de déplacement de l'unité sélectionnée 
	* \param       peuple           Le peuple du joueur courant
	*/
	int getScoreDeplacement(Point dest, double ptsDeplacement, Peuple peuple) const;

public:
	/**
	* \brief       Constructeur de la classe
	* \details	   initialise la carte des cases du jeu, sa taille et le peuple des 2 joueurs
	* \param       carte			La carte des cases du jeu en cours
	* \param       taille			La taille de la carte
	* \param       PeupleJoueur1    Le peuple du joueur 1
	* \param       PeupleJoueur2    Le peuple du joueur 2
	*/
	EXTERNC DLL SuggestionCase(Case** carte, int taille, Peuple PeupleJoueur1, Peuple PeupleJoueur2);

	/**
	* \brief       Retourne des cases suggérées
	* \details	   Les cases suggérées tiennent compte des points que rapportent la case visée, à l'unité se trouvant sur la case visée
	*				ainsi que la petinence de déplacement et des points de déplacement de l'unité sélectionnée
	* \param       x         l'abscisse de la case courante
	* \param       y         l'ordonnée de la case courante
	* \param       unites         La matrice des unités présentes sur la carte
	* \param       ptsDeplacement    le nombre de cases en hauteur/largeur
	* \param       joueur         le nombre de cases en hauteur/largeur
	* \return      Les points de placement pour les 2 unités
	*/
	EXTERNC DLL Point* getSuggestion(int x, int y, Joueur** unites, double** ptsDeplacement, Joueur joueur) const;
};

