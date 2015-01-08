/**
* \file      Point.ccp
* \author    Poilane Pierre
* \version   1.0
* \date      08/01/2015
* \brief     Header pour la définition d'un point
*/

#include "Point.h"
#include "Enum.h"

/**
* \brief       Constructeur un point
* \param       x           Abscisse du point à créer
* \param       y		   Ordonnée du point à créer
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
* \brief       Redéfinition de l'opéateur ==
*/
bool Point::operator==(const Point& pt) const {
	return this->x == pt.x && this->y == pt.y;
}

/**
* \brief       Redéfinition de l'opéateur <
*/
bool Point::operator<(const Point& pt) const {
	return this->x > pt.x || (this->x == pt.x && this->y>pt.y);
}

/**
* \brief       Vérifie qu'un point est Valide
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
* \brief       Vérifie qu'un point correspond à une case Montagne
* \param       size    Taille de la carte du jeu en cours
*/
bool Point::estMontagne(Case** map) const {
	return map[this->x][this->y] == MONTAGNE;
}