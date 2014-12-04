#include "Carte.h"
#include <stdlib.h>
#include <time.h>

Carte::Carte(int nbCases) {	srand((unsigned int)time(NULL));	int controleTab[4] = {0, 0, 0, 0};	int val;	//initialisation
	_carte = new int*[nbCases];
	
	for (int i = 0; i < nbCases; i++)
		_carte[i] = 0;

	for (int i = 0; i < nbCases; i++)
		_carte[i] = new int[nbCases];
	
	//remplissage de la carte
	for (int i = 0; i < nbCases; i++){
		for (int j = 0; j < nbCases; j++){
			do 			{ //0:Plaine, 1:Désert, 2:Montagne, 3:Forêt				/*val = rand() % 4;				_carte[i][j] = val;*/			} 
			while (controleTab[val] == 9);
		}
	}}int** Carte::getCarte(){	return _carte;}