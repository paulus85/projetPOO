#include "Joueur.h"
#include "Enum.h"
#include <stdlib.h>
#include <time.h>

Joueur::Joueur(char nom) {
	Joueur::_nom = nom;
}

void Joueur::setCoordonn�es(int x, int y){
	Joueur::_x = x;
	Joueur::_y = y;
}


