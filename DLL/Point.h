/**
* \file      Point.h
* \author    Poilane Pierre
* \version   1.0
* \date      08/01/2015
* \brief     Header pour la définition d'un point
*/

#pragma once

#include "Enum.h"

/**
* \class Point
* \brief classe permettant gestion de points sur un plan à deux dimensions
*/
class Point {

public:

	//Abscisse
	int x;

	//Odonnée
	int y;

	/**
	* \brief       Constructeur un point
	* \param       x           Abscisse du point à créer
	* \param       y		   Ordonnée du point à créer
	*/
	Point(int x, int y);

	/**
	* \brief       Constructeur vide
	*/
	Point();

	/**
	* \brief       Redéfinition de l'opéateur ==
	*/
	bool operator==(const Point& pt) const;

	/**
	* \brief       Redéfinition de l'opéateur <
	*/
	bool operator<(const Point& pt) const;

	/**
	* \brief       Vérifie qu'un point est Valide
	* \param       size    Taille de la carte du jeu en cours
	*/
	bool estValide(int size) const;

	/**
	* \brief       Vérifie qu'un point correspond à une case Montagne
	* \param       size    Taille de la carte du jeu en cours
	*/
	bool estMontagne(Case** map) const;
};