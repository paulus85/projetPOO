#include "Carte.h"
#include <stdlib.h>
#include <time.h>

Carte::Carte(int nbCases) {
	_carte = new int*[nbCases];
	
	for (int i = 0; i < nbCases; i++)
		_carte[i] = 0;

	for (int i = 0; i < nbCases; i++)
		_carte[i] = new int[nbCases];
	
	//remplissage de la carte
	for (int i = 0; i < nbCases; i++){
		for (int j = 0; j < nbCases; j++){
			do 
			while (controleTab[val] == 9);
		}
	}