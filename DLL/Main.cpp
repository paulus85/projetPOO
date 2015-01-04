#include "SuggestionCase.h"
#include "GenerateurCarte.h"

int main()
{
	int taille = 6;

	Case** carte = GenerateurCarte::genererCarte(taille);
	carte[1][0] = PLAINE;
	carte[1][1] = FORET;
	carte[2][1] = FORET;
	carte[2][0] = MONTAGNE;
	carte[2][2] = PLAINE;
	carte[3][0] = FORET;
	carte[3][1] = FORET;
	SuggestionCase generator = SuggestionCase(carte, taille, ELF, NAIN);


	Joueur** unites = new Joueur*[taille];
	for (int i = 0; i < taille; i++)
	{
		unites[i] = new Joueur[taille];
	}
	unites[0][0] = JOUEUR1;
	//unites[taille - 1][taille - 1] = JOUEUR2;
	//unites[0][1] = JOUEUR1;
	//unites[1][1] = JOUEUR1;

	double** ptsDeplacement = new double*[taille];
	for (int i = 0; i < taille; i++)
	{
		ptsDeplacement[i] = new double[taille];
	}
	ptsDeplacement[2][1] = 0.5;
	//ptsDeplacement[taille - 1][taille - 1] = 1;
	//ptsDeplacement[0][1] = 1;
	//ptsDeplacement[1][1] = 1;

	Point* advice = generator.getSuggestion(2, 1, unites, ptsDeplacement, JOUEUR1);

	int** result = new int*[3];
	for (int i = 0; i < 3; i++) {
		result[i] = new int[2];
		result[i][0] = advice[i].x;
		result[i][1] = advice[i].y;
	}

	return 0;
}