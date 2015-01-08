/**
* \file      Point.ccp
* \author    Poilane Pierre
* \version   1.0
* \date      08/01/2015
* \brief     Header pour la d�finition d'un point
*/

#include "Point.h"
#include "Enum.h"

/**
* \brief       Constructeur un point
* \param       x           Abscisse du point � cr�er
* \param       y		   Ordonn�e du point � cr�er
*/
Point::Point(int x, int y) {
	this->x = x;
	this->y = y;
}

/**
* \brief       Constructeur vide
*/
Point::Point() {
	this->x = -1;
	this->y = -1;
}

/**
* \brief       Red�finition de l'op�ateur ==
*/
bool Point::operator==(const Point& pt) const {
	return this->x == pt.x && this->y == pt.y;
}

/**
* \brief       Red�finition de l'op�ateur <
*/
bool Point::operator<(const Point& pt) const {
	return this->x > pt.x || (this->x == pt.x && this->y>pt.y);
}

/**
* \brief       V�rifie qu'un point est Valide
* \param       size    Taille de la carte du jeu en cours
*/
bool Point::estValide(int size) const {
	if (this->x<0 || this->x >= size) {
		return false;
	}
	if (this->y<0 || this->y >= size) {
		return false;
	}
	return true;
}

/**
* \brief       V�rifie qu'un point correspond � une case Montagne
* \param       size    Taille de la carte du jeu en cours
*/
bool Point::estMontagne(Case** map) const {
	return map[this->x][this->y] == MONTAGNE;
}