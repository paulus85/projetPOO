#include "Algo.h"
#include <stdlib.h>
#include <time.h>



Carte* Carte_Demo(int const nbCases) { 	srand(time(NULL));	int tab[6][6];	int controleTab[4,0];	int val;	//0:Plaine, 1:Désert, 2:Montagne, 3:Forêt	for (int i = 0; i < nbCases; i++){		for (int j = 0; j < nbCases; j++){			do			{				val = rand() % 4;				tab[i][j] = val;			} 			while (controleTab[val == 9])		}	}		return new Carte(); }