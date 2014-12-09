#include "Carte.h"
#include "Enum.h"
#include <stdlib.h>
#include <time.h>



Carte::Carte(int nbCases) {
	srand((unsigned int)time(NULL));
	int controleTab[4] = {0, 0, 0, 0};
	int val;
	//initialisation
	_carte = new Tuile*[nbCases];
	
	for (int i = 0; i < nbCases; i++)
		_carte[i] = 0;

	for (int i = 0; i < nbCases; i++)
		_carte[i] = new Tuile[nbCases];
	
	//remplissage de la carte
	for (int i = 0; i < nbCases; i++){
		for (int j = 0; j < nbCases; j++){
			/*while (controleTab[val] == 9)
			{*/
				val = rand() % 4;
				_carte[i][j] = (Tuile)val;
			//}
		}
	}
}

Tuile** Carte::getCarte(){ 
	return _carte; 
}

Carte* Carte_new(int nbCases){
	return new Carte(nbCases);
}

void Carte_delete(Carte* carte){
	delete carte;
}

Tuile** GetCarte(Carte* carte){
	return carte->getCarte();
}

