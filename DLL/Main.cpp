#include "SuggestionCase.h"
#include "GenerateurCarte.h"

int main()
{
	int taille = 6;

	Case** carte = GenerateurCarte::genererCarte(taille);
	carte[0][0] = FORET;
	carte[0][1] = PLAINE;
	carte[1][0] = DESERT;
	carte[1][1] = FORET;
	carte[2][0] = DESERT;
	carte[2][1] = PLAINE;
	SuggestionCase generator = SuggestionCase(carte, taille, NAIN, ORC);


	Joueur** unites = new Joueur*[taille];
	for (int i = 0; i < taille; i++)
	{
		unites[i] = new Joueur[taille];
		for (int j = 0; j < taille; j++)
			unites[i][j] = NONE;
	}
	unites[0][1] = JOUEUR1;
	unites[1][0] = JOUEUR1;
	//unites[taille - 1][taille - 1] = JOUEUR2;
	//unites[0][1] = JOUEUR1;
	//unites[1][1] = JOUEUR1;

	double** ptsDeplacement = new double*[taille];
	for (int i = 0; i < taille; i++)
	{
		ptsDeplacement[i] = new double[taille];
	}
	ptsDeplacement[0][0] = 1;
	ptsDeplacement[0][1] = 0.5;
	//ptsDeplacement[taille - 1][taille - 1] = 1;
	//ptsDeplacement[0][1] = 1;
	//ptsDeplacement[1][1] = 1;

	Point* advice = generator.getSuggestion(0, 1, unites, ptsDeplacement, JOUEUR1);

	int** result = new int*[3];
	for (int i = 0; i < 3; i++) {
		result[i] = new int[2];
		result[i][0] = advice[i].x;
		result[i][1] = advice[i].y;
	}

	return 0;
}