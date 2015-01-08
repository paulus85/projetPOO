/**
* \file      Point.h
* \author    Poilane Pierre
* \version   1.0
* \date      08/01/2015
* \brief     Header pour la d�finition d'un point
*/

#pragma once

#include "Enum.h"

/**
* \class Point
* \brief classe permettant gestion de points sur un plan � deux dimensions
*/
class Point {

public:

	//Abscisse
	int x;

	//Odonn�e
	int y;

	/**
	* \brief       Constructeur un point
	* \param       x           Abscisse du point � cr�er
	* \param       y		   Ordonn�e du point � cr�er
	*/
	Point(int x, int y);

	/**
	* \brief       Constructeur vide
	*/
	Point();

	/**
	* \brief       Red�finition de l'op�ateur ==
	*/
	bool operator==(const Point& pt) const;

	/**
	* \brief       Red�finition de l'op�ateur <
	*/
	bool operator<(const Point& pt) const;

	/**
	* \brief       V�rifie qu'un point est Valide
	* \param       size    Taille de la carte du jeu en cours
	*/
	bool estValide(int size) const;

	/**
	* \brief       V�rifie qu'un point correspond � une case Montagne
	* \param       size    Taille de la carte du jeu en cours
	*/
	bool estMontagne(Case** map) const;
};