#include "GenerateurCarte.h"
#include "Enum.h"
#include "Point.h"
#include <time.h>

Point* GenerateurCarte::departUnites(int nbCases) {
	Point* result = new Point[2];
	result[0] = Point(0, 0);
	result[1] = Point(nbCases - 1, nbCases - 1);

	return result;
}

Case** GenerateurCarte::genererCarte(int nbCases) {
	// Initialisation:
	int val = 0;
	int controleTab[4] = { 0, 0, 0, 0 };
	Case** carte = new Case*[nbCases];
	for (int i = 0; i<nbCases; i++) {
		carte[i] = new Case[nbCases];
	}

	srand((unsigned int)time(NULL));

	// Remplissage de la carte
	for (int i = 0; i<nbCases; i++) {
		for (int j = 0; j<nbCases; j++) {
			//Vérifie qu'on a à peu près le même nombre de case de chaque type
			//do
			//{
				val = rand() % 4;
				carte[i][j] = (Case)val;
			//} while (controleTab[val] == (int)(nbCases / 4) + 1);
			//controleTab[val]++;
		}
	}

	return carte;
}

