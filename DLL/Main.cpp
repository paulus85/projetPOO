#include "SuggestionCase.h"
#include "GenerateurCarte.h"

int main()
{
	int taille = 5;

	Case** carte = GenerateurCarte::genererCarte(taille);

	Case** carteBis = new Case*[taille];
	for (int i = 0; i<5; i++) {
		carteBis[i] = new Case[taille];
		for (int j = 0; j<taille; j++) {
			carteBis[i][j] = (Case)carte[i][j];
		}
	}
	carteBis[0][0] = MONTAGNE;
	carteBis[0][2] = MONTAGNE;
	SuggestionCase generator = SuggestionCase(carteBis, taille, NAIN, ELF);


	Joueur** unites = new Joueur*[taille];
	for (int i = 0; i < taille; i++)
	{
		unites[i] = new Joueur[taille];
	}
	unites[0][0] = JOUEUR1;
	unites[taille - 1][taille - 1] = JOUEUR2;
	//unites[0][1] = JOUEUR1;
	unites[1][1] = JOUEUR1;

	Joueur** unitesBis = new Joueur*[taille];
	for (int i = 0; i<taille; i++) {
		unitesBis[i] = new Joueur[taille];
		for (int j = 0; j<taille; j++) {
			unitesBis[i][j] = (Joueur)unites[i][j];
		}
	}

	Point* advice = generator.getSuggestion(0, 0, unitesBis, JOUEUR1);

	int** result = new int*[3];
	for (int i = 0; i < 3; i++) {
		result[i] = new int[2];
		result[i][0] = advice[i].x;
		result[i][1] = advice[i].y;
	}

	return 0;
}