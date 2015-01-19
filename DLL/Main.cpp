#include "SuggestionCase.h"
#include "GenerateurCarte.h"

int main()
{
	int taille = 5;

	Case** carte = GenerateurCarte::genererCarte(taille);
	//carte[0][0] = FORET;
	carte[0][taille-1] = PLAINE;
	carte[0][taille-2] = DESERT;
	carte[1][taille-2] = MONTAGNE;
	carte[1][taille-1] = FORET;
	SuggestionCase generator = SuggestionCase(carte, taille, NAIN, ORC);
	int** unites = new int*[taille];
	for (int i = 0; i < taille; i++)
	{
		unites[i] = new int[taille];
		for (int j = 0; j < taille; j++)
			unites[i][j] = 0;
	}
	unites[0][0] = 1;
	//unites[1][0] = JOUEUR1;
	//unites[taille - 1][taille - 1] = JOUEUR2;
	//unites[0][1] = JOUEUR1;
	//unites[1][1] = JOUEUR1;

	double** ptsDeplacement = new double*[taille];
	for (int i = 0; i < taille; i++)
	{
		ptsDeplacement[i] = new double[taille];
	}
	ptsDeplacement[0][taille-1] = 1;
	//ptsDeplacement[0][1] = 0.5;
	//ptsDeplacement[taille - 1][taille - 1] = 1;
	//ptsDeplacement[0][1] = 1;
	//ptsDeplacement[1][1] = 1;

	Point* advice = generator.getSuggestion(0, taille-1, unites, ptsDeplacement, 1);

	int** result = new int*[3];
	for (int i = 0; i < 3; i++) {
		result[i] = new int[2];
		result[i][0] = advice[i].x;
		result[i][1] = advice[i].y;
	}

	return 0;
}